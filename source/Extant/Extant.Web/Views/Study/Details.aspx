<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Extant.Web.Models.StudyModel>" %>
<%@ Import Namespace="Extant.Data.Entities" %>
<%@ Import Namespace="Extant.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Study | <%: Model.StudyName %> | Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2><%: Model.StudyName %> - Details</h2>

    <% using (Html.BeginFormWithAttributes(new { @class = "wide", enctype = "multipart/form-data" })){%>

    <%: Html.HiddenFor(m => m.IsNew) %>
    <% Html.RenderPartial("StudyAndContactDetails", Model); %>

    <fieldset class="withborder">
        <legend>Files</legend>
        <div class="form-row">
            <label>Patient Information Leaflet</label>
            <%: Html.FileFor(m => m.PatientInformationLeaflet) %>
            <%: Html.HelpFor(m => m.PatientInformationLeaflet)%>
            <%: Html.HiddenFor(m => m.PatientInformationLeafletRemoved) %>
<%  if (null != Model.PatientInformationLeafletCurrent) { %>
            <span>
                <a href="/Study/File/<%:Model.PatientInformationLeafletCurrent.Id %>" target="_blank"><%:Model.PatientInformationLeafletCurrent.FileName%></a> <img src="/Images/delete.png" alt="Delete the Patient Information Leaflet" title="Delete the Patient Information Leaflet" class="link remove-file" />
            </span>
<%  }  %>
        </div>

        <div class="form-row">
            <label>Consent Form</label>
            <%: Html.FileFor(m => m.ConsentForm) %>
            <%: Html.HelpFor(m => m.ConsentForm)%>
            <%: Html.HiddenFor(m => m.ConsentFormRemoved)%>
<%  if (null != Model.ConsentFormCurrent){ %>
            <span>
                <a href="/Study/File/<%:Model.ConsentFormCurrent.Id %>" target="_blank"><%:Model.ConsentFormCurrent.FileName %></a> <img src="/Images/delete.png" alt="Delete the Consent Form" title="Delete the Consent Form" class="link remove-file" />
            </span>
<%  }  %>
        </div>

        <%: Html.LabelValidationAndYesNoRadioFor(m => m.HasDataAccessPolicy)%>

        <div class="form-row">
            <label>Data Access Policy</label>
            <%: Html.FileFor(m => m.DataAccessPolicy) %>
            <%: Html.HelpFor(m => m.DataAccessPolicy) %>
            <%: Html.HiddenFor(m => m.DataAccessPolicyRemoved) %>
<%  if (null != Model.DataAccessPolicyCurrent){ %>
            <span>
                <a href="/Study/File/<%:Model.DataAccessPolicyCurrent.Id %>" target="_blank"><%:Model.DataAccessPolicyCurrent.FileName%></a> <img src="/Images/delete.png" alt="Delete the Data Access Policy" title="Delete the Data Access Policy" class="link remove-file" />
            </span>
<%  }  %>
        </div>

        <p>
            <strong>Additional Files</strong>
            <span class="help-icon" title="Click Add File to associate additional files with the study. For each additional file you must enter the file to upload, a description of the file and the type of the file.">?</span>
        </p>
        <p class="error" style="display:none" id="additional-files-error">
            One or more of the Additional Files do not contain all of the required information.
        </p>
        <div id="additional-files" class="form-table">
<%
            int index = 0;
            foreach (var file in Model.AdditionalDocuments)
            {
%>
            <div class="form-row">
                <input type="hidden" name="AdditionalDocuments[<%:index %>].Id" value="<%:file.Id %>" />
                <input type="hidden" name="AdditionalDocuments[<%:index %>].FileRemoved" value="false" />
                <input type="file" class="file" name="AdditionalDocuments[<%:index %>].File" />
                <input type="text" name="AdditionalDocuments[<%:index %>].Description" value="<%:file.Description %>" />
                <select name="AdditionalDocuments[<%:index %>].DocumentType">    
<%
                foreach ( var item in DocumentType.Other.EnumSelectList())
                {
%>
                    <option value="<%:item.Value %>" <%: item.Value == file.DocumentType.ToString() ? "selected=\"selected\"" : "" %>><%:item.Text %></option>
<%        
                } 
%>
                </select>
                <span><a href="/Study/File/<%:file.FileCurrent.Id %>" target="_blank"><%:file.FileCurrent.FileName%></a></span>
                <img src='/Images/delete.png' alt="Remove file" title="Remove file" class="link" />
            </div>
<%
                index++;
            }
%>
        </div>

        <p id="no-additional-files" style="<%: Model.AdditionalDocuments.Any() ? "display: none" : ""  %>"><em>No additional files have been added</em></p>
        <p><button id="add-file-btn">Add File</button></p>

    </fieldset>


    <div class="form-row">
<% if (Model.IsNew) { %>
        <button type="submit">Next&nbsp;&gt;</button>
<% } else { %>
        <button type="submit">Save</button>
        <button id="cancel-btn">Cancel</button>
<% } %>        
    </div>

    <% } %>

    <div id="additional-file-template" style="display:none">
        <div class="form-row">
            <input type="file" class="file" name="AdditionalDocuments[x].File" />
            <input type="text" name="AdditionalDocuments[x].Description" />
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
<% if (Model.IsNew){ %>
            initWindowUnload();
<% } %>

            initStudyDetails();

            setDataAccessPolicyStatus();
            setPortfolioNumberStatus();

            $('.remove-file').click(function () {
                $(this).parent().siblings('[name$=Removed]').val('true');
                $(this).parent().fadeOut();
            });

            $('#additional-files>div>img').click(function () {
                $(this).siblings('[name$=FileRemoved]').val('true');
                $(this).parent().hide();
                if (0 == $('#additional-files>div:visible').length) $('#no-additional-files').show();
            });

            $('#cancel-btn').click(function () {
                location.href = '/Study/Update/<%: Model.Id %>';
                return false;
            });

        });
    </script>
</asp:Content>
