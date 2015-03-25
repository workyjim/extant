<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IDictionary<Extant.Web.Models.DiseaseAreaBasicModel, int>>" %>
<%@ Import Namespace="Extant.Web.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Find a Study
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Find a Study</h2>

<% Html.RenderPartial("SearchBox", new SearchBoxModel{DiseaseAreas = Model.Select(m => m.Key)}); %>

<div class="topborder">
    <h3>Or Browse by Disease Area</h3>
    <table class="disease-areas">
        <tbody>
            <tr>
<%
    int count = 0;
    foreach ( var da in Model.OrderBy(x => x.Key.DiseaseAreaName) )
    {
        if ( count > 0 && 0 == count % 5)
        {
%>
            </tr>
            <tr>
<%            
        }
%>
                <td>
                    <p><strong><%:da.Key.DiseaseAreaName %></strong></p>
                    <p><a href="/Study/ByDiseaseArea/<%:da.Key.Id %>"><%:da.Value %> Studies</a></p>
                </td>
<%        
        count++;
    }
    while ( count % 5 > 0)
    {
%>
                <td>&nbsp;</td>
<%
        count++;
    }
%>
            </tr>
        </tbody>
    </table>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
