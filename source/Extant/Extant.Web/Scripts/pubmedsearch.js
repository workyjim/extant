function initPubMedSearchResultDialog() {
    $('#pubmed-results-list ul li.select-publication').each(function () {
        var checkbox = $(this).find('input');
        var url = $(this).find('a').attr('href');
        $('#selected-publications li').each(function () {
            if ($(this).find('a').is('[href="' + url + '"]'))
                checkbox.prop('checked', true);
        });
    });

    if ($('#pubmed-results-list>ul>li.select-publication').length ==
         $('#pubmed-results-list>ul>li.select-publication>input:checked').length) {
        $('#pubmed-results-list>ul>li.select-all>input').prop('checked', true);
    }

    $('li.select-publication>input').change(function () {
        selectPublication(this, this.checked);
        setSelectAllStatus();
    });

    $('li.select-all>input').change(function () {
        if (this.checked) {
            $('#pubmed-results-list>ul>li.select-publication>input:not(:checked)').each(function () {
                $(this).prop('checked', true);
                selectPublication(this, true);
            });
        } else {
            $('#pubmed-results-list>ul>li.select-publication>input:checked').each(function () {
                $(this).prop('checked', false);
                selectPublication(this, false);
            });
        }
    });

    updateSelectedCount();

}

function updateSelectedCount() {
    $('#selected-count').text($('#selected-publications li').length);
}

function selectPublication(checkbox, checked) {
    if (checked) {
        $(checkbox).closest('li').clone().appendTo('#selected-publications');
    }
    else {
        var url = $(checkbox).next().find('a').attr('href');
        $('#selected-publications li').each(function () {
            if ($(this).find('a').attr('href') == url)
                $(this).remove();
        });
    }
    updateSelectedCount();
}

function setSelectAllStatus() {
    if ($('#pubmed-results-list>ul>li.select-publication').length ==
         $('#pubmed-results-list>ul>li.select-publication>input:checked').length) {
        $('#pubmed-results-list>ul>li.select-all>input').prop('checked', true);
    } else {
        $('#pubmed-results-list>ul>li.select-all>input').prop('checked', false);
    }
}