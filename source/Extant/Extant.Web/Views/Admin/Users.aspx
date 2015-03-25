<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Extant.Web.Models.AdminUserModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Users
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Users</h2>

<p>When a user registers they will receive an email containing a unique validation link.</p>
<p>If the user has not yet responded to this email then their email below will show as <span class="unvalidated-email"> (unvalidated)</span>.</p>
<p>You should only approve a user with an unvalidated email address if you are sure that the email is correct for that account.</p>
<p>You may set the email address to valid in the edit user page.</p>
<table id="users-table">
<%
    foreach(var user in Model)
    {
%>
    <tr id='<%:user.Id %>' class='<%: user.IsApproved ? "approved" : "" %>' >
        <td><%:user.UserName %></td>
        <td class="email"><a href="mailto:<%:user.Email %>"><%:user.Email %></a>
        <% if (!user.EmailValidated) {%>
            <span class="unvalidated-email"> (unvalidated)</span>
        <% } %>
        </td>
        <td><a href="/Admin/EditUser/<%:user.Id %>">Edit</a></td>
        <td><span class="link delete-user">Delete</span></td>
<% if ( !user.IsApproved ) {%>
        <td><span class="link approve-user">Approve</span></td>
<% } %>
<% else if ( user.IsLockedOut ) {%>
        <td><span class="link unlock-user">Unlock</span></td>
<% } else { %>        
        <td></td>
<% } %>
    </tr>
<%        
    } 
%>
</table>

<div class="clear form-row">
    <button onclick="location.href='/Admin/AddUser';">Add User</button>
    <label for="only-unapproved" style="margin-right: 5px;">Show only unapproved users</label><input type="checkbox" id="only-unapproved" class="check" />
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

            $('#users-table').oneSimpleTablePagination({ rowsPerPage: 15, hiddenClass: 'hidden' });

            $('#only-unapproved').click(function () {
                if ($(this).is(':checked')) {
                    $('#users-table tr.approved').addClass('hidden');
                } else {
                    $('#users-table tr.approved').removeClass('hidden');
                }
                $('#tablePagination_reset').click();
            });

            $('.approve-user').click(function () {
                var approveLink = $(this);
                var approveRow = $(this).closest('tr');
                var userId = approveRow.attr('id');
                $.ajax({
                    type: 'POST',
                    url: '/Admin/ApproveUser/'+userId,
                    beforeSend: function () {
                        approveRow.find('td:last').append('<img src="/Images/indicator.gif" class="progress" alt="Working..." title="Working..." />');
                    },
                    complete: function () {
                        approveRow.find('img.progress').remove();
                    },
                    success: function () {
                        alert('The account has been approved successfully');
                        approveLink.remove();
                    },
                    error: function () {
                        alert('An error occurred whilst trying to approve the account');
                    }
                });
            });

            $('.unlock-user').click(function () {
                var unlockLink = $(this);
                var unlockRow = $(this).closest('tr');
                var userId = unlockRow.attr('id');
                $.ajax({
                    type: 'POST',
                    url: '/Admin/UnlockUser/' + userId,
                    beforeSend: function () {
                        unlockRow.find('td:last').append('<img src="/Images/indicator.gif" class="progress" alt="Working..." title="Working..." />');
                    },
                    complete: function () {
                        unlockRow.find("img.progress").remove();
                    },
                    success: function () {
                        alert('The account has been unlocked successfully');
                        unlockLink.remove();
                    },
                    error: function () {
                        alert('An error occurred whilst trying to unlock the account');
                    }
                });
            });

            $('.delete-user').click(function () {
                var email = $(this).parent().siblings('.email').find('a').text();
                if (confirm("Are you sure you want to delete user '" + email + "'?")) {
                    var deleteRow = $(this).closest('tr');
                    var userId = deleteRow.attr('id');
                    $.ajax({
                        type: 'POST',
                        url: '/Admin/DeleteUser/' + userId,
                        beforeSend: function () {
                            deleteRow.find('td:last').append('<img src="/Images/indicator.gif" class="progress" alt="Working..." title="Working..." />');
                        },
                        complete: function () {
                            deleteRow.find("img.progress").remove();
                        },
                        success: function () {
                            deleteRow.remove();
                            $('#tablePagination_reset_currentpage').click();
                        },
                        error: function () {
                            alert('An error occurred whilst trying to delete the account');
                        }
                    });
                }
            });
        });
    </script>
</asp:Content>
