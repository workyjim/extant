<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Extant.Web.Models.PubmedResultModel>" %>

<%@ Import Namespace="NWeH.Paging" %>

<p>You searched for <strong><%: Model.SearchTerm %></strong>.
<% if (Model.SearchResults.Any()) { %>
<p>Showing <%:Model.SearchResults.FirstResult %>-<%:Model.SearchResults.LastResult %> of <%: Model.SearchResults.TotalItemCount %>. Selected: <span id="selected-count">0</span></p>
<ul class="iconed-list compact">
<%
        foreach (var result in Model.SearchResults)
        {
%>
    <li class="select-publication">
        <input type="checkbox" class="check" />
        <p><a href="http://www.ncbi.nlm.nih.gov/pubmed/<%:result.Id%>" target="_blank" title="Click to open the publication in a new browser window"><%:result.Title%></a></p>
        <p><%: string.Join(", ", result.Authors) %></p>
        <p class="journal"><%: result.Journal %> <%:result.PublicationDate %></p>
    </li>
<%
        }
%>
    <li class="select-all">
        <input type="checkbox" class="check" />
        <p><em>Select all</em></p>
    </li>
</ul>

<div class="pager">
    <%=Html.AjaxPager(Model.SearchResults.PageSize, Model.SearchResults.PageIndex, Model.SearchResults.TotalItemCount, "pubmed-results-list",
                                                new { controller = "Pubmed", action = "Search", term = Model.SearchTerm })%>
</div>

<% } else { %>
    <p>No results were returned.</p>
<% } %>

<script type="text/javascript">
    $(document).ready(function () {
        initPubMedSearchResultDialog();
    });
</script>