// See http://jonathonhill.net/2011-03-04/catching-the-javascript-beforeunload-event-the-cross-browser-way/
function initWindowUnload() {
    var message = "If you leave the page any changes that you have made will be lost. Please user the Next and Previous buttons to move between different stages of the add a study process.";
    window.onbeforeunload = function (e) {
        var e = e || window.event;

        // For IE and Firefox prior to version 4
        if (e) {
            e.returnValue = message;
        }

        // For Safari
        return message;
    };
}

function cancelWindowUnload() {
    window.onbeforeunload = null;
}