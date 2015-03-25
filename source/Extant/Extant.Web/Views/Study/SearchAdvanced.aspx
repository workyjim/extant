<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Extant.Web.Models.DiseaseAreaBasicModel>>" %>
<%@ Import Namespace="Extant.Data.Entities" %>
<%@ Import Namespace="Extant.Data.Search" %>
<%@ Import Namespace="Extant.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Find a Study | Advanced
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Find a Study - Advanced</h2>

<p>Use this form to construct an advanced query to search for studies.
<span class="help-icon" title="An advanced search query is constructed by defining a set of query terms that are combined with boolean (AND, OR, NOT) operators. For each query term you must select the field to query and the value to match against.">?</span>
</p>

<div class="clear">
<% using (Html.BeginForm()){%>
    <div id="search-lines">
        <div class="form-row">
            <%: Html.DropDownList("SearchLines[0].SearchOperator", SearchOperator.AND.EnumSelectList(), new { id="", style="visibility: hidden"}) %>
            <%: Html.DropDownList("SearchLines[0].SearchField", SearchField.Any.EnumSelectList(), new { id = "" })%>
            <input type="text" name="SearchLines[0].SearchTerm" />
            <input type="hidden" name="SearchLines[0].IsPhrase" value="false" />
            <img src="/Images/minus.png" class="link remove-search-line sixteenpx" alt="Remove line from search" title="Remove line from search" style="display:none" />
            <img src="/Images/plus.png" class="link add-search-line sixteenpx" alt="Add line to search" title="Add line to search" />
        </div>
    </div>
    <div class="form-row">
        <button type="submit" id="search-btn">Search</button>
    </div>
<% } %>
</div>

<div id="search-results" class="topborder">
</div>

<div id="search-line-template" style="display:none;">
    <div class="form-row">
        <%: Html.DropDownList("SearchLines[x].SearchOperator", SearchOperator.AND.EnumSelectList(), new {id=""}) %>
        <%: Html.DropDownList("SearchLines[x].SearchField", SearchField.Any.EnumSelectList(), new { id = "" })%>
        <input type="text" name="SearchLines[x].SearchTerm" class="generic" />
        <input type="hidden" name="SearchLines[x].IsPhrase" value="false" />
        <img src="/Images/minus.png" class="link remove-search-line sixteenpx" alt="Remove line from search" title="Remove line from search"/>
        <img src="/Images/plus.png" class="link add-search-line sixteenpx" alt="Add line to search" title="Add line to search" />
    </div>
</div>

<div id="text-template" style="display:none;">
    <input type="text" name="SearchTerm" class="generic" />
</div>

<div id="disease-area-template" style="display:none;">
    <%: Html.DropDownList("SearchTerm", Model.Select(da => new SelectListItem{Text=da.DiseaseAreaName}), "-- Please select --", new { id = "" })%>
</div>

<div id="study-design-template" style="display:none;">
    <%: Html.DropDownList("SearchTerm", StudyDesign.Observational.EnumSelectListNoValues(), "-- Please select --", new { id = "" })%>
</div>

<div id="study-status-template" style="display:none;">
    <%: Html.DropDownList("SearchTerm", StudyStatus.Recruiting.EnumSelectListNoValues(), "-- Please select --", new { id = "" })%>
</div>

<div id="sample-template" style="display:none;">
    <%: Html.DropDownList("SearchTerm",
                new[] { "DNA", "Serum", "Plasma", "Whole Blood", "Saliva", "Tissue", "Cell", "Other" }.Select(s => new SelectListItem{Text = s}), 
        "-- Please select --", new { id = "" })%>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

            $('form').ajaxForm({
                beforeSubmit: function () {
                    $('#search-results').empty();
                    $('#search-btn').after('<img src="/Images/indicator.gif" alt="Working..." title="Working..." />');
                },
                success: function (html) {
                    $('#search-btn').next().remove();
                    $('#search-results').html(html);
                },
                error: function () {
                    $('#search-btn').next().remove();
                    alert('An error has occurred. Please try again.');
                }
            });

            $('#search-lines>div>select[name$=SearchField]').change(function () {
                searchFieldChange(this);
            });
            $('#search-lines>div>img.add-search-line').click(function () {
                addSearchLines(this);
            });
            $('#search-lines>div>img.remove-search-line').click(function () {
                removeSearchLines(this);
            });
        });

        function searchFieldChange(dropdown) {
            var searchTerm = $(dropdown).siblings('[name$=SearchTerm]');
            var isPhrase = true;
            var currentValue = searchTerm.val();
            if (searchTerm.is('select')) currentValue = searchTerm.find('option:selected[value!=""]').text();
            switch (+$(dropdown).val()) {
                case 4: //disease area
                    searchTerm.replaceWith($('#disease-area-template>select').clone().attr('name', searchTerm.attr('name')));
                    break;
                case 5: //study design
                    searchTerm.replaceWith($('#study-design-template>select').clone().attr('name', searchTerm.attr('name')));
                    break;
                case 6: //study status
                    searchTerm.replaceWith($('#study-status-template>select').clone().attr('name', searchTerm.attr('name')));
                    break;
                case 9: //samples
                    searchTerm.replaceWith($('#sample-template>select').clone().attr('name', searchTerm.attr('name')));
                    break;
                case 13: //data fields
                    var clone = $('#text-template>input').clone().attr('name', searchTerm.attr('name')).removeClass('generic').val(currentValue);
                    clone.autocomplete({
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
                        minLength: 3
                    });
                    searchTerm.replaceWith(clone);
                    break;
                default:
                    isPhrase = false;
                    if (!searchTerm.hasClass('generic')) {
                        searchTerm.replaceWith($('#text-template>input').clone().attr('name', searchTerm.attr('name')).val(currentValue));
                    }
            }
            $(dropdown).siblings('[name$=IsPhrase]').val(isPhrase);
        }

        function addSearchLines(button) {
            var newLine = $('#search-line-template>div').clone();
            var newIndex = $('#search-lines>div').length;
            newLine.find('input,select').each(function () {
                var newName = $(this).attr('name').replace('[x]', '[' + newIndex + ']');
                setElementName($(this), newName);
            });
            newLine.find('[name$=SearchField]').change(function () {
                searchFieldChange(this);
            });
            newLine.find('img.add-search-line').click(function () {
                addSearchLines(this);
            });
            newLine.find('img.remove-search-line').click(function () {
                removeSearchLines(this);
            });
            $(button).hide();
            $('#search-lines').append(newLine);
            $('#search-lines>div:first>img.remove-search-line').show();
        }

        function removeSearchLines(button) {
            $(button).parent().remove();
            $('#search-lines>div').each(function (index, row) {
                $(row).find('input,select').each(function () {
                    var newName = $(this).attr('name').replace(/\[\d+\]/, '[' + index + ']');
                    setElementName($(this), newName);
                });
            });
            $('#search-lines>div:last>img.add-search-line').show();
            $('#search-lines>div:first>select[name$=SearchOperator]').css('visibility','hidden');
            if (1 == $('#search-lines>div').length)
                $('#search-lines>div>img.remove-search-line').hide();
        }

    </script>
</asp:Content>
