<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Extant.Web.Models.UserModel>" %>
<%@ Import Namespace="Extant.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    My Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>My Details</h2>

<p>Update your details here or <a href="/Account/ChangePassword">change your password</a>.</p>

<% using (Html.BeginForm()) { %>
    <%: Html.ValidationSummary(true, "Updating your details was unsuccessful. Please correct the errors and try again.") %>
        <fieldset>
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
                
            <div class="form-row">
                <button type="submit">Save</button>
            </div>
        </fieldset>
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
