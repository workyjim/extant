/*
Copyright (c) 2008 Joseph Scott, http://josephscott.org/

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

// version: 0.1.2

// Edited 18.03.2010 (LB)
// Added wysiwyg form type, using FCKEditor.
//
// 07/04/2010 (RSH)
// Added jquery.validation integration
//
// 06/05/2010 (RSH)
// Added error handling
//
// 20/09/2010 (RSH)
// Added ability to get contents for editor from a different element than the one clicked on (current_value_id)
//
// 22/10/2010 (RSH)
// Allow arbitrary elements to be hidden when editing (hidden_elements)
//
// 14/11/2011 (RSH)
// Allow a user defined function to be executed once the EIP form is ready
//
(function ($) {
    $.fn.eip = function (save_url, options) {
        // Defaults
        var opt = {
            save_url: save_url,

            save_on_enter: true,
            cancel_on_esc: true,
            focus_edit: true,
            select_text: false,
            edit_event: "click",
            select_options: false,
            data: false,

            form_type: "text", // text, textarea, select
            size: false, // calculate at run time
            max_size: 60,
            rows: false, // calculate at run time
            max_rows: 10,
            cols: 60,

            savebutton_text: "SAVE",
            savebutton_class: "jeip-savebutton",
            cancelbutton_text: "CANCEL",
            cancelbutton_class: "jeip-cancelbutton",

            mouseover_class: "jeip-mouseover",
            editor_class: "jeip-editor",
            editfield_class: "jeip-editfield",

            saving_text: "Saving ...",
            saving_class: "jeip-saving",

            saving: '<span id="saving-#{id}" class="#{saving_class}" style="display: none;">#{saving_text}</span>',

            error_text: "An error occurred",
            error_class: "jeip-error",
            error: '<span id="error-#{id}" class="#{error_class}" style="display: none;"><span>#{error_text}</span><img src="/Images/cross.png" /></span>',

            start_form: '<span id="editor-#{id}" class="#{editor_class}" style="display: none;">',
            form_buttons: '<span><input type="button" id="save-#{id}" class="#{savebutton_class}" value="#{savebutton_text}" /> OR <input type="button" id="cancel-#{id}" class="#{cancelbutton_class}" value="#{cancelbutton_text}" /></span>',
            stop_form: '</span>',

            text_form: '<input type="text" id="edit-#{id}" class="#{editfield_class}" value="#{value}" /> <br />',
            textarea_form: '<textarea cols="#{cols}" rows="#{rows}" id="edit-#{id}" class="#{editfield_class}">#{value}</textarea> <br />',
            wysiwyg_form: '<textarea cols="#{cols}" rows="#{rows}" name="EditContents" id="edit-#{id}" class="#{editfield_class}">#{value}</textarea> <br />',
            start_select_form: '<select id="edit-#{id}" class="#{editfield_clas}">',
            select_option_form: '<option id="edit-option-#{id}-#{option_value}" value="#{option_value}" #{selected}>#{option_text}</option>',
            stop_select_form: '</select>',

            after_save: function (self) {
                for (var i = 0; i < 2; i++) {
                    $(self).fadeOut("fast");
                    $(self).fadeIn("fast");
                }
            },
            on_error: function (msg) {
                alert("Error: " + msg);
            },
            validate: false,
            current_value_id: false,
            hidden_elements: [],
            when_ready: function (self) {
            }
        }; // defaults

        if (options) {
            $.extend(opt, options);
        }

        this.each(function () {
            var self = this;

            $(this).bind("mouseenter mouseleave", function (e) {
                $(this).toggleClass(opt.mouseover_class);
            });

            $(this).bind(opt.edit_event, function (e) {
                _editMode(this);
            });
        }); // this.each

        // Private functions
        var _editMode = function (self) {
            $(self).unbind(opt.edit_event);

            $.each(opt.hidden_elements, function (i, val) {
                $(val).hide();
            });

            $(self).removeClass(opt.mouseover_class);
            $(self).fadeOut("fast", function (e) {
                var id = self.id;
                if (!opt.current_value_id) {
                    var value = $(self).html();
                }
                else {
                    var value = $(opt.current_value_id).html();
                }
                var safe_value = value.replace(/</g, "&lt;");
                safe_value = value.replace(/>/g, "&gt;");
                safe_value = value.replace(/"/g, "&qout;");

                var orig_option_value = false;

                var form = _template(opt.start_form, {
                    id: self.id,
                    editor_class: opt.editor_class
                });

                if (opt.form_type == 'text') {
                    form += _template(opt.text_form, {
                        id: self.id,
                        editfield_class: opt.editfield_class,
                        value: value
                    });
                } // text form
                else if (opt.form_type == 'textarea') {
                    var length = value.length;
                    var rows = (length / opt.cols) + 2;

                    for (var i = 0; i < length; i++) {
                        if (value.charAt(i) == "\n") {
                            rows++;
                        }
                    }

                    if (rows > opt.max_rows) {
                        rows = opt.max_rows;
                    }
                    if (opt.rows != false) {
                        rows = opt.rows;
                    }
                    rows = parseInt(rows);

                    form += _template(opt.textarea_form, {
                        id: self.id,
                        cols: opt.cols,
                        rows: rows,
                        editfield_class: opt.editfield_class,
                        value: value
                    });
                } // textarea form
                else if (opt.form_type == 'wysiwyg') {
                    form += _template(opt.wysiwyg_form, {
                        id: self.id,
                        value: value
                    });
                } // wysiwyg form
                else if (opt.form_type == 'select') {
                    form += _template(opt.start_select_form, {
                        id: self.id,
                        editfield_class: opt.editfield_class
                    });

                    $.each(opt.select_options, function (k, v) {
                        var selected = '';
                        if (v == value) {
                            selected = 'selected="selected"';
                        }

                        if (value == v) {
                            orig_option_value = k;
                        }

                        form += _template(opt.select_option_form, {
                            id: self.id,
                            option_value: k,
                            option_text: v,
                            selected: selected
                        });
                    });

                    form += _template(opt.stop_select_form, {});
                } // select form

                form += _template(opt.form_buttons, {
                    id: self.id,
                    savebutton_class: opt.savebutton_class,
                    savebutton_text: opt.savebutton_text,
                    cancelbutton_class: opt.cancelbutton_class,
                    cancelbutton_text: opt.cancelbutton_text
                });

                form += _template(opt.stop_form, {});

                $(self).after(form);

                //add validation
                if (opt.validate) {
                    $("#editor-" + self.id).validate({
                        rules: opt.validate
                    });
                }

                $("#editor-" + self.id).fadeIn("fast");

                if (opt.focus_edit) {
                    $("#edit-" + self.id).focus();
                }
                //replace the textarea with fckEditor
                if (opt.form_type == 'wysiwyg') {
                    var oFCKeditor = new FCKeditor("edit-" + self.id);
                    oFCKeditor.BasePath = "/Content/FCKeditor/";
                    oFCKeditor.ToolbarSet = "WorkObjectReportView";
                    oFCKeditor.HtmlEncodeOutput = true;
                    oFCKeditor.Width = '60%';
                    oFCKeditor.ReplaceTextarea();
                } //end fckEditor
                if (opt.select_text) {
                    $("#edit-" + self.id).select();
                }

                $("#cancel-" + self.id).bind("click", function (e) {
                    _cancelEdit(self);
                });

                $("#edit-" + self.id).keydown(function (e) {
                    // cancel
                    if (e.which == 27) {
                        _cancelEdit(self);
                    }

                    // save
                    if (opt.form_type != "textarea" && e.which == 13) {
                        _saveEdit(self, orig_option_value);
                    }
                });

                $("#save-" + self.id).bind("click", function (e) {
                    _saveEdit(self, orig_option_value);
                    return false;
                }); // save click

                opt.when_ready(self);

            }); // this fadeOut
        } // function _editMode

        var _template = function (template, values) {
            var replace = function (str, match) {
                return typeof values[match] === "string" || typeof values[match] === 'number' ? values[match] : str;
            };
            return template.replace(/#\{([^{}]*)}/g, replace);
        };

        var _trim = function (str) {
            return str.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
        }

        var _cancelEdit = function (self) {
            $("#editor-" + self.id).fadeOut("fast");
            $("#editor-" + self.id).remove();

            $(self).bind(opt.edit_event, function (e) {
                _editMode(self);
            });

            $(self).removeClass(opt.mouseover_class);
            $(self).fadeIn("fast");

            $.each(opt.hidden_elements, function (i, val) {
                $(val).show();
            });

        };

        var _saveEdit = function (self, orig_option_value) {
            var orig_value = $(self).html();
            var new_value = $("#edit-" + self.id).attr("value");
            if (opt.form_type == 'wysiwyg') {
                FCKeditorAPI.GetInstance("edit-" + self.id).UpdateLinkedField();
                new_value = FCKeditorAPI.GetInstance("edit-" + self.id).GetHTML();
            }
            if (orig_value == new_value) {
                $("#editor-" + self.id).fadeOut("fast");
                $("#editor-" + self.id).remove();

                $(self).bind(opt.edit_event, function (e) {
                    _editMode(self);
                });

                $(self).removeClass(opt.mouseover_class);
                $(self).fadeIn("fast");

                $.each(opt.hidden_elements, function (i, val) {
                    $(val).show();
                });

                return true;
            }

            if (!$("#editor-" + self.id).valid()) {
                return false;
            }

            $("#editor-" + self.id).after(_template(opt.saving, {
                id: self.id,
                saving_class: opt.saving_class,
                saving_text: opt.saving_text
            }));
            $("#saving-" + self.id).after(_template(opt.error, {
                id: self.id,
                error_class: opt.error_class,
                error_text: opt.error_text
            }));
            $("#editor-" + self.id).fadeOut("fast", function () {
                $("#saving-" + self.id).fadeIn("fast");
            });

            var ajax_data = {
                url: location.href,
                element_id: self.id,
                form_type: opt.form_type,
                orig_value: orig_value,
                new_value: new_value,
                data: opt.data
            }

            if (opt.form_type == 'select') {
                ajax_data.orig_option_value = orig_option_value;
                ajax_data.orig_option_text = orig_value;
                ajax_data.new_option_text = $("#edit-option-" + self.id + "-" + new_value).html();
            }

            $.ajax({
                url: opt.save_url,
                type: "POST",
                dataType: "json",
                data: ajax_data,
                success: function (data) {
                    $("#editor-" + self.id).fadeOut("fast");
                    $("#editor-" + self.id).remove();

                    if (data.is_error == true) {
                        opt.on_error(data.error_text);
                    }
                    else {
                        $(self).html(data.html);
                        if (opt.current_value_id) {
                            $(opt.current_value_id).html(new_value);
                        }
                    }

                    $("#saving-" + self.id).fadeOut("fast");
                    $("#saving-" + self.id).remove();
                    $("#error-" + self.id).remove();

                    $(self).bind(opt.edit_event, function (e) {
                        _editMode(self);
                    });

                    $(self).addClass(opt.mouseover_class);
                    $(self).fadeIn("fast");

                    $.each(opt.hidden_elements, function (i, val) {
                        $(val).show();
                    });

                    if (opt.after_save != false) {
                        opt.after_save(self);
                    }

                    $(self).removeClass(opt.mouseover_class);
                }, // success
                error: function (xhr) {
                    $("#saving-" + self.id).fadeOut("fast");
                    $("#saving-" + self.id).remove();
                    $("#error-" + self.id + " span").text(xhr.statusText);
                    $("#error-" + self.id).fadeIn("fast");
                    setTimeout(function () {
                        $("#error-" + self.id).fadeOut("fast", function () {
                            $("#error-" + self.id).remove();
                            $("#editor-" + self.id).fadeIn("fast");
                        });
                    }, 3000);
                } // error
            }); // ajax
        }; // _saveEdit


    }; // inplaceEdit
})(jQuery);
