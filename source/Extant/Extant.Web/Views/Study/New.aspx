<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Extant.Web.Models.StudyModel>" %>
<%@ Import Namespace="Extant.Data.Entities" %>
<%@ Import Namespace="Extant.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    New Study
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>New Study</h2>

    <% using (Html.BeginFormWithAttributes(new { @class = "wide", enctype = "multipart/form-data" })){%>

    <% Html.RenderPartial("StudyAndContactDetails", Model); %>

    <fieldset class="withborder">
        <legend>Files</legend>
        <%: Html.LabelValidationAndFileFor(m => m.PatientInformationLeaflet, null, false)%>
        <%: Html.LabelValidationAndFileFor(m => m.ConsentForm, null, false)%>
        <%: Html.LabelValidationAndYesNoRadioFor(m => m.HasDataAccessPolicy)%>
        <%: Html.LabelValidationAndFileFor(m => m.DataAccessPolicy, null, false)%>

        <p>
            <strong>Additional Files</strong>
            <span class="help-icon" title="Click Add File to associate additional files with the study. For each additional file you must select the file to upload, a description of the file and the type of the file.">?</span>
        </p>
        <p class="error" style="display:none" id="additional-files-error">
            One or more of the Additional Files do not contain all of the required information.
        </p>
        <div id="additional-files" class="form-table">
        </div>
        <p id="no-additional-files"><em>No additional files have been added</em></p>
        <p><button id="add-file-btn">Add File</button></p>

    </fieldset>


    <div class="form-row">
        <button type="submit">Next&nbsp;&gt;</button>
    </div>

    <% } %>

    <div id="additional-file-template" style="display:none">
        <div class="form-row">
            <input type="file" class="file" name="AdditionalDocuments[x].File" />
            <input type="text" name="AdditionalDocuments[x].Description" value="Description" class="unaccessed" />
            <select name="AdditionalDocuments[x].DocumentType">
                <option value="">-- Please select --</option>
<%
    foreach ( var item in DocumentType.Other.EnumSelectList())
    {
%>
                <option value="<%:item.Value %>"><%:item.Text %></option>
<%        
    } 
%>
            </select>
            <img src='/Images/delete.png' alt="Remove file" title="Remove file" class="link" />
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/requiredif.js") %>" type="text/javascript"></script>
    <script src="<%: Url.VersionedContent("~/Scripts/diseaseareas.js") %>" type="text/javascript"></script>
    <script src="<%: Url.VersionedContent("~/Scripts/studydetails.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            initStudyDetails();
        });
    </script>
</asp:Content>
