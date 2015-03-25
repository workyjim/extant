<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create a New Account
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Email Address Validation Failed</h2>

<p>This email validation link is not recognised.</p>
<p>Please ensure that your have copied the whole link above correctly.</p>
<p>For further help please contact <a href="mailto:<%=ConfigurationManager.AppSettings["SupportEmail"]%>"><%=ConfigurationManager.AppSettings["SupportEmail"]%></a></p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
