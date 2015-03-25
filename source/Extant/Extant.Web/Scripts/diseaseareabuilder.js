function initDiseaseAreaBuilder(daId) {

    $('#publish-btn').click(function (event) {
        event.preventDefault();
        validating = true;
        if (validateTree()) {
            $('#published').val('true');
            $('#form').submit();
        } else {
            $('#publish-error').show();
        }
    });

    $('#close-btn').click(function (event) {
        event.preventDefault();
        location.href = '/Admin/DiseaseAreas';
        return false;
    });

    $('#da-name').eip(
        "/Admin/UpdateDiseaseAreaName/"+ daId,
        {
            text_form: '<input type="text" id="edit-#{id}" name="title" class="#{editfield_class}" value="#{value}" />',
            editor_class: 'inline-jeip',
            editfield_class: 'title-edit-box',
            savebutton_text: "Save",
            cancelbutton_text: "Cancel",
            start_form: '<form onsubmit="return false;" id="editor-#{id}" class="#{editor_class}" style="display: none;">',
            form_buttons: '<button id="save-#{id}">#{savebutton_text}</button> <button id="cancel-#{id}">#{cancelbutton_text}</button>',
            stop_form: '</form>',
            validate: { title: { required: true, maxlength: 255} },
            after_save: function (self) {
                $('#tree>ul>li:first>span.dynatree-node>a.dynatree-title').text($(self).text());
                for (var i = 0; i < 2; i++) {
                    $(self).fadeOut("fast");
                    $(self).fadeIn("fast");
                }
            },
            when_ready: function (self) {
                $(self).next().find('button').button();
            }
        });

    $('#tree').dynatree({
        clickFolderMode: 1,
        minExpandLevel: 1,
        keyboard: true,
        checkbox: true,
        selectMode: 2,
        generateIds: true,
        onDblClick: function (node) {
            if (node.data.isFolder) {
                if (node.isChildOf($("#tree").dynatree("getRoot"))) {
                    $('#add-type').val('addcategory').attr('disabled', 'disabled').trigger('change');
                }
                else {
                    $('#edit-category-name').val(node.data.title);
                }
                $('#add-link').trigger('click');
            } else {
                $('#add-type-row').hide();
                $('#add-type').val('');
                $('#add-category-form').hide();
                $('#edit-category-form').hide();
                $('#add-dataitem-form').hide();
                $('#edit-dataitem-form').show();
                $('#edit-dataitem-fullname').val(node.data.tooltip);
                $('#edit-dataitem-id').val(node.data.key.split('-')[2]);
                if (node.data.title != node.data.tooltip)
                    $('#edit-dataitem-shortname').val(node.data.title);
                $('#edit-dataitem-fullname').focus();
                $('#add-link').trigger('click');
            }
        },
        dnd: {
            onDragStart: function (node) {
                if (node.isChildOf($("#tree").dynatree("getRoot")))
                    return false;
                return true;
            },
            onDragEnter: function (node, sourceNode) {
                if (0 == node.getLevel())
                    return false;
                //data item dropped on data item
                if (!sourceNode.data.isFolder && !node.data.isFolder)
                    return ["before", "after"];
                //data item or category dropped on disease area node
                if (node.isChildOf($("#tree").dynatree("getRoot"))) {
                    //data item dropped on disease area node
                    if (!sourceNode.data.isFolder)
                        return false;
                    //category dropped on disease area node
                    return ["over"];
                }
                //data item dropped on category
                if (!sourceNode.data.isFolder && node.data.isFolder)
                    return ["over"];
                //category dropped on category
                if (sourceNode.data.isFolder && node.data.isFolder) {
                    //prevent a category being dropped on one of its own children
                    if (node.isDescendantOf(sourceNode))
                        return false;
                    return ["over", "before", "after"];
                }
            },
            onDrop: function (node, sourceNode, hitMode, ui, draggable) {
                sourceNode.move(node, hitMode);
                node.expand(true);
            }
        }
    });

    $("#tree").dynatree("getRoot").visit(function (node) {
        node.expand(true);
    });

    //hide checkbox for node representing the disease area
    var daNode = $("#tree").dynatree("getRoot").getChildren()[0];
    daNode.data.hideCheckbox = true;
    daNode.render();

    $('#get-started-link').fancybox({
        hideOnOverlayClick: false,
        type: 'inline'
    });

    $('#add-link').fancybox({
        hideOnOverlayClick: false,
        type: 'inline',
        onComplete: function () {
            if (!$('#edit-category-form').is(':visible') &&
                 !$('#add-category-form').is(':visible') &&
                 !$('#add-dataitem-form').is(':visible') &&
                 !$('#edit-dataitem-form').is(':visible')) {
                $('#add-type').focus();
            } else {
                $('#add-form').find('input:visible:first').focus();
            }
        },
        onClosed: function () {
            $('#add-type').removeAttr('disabled');
            $('#category-name').rules('remove');
            $('#category-name').val('');
            $('#category-name').removeClass('error');
            $('#category-name').next('label.error').remove();
            $('#edit-category-name').rules('remove');
            $('#edit-category-name').val('');
            $('#edit-category-name').removeClass('error');
            $('#edit-category-name').next('label.error').remove();
            $('#dataitem-fullname').rules('remove');
            $('#dataitem-fullname').val('');
            $('#dataitem-shortname').val('');
            $('#dataitem-fullname').removeClass('error');
            $('#dataitem-fullname').next('label.error').remove();
            $('#edit-dataitem-fullname').rules('remove');
            $('#edit-dataitem-fullname').val('');
            $('#edit-dataitem-shortname').val('');
            $('#edit-dataitem-fullname').removeClass('error');
            $('#edit-dataitem-fullname').next('label.error').remove();
            $('#edit-dataitem-form').hide();
            $('#add-type-row').show();
        }
    });

    $('#add-type').change(function () {
        if ($(this).val() == 'addcategory') {
            $('#add-dataitem-form').hide();
            $('#edit-category-form').hide();
            $('#add-category-form').show();
            $('#category-name').focus();
        }
        if ($(this).val() == 'editcategory') {
            $('#add-dataitem-form').hide();
            $('#add-category-form').hide();
            $('#edit-category-form').show();
            $('#edit-category-name').focus();
        }
        if ($(this).val() == 'dataitem') {
            $('#add-category-form').hide();
            $('#edit-category-form').hide();
            $('#add-dataitem-form').show();
            $('#dataitem-fullname').focus();
        }
    });

    $('#dataitem-fullname').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/DataItem/Find',
                dataType: 'json',
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.Value,
                            value: item.Value,
                            id: item.Key
                        }
                    }));
                },
                error: function () {
                    response([]);
                }
            });
        },
        minLength: 3,
        select: function (event, ui) {
            $('#dataitem-id').val(ui.item.id);
        }
    });

    $('#edit-dataitem-fullname').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/DataItem/Find',
                dataType: 'json',
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.Value,
                            value: item.Value,
                            id: item.Key
                        }
                    }));
                },
                error: function () {
                    response([]);
                }
            });
        },
        minLength: 3,
        select: function (event, ui) {
            $('#edit-dataitem-id').val(ui.item.id);
        }
    });

    $('#add-category-form').validate();

    $('#add-category-form').submit(function () {
        //form should never be submitted, for validation only
        return false;
    });

    $('#add-category,#add-and-close-category').click(function () {
        $('#category-name').rules('add', { required: true, maxlength: 255 });
        if ($('#add-category-form').valid()) {
            $('#category-name').rules('remove');
            var node = $("#tree").dynatree("getActiveNode");
            node.addChild({
                title: $('#category-name').val(),
                isFolder: true
            });
            node.expand(true);
            validateTree();

            if ($(this).attr('id') == 'add-and-close-category')
                $.fancybox.close();
            $('#category-name').val('');
            $('#category-name').focus();
        }
    });

    $('#edit-category-form').validate();

    $('#edit-category-form').submit(function () {
        //form should never be submitted, for validation only
        return false;
    });

    $('#edit-category').click(function () {
        $('#edit-category-name').rules('add', { required: true, maxlength: 255 });
        if ($('#edit-category-form').valid()) {
            $('#edit-category-name').rules('remove');
            var node = $("#tree").dynatree("getActiveNode");
            node.data.title = $('#edit-category-name').val();
            node.render();
            $.fancybox.close();
            $('#edit-category-name').val('');
        }
    });

    $('#add-dataitem-form').validate();

    $('#add-dataitem-form').submit(function () {
        //form should never be submitted, for validation only
        return false;
    });

    $('#add-dataitem,#add-and-close-dataitem').click(function () {
        $('#dataitem-fullname').rules('add', { required: true, maxlength: 255 });
        if ($('#add-dataitem-form').valid()) {
            $('#dataitem-fullname').rules('remove');
            var node = $("#tree").dynatree("getActiveNode");

            var newNode = node.addChild({
                title: $('#dataitem-shortname').val().length > 0 ? $('#dataitem-shortname').val() : $('#dataitem-fullname').val(),
                tooltip: $('#dataitem-fullname').val(),
                isFolder: false
            });
            newNode.data.key = 'dataitem-0-' + $('#dataitem-id').val() + "-" + newNode.data.key;
            node.expand(true);
            validateTree();

            if ($(this).attr('id') == 'add-and-close-dataitem')
                $.fancybox.close();
            $('#dataitem-fullname').val('');
            $('#dataitem-id').val('0');
            $('#dataitem-shortname').val('');
            $('#dataitem-fullname').focus();
        }
    });

    $('#edit-dataitem-form').validate();

    $('#edit-dataitem-form').submit(function () {
        //form should never be submitted, for validation only
        return false;
    });

    $('#edit-dataitem').click(function () {
        $('#edit-dataitem-fullname').rules('add', { required: true, maxlength: 255 });
        if ($('#edit-dataitem-form').valid()) {
            $('#edit-dataitem-fullname').rules('remove');
            var node = $("#tree").dynatree("getActiveNode");
            node.data.title = $('#edit-dataitem-shortname').val().length > 0 ? $('#edit-dataitem-shortname').val() : $('#edit-dataitem-fullname').val();
            node.data.tooltip = $('#edit-dataitem-fullname').val();
            var parts = node.data.key.split("-");
            var key = parts[0] + "-" + parts[1] + "-" + $('#edit-dataitem-id').val();
            if (parts.length > 3) key += "-" + parts[3];
            node.data.key = key;
            node.render();
            $.fancybox.close();
        }
    });

    $('.cancel-dialog').click(function () {
        $.fancybox.close();
    });

    $('#form').submit(function () {
        synonymsToForm();
        treeToForm();
    });

    $('#remove').click(function (event) {
        event.preventDefault();
        var nodes = $("#tree").dynatree("getSelectedNodes");
        //sort nodes selected to delete so we delete from leaves upwards
        var sorted = merge_sort(nodes, function (left, right) {
            if (left.getLevel() == right.getLevel())
                return 0;
            else if (left.getLevel() < right.getLevel())
                return 1;
            else
                return -1;
        });
        $.each(nodes, function (i, node) {
            node.remove();
        });
        $("#tree").dynatree("getTree").redraw();
        validateTree();
        return false;
    });

    if ('false' === $('#published').val()) {
        $(document).everyTime(60000, function () {
            synonymsToForm();
            treeToForm();
            $('#form').ajaxSubmit({
                beforeSubmit: function () {
                    $('#autosaving').show();
                },
                success: function () {
                    $('#autosaving').hide();
                    $('#autosavesuccess').show();
                    setTimeout(function () {
                        $('#autosavesuccess').hide('slow');
                    }, 5000);
                },
                error: function () {
                    $('#autosaving').hide();
                    $('#autosaveerror').show();
                    setTimeout(function () {
                        $('#autosaveerror').hide('slow');
                    }, 5000);
                }
            });
        });
    }

    $('#help-link').click(function () {
        $('#get-started-link').trigger('click');
    });

    if (false === daNode.hasChildren()) {
        $('#get-started-link').trigger('click');
    }
}

function synonymsToForm() {
    $('#form-synonyms').val($('#da-synonyms').val());
}

function treeToForm() {
    $('#form>input').remove();
    var root = $("#tree").dynatree("getRoot");
    var daNode = root.getChildren()[0];
    $.each(daNode.getChildren(), function (i, node) {
        categoryToForm(node, "Categories[" + i + "].");
    });
}

function categoryToForm(catNode, prefix) {
    var id = "";
    if (0 == catNode.data.key.indexOf('category-')) {
        id = catNode.data.key.substr(9);
    }
    var catName = catNode.data.title;
    $('#form').append('<input type="hidden" name="' + prefix + 'Id" value="' + id + '" />');
    $('#form').append('<input type="hidden" name="' + prefix + 'CategoryName" value="' + catName + '" />');

    if (catNode.hasChildren()) {
        var catIndex = 0;
        var diIndex = 0;
        $.each(catNode.getChildren(), function (i, node) {
            if (node.data.isFolder) {
                categoryToForm(node, prefix + "Subcategories[" + catIndex + "].");
                catIndex++;
            }
            else {
                dataItemToForm(node, prefix + "DataItems[" + diIndex + "].");
                diIndex++;
            }
        });
    }
}

function dataItemToForm(dataItemNode, prefix) {
    var parts = dataItemNode.data.key.split('-');
    var cdiid = parts[1];
    var diid = parts[2];
    var shortName = dataItemNode.data.title;
    var diName = dataItemNode.data.tooltip;
    $('#form').append('<input type="hidden" name="' + prefix + 'Id" value="' + cdiid + '" />');
    $('#form').append('<input type="hidden" name="' + prefix + 'ShortName" value="' + shortName + '" />');
    $('#form').append('<input type="hidden" name="' + prefix + 'DataItem.Id" value="' + diid + '" />');
    $('#form').append('<input type="hidden" name="' + prefix + 'DataItem.DataItemName" value="' + diName + '" />');
}

function validateTree() {
    var root = $("#tree").dynatree("getRoot");
    var daNode = root.getChildren()[0];
    var dataItems = [];
    var valid = true;
    $.each(daNode.getChildren(), function (i, node) {
        valid &= validateCategory(node, dataItems);
    });
    if (valid)
        $('#publish-error').hide();
    return valid;
}

function validateCategory(catNode, dataItems) {
    var catValid = true;
    if (catNode.hasChildren()) {
        $.each(catNode.getChildren(), function (i, node) {
            if (node.data.isFolder) {
                catValid &= validateCategory(node, dataItems);
            }
            else {
                catValid &= validateDataItem(node, dataItems);
            }
        });
    } else {
        addValidationMessage(catNode, "This category contains no data fields or sub-categories");
        catValid = false;
    }
    return catValid;
}

function validateDataItem(dataItemNode, dataItems) {
    var parts = dataItemNode.data.key.split('-');
    var cdiid = parts[1];
    var diid = parts[2];
    if (-1 !== $.inArray(diid, dataItems)) {
        addValidationMessage(dataItemNode, 'This data field has already been added to another category; each data field should only be used once');
        return false;
    } else {
        dataItems.push(diid);
        return true;
    }
}

function addValidationMessage(node, message) {
    var link = $(node.li).addClass('error').find('span>a');
    link.find('span.da-error-message').remove();
    link.append('<span class="da-error-message">' + message + '</span>');
}