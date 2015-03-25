<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Extant.Web.Models.AddUserModel>" %>
<%@ Import Namespace="Extant.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Add User
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Add User</h2>

    <% using (Html.BeginForm(null, null, FormMethod.Post, new { @autocomplete = "off" })){ %>
        <%: Html.ValidationSummary(true, "Creating the account was unsuccessful. Please correct the errors and try again.") %>
        <fieldset class="withborder">
            <legend>User Details</legend>
            <%: Html.LabelValidationAndTextBoxFor(m => m.UserName, new { @class = "login" }, true )%>
            <%: Html.LabelValidationAndTextBoxFor(m => m.Email, new { @class = "login" }, true)%>
            <div class="form-row">
                <label>Disease Areas <span class="required">*</span></label>
            </div>
            <div class="form-row">
                <div id="disease-areas" class="multi-row" style="width: 698px">
                    <div class="form-row">
                        <%: Html.DropDownList("DiseaseAreas[0]", new SelectList(Model.AllDiseaseAreas, "Id", "DiseaseAreaName"), "-- Please select --", new { id = "" })%>
                        <img src="/Images/minus.png" alt="Remove Disease Area" title="Remove Disease Area" class="link remove-disease-area sixteenpx" style="display:none" />
                        <img src="/Images/plus.png" alt="Add Disease Area" title="Add Disease Area" class="link add-disease-area sixteenpx" />
                    </div>
                </div>
            </div>        
        </fieldset>
        <fieldset class="withborder">
            <legend>Roles</legend>
            <%: Html.LabelAndCheckboxFor(m => m.IsAdministrator, new{ @class = "check"}, false) %>
            <%: Html.LabelAndCheckboxFor(m => m.IsHubLead, new { @class = "check" }, false)%>
        </fieldset>
        <fieldset class="withborder">
            <%: Html.LabelValidationAndPasswordFor(m => m.Password, new { @class = "login" }, true)%>
            <%: Html.LabelValidationAndPasswordFor(m => m.ConfirmPassword, new { @class = "login" }, true)%>
        </fieldset>
        <div class="form-row">
            <button type="submit">Add</button>
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

        });
    </script>
</asp:Content>
