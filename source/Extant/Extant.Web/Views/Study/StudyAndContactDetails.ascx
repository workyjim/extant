<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Extant.Web.Models.StudyModel>" %>
<%@ Import Namespace="Extant.Data.Entities" %>
<%@ Import Namespace="Extant.Web.Helpers" %>

    <fieldset class="withborder">
        <legend>Study Details</legend>
        <%: Html.LabelValidationAndTextBoxFor(m => m.StudyName, null, false)%>
        <%: Html.LabelValidationAndTextAreaFor(m => m.Description, new{@rows = 5, @cols = 40} , false) %>
        <%: Html.LabelValidationAndTextBoxFor(m => m.StudySynonyms, null, false)%>
        <%: Html.LabelValidationAndTextBoxFor(m => m.StudyWebsite, null, false)%>
        <%: Html.LabelValidationAndDropDownListFor(m => m.StudyDesign, StudyDesign.Observational.EnumSelectList(), "-- Please select --", null, false)%>

        <div class="form-row">
            <label>Disease Areas <span class="required">*</span></label>
            <div id="disease-areas" class="multi-row" style="width: 698px">
<% 
    if (null == Model.DiseaseAreas || !Model.DiseaseAreas.Any())
    {
%>
                <div class="form-row">
                    <%: Html.DropDownList("DiseaseAreas[0]", new SelectList(Model.AllDiseaseAreas, "Id", "DiseaseAreaName"), "-- Please select --", new { id = "" })%>
                    <img src="/Images/minus.png" alt="Remove Disease Area" title="Remove Disease Area" class="link remove-disease-area sixteenpx" style="display:none" />
                    <img src="/Images/plus.png" alt="Add Disease Area" title="Add Disease Area" class="link add-disease-area sixteenpx" />
                    <span class="help-icon" title="The disease area of the study. If the study covers multiple disease areas then they can be added by clicking on the plus icon">?</span>
                </div>
<%        
    } 
    else
    {
        var index = 0;
        var daCount = Model.DiseaseAreas.Count();
        foreach (var da in Model.DiseaseAreas)
        {
%>
                <div class="form-row">
                    <%: Html.DropDownList(string.Format("DiseaseAreas[{0}]", index), new SelectList(Model.AllDiseaseAreas, "Id", "DiseaseAreaName", da), "-- Please select --", new { id = "" })%>
                    <img src="/Images/minus.png" alt="Remove Disease Area" title="Remove Disease Area" class="link remove-disease-area sixteenpx" <%= 0 == index && 1 == daCount ? "style=\"display:none\"" : "" %> />
                    <img src="/Images/plus.png" alt="Add Disease Area" title="Add Disease Area" class="link add-disease-area sixteenpx" <%= index < (daCount - 1) ? "style=\"display:none\"" : "" %> />
                    <span class="help-icon" title="The disease area of the study. If the study covers multiple disease areas then they can be added by clicking on the plus icon">?</span>
                </div>                
<%
            index++;
        }
    }
%>
            </div>
        </div>

        <%: Html.LabelValidationAndCalendarFor(m => m.StartDate, null, false)%>
        <%: Html.LabelValidationAndDropDownListFor(m => m.StudyStatus, StudyStatus.Recruiting.EnumSelectList(), "-- Please select --", null, false)%>
        <%: Html.LabelValidationAndTextBoxFor(m => m.RecruitmentTarget, null, false)%>
        <%: Html.LabelValidationAndTextBoxFor(m => m.ParticipantsRecruited, null, false)%>
        <%: Html.LabelValidationAndTextBoxFor(m => m.PrincipalInvestigator, null, false)%>
        <%: Html.LabelValidationAndTextBoxFor(m => m.Institution, null, false)%>
        <%: Html.LabelValidationAndTextBoxFor(m => m.Funder, null, false)%>
        <%: Html.LabelValidationAndYesNoRadioFor(m => m.OnPortfolio)%>
        <%: Html.LabelValidationAndTextBoxFor(m => m.PortfolioNumber, null, false)%>
    </fieldset>

    <fieldset class="withborder">
        <legend>Contact Details</legend>
        <p><em>Please enter the contact details of the person who should be contacted for further information on the study.</em></p>
        <%: Html.LabelValidationAndTextBoxFor(m => m.ContactName, null, false)%>
        <%: Html.LabelValidationAndTextAreaFor(m => m.ContactAddress, new{@rows = 5, @cols = 40}, false)%>
        <%: Html.LabelValidationAndTextBoxFor(m => m.ContactPhone, null, false)%>
        <%: Html.LabelValidationAndTextBoxFor(m => m.ContactEmail, null, false)%>
    </fieldset>

    <fieldset class="withborder">
        <legend>Longitudinal Study</legend>
        <%: Html.LabelValidationAndYesNoRadioFor(m => m.IsLongitudinal)%>
        <div class="form-row">
            <label id="time-points-label">Time Points <span class="required">*</span></label>
            <div id="time-points" class="multi-row">
<% 
    if (null != Model.TimePoints && Model.TimePoints.Any())
    {
        int counter = 0;
        foreach (var tp in Model.TimePoints)
        {
%>
                <div class="form-row">
                    <input type="hidden" name="TimePoints[<%:counter%>].Id" value="<%:tp.Id %>" />
                    <input type="text" name="TimePoints[<%:counter%>].Name" value="<%:tp.Name %>" />
                    <img src="/Images/minus.png" alt="Remove Time Point" title="Remove Time Point" class="link remove-time-point sixteenpx" />
                    <img src="/Images/plus.png" alt="Add Time Point" title="Add Time Point" class="link add-time-point sixteenpx" <%=Model.TimePoints.Count() - 1 == counter ? "" : "style=\"display:none\""  %> />
<%
            if (0 == counter)
            {
%>                    
                    <span class="help-icon" title="The time points of a longitudinal study i.e. Baseline, 6 Month Follow-Up etc">?</span>
<%
            }
%>
                </div>    
<%
            counter++;
        }
    } 
    else 
    { 
%>
                <div class="form-row">
                    <input type="hidden" name="TimePoints[0].Id" value="0" />
                    <input type="text" name="TimePoints[0].Name" />
                    <img src="/Images/minus.png" alt="Remove Time Point" title="Remove Time Point" class="link remove-time-point sixteenpx" style="display:none" />
                    <img src="/Images/plus.png" alt="Add Time Point" title="Add Time Point" class="link add-time-point sixteenpx" />
                    <span class="help-icon" title="The time points of a longitudinal study i.e. Baseline, 6 Month Follow-Up etc">?</span>
                </div>
<% 
    } 
%>
            </div>
        </div>
    </fieldset>