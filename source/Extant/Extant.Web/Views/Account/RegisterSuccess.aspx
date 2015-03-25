<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create a New Account
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Request for New Account</h2>

<p>Your request for a user account has been received.</p>
<p>You have been sent an email to verify your ownership of the email address that you supplied.</p>  
<p>If you have not receive an email after 24 hours, please contact <a href="mailto:<%=ConfigurationManager.AppSettings["SupportEmail"]%>"><%=ConfigurationManager.AppSettings["SupportEmail"]%></a></p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
