<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<bool>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create a New Account
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Email Address Validation</h2>

<% if (Model){ %>
<p>Your account has been approved. Please <a href="/Account/LogOn">Log On</a> to start using the <%=ConfigurationManager.AppSettings["OrganisationName"]%> <%=ConfigurationManager.AppSettings["CatalogueName"]%>.</p>
<% } else { %>
<p>Your email address has been validated.</p>  
<p>Your details will now be reviewed and you will be informed by email when your account has been activated.</p>
<p>This may take up to three working days.</p>  
<p>If you have not received an email confirmation by then, please contact <a href="mailto:<%=ConfigurationManager.AppSettings["SupportEmail"]%>"><%=ConfigurationManager.AppSettings["SupportEmail"]%></a></p>
<% } %>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
