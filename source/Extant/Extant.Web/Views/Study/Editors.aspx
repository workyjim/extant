<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Extant.Web.Models.StudyEditorsModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Study | <%: Model.StudyName %> | Editors
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2><%:Model.StudyName %> - Editors</h2>

<div class="clear">
    <div class="form-row">
        <input type="text" id="user-search" title="Type in part of a user's name to search for them and add them as an editor of the study."/>
        <input type="hidden" id="userid" value="0" />
        <button id="add-user">Add</button>
    </div>
</div>

<div class="topborder">

<p id="no-editors" <%= Model.Editors.Any() ? "style=\"display: none;\"" : "" %>>
    <em>No editors have been added to the study.</em>
</p>

<% using (Html.BeginForm()){ %>
<ul id="editor-list" class="deletable-list" style="width: 350px;">
<%
       int counter = 0;
       foreach (var editor in Model.Editors)
       {
%>
    <li>
        <img src="/Images/delete.png" alt="Remove editor" title="Remove editor" class="remove-editor link" />
        <%:editor.UserName%><input type="hidden" name="Editors[<%:counter%>]" value="<%:editor.Id%>" />
    </li>
<%
           counter++;
       }
%>
</ul>

<div class="form-row">
    <button type="submit">Save</button>
    <button id="cancel-btn">Cancel</button>
</div>

<% } %>

</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

            $('#cancel-btn').click(function () {
                location.href = '/Study/Update/<%: Model.Id %>';
                return false;
            });

            $('#user-search').keypress(function (event) {
                if (!((event.which != null && event.which == 13) || (event.keyCode != null && event.keyCode == 13)))
                    $('#userid').val('0');
            });

            $('#user-search').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '/User/Find',
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
                    $('#userid').val(ui.item.id);
                }
            });

            $('#add-user').click(function () {
                if (+($('#userid').val()) > 0) {
                    var newIndex = $('#editor-list>li').length;
                    $('#editor-list').append(
                        '<li>' +
                        '<img src="/Images/delete.png" alt="Remove editor" title="Remove editor" class="remove-editor link" />' +
                        $('#user-search').val() + '<input type="hidden" name="Editors[' + newIndex + ']" value="' + $('#userid').val() + '" /></li>');
                    $('#no-editors').hide();
                    initRemoveEditorButtons();
                    $('#user-search').val('');
                    $('#userid').val('0');
                    $('#search').focus();
                }
                return false;
            });

            initRemoveEditorButtons();
        });

        function initRemoveEditorButtons() {
            $('#editor-list>li>img.remove-editor').unbind('click');
            $('#editor-list>li>img.remove-editor').click(function () {
                $(this).closest('li').remove();
                if (0 == $('#editor-list>li').length) {
                    $('#no-editors').show();
                    return;
                }
                $('#editor-list>li>input').each(function (index) {
                    var newName = $(this).attr('name').replace(/\[\d+\]/, '[' + index + ']');
                    setElementName($(this), newName);
                });
            });
        }
    </script>
</asp:Content>
