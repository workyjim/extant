<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<bool>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Administration
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Administration</h2>

<ul class="arrowed">
    <li><a href="/Admin/Users">Manage Users</a></li>
    <li><a href="/Admin/DiseaseAreas">Manage Disease Areas</a></li>
<% if ( Model ){ %>
    <li><a href="/Admin/RebuildSearchIndex">Rebuild Search Index</a></li>
<% } %>
</ul>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
