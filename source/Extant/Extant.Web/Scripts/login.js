
var loginDialog;
var loginCountdown = null;

function loginCountDown() {
    if (loginCountDown != null)
        window.clearInterval(loginCountdown);

    // sessionLength needs to be set to half of the Forms authentication timeout.
    var sessionLength = 15;
    var startTime = ~ ~(+(new Date()) / 1000);
    loginCountdown = setInterval(function () {
        var now = ~ ~(+(new Date()) / 1000);
        var remaining = (sessionLength*60) - 30 - (now - startTime);
        if (remaining <= 0)
            loginTimeout();
    }, 1000);
}

function loginTimeout() {
    loginCountdown = window.clearInterval(loginCountdown);

    // show a spinner or something via css
    loginDialog = $('<div id="logindialog" class=\'loading\'>Loading...<img src=\'/Images/indicator.gif\' alt=\'*\' /></div>').appendTo('body');

    // open the dialog
    loginDialog.dialog({
        // add a close listener to prevent adding multiple divs to the document
        close: function (event, ui) {
            // remove div with all data and events
            loginDialog.remove();
        },
        modal: true,
        minHeight: 300,
        minWidth: 350,
        bgiframe: true,
        closeOnEscape: false,
        open: function (event, ui) { $(".ui-dialog-titlebar-close").hide(); },
        title: "Login Timeout"
    });

    // load remote content
    loginDialog.load(
        '/Account/Reauthenticate',
        function (responseText, textStatus, XMLHttpRequest) {
            // remove the loading class
            loginDialog.removeClass('loading');
        }
    );
}