<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<form id="reauthenticate-form" action="/Account/Reauthenticate" method="post">
    <fieldset>                
        <div class="form-row">
            <label for="UserName">Email: <span class="required">*</span></label>
        </div>
        <div class="form-row">
            <input type="text" name="username" class="login" />
        </div>
                
        <div class="form-row">
            <label for="Password">Password <span class="required">*</span></label>
        </div>
        <div class="form-row">
            <input type="password" name="password" class="login" />
        </div>
                
        <div class="form-row">
            <button type="submit">Log&nbsp;On</button>
        </div>

        <div class="form-row" id="reauthenticate-error" style="display:none">
            <span class="error">The email address or password provided is incorrect.</span>
        </div>
    </fieldset>
</form>

<script type="text/javascript">
    $(document).ready(function () {
        $('button').button();
        $('#reauthenticate-form').ajaxForm({
            success: function () {
                loginCountDown();
                loginDialog.dialog('close');
            },
            error: function () {
                $('#reauthenticate-error').show();
            }
        });
    });
</script>