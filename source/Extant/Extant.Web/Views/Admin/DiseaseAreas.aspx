<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Extant.Web.Models.DiseaseAreasModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Disease Areas
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Disease Areas</h2>

<ul class="arrowed">
<% 
foreach (var da in Model.DiseaseAreas)
{
%>
    <li><a href="/Admin/DiseaseArea/<%:da.Id %>"><%:da.DiseaseAreaName %></a></li>
<%  
}    
%>
</ul>

<% if ( Model.IsAdmin ){ %>
<div class="topborder">
    <h3>Add a new disease area</h3>
    <form action="/Admin/AddDiseaseArea" method="post">
        <div class="form-row">
            <label for="new-da-name">Name</label>
            <input type="text" name="daname" id="new-da-name" />
            <button type="submit">Add</button>
        </div>
    </form>
</div>
<% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
