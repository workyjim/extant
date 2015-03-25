<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Extant.Web.Models.SearchBoxModel>" %>
<%@ Import Namespace="Extant.Data.Entities" %>
<%@ Import Namespace="Extant.Web.Helpers" %>

<p>Enter some search terms, or alternatively use the <a href="/Study/SearchAdvanced">Advanced Search</a></p>

<form method="get" action="/Study/Search">
    <fieldset>
        <div class="form-row">
            <input type="text" name="term" class="search" value="<%:Model.Term %>" />
            <button type="submit">Search</button>
            <span class="link" id="show-filters"><%: Model.FiltersUsed ? "Hide" : "Show" %> filters</span>
            <span class="help-icon" id="search-help-link" title="Click here to see help information about searching">?</span>
        </div>
        <div id="filters" class="narrow" <%= !Model.FiltersUsed ? "style=\"display:none\"" : "" %>>
            <div class="form-row">
                <label>Disease Area</label>
                <select name="da" <%= !Model.FiltersUsed ? "disabled=\"disabled\"" : "" %>>
                    <option value="">Any</option>
<% 
    foreach ( var da in Model.DiseaseAreas)
    {
%>
                    <option value="<%:da.Id %>" <%: da.Id == Model.DiseaseArea ? "selected=\"selected\"" : "" %>><%:da.DiseaseAreaName %></option>
<%        
    }
%>
                </select>
            </div>
            <div class="form-row">
                <label>Study Design</label>
                <select name="sd" <%= !Model.FiltersUsed ? "disabled=\"disabled\"" : "" %>>
                    <option value="">Any</option>
<%
    foreach ( var sd in StudyDesign.Observational.EnumSelectList())
    {
%>
                    <option value="<%:sd.Value %>" <%: Convert.ToInt32(sd.Value) == Model.StudyDesign ? "selected=\"selected\"" : "" %>><%:sd.Text %></option>
<%        
    }     
%>
                </select>
            </div>
            <div class="form-row">
                <label>Status</label>
                <select name="st" <%= !Model.FiltersUsed ? "disabled=\"disabled\"" : "" %>>
                    <option value="">Any</option>
<%
    foreach ( var st in StudyStatus.Recruiting.EnumSelectList())
    {
%>
                    <option value="<%:st.Value %>" <%: Convert.ToInt32(st.Value) == Model.StudyStatus ? "selected=\"selected\"" : "" %>><%:st.Text %></option>
<%        
    }     
%>
                </select>
            </div>
            <div class="form-row">
                <label>Samples</label>
                <select name="s" <%= !Model.FiltersUsed ? "disabled=\"disabled\"" : "" %>>
                    <option value="">Any</option>
<% 
    foreach (var s in new[] { "DNA", "Serum", "Plasma", "Whole Blood", "Saliva", "Tissue", "Cell", "Other" })
    {
%>
                    <option <%:string.Equals(s, Model.Samples) ? "selected=\"selected\"" : ""%>><%:s%></option>
<%
    }
%>
                </select>
            </div>
        </div>
    </fieldset>
</form>

<div id="search-help" class="info" style="display:none">
    <ul>
        <li>Searches are not case sensitive, so <strong>arthritis</strong> will match arthritis, Arthritis and ARTHRITIS.</li>
        <li>Use quotes to group words into phrases e.g. <strong>&quot;Rheumatoid Arthritis&quot;</strong></li>
        <li>Use the * character for wildcard searches e.g. <strong>Rheuma*</strong> will match Rheumatoid, Rheumatology, Rheumatism</li>
        <li>By default search terms are combined with an OR operator, so <strong>Rheumatoid Observational</strong> and <strong>Rheumatoid OR Observational</strong> 
        are equivalent, and both will match studies that contain either rheumatoid or observational.</li>
        <li>Use the AND operator to specify that all search terms must be found e.g. <strong>Rheumatoid AND Observational</strong> will match
        studies that contain both rheumatoid and observational.</li>
    </ul>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        $('#search-help-link').click(function () {
            if ($('#search-help').is(':visible')) {
                $('#search-help').slideUp();
            } else {
                $('#search-help').slideDown();
            }
        });

        $('#show-filters').click(function () {
            if ($('#filters').is(':visible')) {
                $('#filters').slideUp();
                $('#filters select').attr('disabled', 'disabled');
                $(this).text("Show filters");
            } else {
                $('#filters').slideDown();
                $('#filters select').removeAttr('disabled');
                $(this).text("Hide filters");
            }
        });
    });
</script>