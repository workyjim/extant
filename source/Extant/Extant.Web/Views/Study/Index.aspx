<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Extant.Web.Models.StudyIndexModel>" %>
<%@ Import Namespace="Extant.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Study | <%: Model.StudyName %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2 class="study-name">
    <%: Model.StudyName %>
    <div id="study-update">
        <span id="last-updated">Last updated <%:Model.StudyUpdated %></span>
<% 
    if (Model.CanEdit)
    {
%>
        <span id="edit-study-span"><button id="edit-study">Edit Study</button></span>
<%
    }
%>        
    </div>
</h2>
<p><em><%:Model.Description %></em></p>

<div id="tabs">
    <ul>
        <li><a href="#details">Study Details</a></li>
        <li><a href="#publications">Publications</a></li>
        <li><a href="#dataitems">Data Fields</a></li>
        <li><a href="#samples">Samples</a></li>
    </ul>

    <div id="details">
        <div class="fieldset">
            <span class="legend">Study Details</span>
            <p><strong>Synonyms: </strong> <%:Model.StudySynonyms %></p>
<% if (!string.IsNullOrEmpty(Model.StudyWebsite)){ %>            
            <p><strong>Website: </strong> <a href="<%:Model.StudyWebsite %>" target="_blank"><%:Model.StudyWebsite %></a></p>
<% } %>   
            <p><strong>Disease Areas: </strong> <%:Model.DiseaseAreasText %></p>
            <p><strong>Study Design: </strong> <%:Model.StudyDesign %></p>
<% if (!string.IsNullOrEmpty(Model.StartDate)){ %>
            <p><strong>Start Date: </strong> <%:Model.StartDate %></p>
<% } %>   
            <p><strong>Status: </strong> <%:Model.StudyStatus %></p>
<% if (!string.IsNullOrEmpty(Model.RecruitmentTarget)){ %>
            <p><strong>Recruitment Target: </strong> <%:Model.RecruitmentTarget %></p>
<% } %>  
<% if (!string.IsNullOrEmpty(Model.ParticipantsRecruited)){ %>
            <p><strong>Participants Recruited: </strong> <%:Model.ParticipantsRecruited %>
<%      if (!string.IsNullOrEmpty(Model.ParticipantsRecruited)){ %>            
            (<em>last updated <%:Model.ParticipantsRecruitedUpdated%></em>)
<%      } %>            
            </p>
<% } %>  
            <p><strong>Chief Investigator: </strong> <%:Model.PrincipalInvestigator %></p>
<% if (!string.IsNullOrEmpty(Model.Institution)){ %>
            <p><strong>Institution: </strong> <%:Model.Institution %></p>
<% } %>  
<% if (!string.IsNullOrEmpty(Model.Funder)){ %>
            <p><strong>Funder: </strong> <%:Model.Funder %></p>
<% } %>  
            <p><strong>Adopted on UKCRN Portfolio:</strong> <%:Model.OnPortfolio %></p>
            
<% if (!string.IsNullOrEmpty(Model.PortfolioNumber)){ %>            
            <p><strong>UKCRN ID:</strong> 
            <a href="http://public.ukcrn.org.uk/Search/StudyDetail.aspx?StudyID=<%:Model.PortfolioNumber %>" target="_blank" title="Click here to open the study's page on the UKCRN Portfolio"><%:Model.PortfolioNumber %></a>
            </p>
<% } %>            
        </div>

        <div class="fieldset">
            <span class="legend">Contact Details</span>
            <p><strong>Name: </strong> <%:Model.ContactName %></p>
            <p><strong>Address: </strong></p>
            <p>
<% foreach ( var part in Model.ContactAddress.Split('\n')){ %>                
                <%:part %><br />
<% }%>
            </p>
<% if (!string.IsNullOrEmpty(Model.ContactPhone)){ %>
            <p><strong>Phone: </strong> <%:Model.ContactPhone %></p>
<% }%>
            <p><strong>Email: </strong> 
<%  
    if (!String.IsNullOrEmpty(Model.ContactEmail))
    {
        if (Request.IsAuthenticated)
        {
%>
            <a href="mailto:<%:Model.ContactEmail%>"><%:Model.ContactEmail%></a>    
<%
        }
        else
        {
%>
            <span class="link" onclick="mailto('<%:Model.ContactEmailEncoded %>');">Click here to send an email</span>
<%
        }
    } 
%>
            </p>
        </div>

        <div class="fieldset">
            <span class="legend">Longitudinal Studies</span>
            <p><strong>Is the study longitudinal: </strong> <%:Model.IsLongitudinal %></p>
<% 
    if ( Model.TimePoints.Any() )
    {
%>
            <p><strong>Time Points:</strong></p>
            <p>
<%
        foreach ( var tp in Model.TimePoints )
        {
%>            
                <%:tp.Name %><br />
<%
        }
%>
            </p>
<%        
    }    
%>
        </div>

        <div class="fieldset">
            <span class="legend">Files</span>
            <p><strong>Patient Information Leaflet: </strong> 
<%  if (null != Model.PatientInformationLeaflet){ %>
            <a href="/Study/File/<%:Model.PatientInformationLeaflet.Id %>">Download</a> (<%:Model.PatientInformationLeaflet.FileSize %>)
<%  } else { %>
            N/A
<%  } %>
            </p>
            <p><strong>Consent Form: </strong> 
        <% if (null != Model.ConsentForm){ %>
            <a href="/Study/File/<%:Model.ConsentForm.Id %>">Download</a> (<%:Model.ConsentForm.FileSize %>)
        <% } else {%>
            N/A
        <% } %>
            </p>
            <p><strong>Has Data Access Policy: </strong><%:Model.HasDataAccessPolicy %></p>
            <p><strong>Data Access Policy: </strong> 
        <% if (null != Model.DataAccessPolicy){ %>
            <a href="/Study/File/<%:Model.DataAccessPolicy.Id %>">Download</a> (<%:Model.DataAccessPolicy.FileSize%>)
        <% } else {%>
            N/A
        <% } %>
            </p>
<% 
    if (Model.AdditionalDocuments.Any())
    {
%>
            <p><strong>Additional Files</strong></p>
<%
        foreach (var file in Model.AdditionalDocuments)
        {
%>
            <p><%:file.Description %> (<%:file.DocumentType %>) - <a href="/Study/File/<%:file.File.Id %>">Download</a> (<%:file.File.FileSize%>)</p>
<%
        }
    }
%>
        </div>
    </div>

    <div id="publications">
<%  if (Model.Publications.Any()) { %>
        <ul class="arrowed compact">
<%      foreach (var pub in Model.Publications) { %>        
            <li>
            <p><a href="<%:pub.Url%>" target="_blank"><%:pub.Title%></a></p>
            <p><%: string.Join(", ", pub.Authors) %></p>
            <p class="journal"><%:pub.Journal %> <%:pub.PublicationDate %></p>
<% 
            if (pub.MeshTerms.Any())
            {
%>
            <p><img src="/Images/plus.png" alt="Show MeSH Terms" class="link show-mesh" /> <span class="link show-mesh">MeSH Terms</span></p>
            <ul class="meshterms" style="display:none">
<%
                foreach (var mt in pub.MeshTerms)
                {
%>                
                <li><%: mt %></li>
<%
                }
%>
            </ul>
<%
            }
%>
            </li>
<%      } %>
        </ul>
<%  } else { %>
        <em>No publications have been associated with this study.</em>
<%  } %>
    </div>

    <div id="dataitems">
<% if ( Model.DataItems.Any() ){ %>
        <table class="data-items" id="data-items-table">
<%      if (Model.UseTimePoints){ %>
            <thead>
                <tr id="time-point-row">
                    <th>&nbsp;</th>
<%          foreach ( var tp in Model.TimePoints ){ %>
                    <th class="tp-check"><span><%:string.Join(" ", tp.Name.ToUpper().ToArray()) %></span></th>
<%          } %>    
                </tr>
            </thead>
            <tbody class="<%: Model.UseTimePoints ? "highlight" : "" %>">
<%
        }
%>
        <%=Model.DiseaseAreas.DataItemTable(Model.DataItems, Model.UseTimePoints, Model.TimePoints) %>
         
            </tbody>
        </table>
<%
} else { %>
        <em>No data fields have been associated with this study.</em>
<%  } %>

    </div>

    <div id="samples">
<%
    if ( Model.SampleNumbers.Any() ){
%>
        <ul>
<%            
        foreach ( var sample in Model.SampleNumbers)
        {
%>            
            <li><strong><%:sample.Key %>:</strong> <%:sample.Value %> samples</li>
<%
        }
%>        
        </ul>

<%
        if (Model.Samples.Any())
        {
            foreach (var sample in Model.Samples)
            {
%>    
        <div class="window ui-corner-all">
            <div class="window-title ui-widget-header ui-corner-all">
                <%:sample.SampleType %> Samples
            </div>
            <div class="window-body ui-dialog-content xxwide">

                <p><strong>Source biological material: </strong> <%=string.Join(", ", sample.SourceBiologicalMaterial)%></p>
<%
                if (!string.IsNullOrEmpty(sample.TissueSamplesPreserved))
                {
%>
                <p><strong>How were the tissue samples preserved? </strong> <%=sample.TissueSamplesPreserved %>
                <%:string.IsNullOrEmpty(sample.TissueSamplesPreservedSpecify)
                                          ? ""
                                          : " - " + sample.TissueSamplesPreservedSpecify%>
                </p>
<%
                }
%>
                <p><strong>Number of samples in collection: </strong> <%=sample.NumberOfSamples%>
                <%:sample.NumberOfSamplesExact.HasValue
                                      ? " - " + sample.NumberOfSamplesExact
                                      : ""%>
                </p>

                <p><strong>Volume range of samples within collection: </strong> <%=sample.SampleVolume%>
                <%:string.IsNullOrEmpty(sample.SampleVolumeSpecify)
                                      ? ""
                                      : " - " + sample.SampleVolumeSpecify%>
                </p>

<%
                if (sample.CellCount.HasValue)
                {
%>
                <p><strong>Estimation of the cell count (cells/ml)</strong> <%:sample.CellCount%></p>
<%
                }
%>

<%
                if (!string.IsNullOrEmpty(sample.Concentration))
                {
%>
                <p><strong>Concentration range of the samples: </strong> <%=sample.Concentration%>
                <%:string.IsNullOrEmpty(sample.ConcentrationSpecify)
                                          ? ""
                                          : " - " + sample.ConcentrationSpecify%>
                </p>
<%
                }
%>

<%
                if (!string.IsNullOrEmpty(sample.NumberOfAliquots))
                {
%>
                <p><strong>Number of aliquots per individual:</strong> <%=sample.NumberOfAliquots%></p>
<%
                }
%>

                <p><strong>How long ago the samples were collected: </strong> <%=sample.WhenCollected%></p>

<%
                if (!string.IsNullOrEmpty(sample.SnapFrozen))
                {
%>
                <p><strong>Tissue snap frozen: </strong> <%=sample.SnapFrozen%></p>
<%
                }
%>

<%
                if (!string.IsNullOrEmpty(sample.HowDnaExtracted))
                {
%>
                <p><strong>DNA extraction method: </strong> <%=sample.HowDnaExtracted%>
                <%:string.IsNullOrEmpty(sample.HowDnaExtractedSpecify)
                                          ? ""
                                          : " - " + sample.HowDnaExtractedSpecify%>
                </p>
<%
                }
%>

<%
                if (!string.IsNullOrEmpty(sample.TimeBetweenCollectionAndStorage))
                {
%>
                <p><strong>Elapsed time between sample collection and final storage: </strong> <%=sample.TimeBetweenCollectionAndStorage%></p>
<%
                }
%>

                <p><strong>Temperature of samples between collection and storage:</strong> <%=sample.CollectionToStorageTemp%>
                <%:string.IsNullOrEmpty(sample.CollectionToStorageTempSpecify)
                                      ? ""
                                      : " - " + sample.CollectionToStorageTempSpecify%>
                </p>

                <p><strong>Storage temperature of samples: </strong> <%=sample.StorageTemp%>
                <%:string.IsNullOrEmpty(sample.StorageTempSpecify)
                                      ? ""
                                      : " - " + sample.StorageTempSpecify%>
                </p>

                <p><strong>Have the samples always been stored at this temperature?</strong> <%=sample.AlwayStoredAtThisTemp%></p>

<%
                if (sample.DnaQuality.Any())
                {
%>
                <p><strong>DNA quality assessed by: </strong> <%=string.Join(", ", sample.DnaQuality)%></p>
    
<%
                }
%>

                <p><strong>Number of freeze/thaw cycles: </strong> <%=sample.FreezeThawCycles%></p>

<%
                if (sample.Analysis.Any())
                {
%>
                <p><strong>Successful analysis since collection: </strong> <%=string.Join(", ", sample.Analysis)%></p>
    
<%
                }
%>

<%
                if (!string.IsNullOrEmpty(sample.DnaExtracted))
                {
%>
                <p><strong>DNA successfully extracted: </strong> <%=sample.DnaExtracted%></p>
<%
                }
%>

<%
                if (!string.IsNullOrEmpty(sample.CellsGrown))
                {
%>
                <p><strong>Cells successfully grown up since collection and storage?</strong> <%=sample.CellsGrown%></p>
<%
                }
%>

            </div>
        </div>
<%
            }
        }
    }
    else
    {
%>
        <em>No samples have been associated with this study.</em>
<%
    }
%>
    </div>

</div>

<div style="display:none">
    <a href="#by-data-item" id="by-data-item-link">Open</a>
    <div id="by-data-item">
    </div>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/Content/fancybox/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="<%: Url.Content("~/Content/fancybox/jquery.fancybox-1.3.4.pack.js") %>" type="text/javascript"></script>
    <script src="<%: Url.VersionedContent("~/Scripts/studyhome.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            initStudyHome(<%: Model.Id %>);
        });
    </script>
</asp:Content>
