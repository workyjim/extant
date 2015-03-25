function addDiseaseArea(button) {
    var newDa = $(button).parent().clone();
    newDa.find('select').each(function () {
        var newName = $(this).attr('name').replace(/\[\d+\]/, '[' + $('#disease-areas>div').length + ']');
        setElementName($(this), newName);
    });
    newDa.find('select').removeClass('error');
    newDa.find('label.error').remove();
    newDa.find('select>option:first').attr('selected', 'selected');
    newDa.find('img.add-disease-area').click(function () {
        addDiseaseArea(this);
    });
    newDa.find('img.remove-disease-area').show().click(function () {
        removeDiseaseArea(this);
    });
    newDa.find('span.help-icon').remove();
    $(button).hide();
    $(button).prev().show();
    $('#disease-areas').append(newDa);

}

function removeDiseaseArea(button) {
    if (0 == $('#disease-areas>div').index($(button).parent())) {
        $(button).parent().next().append($(button).siblings('.help-icon').detach());
    }
    $(button).parent().remove();
    resetDiseaseAreaIndices();
    $('#disease-areas>div:last>img.add-disease-area').show();
    if (1 == $('#disease-areas>div').length) {
        $('#disease-areas>div:first>img.remove-disease-area').hide();
    }
}

function resetDiseaseAreaIndices() {
    $('#disease-areas>div>select').each(function (index) {
        var newName = $(this).attr('name').replace(/\[\d+\]/, '[' + index + ']');
        setElementName($(this), newName);
    });
}

function validateDiseaseAreas() {
    $('#disease-areas>div:first>label.error').remove();
    $('#disease-areas>div:first>select').removeClass('error');
    $('#disease-areas>div').not(':first').each(function () {
        if (0 == $(this).find('select').val().length) $(this).remove();
    });
    resetDiseaseAreaIndices();
    if (1 == $('#disease-areas>div').length) {
        $('#disease-areas>div:first>img.add-disease-area').show();
        $('#disease-areas>div:first>img.remove-disease-area').hide();
    }
    $('#disease-areas>div:last>img.add-disease-area').show();
    if (0 == $('#disease-areas>div:first>select').val().length) {
        $('#disease-areas>div:first>*:last').after('<label class="error">At least one Disease Area must be selected</label>');
        $('#disease-areas>div:first>select').addClass('error').unbind('change').change(function () {
            if ($(this).val().length > 0) {
                $(this).removeClass("error");
                $(this).siblings('label.error').remove();
            }
        });
        return false;
    }
    return true;
}
