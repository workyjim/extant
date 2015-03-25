<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Extant.Web.Models.ListStudiesModel>" %>
<%@ Import Namespace="Extant.Web.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    My Studies
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Studies</h2>

<% 
    if (Model.MyStudies.Any())
    {
%>
    <div id="study-list">
    <% Html.RenderPartial("MyStudies", new PagedStudyListModel { Studies = Model.MyStudies, Controller = "Study", Action = "HubStudies" }); %>        
    </div>
<%
    }
    else
    {
%>
    <p><em>There are not currently any studies in your disease area(s).</em></p>
<%
    }
%>

    <p><a href="/Study/List">Back to My Studies</a></p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
