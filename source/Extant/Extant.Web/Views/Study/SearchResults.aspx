<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Extant.Web.Models.SearchResultsModel>" %>
<%@ Import Namespace="Extant.Web.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Find a Study
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Search Results</h2>

<% Html.RenderPartial("SearchBox", new SearchBoxModel{ Term = Model.Term, DiseaseArea = Model.DiseaseArea, StudyDesign = Model.StudyDesign, 
                                                       StudyStatus = Model.StudyStatus, Samples = Model.Samples, DiseaseAreas = Model.DiseaseAreas }); %>

<div class="topborder">
<% if (Model.Studies.Any()) { %>
<div id="search-results">
<% Html.RenderPartial("SearchResultsPartial", Model); %>
</div>
<% } else { %>
<p>Your search returned no results.</p>
<% } %>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
