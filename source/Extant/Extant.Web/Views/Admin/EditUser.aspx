<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Extant.Web.Models.EditUserModel>" %>
<%@ Import Namespace="Extant.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit User
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Edit User</h2>

    <% using (Html.BeginForm(null, null, FormMethod.Post, new{@autocomplete="off"})) { %>
        <%: Html.ValidationSummary(true, "Editing the account was unsuccessful. Please correct the errors and try again.") %>
        <fieldset class="withborder">
            <legend>User Details</legend>
            <%: Html.LabelValidationAndTextBoxFor(m => m.UserName, new { @class = "login" }, true )%>
            <%: Html.LabelValidationAndTextBoxFor(m => m.Email, new { @class = "login" }, true)%>
            <div class="form-row">
                <label>Disease Areas <span class="required">*</span></label>
            </div>
            <div class="form-row">
                <div id="disease-areas" class="multi-row" style="width: 698px">
<%        
    var index = 0;
    var daCount = Model.DiseaseAreas.Count();
    foreach (var da in Model.DiseaseAreas)
    {
%>
                    <div class="form-row">
                        <%: Html.DropDownList(string.Format("DiseaseAreas[{0}]", index), new SelectList(Model.AllDiseaseAreas, "Id", "DiseaseAreaName", da), "-- Please select --", new { id = "" })%>
                        <img src="/Images/minus.png" alt="Remove Disease Area" title="Remove Disease Area" class="link remove-disease-area sixteenpx" <%= 0 == index && 1 == daCount ? "style=\"display:none\"" : "" %> />
                        <img src="/Images/plus.png" alt="Add Disease Area" title="Add Disease Area" class="link add-disease-area sixteenpx" <%= index < (daCount - 1) ? "style=\"display:none\"" : "" %> />
                    </div>                
<%
        index++;
    }
%>
                </div>
            </div>
        </fieldset>
<%  if ( Model.HasAdminRole ){ %>
        <fieldset class="withborder">
            <legend>Roles</legend>
            <%: Html.LabelAndCheckboxFor(m => m.IsAdministrator, new{@class="check"}, false) %>
            <%: Html.LabelAndCheckboxFor(m => m.IsHubLead, new { @class = "check" }, false)%>
        </fieldset>
<% } %>
        <fieldset class="withborder">
            <legend>Password</legend>
            <p><em>Password and Confirm Password need only be completed if you wish to change the user's password.</em></p>
            <%: Html.LabelValidationAndPasswordFor(m => m.Password, new { @class = "login" }, true)%>
            <%: Html.LabelValidationAndPasswordFor(m => m.ConfirmPassword, new { @class = "login" }, true)%>
        </fieldset>
        <fieldset class="withborder">
            <legend>Status</legend>
            <span id="userId" style="display:none"><%:Model.Id %></span>
            <span id="currentemail" style="display:none"><%:Model.Email %></span>
            <p>Approved: <span id="approved"><%: Model.IsApproved ? "Yes" : "No" %></span>
<% if (!Model.IsApproved) { %>
            <span>(<span id="approve" class="link">Approve</span>)</span>
<% } %>        
            </p>
            <p>Email Validated: <span id="emailValidated"><%: Model.EmailValidated ? "Yes" : "No" %></span>
            <% if (!Model.EmailValidated) { %>
                    <span>(<span id="validateEmail" class="link">Validate</span>)</span>
            <% } %>        
            </p>
            <p>Locked: <span id="locked"><%: Model.IsLockedOut ? "Yes" : "No" %></span>
<% if (Model.IsLockedOut) { %>
            <span>(<span id="unlock" class="link">Unlock</span>)</span>
<% } %>        
            </p>  
            <p>Last Login: <%:Model.LastLoginDate %></p>  
        </fieldset>
        <div class="form-row">
            <button type="submit">Update</button>
        </div>
    <% } %>



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.VersionedContent("~/Scripts/diseaseareas.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#page-body form').data("validator").settings.submitHandler = function (form) {
                if (validateDiseaseAreas()) {
                    form.submit();
                }
            };
            $('#page-body form').bind("invalid-form.validate", function () {
                validateDiseaseAreas();
            });

            $('.add-disease-area').click(function () {
                addDiseaseArea(this);
            });
            $('.remove-disease-area').click(function () {
                removeDiseaseArea(this);
            });

            $('#approve').click(function () {
                var userId = $('#userId').text();
                $.ajax({
                    type: 'POST',
                    url: '/Admin/ApproveUser/'+userId,
                    beforeSend: function () {
                        $('#approve').after('<img src="/Images/indicator.gif" alt="Working..." title="Working..." />');
                    },
                    success: function () {
                        alert('The account has been approved successfully');
                        $('#approve').parent().remove();
                        $('#approved').text('Yes');
                    },
                    error: function () {
                        alert('An error occurred whilst trying to approve the account');
                        $('#approve').next().remove();
                    }
                });
            });

            $('#validateEmail').click(function () {
                var userId = $('#userId').text();
                $.ajax({
                    type: 'POST',
                    url: '/Admin/ValidateEmail/' + userId,
                    beforeSend: function () {
                        $('#validateEmail').after('<img src="/Images/indicator.gif" alt="Working..." title="Working..." />');
                    },
                    success: function () {
                        alert('The email has been set to validated');
                        $('#validateEmail').parent().remove();
                        $('#emailValidated').text('Yes');
                    },
                    error: function () {
                        alert('An error occurred whilst trying to set the email to validated');
                        $('#validateEmail').next().remove();
                    }
                });
            });

            $('#unlock').click(function () {
                var userId = $('#userId').text();
                $.ajax({
                    type: 'POST',
                    url: '/Admin/UnlockUser/' + userId,
                    beforeSend: function () {
                        $('#unlock').after('<img src="/Images/indicator.gif" alt="Working..." title="Working..." />');
                    },
                    success: function () {
                        alert('The account has been unlocked successfully');
                        $('#unlock').parent().remove();
                        $('#locked').text('No');
                    },
                    error: function () {
                        alert('An error occurred whilst trying to unlock the account');
                        $('#unlock').next().remove();
                    }
                });
            });

        });
    </script>
</asp:Content>
