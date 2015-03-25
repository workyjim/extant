<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Extant.Web.Models.StudiesByDataItemModel>" %>


    <h3><%: Model.DataItemName %></h3>

    <p>Included in the following studies:</p>

    <div style="overflow:auto; height: 240px; margin-bottom: 10px;">
    <ul class="arrowed">
    <% foreach (var study in Model.Studies.OrderBy(s => s.StudyName)) { %>
        <li><a href="/Study/Index/<%:study.Id %>"><%: study.StudyName%></a></li>
    <% } %>
    </ul>
    </div>

    <div class="form-row">
        <button id="close-btn">Close</button>
    </div>


<script type="text/javascript">
    $(document).ready(function () {
        $('button').button();
        $('#close-btn').click(function () {
            $.fancybox.close();
        });
    });
</script>