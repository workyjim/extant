<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="loginTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Log On
</asp:Content>

<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Log On</h2>
    <p>
        Please enter your user name and password. <%: Html.ActionLink("Register", "Register") %> if you don't have an account.
    </p>

    <% using (Html.BeginForm()) { %>
        <fieldset>                
            <div class="form-row">
                <label for="UserName">Email: <span class="required">*</span></label>
            </div>
            <div class="form-row">
                <input type="text" name="username" class="login" />
            </div>
                
            <div class="form-row">
                <label for="Password">Password <span class="required">*</span></label>
            </div>
            <div class="form-row">
                <input type="password" name="password" class="login" />
            </div>
                
            <div class="form-row">
                <button type="submit">Log&nbsp;On</button>
            </div>
        </fieldset>
        <%: Html.ValidationMessage("_FORM") %>
    <% } %>

    <div style="border-top: 1px dotted #888888; margin-top: 30px; padding-top: 10px;">
    <h3>Forgotten your password?</h3>
    <p>Enter your email below to reset your password.</p>
    <%: Html.ValidationMessage("reset") %>
    <form method="post" action="/Account/ResetPassword" autocomplete="off">
        <fieldset>
            <div class="form-row">
                <label>Email: <span class="required">*</span></label>
            </div>
            <div class="form-row">
                <input type="text" name="email" class="login" />
            </div>
            <div class="form-row">
                <button type="submit">Reset</button>
            </div>
        </fieldset>
    </form>

    </div>
</asp:Content>
