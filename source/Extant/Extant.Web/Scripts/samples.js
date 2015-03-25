var sampleTypes = {};
sampleTypes.dna = 1;
sampleTypes.serum = 2;
sampleTypes.plasma = 3;
sampleTypes.wholeblood = 4;
sampleTypes.saliva = 5;
sampleTypes.tissue = 6;
sampleTypes.cell = 7;
sampleTypes.other = 8;

var ExactValue = "8";
var NoneValue = "0";

function initSamples(studyId) {

    $('#prev-btn').click(function () {
        $('#action').val('previous');
    });

    $('#finish-btn').click(function () {
        $('#action').val('finish');
    });

    $('#publish-btn').click(function () {
        $('#action').val('publish');
    });

    $('#cancel-btn').click(function () {
        location.href = '/Study/Update/'+studyId;
        return false;
    });

    $('#samples-basic .please-specify').each(function () {
        if (ExactValue == $(this).prev().val()) {
            $(this).show();
            if (0 == $(this).val().length) {
                $(this).val('Please specify').addClass('unaccessed').focus(function () {
                    $(this).val('');
                    $(this).removeClass('unaccessed');
                    $(this).unbind('focus');
                });
            }
            $(this).blur(function () {
                var exactValue = $(this).val();
                if ($('#DetailedSampleInfo').is(':checked')) {
                    $('#samples-form>div.window').each(function () {
                        if ($(this).find('div.window-title>span').text() == sampleType) {
                            //update number of samples exact
                            $(this).find('input[name$=NumberOfSamplesExact]').val(exactValue);
                        }
                    });
                }
            });
        } else {
            $(this).attr('disabled', 'disabled');
        }
    });

    $('#samples-basic select').change(function () {
        var numberOfSamples = $(this).val();
        if (numberOfSamples == ExactValue) {
            $(this).next().removeAttr('disabled').show().val('Please specify').addClass('unaccessed')
                .focus(function () {
                    $(this).val('');
                    $(this).removeClass('unaccessed');
                    $(this).unbind('focus');
                })
                .blur(function () {
                    var exactValue = $(this).val();
                    if ($('#DetailedSampleInfo').is(':checked')) {
                        $('#samples-form>div.window').each(function () {
                            if ($(this).find('div.window-title>span').text() == sampleType) {
                                //update number of samples exact
                                $(this).find('input[name$=NumberOfSamplesExact]').val(exactValue);
                            }
                        });
                    }
                });
        } else {
            $(this).next().hide().attr('disabled', 'disabled').val('').removeClass('unaccessed').unbind('focus');
        }

        var sampleType = $(this).prev().text();
        if ($('#DetailedSampleInfo').is(':checked')) {
            if (numberOfSamples == NoneValue) {
                $('#samples-form>div.window').each(function () {
                    if ($(this).find('div.window-title>span').text() == sampleType) {
                        $(this).remove();
                        resetSampleIndices();
                    }
                });
            } else {
                var present = false;
                $('#samples-form>div.window').each(function () {
                    if ($(this).find('div.window-title>span').text() == sampleType) {
                        //update number of samples
                        $(this).find('input[name$=NumberOfSamples]').val(numberOfSamples);
                        if (numberOfSamples != ExactValue) $(this).find('input[name$=NumberOfSamplesExact]').val('');
                        present = true;
                    }
                });
                if (!present) {
                    var samplesCount = $('#samples-form>div.window').filter(function () {
                        return $(this).find('input').length > 0;
                    }).length;
                    var newSample = createSampleForm(samplesCount, sampleType, findSampleCountSelect(sampleType));
                    $('#buttons').before(newSample);
                }
            }
        }
    });

    $('#DetailedSampleInfo').click(function () {
        if ($(this).is(':checked')) {
            $('#samples-basic select').each(function () {
                if ($(this).val() != NoneValue) {
                    var sampleType = $(this).prev().text();
                    var samplesCount = $('#samples-form>div.window').length;
                    var newSample = createSampleForm(samplesCount, sampleType, this);
                    $('#buttons').before(newSample);
                }
            });
        }
        else {
            $('#samples-form div.window').remove();
        }
    });

    $('#samples-form').validate({
        submitHandler: function (form) {
            cancelWindowUnload();
            form.submit();
        }
    });

    $('#samples-form>div.window').filter(function () {
        return $(this).find('input').length > 0;
    }).each(function () {
        initSampleForm($(this));
    });

    $('#samples-form>div.window').filter(function () {
        return $(this).find('input').length == 0;
    }).each(function () {
        initChangeMind($(this));
    });
}

function createSampleForm(sampleIndex, sampleType, sampleCountSelect) {
    var newSample = $('#sample-form-template>div.window').clone();
    newSample.find('[name^=Samples]').each(function (index, input) {
        var newName = $(this).attr('name').replace('[x]', '[' + sampleIndex + ']');
        setElementName($(this), newName);
    });
    newSample.find('[id^="Samples_x_"]').each(function (index, item) {
        $(this).attr('id', $(this).attr('id').replace('_x_', '_' + sampleIndex + '_'));
    });
    newSample.find('[for^="Samples_x_"]').each(function (index, item) {
        $(this).attr('for', $(this).attr('for').replace('_x_', '_' + sampleIndex + '_'));
    });

    newSample.find('div.window-title>span').text(sampleType);
    newSample.find('span.sampleType').text('Other' == sampleType ? '' : sampleType);
    newSample.find('input[name$=NumberOfSamples]').val($(sampleCountSelect).val());
    newSample.find('input[name$=NumberOfSamplesExact]').val($(sampleCountSelect).siblings('[name$=Exact]:enabled').val());

    initSampleForm(newSample);

    return newSample;
}

function initSampleForm(sampleForm) {
    var sampleClass = sampleForm.find("div.window-title>span").text().toLowerCase().replace(' ', '');
    sampleForm.find('input[name$=SampleType]').val(sampleTypes[sampleClass]);
    sampleForm.find('div.window-body div,fieldset').filter('.all,.' + sampleClass).show();
    sampleForm.find('div.window-body div,fieldset').not('.all,.' + sampleClass).hide();

    var volumeRange = sampleForm.find('select[name$=SampleVolume]');
    volumeRange.children('option').each(function () {
        var optValue = +$(this).attr('value');
        if (("wholeblood" == sampleClass && optValue >= 1 && optValue <= 8) ||
                    ("wholeblood" != sampleClass && optValue >= 9 && optValue <= 12)) {
            $(this).remove();
        }
    });

    sampleForm.find('select').each(function () {
        if ($(this).find('option.specify').length > 0) {
            var specifyValue = $(this).find('option.specify:first').attr('value');
            selectPleaseSpecify(this, specifyValue);
            $(this).change(function () {
                selectPleaseSpecify(this, specifyValue);
            });
        }
    });

    sampleForm.find('input.specify[type="checkbox"]').each(function () {
        checkboxPleaseSpecify(this);
        $(this).click(function () {
            checkboxPleaseSpecify(this);
        });
    });

    sampleForm.find('div.window-title>img').click(function () {
        $(this).remove();
        sampleForm.find('div.window-body').empty().append(
                    '<p><em>I do not want to provide detailed information on this sample type. <span class="link change-mind">Click here</span> to change your mind.</em></p>');
        resetSampleIndices();
        initChangeMind(sampleForm);
    });
}

function checkboxPleaseSpecify(checkbox) {
    if ($(checkbox).is(':checked')) {
        $(checkbox).parent().find('input[type="text"]').removeAttr('disabled');
    } else {
        $(checkbox).parent().find('input[type="text"]').attr('disabled', 'disabled').val('').removeClass("error").next('label').remove();
    }
}

function selectPleaseSpecify(select, specifyValue) {
    var specifyRow = $(select).parent().next();
    if ($(select).val() == specifyValue) {
        specifyRow.find("label").removeClass('disabled');
        specifyRow.find("input").removeAttr('disabled');
    } else {
        specifyRow.find("label").addClass('disabled');
        specifyRow.find("input").attr('disabled', 'disabled').val('').removeClass("error").next('label').remove();
    }
}

function initChangeMind(sampleForm) {
    sampleForm.find('div.window-body span.change-mind').click(function () {
        var sampleIndex = $('#samples-form>div.window').index(sampleForm);
        var sampleType = sampleForm.find('div.window-title>span').text();
        var newSampleForm = createSampleForm(sampleIndex, sampleType, findSampleCountSelect(sampleType));
        sampleForm.replaceWith(newSampleForm);
        resetSampleIndices();
    });
}

function resetSampleIndices() {
    var sampleIndex = 0;
    $('#samples-form>div.window').each(function () {
        if ($(this).find('input').length > 0) {
            $(this).find('[name^=Samples]').each(function () {
                var newName = $(this).attr('name').replace(/\[\d\]/, '[' + sampleIndex + ']');
                setElementName($(this), newName);
            });
            $(this).find('[id^="Samples_x_"]').each(function () {
                $(this).attr('id', $(this).attr('id').replace(/_\d_/, '_' + sampleIndex + '_'));
            });
            $(this).find('[for^="Samples_x_"]').each(function () {
                $(this).attr('for', $(this).attr('for').replace(/_\d_/, '_' + sampleIndex + '_'));
            });

            sampleIndex++;
        }
    });

}

function findSampleCountSelect(sampleType) {
    var select;
    $('#samples-basic>div.form-row>label').each(function () {
        if (sampleType == $(this).text()) {
            select = $(this).next();
        }
    });
    return select;
}