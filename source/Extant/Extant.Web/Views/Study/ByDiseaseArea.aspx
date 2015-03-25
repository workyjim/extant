<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Extant.Web.Models.ByDiseaseAreaModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Studies By Disease Area | <%:Model.DiseaseArea.DiseaseAreaName %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2><%:Model.DiseaseArea.DiseaseAreaName %></h2>

<p>This disease area has <%: Model.Studies.TotalItemCount %> <%: 1 == Model.Studies.TotalItemCount ? "study" : "studies" %>.</p>

<% if (Model.Studies.Any()) { %>
<div id="search-results">
<% Html.RenderPartial("ByDiseaseAreaPartial", Model); %>
</div>
<% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
