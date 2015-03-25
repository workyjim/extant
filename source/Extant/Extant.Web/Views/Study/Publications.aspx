<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Extant.Web.Models.StudyPublicationsModel>" %>
<%@ Import Namespace="Extant.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Study | <%: Model.StudyName %> | Publications
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2><%: Model.StudyName %> - Publications</h2>

<div>
    <h3>Find publications for the study by searching PubMed</h3>

    <p>Enter PubMed Document IDs or search terms to search the PubMed database for citations</p>

    <form id="search-publication" action="/Pubmed/Search" method="get">
        <fieldset>
            <div class="form-row">
                <input type="text" name="term" id="term" class="search" />
                <span class="help-icon" title="To search for publications enter a search term exactly as you would when searching on the PubMed website itself. To search by author enter the author as &quot;surname initials&quot; e.g. &quot;Smith JA&quot;">?</span>
                <button type="submit">Search</button>    
                <img src="/Images/indicator.gif" style="display: none" alt="Working..." />
            </div>
        </fieldset>
    </form>

</div>

<div class="topborder">
    <h3>Associated publications <span class="help-icon" title="These are the publications that have been associated with the study. To remove a publication from the study click on the cross icon adjacent to it. Clicking on the title of the publication will open the citation in PubMed in a new browser window.">?</span></h3>
    <p id="has-publications" <%= Model.Publications.Any() ? "" : "style=\"display: none;\"" %>>The following publications are associated with the study:</p>
    <p id="no-publications" <%= Model.Publications.Any() ? "style=\"display: none;\"" : "" %>><em>No publications have been associated with the study.</em></p>
    <form action="/Study/Publications/<%: Model.Id %>" method="post" id="publications">
        <input type="hidden" name="IsNew" value="<%: Model.IsNew %>" />
        <input type="hidden" name="action" value="" id="action" />
        <ul class="deletable-list compact">
<% 
    foreach (var pub in Model.Publications.Select((p,i) => new { Index = i, Publication = p}))
    {
%>
            <li>
                <img src="/Images/delete.png" alt="Remove publication from study" title="Remove publication from study" class="remove-from-study link" />
                <p><a href="<%:pub.Publication.Url %>" target="_blank" title="Click to open the publication in a new browser window"><%:pub.Publication.Title %></a></p>
                <p><%: string.Join(", ", pub.Publication.Authors) %></p>
                <p class="journal"><%:pub.Publication.Journal %> <%:pub.Publication.PublicationDate %></p>
                <input type="hidden" name="Publications[<%: pub.Index %>].Id" value="<%:pub.Publication.Id %>" />
                <input type="hidden" name="Publications[<%: pub.Index %>].Title" value="<%:pub.Publication.Title %>" />
                <input type="hidden" name="Publications[<%: pub.Index %>].Url" value="<%:pub.Publication.Url %>" />
                <input type="hidden" name="Publications[<%: pub.Index %>].Pmid" value="<%:pub.Publication.Pmid %>" />
            </li>
<%
    }
%>
        </ul>
        <div class="form-row">
<% if (Model.IsNew) { %>
            <button type="submit" id="prev-btn">&lt;&nbsp;Previous</button>
            <button type="submit" id="next-btn">Next&nbsp;&gt;</button>
<% } else { %>
            <button type="submit">Save</button>
            <button id="cancel-btn">Cancel</button>
<% } %>
        </div>
    </form>
</div>

<div style="display:none">
    <a href="#pubmed-search-results" id="pubmed-link">Open</a>
    <div id="pubmed-search-results">
        <h3>Pubmed Search Results <span class="help-icon" title="To select publications to associate with the study check the relevant checkboxes. You can select publications in more than one page of the results. When you are finished click Save to add all selected publications to the study.">?</span></h3>
        <div id="pubmed-results-list"></div>
        <ul id="selected-publications" style="display:none"></ul>
        <div class="topborder">
            <div class="form-row">
                <button id="save-pubmed-results">Save</button>
                <button id="cancel-pubmed-results">Cancel</button>
            </div>
        </div>
    </div>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/Content/fancybox/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="<%: Url.Content("~/Content/fancybox/jquery.fancybox-1.3.4.pack.js") %>" type="text/javascript"></script>
    <script src="<%: Url.VersionedContent("~/Scripts/pubmedsearch.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
<% if (Model.IsNew){ %>
            initWindowUnload();
<% } %>

            $('#cancel-btn').click(function () {
                location.href = '/Study/Update/<%: Model.Id %>';
                return false;
            });

            $('#publications').submit(function(){
                cancelWindowUnload();
            });

            $('#prev-btn').click(function(){
                $('#action').val('previous');
            });

            $('#next-btn').click(function(){
                $('#action').val('next');
            });

            $('#pubmed-link').fancybox({
                hideOnOverlayClick: false,
                type: 'inline',
                onClosed: function () {
                    $('#selected-publications').empty();
                }
            });

            $('#save-pubmed-results').click(function () {
                $('#selected-publications li').each(function () {
                    var index = $('#publications ul li').length;
                    var item = $(this).clone();
                    var title = item.find('a').text();
                    var url = item.find('a').attr('href');
                    var pmid = url.substr(url.lastIndexOf('/') + 1);
                    item.find('input').replaceWith('<img src="/Images/delete.png" alt="Remove publication from study" title="Remove publication from study" class="remove-from-study link" />');
                    item.append('<input type="hidden" name="Publications[' + index + '].Title" value="' + title + '" />');
                    item.append('<input type="hidden" name="Publications[' + index + '].Url" value="' + url + '" />');
                    item.append('<input type="hidden" name="Publications[' + index + '].Pmid" value="' + pmid + '" />');
                    $('#publications ul').append(item);
                });

                if ($('#publications ul li').length > 0) {
                    $('#has-publications').show();
                    $('#no-publications').hide();
                }
                initRemovePublicationButtons();

                $.fancybox.close();
            });

            $('#cancel-pubmed-results').click(function () {
                $.fancybox.close();
            });

            $('#search-publication').ajaxForm({
                beforeSend: function () {
                    $('#search-publication button').next().show();
                },
                complete: function () {
                    $('#search-publication button').next().hide();
                },
                success: function (html) {
                    $('#pubmed-results-list').html(html);
                    $('#pubmed-link').trigger('click');
                }
            });

            initRemovePublicationButtons();
        });

        function initRemovePublicationButtons() {
            $('#publications ul img.remove-from-study').unbind('click');
            $('#publications ul img.remove-from-study').click(function () {
                $(this).closest('li').remove();
                $('#publications ul li').each(function (i) {
                    $(this).find('input').each(function () {
                        var newName = $(this).attr('name').replace(/\[\d+\]/, '[' + i + ']');
                        setElementName($(this), newName);
                    });
                });
                if (0 == $('#publications ul li').length) {
                    $('#has-publications').hide();
                    $('#no-publications').show();
                }
            });
        }

    </script>
</asp:Content>
