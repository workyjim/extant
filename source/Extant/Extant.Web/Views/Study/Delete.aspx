<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<string>" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
    Study Deleted
</asp:Content>
<asp:Content runat="server" ID="Head" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
<h2>Study Deleted</h2>
<p>The study '<%:Model %>' has been successfully deleted.</p>
</asp:Content>
