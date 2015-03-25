<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Extant.Web.Models.PagedStudyListModel>" %>
<%@ Import Namespace="NWeH.Paging" %>

<ul class="arrowed">
<% foreach (var study in Model.Studies){ %>
    <li>
        <a href="/Study/Index/<%:study.Id %>"><%: study.StudyName %></a>
        <div class="description"><span class="ellipsis_text"><%:study.Description %></span></div>
    </li>
<% } %>
</ul>

<div class="pager">
    <%=Html.AjaxPager(Model.Studies.PageSize, Model.Studies.PageIndex, Model.Studies.TotalItemCount, "study-list",
                        new { controller = Model.Controller, action = Model.Action })%>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        $('.description').ThreeDots({ max_rows: 2 });
    });
</script>