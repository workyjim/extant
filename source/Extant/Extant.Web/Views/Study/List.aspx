<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Extant.Web.Models.ListStudiesModel>" %>
<%@ Import Namespace="Extant.Web.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    My Studies
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>My Studies</h2>

<% 
    if (Model.MyStudies.Any())
    {
%>
    <div id="study-list">
        <% Html.RenderPartial("MyStudies", new PagedStudyListModel { Studies = Model.MyStudies, Controller = "Study", Action = "MyStudies" }); %>        
    </div>
<%
    }
    else
    {
%>
    <p><em>You do not currently have any studies.</em></p>
<%
    }
%>

<% if (Model.IsHubLead){ %>
    <p><a href="/Study/ListHub">Studies in My Disease Area(s)</a></p>
<% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
