<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Extant.Web.Models.StudySamplesModel>" %>
<%@ Import Namespace="Extant.Data.Entities" %>
<%@ Import Namespace="Extant.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Study | <%: Model.StudyName %> | Samples
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2><%: Model.StudyName %> - Samples</h2>

<form action="/Study/Samples/<%:Model.Id %>" method="post" id="samples-form">
    <p>Please select the number of samples in this study's collection for each of the sample types below. <span class="help-icon" title="Please specify this in terms of the number of patients for which there are samples (rather than the total number of samples in the case of multiple samples of the same type being collected for each patient.">?</span></p>
    <fieldset class="narrow" id="samples-basic">
        <input type="hidden" name="isnew" value="<%:Model.IsNew %>" />
        <input type="hidden" name="action" value="" id="action" />
        <div class="form-row">
            <label for="NumberOfDnaSamples">DNA</label>
            <%: Html.DropDownListFor(m => m.NumberOfDnaSamples, NumberOfSamples.None.EnumSelectList()) %>
            <%: Html.TextBoxFor(m => m.NumberOfDnaSamplesExact, new { @style = "display: none", @class = "please-specify required-unaccessed integer" })%>
        </div>
        <div class="form-row">
            <label for="NumberOfSerumSamples">Serum</label>
            <%: Html.DropDownListFor(m => m.NumberOfSerumSamples, NumberOfSamples.None.EnumSelectList()) %>
            <%: Html.TextBoxFor(m => m.NumberOfSerumSamplesExact, new { @style = "display: none", @class = "please-specify required-unaccessed integer" })%>
        </div>
        <div class="form-row">
            <label for="NumberOfPlasmaSamples">Plasma</label>
            <%: Html.DropDownListFor(m => m.NumberOfPlasmaSamples, NumberOfSamples.None.EnumSelectList())%>
            <%: Html.TextBoxFor(m => m.NumberOfPlasmaSamplesExact, new { @style = "display: none", @class = "please-specify required-unaccessed integer" })%>
        </div>
        <div class="form-row">
            <label for="NumberOfWholeBloodSamples">Whole Blood</label>
            <%: Html.DropDownListFor(m => m.NumberOfWholeBloodSamples, NumberOfSamples.None.EnumSelectList()) %>
            <%: Html.TextBoxFor(m => m.NumberOfWholeBloodSamplesExact, new { @style = "display: none", @class = "please-specify required-unaccessed integer" })%>
        </div>
        <div class="form-row">
            <label for="NumberOfSalivaSamples">Saliva</label>
            <%: Html.DropDownListFor(m => m.NumberOfSalivaSamples, NumberOfSamples.None.EnumSelectList())%>
            <%: Html.TextBoxFor(m => m.NumberOfSalivaSamplesExact, new { @style = "display: none", @class = "please-specify required-unaccessed integer" })%>
        </div>
        <div class="form-row">
            <label for="NumberOfTissueSamples">Tissue</label>
            <%: Html.DropDownListFor(m => m.NumberOfTissueSamples, NumberOfSamples.None.EnumSelectList())%>
            <%: Html.TextBoxFor(m => m.NumberOfTissueSamplesExact, new { @style = "display: none", @class = "please-specify required-unaccessed integer" })%>
        </div>
        <div class="form-row">
            <label for="NumberOfCellSamples">Cell</label>
            <%: Html.DropDownListFor(m => m.NumberOfCellSamples, NumberOfSamples.None.EnumSelectList())%>
            <%: Html.TextBoxFor(m => m.NumberOfCellSamplesExact, new { @style = "display: none", @class = "please-specify required-unaccessed integer" })%>
        </div>
        <div class="form-row">
            <label for="NumberOfOtherSamples">Other</label>
            <%: Html.DropDownListFor(m => m.NumberOfOtherSamples, NumberOfSamples.None.EnumSelectList())%>
            <%: Html.TextBoxFor(m => m.NumberOfOtherSamplesExact, new { @style = "display: none", @class = "please-specify required-unaccessed integer" })%>
        </div>
        <div class="form-row">
            <%: Html.CheckBoxFor(m => m.DetailedSampleInfo, new{@class="check"}) %>
            <label for="DetailedSampleInfo" class="trailing">I would like to provide detailed information about the samples in this study's collection.</label>
            <span class="help-icon" title="Select this option if you would like to provide detailed information about the samples in your collection (e.g. source material, volume range, storage temperature etc.)">?</span>
        </div>
    </fieldset>

<%
    if ( Model.DetailedSampleInfo )
    {
        var sampleIndex = 0;
        var sampleTypes = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        var sampleTypeTitleNames = new[] { "DNA", "Serum", "Plasma", "Whole Blood", "Saliva", "Tissue", "Cell", "Other" };
        var sampleTypeNames = new[] { "DNA", "Serum", "Plasma", "Whole Blood", "Saliva", "Tissue", "Cell", "" };
        var sampleSelected = new[]
                                 {
                                    Model.NumberOfDnaSamples != (int)NumberOfSamples.None,
                                    Model.NumberOfSerumSamples != (int)NumberOfSamples.None,
                                    Model.NumberOfPlasmaSamples != (int)NumberOfSamples.None,
                                    Model.NumberOfWholeBloodSamples != (int)NumberOfSamples.None,
                                    Model.NumberOfSalivaSamples != (int)NumberOfSamples.None,
                                    Model.NumberOfTissueSamples != (int)NumberOfSamples.None,
                                    Model.NumberOfCellSamples != (int)NumberOfSamples.None,
                                    Model.NumberOfOtherSamples != (int)NumberOfSamples.None
                                 };
        for (int i = 0; i < 8; i++)
        {
            if (sampleSelected[i])
            {
                var sampleDetails = Model.Samples.SingleOrDefault(s => s.SampleType == sampleTypes[i]);
%>
    <div class="window ui-corner-all">
        <div class="window-title ui-widget-header ui-corner-all">
            <span class="sampleType"><%:sampleTypeTitleNames[i] %></span>&nbsp;Samples
<%
                if (null != sampleDetails)
                {
%>
            <img src="/Images/delete.png" alt="I don't want to give detailed information about this sample type" title="I don't want to give detailed information about this sample type" class="link remove-detailed" />
<%
                }
%>
        </div>
            
        <div class="window-body ui-dialog-content xxxwide">
<% 
                if (null == sampleDetails)
                {
%>
            <p><em>I do not want to provide detailed information on this sample type. <span class="link change-mind">Click here</span> to change your mind.</em></p>
<%                    
                }
                else
                {
%>
            <input type="hidden" name="Samples[<%:sampleIndex%>].SampleType" value="<%:sampleDetails.SampleType %>" />
            <input type="hidden" name="Samples[<%:sampleIndex%>].NumberOfSamples" value="<%:sampleDetails.NumberOfSamples %>" />
            <input type="hidden" name="Samples[<%:sampleIndex%>].NumberOfSamplesExact" value="<%:sampleDetails.NumberOfSamplesExact %>" />
            <fieldset class="other" style="display: none">
                <div class="form-row all">
                    <label>Please specify the type of the samples: <span class="required">*</span></label>
                    <input type="text" name="Samples[<%:sampleIndex%>].SampleTypeSpecify" class="required" value="<%:sampleDetails.SampleTypeSpecify %>" />
                </div>
            </fieldset>
            <fieldset class="dna plasma wholeblood saliva tissue cell other" style="display: none">
                <div class="form-row all" style="display: none">
                    <label class="fullwidth">Please specify the source biological material from which the <span class="sampleType"><%:sampleTypeNames[i]%></span>&nbsp;samples were isolated.</label>
                </div>
                <div class="all">
                    <div class="radio-row dna" style="display: none">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].BioMatWholeBlood" value="true" id="Samples_<%:sampleIndex%>_BioMatWholeBlood" <%=sampleDetails.BioMatWholeBlood ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_BioMatWholeBlood" class="trailing">Whole Blood</label>
                    </div>
                    <div class="radio-row dna" style="display: none">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].BioMatBuffyCoat" value="true" id="Samples_<%:sampleIndex%>_BioMatBuffyCoat" <%=sampleDetails.BioMatBuffyCoat ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_BioMatBuffyCoat" class="trailing">Buffy Coat</label>
                    </div>
                    <div class="radio-row dna" style="display: none">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].BioMatSaliva" value="true" id="Samples_<%:sampleIndex%>_BioMatSaliva" <%=sampleDetails.BioMatSaliva ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_BioMatSaliva" class="trailing">Saliva</label>
                    </div>
                    <div class="radio-row dna" style="display: none">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].BioMatBuccalSwabs" value="true" id="Samples_<%:sampleIndex%>_BioMatBuccalSwabs" <%=sampleDetails.BioMatBuccalSwabs ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_BioMatBuccalSwabs" class="trailing">Buccal Swabs</label>
                    </div>
                    <div class="radio-row dna wholeblood cell" style="display: none">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].BioMatAcidCitrateDextrose" value="true" id="Samples_<%:sampleIndex%>_BioMatAcidCitrateDextrose" <%=sampleDetails.BioMatAcidCitrateDextrose ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_BioMatAcidCitrateDextrose" class="trailing">Acid citrate dextrose (ACD)</label>
                    </div>
                    <div class="radio-row dna" style="display: none">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].BioMatSynovialFluid" value="true" id="Samples_<%:sampleIndex%>_BioMatSynovialFluid" <%=sampleDetails.BioMatSynovialFluid ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_BioMatSynovialFluid" class="trailing">Synovial Fluid</label>
                    </div>
                    <div class="radio-row dna tissue" style="display: none">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].BioMatSynovialTissue" value="true" id="Samples_<%:sampleIndex%>_BioMatSynovialTissue" <%=sampleDetails.BioMatSynovialTissue ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_BioMatSynovialTissue" class="trailing">Synovial Tissue</label>
                    </div>
                    <div class="radio-row dna" style="display: none">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].BioMatSerumSeparatorTube" value="true" id="Samples_<%:sampleIndex%>_BioMatSerumSeparatorTube" <%=sampleDetails.BioMatSerumSeparatorTube ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_BioMatSerumSeparatorTube" class="trailing">Serum Separator Tube</label>
                    </div>
                    <div class="radio-row dna plasma" style="display: none">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].BioMatPlasmaSeparatorTube" value="true" id="Samples_<%:sampleIndex%>_BioMatPlasmaSeparatorTube" <%=sampleDetails.BioMatPlasmaSeparatorTube ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_BioMatPlasmaSeparatorTube" class="trailing">Plasma Separator Tube</label>
                    </div>
                    <div class="radio-row dna" style="display: none">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].BioMatUrine" value="true" id="Samples_<%:sampleIndex%>_BioMatUrine" <%=sampleDetails.BioMatUrine ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_BioMatUrine" class="trailing">Urine</label>
                    </div>
                    <div class="radio-row dna" style="display: none">
                        <input type="checkbox" class="check specify" name="Samples[<%:sampleIndex%>].BioMatOtherTubes" value="true" id="Samples_<%:sampleIndex%>_BioMatOtherTubes" <%=sampleDetails.BioMatOtherTubes ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_BioMatOtherTubes" class="trailing">Other blood collection tubes (please specify if type known)</label>
                        <input type="text" name="Samples[<%:sampleIndex%>].BioMatOtherTubesSpecify" disabled="disabled" class="required" value="<%=sampleDetails.BioMatOtherTubesSpecify %>" />
                    </div>
                    <div class="radio-row plasma wholeblood" style="display: none">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].BioMatEdtaBlood" value="true" id="Samples_<%:sampleIndex%>_BioMatEdtaBlood" <%=sampleDetails.BioMatEdtaBlood ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_BioMatEdtaBlood" class="trailing">EDTA Blood</label>
                    </div>
                    <div class="radio-row saliva" style="display: none">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].BioMatSalivaNoAdditive" value="true" id="Samples_<%:sampleIndex%>_BioMatSalivaNoAdditive" <%=sampleDetails.BioMatSalivaNoAdditive ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_BioMatSalivaNoAdditive" class="trailing">Saliva (No additive)</label>
                    </div>
                    <div class="radio-row saliva" style="display: none">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].BioMatSalivaOragene" value="true" id="Samples_<%:sampleIndex%>_BioMatSalivaOragene" <%=sampleDetails.BioMatSalivaOragene ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_BioMatSalivaOragene" class="trailing">Saliva (Oragene)</label>
                    </div>
                    <div class="radio-row cell" style="display: none">
                        <input type="checkbox" class="check specify" name="Samples[<%:sampleIndex%>].BioMatCulture" value="true" id="Samples_<%:sampleIndex%>_BioMatCulture" <%=sampleDetails.BioMatCulture ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_BioMatCulture" class="trailing">Culture (please specify the cell line)</label>
                        <input type="text" name="Samples[<%:sampleIndex%>].BioMatCultureSpecify" disabled="disabled" class="required" value="<%=sampleDetails.BioMatCultureSpecify %>" />
                    </div>
                    <div class="radio-row all" style="display: none">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].BioMatUnknown" value="true" id="Samples_<%:sampleIndex%>_BioMatUnknown" <%=sampleDetails.BioMatUnknown ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_BioMatUnknown">Unknown</label>
                    </div>
                    <div class="radio-row all" style="display: none">
                        <input type="checkbox" class="check specify" name="Samples[<%:sampleIndex%>].BioMatOther" value="true" id="Samples_<%:sampleIndex%>_BioMatOther" <%=sampleDetails.BioMatOther ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_BioMatOther" class="trailing">Other (please specify)</label>
                        <input type="text" name="Samples[<%:sampleIndex%>].BioMatOtherSpecify" disabled="disabled" class="required" value="<%=sampleDetails.BioMatOtherSpecify %>" />
                    </div>
                </div>  
            </fieldset>

            <fieldset class="tissue" style="display: none">
                <div class="form-row tissue" style="display: none">
                    <label>How were the tissue samples preserved? <span class="required">*</span></label>
                    <select name="Samples[<%:sampleIndex%>].TissueSamplesPreserved" class="required">
                        <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                    <%
                    foreach (var item in TissueSamplesPreserved.Unknown.EnumSelectList())
                    {
                        var specify = item.Text.ToLower().Contains("please specify");
                        var isSelected = item.Value == sampleDetails.TissueSamplesPreserved.ToString();
%>
                        <option value="<%:item.Value%>" <%=isSelected ? "Selected=\"selected\"" : "" %> <%=specify ? "class=\"specify\"" : ""%>><%=item.Text%></option>
                    <%
                    }
%>
                    </select>            
                </div>
                <div class="form-row tissue" style="display: none">
                    <label class="disabled">Please specify: <span class="required">*</span></label>
                    <input type="text" name="Samples[<%:sampleIndex%>].TissueSamplesPreservedSpecify" disabled="disabled" class="required" value="<%=sampleDetails.TissueSamplesPreservedSpecify %>" />
                </div>    
            </fieldset>

            <fieldset class="dna serum plasma wholeblood saliva cell other" style="display: none">
                <div class="form-row all" style="display: none">
                    <label>What is the volume range of the <span class="sampleType"><%:sampleTypeNames[i]%></span>&nbsp;samples within your collection? <span class="required">*</span></label>
                    <select name="Samples[<%:sampleIndex%>].SampleVolume" class="required">
                        <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                    <%
                    foreach (var item in SampleVolume.Unknown.EnumSelectList())
                    {
                        var specify = item.Text.ToLower().Contains("please specify");
                        var isSelected = item.Value == sampleDetails.SampleVolume.ToString();
%>
                        <option value="<%:item.Value%>" <%=isSelected ? "Selected=\"selected\"" : "" %> <%=specify ? "class=\"specify\"" : ""%>><%=item.Text%></option>
                    <%
                    }
%>
                    </select>
                </div>
                <div class="form-row all" style="display: none">
                    <label class="disabled">Please specify: <span class="required">*</span></label>
                    <input type="text" name="Samples[<%:sampleIndex%>].SampleVolumeSpecify" disabled="disabled" class="required" />
                </div>  
            </fieldset>

            <fieldset class="cell" style="display: none">
                <div class="form-row cell" style="display: none">
                    <label>Please give an estimation of the cell count (cells/ml). <span class="required">*</span></label>
                    <input type="text" name="Samples[<%:sampleIndex%>].CellCount" class="required number" />
                </div>
            </fieldset>

            <fieldset class="dna" style="display: none">
                <div class="form-row dna" style="display: none">
                    <label>What is the concentration range of the <span class="sampleType"><%:sampleTypeNames[i]%></span>&nbsp;samples? <span class="required">*</span></label>
                    <select name="Samples[<%:sampleIndex%>].Concentration" class="required">
                        <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                    <%
                    foreach (var item in Concentration.Unknown.EnumSelectList())
                    {
                        var specify = item.Text.ToLower().Contains("please specify");
                        var isSelected = item.Value == sampleDetails.Concentration.ToString();
%>
                        <option value="<%:item.Value%>" <%=isSelected ? "Selected=\"selected\"" : "" %> <%=specify ? "class=\"specify\"" : ""%>><%=item.Text%></option>
                    <%
                    }
%>
                    </select>
                </div>
                <div class="form-row dna" style="display: none">
                    <label class="disabled">Please specify: <span class="required">*</span></label>
                    <input type="text" name="Samples[<%:sampleIndex%>].ConcentrationSpecify" disabled="disabled" class="required" />
                </div>
            </fieldset>

            <fieldset class="serum plasma" style="display: none">
                <div class="form-row serum plasma" style="display: none">
                    <label>Please estimate the number of aliquots per individual in your collection. <span class="required">*</span></label>
                    <select name="Samples[<%:sampleIndex%>].NumberOfAliquots" class="required">
                        <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                    <%
                    foreach (var item in NumberOfAliquots.Unknown.EnumSelectList())
                    {
                        var specify = item.Text.ToLower().Contains("please specify");
                        var isSelected = item.Value == sampleDetails.NumberOfAliquots.ToString();
%>
                        <option value="<%:item.Value%>" <%=isSelected ? "Selected=\"selected\"" : "" %> <%=specify ? "class=\"specify\"" : ""%>><%=item.Text%></option>
                    <%
                    }
%>
                    </select>
                </div>
            </fieldset>

            <fieldset class="all" style="display: none">
                <div class="form-row all" style="display: none">
                    <label>Please estimate how long ago the <span class="sampleType"><%:sampleTypeNames[i]%></span>&nbsp;samples were collected. <span class="required">*</span></label>
                    <select name="Samples[<%:sampleIndex%>].WhenCollected" class="required">
                        <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                    <%
                    foreach (var item in WhenCollected.Unknown.EnumSelectList())
                    {
                        var specify = item.Text.ToLower().Contains("please specify");
                        var isSelected = item.Value == sampleDetails.WhenCollected.ToString();
%>
                        <option value="<%:item.Value%>" <%=isSelected ? "Selected=\"selected\"" : "" %> <%=specify ? "class=\"specify\"" : ""%>><%=item.Text%></option>
                    <%
                    }
%>
                    </select>
                </div>
            </fieldset>

            <fieldset class="tissue" style="display: none">
                <div class="form-row tissue" style="display: none">
                    <label>Was the tissue snap frozen? <span class="required">*</span></label>
                    <select name="Samples[<%:sampleIndex%>].SnapFrozen" class="required">
                        <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                    <%
                    foreach (var item in YesNoUnknown.Unknown.EnumSelectList())
                    {
                        var specify = item.Text.ToLower().Contains("please specify");
                        var isSelected = item.Value == sampleDetails.SnapFrozen.ToString();
%>
                        <option value="<%:item.Value%>" <%=isSelected ? "Selected=\"selected\"" : "" %> <%=specify ? "class=\"specify\"" : ""%>><%=item.Text%></option>
                    <%
                    }
%>
                    </select>
                </div>
            </fieldset>

            <fieldset class="dna" style="display: none">
                <div class="form-row dna" style="display: none">
                    <label>How was the DNA extracted? <span class="required">*</span></label>
                    <select name="Samples[<%:sampleIndex%>].HowDnaExtracted" class="required">
                        <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                    <%
                    foreach (var item in HowDnaExtracted.Unknown.EnumSelectList())
                    {
                        var specify = item.Text.ToLower().Contains("please specify");
                        var isSelected = item.Value == sampleDetails.HowDnaExtracted.ToString();
%>
                        <option value="<%:item.Value%>" <%=isSelected ? "Selected=\"selected\"" : "" %> <%=specify ? "class=\"specify\"" : ""%>><%=item.Text%></option>
                    <%
                    }
%>
                    </select>
                </div>
                <div class="form-row dna" style="display: none">
                    <label class="disabled">Please specify: <span class="required">*</span></label>
                    <input type="text" name="Samples[<%:sampleIndex%>].HowDnaExtractedSpecify" disabled="disabled" class="required" value="<%=sampleDetails.HowDnaExtractedSpecify %>" />
                </div>
            </fieldset>

            <fieldset class="serum plasma wholeblood saliva tissue cell other" style="display: none">
            <div class="form-row serum plasma wholeblood saliva tissue cell other" style="display: none">
                <label>What was the elapsed time between sample collection and final storage of the sample? <span class="required">*</span></label>
                <select name="Samples[<%:sampleIndex%>].TimeBetweenCollectionAndStorage" class="required">
                    <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                <%
                    foreach (var item in TimeBetweenCollectionAndStorage.Unknown.EnumSelectList())
                    {
                        var specify = item.Text.ToLower().Contains("please specify");
                        var isSelected = item.Value == sampleDetails.TimeBetweenCollectionAndStorage.ToString();
%>
                    <option value="<%:item.Value%>" <%=isSelected ? "Selected=\"selected\"" : "" %> <%=specify ? "class=\"specify\"" : ""%>><%=item.Text%></option>
                <%
                    }
%>
                </select>
            </div>
            </fieldset>

            <fieldset class="all" style="display: none">
                <div class="form-row all" style="display: none">
                    <label>What temperature were the <span class="sampleType"><%:sampleTypeNames[i]%></span>&nbsp;samples kept at between sample collection and storage? <span class="required">*</span></label>
                    <select name="Samples[<%:sampleIndex%>].CollectionToStorageTemp" class="required">
                        <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                    <%
                    foreach (var item in CollectionToStorageTemp.Unknown.EnumSelectList())
                    {
                        var specify = item.Text.ToLower().Contains("please specify");
                        var isSelected = item.Value == sampleDetails.CollectionToStorageTemp.ToString();
%>
                        <option value="<%:item.Value%>" <%=isSelected ? "Selected=\"selected\"" : "" %> <%=specify ? "class=\"specify\"" : ""%>><%=item.Text%></option>
                    <%
                    }
%>
                    </select>
                </div>
                <div class="form-row all" style="display: none">
                    <label class="disabled">Please specify: <span class="required">*</span></label>
                    <input type="text" name="Samples[<%:sampleIndex%>].CollectionToStorageTempSpecify" disabled="disabled" class="required" value="<%=sampleDetails.CollectionToStorageTempSpecify %>" />
                </div>
            </fieldset>

            <fieldset class="all" style="display: none">
                <div class="form-row all" style="display: none">
                    <label>Under what temperature conditions are the <span class="sampleType"><%:sampleTypeNames[i]%></span>&nbsp;samples currently stored? <span class="required">*</span></label>
                    <select name="Samples[<%:sampleIndex%>].StorageTemp" class="required">
                        <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                    <%
                    foreach (var item in StorageTemp.Unknown.EnumSelectList())
                    {
                        var specify = item.Text.ToLower().Contains("please specify");
                        var isSelected = item.Value == sampleDetails.StorageTemp.ToString();
%>
                        <option value="<%:item.Value%>" <%=isSelected ? "Selected=\"selected\"" : "" %> <%=specify ? "class=\"specify\"" : ""%>><%=item.Text%></option>
                    <%
                    }
%>
                    </select>
                </div>
                <div class="form-row all" style="display: none">
                    <label class="disabled">Please specify: <span class="required">*</span></label>
                    <input type="text" name="Samples[<%:sampleIndex%>].StorageTempSpecify" disabled="disabled" class="required" value="<%=sampleDetails.StorageTempSpecify %>" />
                </div>
            </fieldset>

            <fieldset class="all" style="display: none">
                <div class="form-row all" style="display: none">
                    <label>Have the <span class="sampleType"><%:sampleTypeNames[i]%></span>&nbsp;samples always been stored at this temperature? <span class="required">*</span></label>
                    <select name="Samples[<%:sampleIndex%>].AlwayStoredAtThisTemp" class="required">
                        <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                    <%
                    foreach (var item in YesNoUnknown.Unknown.EnumSelectList())
                    {
                        var specify = item.Text.ToLower().Contains("please specify");
                        var isSelected = item.Value == sampleDetails.AlwayStoredAtThisTemp.ToString();
%>
                        <option value="<%:item.Value%>" <%=isSelected ? "Selected=\"selected\"" : "" %> <%=specify ? "class=\"specify\"" : ""%>><%=item.Text%></option>
                    <%
                    }
%>
                    </select>
                </div>
            </fieldset>

            <fieldset class="dna" style="display: none">
                <div class="form-row dna">
                    <label class="fullwidth">Has the DNA quality been assessed by any of the following?</label>
                </div>
                <div class="dna">
                    <div class="radio-row dna">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].DnaQualityAbsorbance" value="true" id="Samples_<%:sampleIndex%>_DnaQualityAbsorbance" <%=sampleDetails.DnaQualityAbsorbance ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_DnaQualityAbsorbance" class="trailing">Absorbance at 260/280 OD readings</label>
                    </div>
                    <div class="radio-row dna">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].DnaQualityGel" value="true" id="Samples_<%:sampleIndex%>_DnaQualityGel" <%=sampleDetails.DnaQualityGel ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_DnaQualityGel" class="trailing">Samples have been run on a gel</label>
                    </div>
                    <div class="radio-row dna">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].DnaQualityCommercialKit" value="true" id="Samples_<%:sampleIndex%>_DnaQualityCommercialKit" <%=sampleDetails.DnaQualityCommercialKit ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_DnaQualityCommercialKit" class="trailing">Samples have been analysed using a commercial testing kit e.g. DNA OK</label>
                    </div>
                    <div class="radio-row dna">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].DnaQualityPcr" value="true" id="Samples_<%:sampleIndex%>_DnaQualityPcr" <%=sampleDetails.DnaQualityPcr ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_DnaQualityPcr" class="trailing">Real time PCR analysis</label>
                    </div>
                    <div class="radio-row dna">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].DnaQualityPicoGreen" value="true" id="Samples_<%:sampleIndex%>_DnaQualityPicoGreen" <%=sampleDetails.DnaQualityPicoGreen ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_DnaQualityPicoGreen" class="trailing">PicoGreen</label>
                    </div>
                    <div class="radio-row dna">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].DnaQualityUnknown" value="true" id="Samples_<%:sampleIndex%>_DnaQualityUnknown" <%=sampleDetails.DnaQualityUnknown ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_DnaQualityUnknown" class="trailing">Unknown</label>
                    </div>
                    <div class="radio-row dna">
                        <input type="checkbox" class="check specify" name="Samples[<%:sampleIndex%>].DnaQualityOther" value="true" id="Samples_<%:sampleIndex%>_DnaQualityOther" <%=sampleDetails.DnaQualityOther ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_DnaQualityOther" class="trailing">Other (please specify)</label>
                        <input type="text" name="Samples[<%:sampleIndex%>].DnaQualityOtherSpecify" disabled="disabled" class="required" value="<%=sampleDetails.DnaQualityOtherSpecify %>" />
                    </div>
                </div>
            </fieldset>

            <fieldset class="all" style="display: none">
                <div class="form-row all" style="display: none">
                    <label>How many freeze/thaw cycles have the <span class="sampleType"><%:sampleTypeNames[i]%></span>&nbsp;samples been exposed to? (Please estimate if required). <span class="required">*</span></label>
                    <select name="Samples[<%:sampleIndex%>].FreezeThawCycles" class="required">
                        <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                    <%
                    foreach (var item in FreezeThawCycles.Unknown.EnumSelectList())
                    {
                        var specify = item.Text.ToLower().Contains("please specify");
                        var isSelected = item.Value == sampleDetails.FreezeThawCycles.ToString();
%>
                        <option value="<%:item.Value%>" <%=isSelected ? "Selected=\"selected\"" : "" %> <%=specify ? "class=\"specify\"" : ""%>><%=item.Text%></option>
                    <%
                    }
%>
                    </select>
                </div>
            </fieldset>

            <fieldset class="dna serum plasma saliva tissue other" style="display: none">
                <div class="form-row all">
                    <label class="fullwidth">Has successful analysis been performed on the <span class="sampleType"><%:sampleTypeNames[i]%></span>&nbsp;samples since collection?</label>
                </div>
                <div class="all">
                    <div class="radio-row all" style="display:none">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].AnalysisNo" value="true" id="Samples_<%:sampleIndex%>_AnalysisNo" <%=sampleDetails.AnalysisNo ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_AnalysisNo" class="trailing">No successful analysis performed</label>
                    </div>
                    <div class="radio-row dna" style="display:none">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].AnalysisSequencing" value="true" id="Samples_<%:sampleIndex%>_AnalysisSequencing" <%=sampleDetails.AnalysisSequencing ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_AnalysisSequencing" class="trailing">Sequencing</label>
                    </div>
                    <div class="radio-row dna" style="display:none">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].AnalysisRealTimePcr" value="true" id="Samples_<%:sampleIndex%>_AnalysisRealTimePcr" <%=sampleDetails.AnalysisRealTimePcr ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_AnalysisRealTimePcr" class="trailing">Real Time PCR</label>
                    </div>
                    <div class="radio-row dna" style="display:none">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].AnalysisPcr" value="true" id="Samples_<%:sampleIndex%>_AnalysisPcr" <%=sampleDetails.AnalysisPcr ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_AnalysisPcr" class="trailing">PCR</label>
                    </div>
                    <div class="radio-row dna" style="display:none">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].AnalysisGenotyping" value="true" id="Samples_<%:sampleIndex%>_AnalysisGenotyping" <%=sampleDetails.AnalysisGenotyping ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_AnalysisGenotyping" class="trailing">Genotyping</label>
                    </div>
                    <div class="radio-row serum plasma" style="display:none">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].AnalysisBiochemistry" value="true" id="Samples_<%:sampleIndex%>_AnalysisBiochemistry" <%=sampleDetails.AnalysisBiochemistry ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_AnalysisBiochemistry" class="trailing">Biochemistry</label>
                    </div>
                    <div class="radio-row serum plasma" style="display:none">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].AnalysisImmunochemistry" value="true" id="Samples_<%:sampleIndex%>_AnalysisImmunochemistry" <%=sampleDetails.AnalysisImmunochemistry ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_AnalysisImmunochemistry" class="trailing">Immunochemistry</label>
                    </div>
                    <div class="radio-row saliva tissue" style="display:none">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].AnalysisDnaExtraction" value="true" id="Samples_<%:sampleIndex%>_AnalysisDnaExtraction" <%=sampleDetails.AnalysisDnaExtraction ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_AnalysisDnaExtraction" class="trailing">DNA extraction</label>
                    </div>
                    <div class="radio-row tissue" style="display:none">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].AnalysisImmunohistochemistry" value="true" id="Samples_<%:sampleIndex%>_AnalysisImmunohistochemistry" <%=sampleDetails.AnalysisImmunohistochemistry ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_AnalysisImmunohistochemistry" class="trailing">Immunohistochemisty</label>
                    </div>
                    <div class="radio-row tissue" style="display:none">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].AnalysisCellLinesDerived" value="true" id="Samples_<%:sampleIndex%>_AnalysisCellLinesDerived" <%=sampleDetails.AnalysisCellLinesDerived ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_AnalysisCellLinesDerived" class="trailing">Cell lines derived</label>
                    </div>
                    <div class="radio-row tissue" style="display:none">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].AnalysisRnaExtraction" value="true" id="Samples_<%:sampleIndex%>_AnalysisRnaExtraction" <%=sampleDetails.AnalysisRnaExtraction ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_AnalysisRnaExtraction" class="trailing">RNA extraction</label>
                    </div>
                    <div class="radio-row all" style="display:none">
                        <input type="checkbox" class="check" name="Samples[<%:sampleIndex%>].AnalysisUnknown" value="true" id="Samples_<%:sampleIndex%>_AnalysisUnknown" <%=sampleDetails.AnalysisUnknown ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_AnalysisUnknown" class="trailing">Unknown</label>
                    </div>
                    <div class="radio-row all" style="display:none">
                        <input type="checkbox" class="check specify" name="Samples[<%:sampleIndex%>].AnalysisOther" value="true" id="Samples_<%:sampleIndex%>_AnalysisOther" <%=sampleDetails.AnalysisOther ? "checked=\"checked\"" : ""%> />
                        <label for="Samples_<%:sampleIndex%>_AnalysisOther" class="trailing">Any other analysis (please specify)</label>
                        <input type="text" name="Samples[<%:sampleIndex%>].AnalysisOtherSpecify" disabled="disabled" class="required" value="<%=sampleDetails.AnalysisOtherSpecify %>" />
                    </div>
                </div>

            </fieldset>

            <fieldset class="wholeblood" style="display: none">
                <div class="form-row wholeblood" style="display: none">
                    <label>Has DNA been successfully extracted from the whole blood samples? <span class="required">*</span></label>
                    <select name="Samples[<%:sampleIndex%>].DnaExtracted" class="required">
                        <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                    <%
                    foreach (var item in YesNoUnknown.Unknown.EnumSelectList())
                    {
                        var specify = item.Text.ToLower().Contains("please specify");
                        var isSelected = item.Value == sampleDetails.DnaExtracted.ToString();
%>
                        <option value="<%:item.Value%>" <%=isSelected ? "Selected=\"selected\"" : "" %> <%=specify ? "class=\"specify\"" : ""%>><%=item.Text%></option>
                    <%
                    }
%>
                    </select>
                </div>
            </fieldset>

            <fieldset class="cell" style="display: none">
                <div class="form-row cell" style="display: none">
                    <label>Have the cells been successfully grown up since collection and storage? <span class="required">*</span></label>
                    <select name="Samples[<%:sampleIndex%>].CellsGrown" class="required">
                        <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                    <%
                    foreach (var item in YesNoUnknown.Unknown.EnumSelectList())
                    {
                        var specify = item.Text.ToLower().Contains("please specify");
                        var isSelected = item.Value == sampleDetails.CellsGrown.ToString();
%>
                        <option value="<%:item.Value%>" <%=isSelected ? "Selected=\"selected\"" : "" %> <%=specify ? "class=\"specify\"" : ""%>><%=item.Text%></option>
                    <%
                    }
%>
                    </select>
                </div>
            </fieldset>
<%
                    sampleIndex++;
                }
%>
        </div>
    </div>
<%
            }
%>

<%
        }
    }
%>

    <div class="form-row" id="buttons">
<% if (Model.IsNew) { %>
        <button type="submit" id="prev-btn">&lt;&nbsp;Previous</button>
        <button type="submit" id="finish-btn">Finish</button>
        <button type="submit" id="publish-btn">Finish &amp; Publish</button>
<% } else { %>
        <button type="submit">Save</button>
        <button id="cancel-btn">Cancel</button>
<% } %>
    </div>
</form>

<div id="sample-form-template" style="display: none">
    <div class="window ui-corner-all">
        <div class="window-title ui-widget-header ui-corner-all">
            <span></span>&nbsp;Samples
            <img src="/Images/delete.png" alt="I don't want to give detailed information about this sample type" title="I don't want to give detailed information about this sample type" class="link remove-detailed" />
        </div>
        <div class="window-body ui-dialog-content xxwide">
            <input type="hidden" name="Samples[x].SampleType" value="" />
            <input type="hidden" name="Samples[x].NumberOfSamples" value="" />
            <input type="hidden" name="Samples[x].NumberOfSamplesExact" value="" />
            
            <fieldset class="other" style="display: none">
                <div class="form-row all">
                    <label>Please specify the type of the samples: <span class="required">*</span></label>
                    <input type="text" name="Samples[x].SampleTypeSpecify" class="required" />
                </div>
            </fieldset>
            <fieldset class="dna plasma wholeblood saliva tissue cell other" style="display: none">
                <div class="form-row all" style="display: none">
                    <label class="fullwidth">Please specify the source biological material from which the <span class="sampleType"></span>&nbsp;samples were isolated.</label>
                </div>
                <div class="all">
                    <div class="radio-row dna" style="display: none">
                        <input type="checkbox" class="check" name="Samples[x].BioMatWholeBlood" value="true" id="Samples_x_BioMatWholeBlood" />
                        <label for="Samples_x_BioMatWholeBlood" class="trailing">Whole Blood</label>
                    </div>
                    <div class="radio-row dna" style="display: none">
                        <input type="checkbox" class="check" name="Samples[x].BioMatBuffyCoat" value="true" id="Samples_x_BioMatBuffyCoat" />
                        <label for="Samples_x_BioMatBuffyCoat" class="trailing">Buffy Coat</label>
                    </div>
                    <div class="radio-row dna" style="display: none">
                        <input type="checkbox" class="check" name="Samples[x].BioMatSaliva" value="true" id="Samples_x_BioMatSaliva" />
                        <label for="Samples_x_BioMatSaliva" class="trailing">Saliva</label>
                    </div>
                    <div class="radio-row dna" style="display: none">
                        <input type="checkbox" class="check" name="Samples[x].BioMatBuccalSwabs" value="true" id="Samples_x_BioMatBuccalSwabs" />
                        <label for="Samples_x_BioMatBuccalSwabs" class="trailing">Buccal Swabs</label>
                    </div>
                    <div class="radio-row dna wholeblood cell" style="display: none">
                        <input type="checkbox" class="check" name="Samples[x].BioMatAcidCitrateDextrose" value="true" id="Samples_x_BioMatAcidCitrateDextrose" />
                        <label for="Samples_x_BioMatAcidCitrateDextrose" class="trailing">Acid citrate dextrose (ACD)</label>
                    </div>
                    <div class="radio-row dna" style="display: none">
                        <input type="checkbox" class="check" name="Samples[x].BioMatSynovialFluid" value="true" id="Samples_x_BioMatSynovialFluid" />
                        <label for="Samples_x_BioMatSynovialFluid" class="trailing">Synovial Fluid</label>
                    </div>
                    <div class="radio-row dna tissue" style="display: none">
                        <input type="checkbox" class="check" name="Samples[x].BioMatSynovialTissue" value="true" id="Samples_x_BioMatSynovialTissue" />
                        <label for="Samples_x_BioMatSynovialTissue" class="trailing">Synovial Tissue</label>
                    </div>
                    <div class="radio-row dna" style="display: none">
                        <input type="checkbox" class="check" name="Samples[x].BioMatSerumSeparatorTube" value="true" id="Samples_x_BioMatSerumSeparatorTube" />
                        <label for="Samples_x_BioMatSerumSeparatorTube" class="trailing">Serum Separator Tube</label>
                    </div>
                    <div class="radio-row dna plasma" style="display: none">
                        <input type="checkbox" class="check" name="Samples[x].BioMatPlasmaSeparatorTube" value="true" id="Samples_x_BioMatPlasmaSeparatorTube" />
                        <label for="Samples_x_BioMatPlasmaSeparatorTube" class="trailing">Plasma Separator Tube</label>
                    </div>
                    <div class="radio-row dna" style="display: none">
                        <input type="checkbox" class="check" name="Samples[x].BioMatUrine" value="true" id="Samples_x_BioMatUrine" />
                        <label for="Samples_x_BioMatUrine" class="trailing">Urine</label>
                    </div>
                    <div class="radio-row dna" style="display: none">
                        <input type="checkbox" class="check specify" name="Samples[x].BioMatOtherTubes" value="true" id="Samples_x_BioMatOtherTubes" />
                        <label for="Samples_x_BioMatOtherTubes" class="trailing">Other blood collection tubes (please specify if type known)</label>
                        <input type="text" name="Samples[x].BioMatOtherTubesSpecify" disabled="disabled" class="required" />
                    </div>
                    <div class="radio-row plasma wholeblood" style="display: none">
                        <input type="checkbox" class="check" name="Samples[x].BioMatEdtaBlood" value="true" id="Samples_x_BioMatEdtaBlood" />
                        <label for="Samples_x_BioMatEdtaBlood" class="trailing">EDTA Blood</label>
                    </div>
                    <div class="radio-row saliva" style="display: none">
                        <input type="checkbox" class="check" name="Samples[x].BioMatSalivaNoAdditive" value="true" id="Samples_x_BioMatSalivaNoAdditive" />
                        <label for="Samples_x_BioMatSalivaNoAdditive" class="trailing">Saliva (No additive)</label>
                    </div>
                    <div class="radio-row saliva" style="display: none">
                        <input type="checkbox" class="check" name="Samples[x].BioMatSalivaOragene" value="true" id="Samples_x_BioMatSalivaOragene" />
                        <label for="Samples_x_BioMatSalivaOragene" class="trailing">Saliva (Oragene)</label>
                    </div>
                    <div class="radio-row cell" style="display: none">
                        <input type="checkbox" class="check specify" name="Samples[x].BioMatCulture" value="true" id="Samples_x_BioMatCulture" />
                        <label for="Samples_x_BioMatCulture" class="trailing">Culture (please specify the cell line)</label>
                        <input type="text" name="Samples[x].BioMatCultureSpecify" disabled="disabled" class="required" />
                    </div>
                    <div class="radio-row all" style="display: none">
                        <input type="checkbox" class="check" name="Samples[x].BioMatUnknown" value="true" id="Samples_x_BioMatUnknown" />
                        <label for="Samples_x_BioMatUnknown">Unknown</label>
                    </div>
                    <div class="radio-row all" style="display: none">
                        <input type="checkbox" class="check specify" name="Samples[x].BioMatOther" value="true" id="Samples_x_BioMatOther" />
                        <label for="Samples_x_BioMatOther" class="trailing">Other (please specify)</label>
                        <input type="text" name="Samples[x].BioMatOtherSpecify" disabled="disabled" class="required" />
                    </div>
                </div>  
            </fieldset>

            <fieldset class="tissue" style="display: none">
                <div class="form-row tissue" style="display: none">
                    <label>How were the tissue samples preserved? <span class="required">*</span></label>
                    <select name="Samples[x].TissueSamplesPreserved" class="required">
                        <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                    <%
                        foreach (var item in TissueSamplesPreserved.Unknown.EnumSelectList())
                        {
                            var specify = item.Text.ToLower().Contains("please specify");
                    %>
                        <option value="<%:item.Value %>" <%= specify ? "class=\"specify\"" : "" %>><%=item.Text %></option>
                    <%                
                        }
                    %>
                    </select>            
                </div>
                <div class="form-row tissue" style="display: none">
                    <label class="disabled">Please specify: <span class="required">*</span></label>
                    <input type="text" name="Samples[x].TissueSamplesPreservedSpecify" disabled="disabled" class="required" />
                </div>    
            </fieldset>

            <fieldset class="dna serum plasma wholeblood saliva cell other" style="display: none">
                <div class="form-row all" style="display: none">
                    <label>What is the volume range of the <span class="sampleType"></span>&nbsp;samples within your collection? <span class="required">*</span></label>
                    <select name="Samples[x].SampleVolume" class="required">
                        <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                    <%
                        foreach (var item in SampleVolume.Unknown.EnumSelectList())
                        {
                            var specify = item.Text.ToLower().Contains("please specify");
                    %>
                        <option value="<%:item.Value %>" <%= specify ? "class=\"specify\"" : "" %>><%=item.Text %></option>
                    <%                
                        }
                    %>
                    </select>
                </div>
                <div class="form-row all" style="display: none">
                    <label class="disabled">Please specify: <span class="required">*</span></label>
                    <input type="text" name="Samples[x].SampleVolumeSpecify" disabled="disabled" class="required" />
                </div>  
            </fieldset>

            <fieldset class="cell" style="display: none">
                <div class="form-row cell" style="display: none">
                    <label>Please give an estimation of the cell count (cells/ml). <span class="required">*</span></label>
                    <input type="text" name="Samples[x].CellCount" class="required number" />
                </div>
            </fieldset>

            <fieldset class="dna" style="display: none">
                <div class="form-row dna" style="display: none">
                    <label>What is the concentration range of the <span class="sampleType"></span>&nbsp;samples? <span class="required">*</span></label>
                    <select name="Samples[x].Concentration" class="required">
                        <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                    <%
                        foreach (var item in Concentration.Unknown.EnumSelectList())
                        {
                            var specify = item.Text.ToLower().Contains("please specify");
                    %>
                        <option value="<%:item.Value %>" <%= specify ? "class=\"specify\"" : "" %>><%=item.Text %></option>
                    <%                
                        }
                    %>
                    </select>
                </div>
                <div class="form-row dna" style="display: none">
                    <label class="disabled">Please specify: <span class="required">*</span></label>
                    <input type="text" name="Samples[x].ConcentrationSpecify" disabled="disabled" class="required" />
                </div>
            </fieldset>

            <fieldset class="serum plasma" style="display: none">
                <div class="form-row serum plasma" style="display: none">
                    <label>Please estimate the number of aliquots per individual in your collection. <span class="required">*</span></label>
                    <select name="Samples[x].NumberOfAliquots" class="required">
                        <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                    <%
                        foreach (var item in NumberOfAliquots.Unknown.EnumSelectList())
                        {
                            var specify = item.Text.ToLower().Contains("please specify");
                    %>
                        <option value="<%:item.Value %>" <%= specify ? "class=\"specify\"" : "" %>><%=item.Text %></option>
                    <%                
                        }
                    %>
                    </select>
                </div>
            </fieldset>

            <fieldset class="all" style="display: none">
                <div class="form-row all" style="display: none">
                    <label>Please estimate how long ago the <span class="sampleType"></span>&nbsp;samples were collected. <span class="required">*</span></label>
                    <select name="Samples[x].WhenCollected" class="required">
                        <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                    <%
                        foreach (var item in WhenCollected.Unknown.EnumSelectList())
                        {
                            var specify = item.Text.ToLower().Contains("please specify");
                    %>
                        <option value="<%:item.Value %>" <%= specify ? "class=\"specify\"" : "" %>><%=item.Text %></option>
                    <%                
                        }
                    %>
                    </select>
                </div>
            </fieldset>

            <fieldset class="tissue" style="display: none">
                <div class="form-row tissue" style="display: none">
                    <label>Was the tissue snap frozen? <span class="required">*</span></label>
                    <select name="Samples[x].SnapFrozen" class="required">
                        <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                    <%
                        foreach (var item in YesNoUnknown.Unknown.EnumSelectList())
                        {
                            var specify = item.Text.ToLower().Contains("please specify");
                    %>
                        <option value="<%:item.Value %>" <%= specify ? "class=\"specify\"" : "" %>><%=item.Text %></option>
                    <%                
                        }
                    %>
                    </select>
                </div>
            </fieldset>

            <fieldset class="dna" style="display: none">
                <div class="form-row dna" style="display: none">
                    <label>How was the DNA extracted? <span class="required">*</span></label>
                    <select name="Samples[x].HowDnaExtracted" class="required">
                        <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                    <%
                        foreach (var item in HowDnaExtracted.Unknown.EnumSelectList())
                        {
                            var specify = item.Text.ToLower().Contains("please specify");
                    %>
                        <option value="<%:item.Value %>" <%= specify ? "class=\"specify\"" : "" %>><%=item.Text %></option>
                    <%                
                        }
                    %>
                    </select>
                </div>
                <div class="form-row dna" style="display: none">
                    <label class="disabled">Please specify: <span class="required">*</span></label>
                    <input type="text" name="Samples[x].HowDnaExtractedSpecify" disabled="disabled" class="required" />
                </div>
            </fieldset>

            <fieldset class="serum plasma wholeblood saliva tissue cell other" style="display: none">
            <div class="form-row serum plasma wholeblood saliva tissue cell other" style="display: none">
                <label>What was the elapsed time between sample collection and final storage of the sample? <span class="required">*</span></label>
                <select name="Samples[x].TimeBetweenCollectionAndStorage" class="required">
                    <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                <%
                    foreach (var item in TimeBetweenCollectionAndStorage.Unknown.EnumSelectList())
                    {
                        var specify = item.Text.ToLower().Contains("please specify");
                %>
                    <option value="<%:item.Value %>" <%= specify ? "class=\"specify\"" : "" %>><%=item.Text %></option>
                <%                
                    }
                %>
                </select>
            </div>
            </fieldset>

            <fieldset class="all" style="display: none">
                <div class="form-row all" style="display: none">
                    <label>What temperature were the <span class="sampleType"></span>&nbsp;samples kept at between sample collection and storage? <span class="required">*</span></label>
                    <select name="Samples[x].CollectionToStorageTemp" class="required">
                        <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                    <%
                        foreach (var item in CollectionToStorageTemp.Unknown.EnumSelectList())
                        {
                            var specify = item.Text.ToLower().Contains("please specify");
                    %>
                        <option value="<%:item.Value %>" <%= specify ? "class=\"specify\"" : "" %>><%=item.Text %></option>
                    <%                
                        }
                    %>
                    </select>
                </div>
                <div class="form-row all" style="display: none">
                    <label class="disabled">Please specify: <span class="required">*</span></label>
                    <input type="text" name="Samples[x].CollectionToStorageTempSpecify" disabled="disabled" class="required" />
                </div>
            </fieldset>

            <fieldset class="all" style="display: none">
                <div class="form-row all" style="display: none">
                    <label>Under what temperature conditions are the <span class="sampleType"></span>&nbsp;samples currently stored? <span class="required">*</span></label>
                    <select name="Samples[x].StorageTemp" class="required">
                        <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                    <%
                        foreach (var item in StorageTemp.Unknown.EnumSelectList())
                        {
                            var specify = item.Text.ToLower().Contains("please specify");
                    %>
                        <option value="<%:item.Value %>" <%= specify ? "class=\"specify\"" : "" %>><%=item.Text %></option>
                    <%                
                        }
                    %>
                    </select>
                </div>
                <div class="form-row all" style="display: none">
                    <label class="disabled">Please specify: <span class="required">*</span></label>
                    <input type="text" name="Samples[x].StorageTempSpecify" disabled="disabled" class="required" />
                </div>
            </fieldset>

            <fieldset class="all" style="display: none">
                <div class="form-row all" style="display: none">
                    <label>Have the <span class="sampleType"></span>&nbsp;samples always been stored at this temperature? <span class="required">*</span></label>
                    <select name="Samples[x].AlwayStoredAtThisTemp" class="required">
                        <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                    <%
                        foreach (var item in YesNoUnknown.Unknown.EnumSelectList())
                        {
                            var specify = item.Text.ToLower().Contains("please specify");
                    %>
                        <option value="<%:item.Value %>" <%= specify ? "class=\"specify\"" : "" %>><%=item.Text %></option>
                    <%                
                        }
                    %>
                    </select>
                </div>
            </fieldset>

            <fieldset class="dna" style="display: none">
                <div class="form-row dna">
                    <label class="fullwidth">Has the DNA quality been assessed by any of the following?</label>
                </div>
                <div class="dna">
                    <div class="radio-row dna">
                        <input type="checkbox" class="check" name="Samples[x].DnaQualityAbsorbance" value="true" id="Samples_x_DnaQualityAbsorbance" />
                        <label for="Samples_x_DnaQualityAbsorbance" class="trailing">Absorbance at 260/280 OD readings</label>
                    </div>
                    <div class="radio-row dna">
                        <input type="checkbox" class="check" name="Samples[x].DnaQualityGel" value="true" id="Samples_x_DnaQualityGel" />
                        <label for="Samples_x_DnaQualityGel" class="trailing">Samples have been run on a gel</label>
                    </div>
                    <div class="radio-row dna">
                        <input type="checkbox" class="check" name="Samples[x].DnaQualityCommercialKit" value="true" id="Samples_x_DnaQualityCommercialKit" />
                        <label for="Samples_x_DnaQualityCommercialKit" class="trailing">Samples have been analysed using a commercial testing kit e.g. DNA OK</label>
                    </div>
                    <div class="radio-row dna">
                        <input type="checkbox" class="check" name="Samples[x].DnaQualityPcr" value="true" id="Samples_x_DnaQualityPcr" />
                        <label for="Samples_x_DnaQualityPcr" class="trailing">Real time PCR analysis</label>
                    </div>
                    <div class="radio-row dna">
                        <input type="checkbox" class="check" name="Samples[x].DnaQualityPicoGreen" value="true" id="Samples_x_DnaQualityPicoGreen" />
                        <label for="Samples_x_DnaQualityPicoGreen" class="trailing">PicoGreen</label>
                    </div>
                    <div class="radio-row dna">
                        <input type="checkbox" class="check" name="Samples[x].DnaQualityUnknown" value="true" id="Samples_x_DnaQualityUnknown" />
                        <label for="Samples_x_DnaQualityUnknown" class="trailing">Unknown</label>
                    </div>
                    <div class="radio-row dna">
                        <input type="checkbox" class="check specify" name="Samples[x].DnaQualityOther" value="true" id="Samples_x_DnaQualityOther" />
                        <label for="Samples_x_DnaQualityOther" class="trailing">Other (please specify)</label>
                        <input type="text" name="Samples[x].DnaQualityOtherSpecify" disabled="disabled" class="required" />
                    </div>
                </div>
            </fieldset>

            <fieldset class="all" style="display: none">
                <div class="form-row all" style="display: none">
                    <label>How many freeze/thaw cycles have the <span class="sampleType"></span>&nbsp;samples been exposed to? (Please estimate if required). <span class="required">*</span></label>
                    <select name="Samples[x].FreezeThawCycles" class="required">
                        <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                    <%
                        foreach (var item in FreezeThawCycles.Unknown.EnumSelectList())
                        {
                            var specify = item.Text.ToLower().Contains("please specify");
                    %>
                        <option value="<%:item.Value %>" <%= specify ? "class=\"specify\"" : "" %>><%=item.Text %></option>
                    <%                
                        }
                    %>
                    </select>
                </div>
            </fieldset>

            <fieldset class="dna serum plasma saliva tissue other" style="display: none">
                <div class="form-row all">
                    <label class="fullwidth">Has successful analysis been performed on the sample since collection?</label>
                </div>
                <div class="all">
                    <div class="radio-row all" style="display:none">
                        <input type="checkbox" class="check" name="Samples[x].AnalysisNo" value="true" id="Samples_x_AnalysisNo" />
                        <label for="Samples_x_AnalysisNo" class="trailing">No successful analysis performed</label>
                    </div>
                    <div class="radio-row dna" style="display:none">
                        <input type="checkbox" class="check" name="Samples[x].AnalysisSequencing" value="true" id="Samples_x_AnalysisSequencing" />
                        <label for="Samples_x_AnalysisSequencing" class="trailing">Sequencing</label>
                    </div>
                    <div class="radio-row dna" style="display:none">
                        <input type="checkbox" class="check" name="Samples[x].AnalysisRealTimePcr" value="true" id="Samples_x_AnalysisRealTimePcr" />
                        <label for="Samples_x_AnalysisRealTimePcr" class="trailing">Real Time PCR</label>
                    </div>
                    <div class="radio-row dna" style="display:none">
                        <input type="checkbox" class="check" name="Samples[x].AnalysisPcr" value="true" id="Samples_x_AnalysisPcr" />
                        <label for="Samples_x_AnalysisPcr" class="trailing">PCR</label>
                    </div>
                    <div class="radio-row dna" style="display:none">
                        <input type="checkbox" class="check" name="Samples[x].AnalysisGenotyping" value="true" id="Samples_x_AnalysisGenotyping" />
                        <label for="Samples_x_AnalysisGenotyping" class="trailing">Genotyping</label>
                    </div>
                    <div class="radio-row serum plasma" style="display:none">
                        <input type="checkbox" class="check" name="Samples[x].AnalysisBiochemistry" value="true" id="Samples_x_AnalysisBiochemistry" />
                        <label for="Samples_x_AnalysisBiochemistry" class="trailing">Biochemistry</label>
                    </div>
                    <div class="radio-row serum plasma" style="display:none">
                        <input type="checkbox" class="check" name="Samples[x].AnalysisImmunochemistry" value="true" id="Samples_x_AnalysisImmunochemistry" />
                        <label for="Samples_x_AnalysisImmunochemistry" class="trailing">Immunochemistry</label>
                    </div>
                    <div class="radio-row saliva tissue" style="display:none">
                        <input type="checkbox" class="check" name="Samples[x].AnalysisDnaExtraction" value="true" id="Samples_x_AnalysisDnaExtraction" />
                        <label for="Samples_x_AnalysisDnaExtraction" class="trailing">DNA extraction</label>
                    </div>
                    <div class="radio-row tissue" style="display:none">
                        <input type="checkbox" class="check" name="Samples[x].AnalysisImmunohistochemistry" value="true" id="Samples_x_AnalysisImmunohistochemistry" />
                        <label for="Samples_x_AnalysisImmunohistochemistry" class="trailing">Immunohistochemisty</label>
                    </div>
                    <div class="radio-row tissue" style="display:none">
                        <input type="checkbox" class="check" name="Samples[x].AnalysisCellLinesDerived" value="true" id="Samples_x_AnalysisCellLinesDerived" />
                        <label for="Samples_x_AnalysisCellLinesDerived" class="trailing">Cell lines derived</label>
                    </div>
                    <div class="radio-row tissue" style="display:none">
                        <input type="checkbox" class="check" name="Samples[x].AnalysisRnaExtraction" value="true" id="Samples_x_AnalysisRnaExtraction" />
                        <label for="Samples_x_AnalysisRnaExtraction" class="trailing">RNA extraction</label>
                    </div>
                    <div class="radio-row all" style="display:none">
                        <input type="checkbox" class="check" name="Samples[x].AnalysisUnknown" value="true" id="Samples_x_AnalysisUnknown" />
                        <label for="Samples_x_AnalysisUnknown" class="trailing">Unknown</label>
                    </div>
                    <div class="radio-row all" style="display:none">
                        <input type="checkbox" class="check specify" name="Samples[x].AnalysisOther" value="true" id="Samples_x_AnalysisOther" />
                        <label for="Samples_x_AnalysisOther" class="trailing">Any other analysis (please specify)</label>
                        <input type="text" name="Samples[x].AnalysisOtherSpecify" disabled="disabled" class="required" />
                    </div>
                </div>

            </fieldset>

            <fieldset class="wholeblood" style="display: none">
                <div class="form-row wholeblood" style="display: none">
                    <label>Has DNA been successfully extracted from the whole blood samples? <span class="required">*</span></label>
                    <select name="Samples[x].DnaExtracted" class="required">
                        <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                    <%
                        foreach (var item in YesNoUnknown.Unknown.EnumSelectList())
                        {
                            var specify = item.Text.ToLower().Contains("please specify");
                    %>
                        <option value="<%:item.Value %>" <%= specify ? "class=\"specify\"" : "" %>><%=item.Text %></option>
                    <%                
                        }
                    %>
                    </select>
                </div>
            </fieldset>

            <fieldset class="cell" style="display: none">
                <div class="form-row cell" style="display: none">
                    <label>Have the cells been successfully grown up since collection and storage? <span class="required">*</span></label>
                    <select name="Samples[x].CellsGrown" class="required">
                        <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                    <%
                        foreach (var item in YesNoUnknown.Unknown.EnumSelectList())
                        {
                            var specify = item.Text.ToLower().Contains("please specify");
                    %>
                        <option value="<%:item.Value %>" <%= specify ? "class=\"specify\"" : "" %>><%=item.Text %></option>
                    <%                
                        }
                    %>
                    </select>
                </div>
            </fieldset>

        </div>
    </div>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%: Url.VersionedContent("~/Scripts/samples.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
<% if (Model.IsNew){ %>
            initWindowUnload();
<% } %>            

            initSamples(<%: Model.Id %>);
        });
    </script>
</asp:Content>
