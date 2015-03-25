function initStudyHome(studyId) {
    $('#tabs').tabs();

    $('#by-data-item-link').fancybox({
        scrolling: 'no',
        type: 'inline',
        onClosed: function () {
            $('#by-data-item').empty();
        }
    });

    $('span.dataitem').click(function () {
        var diid = $(this).attr('id').split('-')[2];
        var span = $(this);
        $.ajax({
            url: '/Study/ByDataItem/' + diid,
            type: 'GET',
            beforeSend: function () {
                span.after('<img src="/Images/indicator.gif" alt="Loading..." title="Loading..." />');
            },
            complete: function () {
                span.next().remove();
            },
            success: function (html) {
                $('#by-data-item').html(html);
                $('#by-data-item-link').click();
            }
        });
    });

    //hide empty categories
    $('#data-items-table').find('tr.category').each(function () {
        var levelClass = getLevelClass($(this));
        var level = getLevelNumber(levelClass);
        var nextRow = $(this).next('tr');
        var hasDataItems = false;
        while (nextRow.length > 0) {
            if (nextRow.hasClass('dataitem')) {
                hasDataItems = true;
                break;
            }
            if (nextRow.hasClass(levelClass) || nextRow.hasClass('disease-area') || getLevelNumber(getLevelClass(nextRow)) < level)
                break;

            nextRow = nextRow.next('tr');
        }
        if (!hasDataItems)
            $(this).hide();
    });

    $('#edit-study').click(function () {
        location.href = '/Study/Update/'+studyId;
    });

    $('.show-mesh').click(function () {
        var meshdiv = $(this).parent().siblings('.meshterms');
        if (meshdiv.is(':visible')) {
            meshdiv.slideUp();
            $(this).parent().find('img').attr('src', '/Images/plus.png').attr('alt', 'Show MeSH Terms');
        } else {
            meshdiv.slideDown();
            $(this).parent().find('img').attr('src', '/Images/minus.png').attr('alt', 'Hide MeSH Terms');
        }
    });
}

function mailto(email) {
    var decoded = email.replace(/[a-zA-Z]/g, function (c) {
        var cc = c.charCodeAt(0)
        return String.fromCharCode(cc - 9 < 65 || cc - 9 < 97 ? cc - 9 + 26 : cc - 9);
    });
    location.href = "mailto:" + decoded;
}

function getLevelClass(obj) {
    var classList = obj.attr('class').split(/\s+/);
    var levelClass = "";
    $.each(classList, function (index, item) {
        if (0 == item.indexOf('level-')) {
            levelClass = item;
        }
    });
    return levelClass;
}

function getLevelNumber(levelClass) {
    return levelClass.substr(6);
}