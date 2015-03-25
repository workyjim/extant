<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Extant.Web.Models.StudyBasicModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Study | <%: Model.StudyName %> | Update
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2><%: Model.StudyName %> - Update</h2>

<% if ( !Model.Published ){ %>
<p><em>This study has not been published; it will not appear in the searchable catalogue until it is published</em></p>
<%} %>

<p>Study Owner: <%:Model.Owner.UserName %></p>

<ul class="arrowed">
    <li><a href="/Study/Details/<%:Model.Id %>">Update Study Details</a></li>
    <li><a href="/Study/Publications/<%:Model.Id %>">Update Publications</a></li>
    <li><a href="/Study/DataFields/<%:Model.Id %>">Update Data Fields</a></li>
    <li><a href="/Study/Samples/<%:Model.Id %>">Update Samples</a></li>
    <li><a href="/Study/Editors/<%:Model.Id %>">Update Study Editors</a></li>
</ul>
<% if ( Model.CanDelete || !Model.Published ){ %>
<ul class="arrowed">
<%      if ( Model.CanDelete ){ %>
    <li><a href="/Study/Delete/<%:Model.Id %>" onclick="return confirmDelete();">Delete Study</a></li>
<%      } %>
<%      if ( !Model.Published ){ %>
    <li><a href="/Study/Publish/<%:Model.Id %>" onclick="return confirmPublish();">Publish Study</a></li>
<%      } %>
</ul>
<% } %>

<p><a href="/Study/Index/<%:Model.Id %>">Return to study home page</a></p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
    function confirmDelete() {
        return confirm("The study '<%: Model.StudyName %>' will be permanently deleted. Are you sure you wish to continue?");
    }
    function confirmPublish() {
        return confirm("Once the study '<%: Model.StudyName %>' is published it will appear in the searchable catalogue. Are you sure you wish to continue?");
    }
</script>
</asp:Content>
