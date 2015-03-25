<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Extant.Web.Models.SearchResultsModel>" %>
<%@ Import Namespace="NWeH.Paging" %>

<p>Results: <%: Model.Studies.FirstResult %>-<%: Model.Studies.LastResult %> of <%: Model.Studies.TotalItemCount %></p>

<ul class="arrowed">
<% foreach (var study in Model.Studies){ %>
    <li><a href="/Study/Index/<%:study.Id %>"><%: study.StudyName %></a>
    <div class="description"><span class="ellipsis_text"><%:study.Description %></span></div>
    </li>
<% } %>
</ul>

<div class="pager">
    <%=Html.AjaxPager(Model.Studies.PageSize, Model.Studies.PageIndex, Model.Studies.TotalItemCount, "search-results",
                        new { controller = "Study", action = "SearchResults", term = Model.Term, da = Model.DiseaseArea, 
                              sd = Model.StudyDesign, st = Model.StudyStatus, s = Model.Samples })%>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        $('.description').ThreeDots({ max_rows: 2 });
    });
</script>