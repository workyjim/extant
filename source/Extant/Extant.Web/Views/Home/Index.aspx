<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Extant.Web.Models.StudyBasicModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="home-content">
        <div id="home-search">
            <div class="box-orange-border">
                <div class="boxtopleft">
                    <div class="boxtopright">
                        <div class="boxcontent">
                            <div id="home-search-top">
                                Search for a study
                            </div>
                            <div id="home-search-bottom">
                                <form action="/Study/Search" method="get">
                                    <input type="text" name="term" id="home-search-textbox" />
                                    <input type="submit" class="home-search-button" value="" />
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="boxbottomleft">
                    <div class="boxbottomright">
                    </div>
                </div>
            </div>
        </div>

        <h2>Welcome</h2>

        <p>Welcome to the <%=ConfigurationManager.AppSettings["OrganisationName"]%> <%=ConfigurationManager.AppSettings["CatalogueName"]%>.</p>

        <p>The catalogue is intended to record the details of all <%=ConfigurationManager.AppSettings["OrganisationName"]%> funded research studies in an easily searchable form. To search for a study click the button to the right.</p>

        <p>If you are a researcher or study coordinator who would like to add their study or studies to the catalogue you first need to <a href="/Account/Register">Register</a> for a user account. 
        Once your account has been approved you will be able to enter your studies onto the system.</p>

    </div>

    <div class="topborder clear">
        <p><em>The most recent studies added to the catalogue:</em></p>
        <ul class="arrowed">
    <% foreach (var study in Model){ %>
            <li><a href="/Study/Index/<%:study.Id %>"><%: study.StudyName %></a>
                <div class="description"><span class="ellipsis_text"><%:study.Description %></span></div>
            </li>
    <% } %>
        </ul>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>