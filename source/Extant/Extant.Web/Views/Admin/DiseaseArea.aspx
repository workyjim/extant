<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Extant.Web.Models.DiseaseAreaModel>" %>

<%@ Import Namespace="Extant.Web.Helpers" %>

<%@ Import Namespace="Extant.Web.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Disease Area | <%: Model.DiseaseAreaName %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Disease Area - <span id="da-name" title="Click to edit"><%: Model.DiseaseAreaName %></span></h2>

<div class="form-row" style="margin-bottom: 10px;">
    <label for="da-synonyms">Synonyms:</label>
    <input type="text" id="da-synonyms" value="<%:Model.DiseaseAreaSynonyms %>" />
    <span class="help-icon" title="Any other names that the disease area is commonly known by, abbrevations etc.">?</span>
</div>

<p>Commonly used data fields for this disease area. 
<% if (!Model.Categories.Any()){ %>
To start designing the data fields in this disease area double-click the disease area name below.
<% } %>
Click for <span class="link" id="help-link">additional help</span>.
</p>

<p><em>Only data fields need to be added here; study information such as contact details, publications and samples is automatically provided.</em></p>

<div class="clear">
    <div id="tree">
        <%: Model.DataItemTree()%>
    </div>

    <div class="form-row">
        <button id="remove">Remove&nbsp;Selected</button>
    </div>
</div>

<div class="topborder">
    <form action="/Admin/DiseaseArea/<%: Model.Id %>" method="post" id="form">
        <div class="form-row">
            <input type="hidden" id="published" name="published" value="<%:Model.Published.ToString().ToLower() %>" />
            <input type="hidden" id="form-synonyms" name="synonyms" value="<%:Model.DiseaseAreaSynonyms %>" />
<% if (Model.Published){ %>
            <button type="submit" id="publish-btn">Save</button>
<% } else { %>
            <button type="submit" id="save-btn">Save</button>
            <button type="submit" id="publish-btn">Publish</button>
<% } %>
            <button id="close-btn">Close</button>
            <span id="autosaving" style="display: none"><img src="/Images/indicator.gif" alt="Saving..." /> <em>Auto-saving...</em></span>
            <span id="autosavesuccess" style="display: none"><em>Auto-save successful</em></span>
            <span id="autosaveerror" style="display: none"><em>Auto-save failed</em></span>
        </div>
        <div class="form-row" id="publish-error" style="display:none">
<% if (Model.Published){ %>            
            <span class="error">The disease area did not pass validation; all errors must be corrected before it may be published.</span>
<% } else { %>
            <span class="error">The disease area did not pass validation; all errors must be corrected before a published diease area may be saved.</span>
<% } %>
        </div>
    </form>
</div>


<div style="display:none">
    <a href="#add-form" id="add-link">Open</a>
    <div id="add-form" style="height: auto; width: 300px;">
        <div class="form-row" id="add-type-row">
            <select id="add-type">
                <option value="" disabled="disabled" selected="selected">-- Please select --</option>
                <option value="editcategory">Edit this category</option>
                <option value="addcategory">Add a sub-category</option>
                <option value="dataitem">Add a data field</option>
            </select>
        </div>
        <form id="edit-category-form" action="#" style="display: none;">
            <div class="form-row">
                <label class="narrow">Name <span class="required">*</span></label>
                <input id="edit-category-name" name="categoryname" type="text" />
            </div>
            <div class="form-row">
                <button id="edit-category">OK</button>
                <button class="cancel-dialog">Cancel</button>
            </div>
        </form>
        <form id="add-category-form" action="#" style="display: none;">
            <div class="form-row">
                <label class="narrow">Name <span class="required">*</span></label>
                <input id="category-name" name="categoryname" type="text" />
            </div>
            <div class="form-row">
                <button id="add-category">Save</button>
                <button id="add-and-close-category">Save &amp; Close</button>
                <button class="cancel-dialog">Close</button>
            </div>
        </form>
        <form id="add-dataitem-form" action="#" style="display: none;">
            <div class="form-row">
                <label class="narrow">Name <span class="required">*</span></label>
                <input id="dataitem-fullname" name="dataitemfullname" type="text" />
                <input id="dataitem-id" type="hidden" value="0" />
            </div>
            <div class="form-row">
                <label class="narrow">Display Name</label>
                <input id="dataitem-shortname" type="text" />
            </div>
            <div class="form-row">
                <button id="add-dataitem">Save</button>
                <button id="add-and-close-dataitem">Save &amp; Close</button>
                <button class="cancel-dialog">Close</button>
            </div>
        </form>    
        <form id="edit-dataitem-form" action="#" style="display: none;">
            <div class="form-row">
                <label class="narrow">Name <span class="required">*</span></label>
                <input id="edit-dataitem-fullname" name="dataitemfullname" type="text" />
                <input id="edit-dataitem-id" type="hidden" value="0" />
            </div>
            <div class="form-row">
                <label class="narrow">Display Name</label>
                <input id="edit-dataitem-shortname" type="text" />
            </div>
            <div class="form-row">
                <button id="edit-dataitem">OK</button>
                <button class="cancel-dialog">Cancel</button>
            </div>
        </form>    
    </div>

    <a href="#get-started" id="get-started-link">Open</a>
    <div id="get-started" style="height: auto; width: 800px;">
        <h2>Disease Area Help</h2>
        <p>Configuration of a disease area consists of defining a list of commonly used data fields for studies belonging to the 
        disease area. The data fields may be arranged into categories to make it easier to find similar data fields.</p>
        <ul>
            <li>Disease Area – the disease area is the top-level item and cannot be removed or edited. Only Categories may be added to a Disease Area</li>
            <li>Category – a “folder” to logically group associated data fields</li>
            <li>Sub-Category – a category of a category; there is no limit on the number of levels of categories that you can define</li>
            <li>Data Field – an item of data that may be collected in studies belonging to the disease area.</li>
        </ul>
        <p>When a disease area is first created only the disease area itself is displayed. First, categories must be added to the disease area; this is done by double-clicking on the disease area and entering the name of the category in the dialog. If you wish to add another category click Save, the category will be added to the disease area and the dialog will remain open to allow another category to be added. To add the category and close the dialog click Save &amp; Close.</p>
        <p>If you double click on a category three options are presented:</p>
        <ul>
            <li>Edit this category – allows the name of the category to be edited</li>
            <li>Add a sub-category – allows a sub-category to be added to the category</li>
            <li>Add a data field – allows a data field to be added to the category. Start typing in the name of the data field and once three or more characters have been typed a search of existing data fields used in the catalogue will be performed and a list of search results displayed. Select the relevant data field, or if it isn’t found type in the full name of the data field. If you wish the data field to appear in the list of data fields for this disease area with a different name complete the Display Name (this is commonly used if – for example – the data field Biologics Dose is added to a category Biologics; a Display Name of Dose could be entered since the word Biologics in the data field name is redundant. If you wish to add another data field click Save, the category will be added to the disease area and the dialog will remain open to allow another data field to be added. To add the data field and close the dialog click Save &amp; Close.</li>
        </ul>
        <p>To remove data fields or categories tick the checkbox next to the relevant category or data field and click the “Remove Selected” button.</p>
        <p>Both data fields and categories/sub-categories may be reordered via “drag and drop”; click on the data field/category and hold the mouse button down, move the mouse to re-position the data-field/category then release the mouse button to move the data-field/category to the new position. In this way categories, sub-categories and data fields can be reordered, a sub-category moved to be under a different category, and data fields moved to be under a different category or sub-category.</p>
        <p>The disease area may be renamed by clicking on its name in the page title after “Disease Area –“. The text of the disease area name will change to a textbox where it can be edited.</p>
        <p>Additionally, synonyms for the disease area may be added using the Synonyms textbox just under the page title. Synonyms for the disease area could be alternative names or abbreviations for it and including them will help users to search for studies.</p>
    </div>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/Content/fancybox/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="/Content/dynatree/skin/ui.dynatree.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="<%: Url.Content("~/Content/fancybox/jquery.fancybox-1.3.4.pack.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Content/dynatree/jquery.dynatree.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/mergesort.js") %>" type="text/javascript"></script>
    <script src="<%: Url.VersionedContent("/Scripts/diseaseareabuilder.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            initDiseaseAreaBuilder(<%:Model.Id %>);
        });
    </script>
</asp:Content>
