<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Extant.Web.Models.StudyDataItemsModel>" %>

<%@ Import Namespace="Extant.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Study | <%: Model.StudyName %> | Data Fields
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2><%: Model.StudyName %> - Data Fields</h2>

<p>There is <span id="help-link" class="link">additional help</span> available for using this page.</p>

<div class="split above-bottom-bar">
    <div class="left">
        <h4>Recommended Data Fields <span class="help-icon" title="This tree contains data fields that are commonly associated with studies in the same disease area(s). Tick the data fields which are collected for your study to add them to the selected data fields list to the right; you can check at any level of the tree to allow multiple data fields to be selected at once.">?</span></h4>
        <div id="tree">
            <%: Model.DiseaseAreas.DataItemTree(Model.DataItems) %>
        </div>
    </div>
    <div class="right">
        <h4>Selected Data Fields <span class="help-icon" title="This list contains all of the data fields which are collected for your study, including both data fields selected in the recommended data fields section (left) and any additional data fields which can be added using the search box at the bottom of this list.">?</span></h4>
        <form action="/Study/DataFields/<%:Model.Id %>" method="post" id="data-item-form">
            <input type="hidden" name="isnew" value="<%:Model.IsNew %>" />
            <input type="hidden" name="action" value="" id="submit-field" />
            <div class="form-row" <%= !Model.IsLongitudinal ? "style=\"display: none\"" : "" %>>
                <label for="usetimepoints">Select at which time points each data field is collected</label>
                <input type="checkbox" name="usetimepoints" id="usetimepoints" value="true" class="check" <%=Model.UseTimePoints ? "checked=\"checked\"" : "" %> />
                <span class="help-icon" title="Select this option if you would like to define which data fields are collected at each of the time points in the study.">?</span>
            </div>
            <table class="data-items" style="display:none">
                <thead>
                    <tr id="time-point-row">
                        <th>&nbsp;</th>
<% foreach ( var tp in Model.TimePoints ){ %>
                        <th class="tp-check"><span><%:string.Join(" ", tp.Name.ToUpper().ToArray()) %></span></th>
<% } %>    
                        <th class="del-btn">&nbsp;</th>          
                    </tr>
                    <tr id="select-all-row" class="select-all-dfs">
                        <th><em>Select all</em></th>
<% foreach ( var tp in Model.TimePoints ){ %>
                        <th><input type="checkbox" class="check" id="select-all-<%:tp.Id %>" /></th>
<% } %> 
                        <th>&nbsp;</th>                       
                    </tr>
                </thead>
                <tbody class="highlight">
<%
    int counter = 0;
    foreach (var dataItem in Model.DataItems){ 
%>       
                    <tr id="dataitem-<%:dataItem.Id %>" class="<%= 0 == counter ? "first" : "" %>">
                        <td>
                            <%:dataItem.DataItemName %>
                            <input type="hidden" name="DataItems[x].DataItemName" value="<%:dataItem.DataItemName %>" />
                            <input type="hidden" name="DataItems[x].Id" value="<%:dataItem.Id %>" class="dataitemid" />
                        </td>
<%      foreach ( var tp in Model.TimePoints ){%>
                        <td class="tp-check">
                            <input type="checkbox" class="check" value="<%:tp.Id %>" name="DataItems[x].TimePoints" <%= null != dataItem.TimePoints && dataItem.TimePoints.Contains(tp.Id) ? "checked=\"checked\"" : "" %> />
                        </td>
<%      } %>  
                        <td class="del-btn"><img src="/Images/delete.png" alt="Remove data field" title="Remove data field" class="link remove-dataitem" /></td>
                    </tr>
<%
        counter++;
    } 
%>         
                </tbody>
            </table>
        </form>

        <p id="no-data-fields" style="display:none"><em>No data fields have been selected.</em></p>

        <div class="topborder">
            <form action="#" method="get" id="add-dataitem">
                <div class="form-row">
                    <input type="text" id="search" />
                    <span class="help-icon" title="Use this search box to add data fields that do not appear in the recommended data fields section. Type in the first few characters of the data field and the search may find and display the data field you require, which can then be selected from a drop-down list and added to the list above by clicking the add button. If the data field cannot be found by the search type its full name and click Add.">?</span>
                    <button type="submit">Add</button>
                    <input type="hidden" id="dataitemid" value="0" />
                </div>
            </form>
        </div>
    </div>
</div>


<div class="topborder bottom-bar">

    <div class="form-row">
<% if (Model.IsNew) { %>
        <button id="prev-btn">&lt;&nbsp;Previous</button>
        <button id="next-btn">Next&nbsp;&gt;</button>
<% } else { %>
        <button id="save-btn">Save</button>
        <button id="cancel-btn">Cancel</button>
<% } %>        
    </div>

</div>

<div style="display:none">
   <a href="#data-item-help" id="data-item-help-link">Open</a>
    <div id="data-item-help" style="height: auto; width: 800px;">
        <h2>Data Items Help</h2>
        <p>The third stage in the Add Study wizard is selecting the data fields that have been collected by the study. A good way of picturing the data fields is to imagine all the data collected by the study being held in a single spreadsheet; if each row represented a subject recruited into the study then the columns in the spreadsheet represent the data fields.</p>
        <p>The left-hand half of the screen displays a list of recommended data fields that are commonly used by studies in the disease area or areas that the study belongs to. The data fields are organized into categories and are selected by checking the relevant checkbox. To select all data fields in a category check the category itself.</p>
        <p>When data fields are selected in the list of recommended data fields they are also added to the list in the right-hand half of the screen, which contains only the data fields selected for the study. If a data field in your study is not displayed in the recommended data fields it can be added directly to the selected data fields using the text entry field at the bottom of the list. Enter three or more characters of the data field and a search will be performed for matching data fields that are already used by other studies. The results are displayed in a dropdown list, select the relevant data field. If this search does not return the data field that you wish to add then type in its name completely and click Add.</p>
        <p>If the study was defined as a longitudinal study in the first stage of the wizard then it is possible to define at which time points each data field is collected. If this option is selected then a column of checkboxes will be added to the list of selected data fields for each time point. For each data field select the time point(s) at which it is collected.</p>
    </div>

</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/Content/dynatree/skin/ui.dynatree.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="/Content/fancybox/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="<%: Url.Content("~/Content/fancybox/jquery.fancybox-1.3.4.pack.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Content/dynatree/jquery.dynatree.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.VersionedContent("/Scripts/dataitems.js") %>" type="text/javascript"></script>
    <script type="text/javascript">

        var timepoints = [];
<% foreach ( var tp in Model.TimePoints){%>
        timepoints.push(<%:tp.Id %>);
<% } %>

        var isNew = false;
<% if (Model.IsNew){ %>
            isNew = true;
            initWindowUnload();
<% } %>     

        $(document).ready(function () {
            initDataItems(isNew);
        });
    </script>
</asp:Content>
