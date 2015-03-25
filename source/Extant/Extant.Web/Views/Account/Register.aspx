<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Extant.Web.Models.RegisterModel>" %>

<%@ Import Namespace="Extant.Web.Helpers" %>

<asp:Content ID="registerTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Create a New Account
</asp:Content>

<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Create a New Account</h2>
    <p>
        Use the form below to create a new account. 
    </p>
    <p>
        Passwords are required to be a minimum of <%: Membership.MinRequiredPasswordLength %> characters in length.
    </p>

    <% using (Html.BeginForm(null, null, FormMethod.Post, new { @autocomplete = "off" })){ %>
        <%: Html.ValidationSummary(true, "Account creation was unsuccessful. Please correct the errors and try again.") %>
        <fieldset>
            <%: Html.LabelValidationAndTextBoxFor(m => m.Name, new { @class = "login" }, true )%>
            <%: Html.LabelValidationAndTextBoxFor(m => m.Email, new { @class = "login" }, true)%>
            <%: Html.LabelValidationAndDropDownListFor(m => m.DiseaseAreaId, new SelectList(Model.DiseaseAreas, "Id", "DiseaseAreaName"), "-- Please select --", null, true)%>
            <div class="form-row"><em>If you are involved in multiple disease areas please select the one which you work in most of the time</em></div>
            <%: Html.LabelValidationAndPasswordFor(m => m.Password, new { @class = "login" }, true)%>
            <%: Html.LabelValidationAndPasswordFor(m => m.ConfirmPassword, new { @class = "login" }, true)%>
                
            <div class="form-row">
                <button type="submit">Register</button>
            </div>
        </fieldset>
    <% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>
</asp:Content>