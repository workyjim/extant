function initStudyDetails() {

    $('.datepicker').datepicker({
        changeMonth: true,
        changeYear: true,
        yearRange: '-120:+0',
        showOn: 'both',
        buttonImage: '/Images/calendar.png',
        buttonImageOnly: true,
        dateFormat: 'dd-mm-yy'
    });

    $('#DataAccessPolicy').attr('disabled', 'disabled').prev().addClass('disabled');
    $('input[name=HasDataAccessPolicy]').click(function () {
        setDataAccessPolicyStatus();
    });

    $('#PortfolioNumber').attr('disabled', 'disabled').prev().addClass('disabled');
    $('input[name=OnPortfolio]').click(function () {
        setPortfolioNumberStatus();
    });

    $('#page-body form').data("validator").settings.submitHandler = function (form) {
        if (validateDiseaseAreas() && validateAdditionalFiles() && validateTimePoints()) {
            cancelWindowUnload();
            form.submit();
        }
    };
    $('#page-body form').bind("invalid-form.validate", function () {
        validateDiseaseAreas();
        validateAdditionalFiles();
        validateTimePoints();
    });

    $('#add-file-btn').click(function () {
        var newfile = $('#additional-file-template>div').clone();
        var newIndex = $('#additional-files>div').length;
        newfile.find('input,select').each(function () {
            var newName = $(this).attr('name').replace('[x]', '[' + newIndex + ']');
            setElementName($(this), newName);
        });
        newfile.find('img').click(function () {
            $(this).parent().remove();
            $('#additional-files>div').each(function (index, row) {
                $(row).find('input,select').each(function () {
                    var newName = $(this).attr('name').replace(/\[\d+\]/, '[' + index + ']');
                    setElementName($(this), newName);
                });
            });
            if (0 == $('#additional-files>div:visible').length) {
                $('#additional-files-error').hide();
                $('#no-additional-files').show();
            }
        });
        newfile.find('input[name$=Description]').focus(function () {
            $(this).val('');
            $(this).removeClass('unaccessed');
            $(this).unbind('focus');
        });
        newfile.appendTo('#additional-files');
        $('#no-additional-files').hide();
        return false;
    });

    $('.add-disease-area').click(function () {
        addDiseaseArea(this);
    });
    $('.remove-disease-area').click(function () {
        removeDiseaseArea(this);
    });

    if ($('#IsLongitudinal-no').is(':checked')) {
        setTimePointsStatus();
    } else {
        $('.add-time-point').click(function () {
            addTimePoint(this);
        });
        $('.remove-time-point').click(function () {
            removeTimePoint(this);
        });
    }
    $('input[name=IsLongitudinal]').click(function () {
        setTimePointsStatus();
    });
}


function setPortfolioNumberStatus() {
    if ($('#OnPortfolio-yes').is(':checked')) {
        $('#PortfolioNumber').removeAttr('disabled').prev().removeClass('disabled');
    } else {
        $('#PortfolioNumber').val('').attr('disabled', 'disabled').removeClass('input-validation-error')
                .prev().addClass('disabled')
                .next().next().next().text('');
    }
}

function setDataAccessPolicyStatus() {
    if ($('#HasDataAccessPolicy-yes').is(':checked')) {
        $('#DataAccessPolicy').removeAttr('disabled').prev().removeClass('disabled');
    } else {
        $('#DataAccessPolicy').val('').attr('disabled', 'disabled').prev().addClass('disabled');
    }
}

function validateAdditionalFiles() {
    var filesOk = true;
    $('#additional-files>div').each(function () {
        var rowOk = true;
        $(this).find('input[type!=hidden],select').each(function () {
            if ($(this).attr('type') == 'file') {
                rowOk &= ($(this).siblings('[name$=Id]').length > 0 || $(this).val().length > 0);
            } else {
                rowOk &= ($(this).val().length > 0 && !$(this).hasClass('unaccessed'));
            }
        });
        if (rowOk)
            $(this).removeClass('error');
        else
            $(this).addClass('error');
        filesOk &= rowOk;
    });
    if (filesOk)
        $('#additional-files-error').hide();
    else
        $('#additional-files-error').show();
    return filesOk;
}

function validateTimePoints() {
    if ($('#IsLongitudinal-no').is(':checked')) return true;

    $('#time-points>div:first>label.error').remove();
    $('#time-points>div:first>input[type=text]').removeClass('error');
    $('#time-points>div').not(':first').each(function () {
        if (0 == $(this).find('input[name$=Name]').val().length) $(this).remove();
    });
    resetTimePointIndices()
    if (1 == $('#time-points>div').length) {
        $('#time-points>div:first>img.add-time-point').show();
        $('#time-points>div:first>img.remove-time-point').hide();
    }
    $('#time-points>div:last>img.add-time-point').show();
    if (0 == $('#time-points>div:first>input[name$=Name]').val().length) {
        $('#time-points>div:first>*:last').after('<label class="error">At least one Time Point must be defined</label>');
        $('#time-points>div:first>input[type=text]').addClass('error');
        return false;
    }
    return true;
}

function setTimePointsStatus() {
    if ($('#IsLongitudinal-yes').is(':checked')) {
        $('#time-points>div:first>input[name$=Name]').removeAttr('disabled').val('Baseline');
        $('#time-points-label').removeClass('disabled');
        $('.add-time-point').click(function () {
            addTimePoint(this);
        });
        $('.remove-time-point').click(function () {
            removeTimePoint(this);
        });
    } else {
        $('#time-points>div').not(':first').remove();
        $('#time-points>div:first>input[name$=Id]').val('0').attr('disabled', 'disabled');
        $('#time-points>div:first>input[name$=Name]').val('').attr('disabled', 'disabled');
        $('#time-points>div:first>img.remove-time-point').hide();
        $('#time-points>div:first>img.add-time-point').show();
        $('#time-points-label').addClass('disabled');
        $('.add-time-point').unbind('click');
        $('.remove-time-point').unbind('click');
    }
}

function addTimePoint(button) {
    var newTp = $(button).parent().clone();
    var newIndex = $('#time-points>div').length;
    newTp.find('input').each(function () {
        var newName = $(this).attr('name').replace(/\[\d+\]/, '[' + newIndex + ']');
        setElementName($(this), newName);
    });
    newTp.find('input[name$=Name]').val('').removeClass('error');
    newTp.find('label.error').remove();
    newTp.find('input[name$=Id]').val('0');
    newTp.find('img.add-time-point').click(function () {
        addTimePoint(this);
    });
    newTp.find('img.remove-time-point').show().click(function () {
        removeTimePoint(this);
    });
    newTp.find('span.help-icon').remove();
    $(button).hide();
    $(button).prev().show();
    $('#time-points').append(newTp);
}

function removeTimePoint(button) {
    if (0 == $('#time-points>div').index($(button).parent())) {
        $(button).parent().next().append($(button).siblings('.help-icon').detach());
    }
    $(button).parent().remove();
    resetTimePointIndices();
    $('#time-points>div:last>img.add-time-point').show();
    if (1 == $('#time-points>div').length) {
        $('#time-points>div:first>img.remove-time-point').hide();
    }
}

function resetTimePointIndices() {
    $('#time-points>div').each(function (index) {
        $(this).find('input').each(function() {
            var newName = $(this).attr('name').replace(/\[\d+\]/, '[' + index + ']');
            setElementName($(this), newName);
        });
    });
}
