function initDataItems(isNew) {

    if (isNew) {
        initWindowUnload();
    }

    $('#data-item-help-link').fancybox({
        hideOnOverlayClick: false,
        type: 'inline'
    });

    setByTimePointStatus();
    $('table.data-items').show();
    $('#usetimepoints').click(setByTimePointStatus);

    if (0 == $('table.data-items>tbody>tr').length) $('#no-data-fields').show();

    $('#prev-btn').click(function () {
        submitForm('previous');
    });

    $('#next-btn').click(function () {
        submitForm('next');
    });

    $('#save-btn').click(function () {
        submitForm('save');
    });

    $('#cancel-btn').click(function () {
        location.href = '/Study/Update/<%: Model.Id %>';
    });

    $('#select-all-row input').each(function () {
        var tpid = $(this).attr('id').split('-')[2];
        var allChecked = true;
        $('table.data-items>tbody>tr>td>input[value=' + tpid + ']').each(function () {
            allChecked &= $(this).is(':checked');
        });
        $(this).prop('checked', allChecked);
    });

    $('#select-all-row input').click(function () {
        var tpid = $(this).attr('id').split('-')[2];
        $('table.data-items>tbody>tr>td>input[value=' + tpid + ']').prop('checked', $(this).is(':checked'));
    });

    initRemoveDataItemButtons();

    $('#tree').dynatree({
        minExpandLevel: 1,
        keyboard: true,
        checkbox: true,
        selectMode: 3,
        generateIds: true,
        onSelect: function (flag, node) {
            if (node.data.isFolder) {
                node.visit(function (childNode) {
                    if (!childNode.data.isFolder) {
                        selectEquivalentDataItem(childNode, flag);
                        updateListWithTreeDataItem(childNode, flag);
                    }
                });
            } else {
                selectEquivalentDataItem(node, flag);
                updateListWithTreeDataItem(node, flag);
            }
        }
    });

    $("#tree").dynatree("getRoot").visit(function (node) {
        node.expand(true);
    });

    $('#search').keypress(function (event) {
        if (!((event.which != null && event.which == 13) || (event.keyCode != null && event.keyCode == 13)))
            $('#dataitemid').val('0');
    });

    $('#search').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/DataItem/Find',
                dataType: 'json',
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.Value,
                            value: item.Value,
                            id: item.Key
                        }
                    }));
                },
                error: function () {
                    response([]);
                }
            });
        },
        minLength: 3,
        position: { my: "left top", at: "left bottom", collision: "flip" },
        select: function (event, ui) {
            $('#dataitemid').val(ui.item.id);
        },
        open: function (event, ui) {
            $('ul.ui-autocomplete').css('z-index', '200');
        }
    });

    $('#add-dataitem').submit(function () {
        if ($('#search').val().length > 0) {
            var dataItemId = $('#dataitemid').val();
            if (0 == +dataItemId) {
                buildDataItemRow($('#search').val(), +dataItemId);
            } else {
                if (0 == $('#dataitem-' + dataItemId).length) {
                    buildDataItemRow($('#search').val(), +dataItemId);
                    updateTreeFromList(dataItemId, true);
                }
            }
            $('#search').val('');
            $('#dataitemid').val('0');
            $('#search').focus();
            $('#no-data-fields').hide();
        }
        return false;
    });

    $('#help-link').click(function () {
        $('#data-item-help-link').trigger('click');
    });

    if (0 == $('table.data-items>tbody>tr').length) {
        $('#data-item-help-link').trigger('click');
    }
}

function submitForm(action) {
    $('#submit-field').val(action);
    $('table.data-items>tbody>tr').each(function (index) {
        $(this).find('input').each(function () {
            var newName = $(this).attr('name').replace('[x]', '[' + index + ']');
            setElementName($(this), newName);
        });
    });
    cancelWindowUnload();
    $('#data-item-form').submit();
}

function buildDataItemRow(dataItemName, dataItemId) {
    var newDataItemHtml = '<tr';
    if (dataItemId > 0)
        newDataItemHtml += ' id="dataitem-' + dataItemId + '"';
    if (0 == $('table.data-items>tbody>tr').length)
        newDataItemHtml += ' class="first"';
    newDataItemHtml += '>';
    newDataItemHtml += '<td>' + dataItemName + '<input type="hidden" name="DataItems[x].DataItemName" value="' + dataItemName + '" />' +
                               '<input type="hidden" name="DataItems[x].Id" value="' + dataItemId + '" class="dataitemid" /></td>';
    $.each(timepoints, function (index, value) {
        newDataItemHtml += '<td class="tp-check"><input type="checkbox" class="check" value="' + value + '" name="DataItems[x].TimePoints" /></td>';
    });
    newDataItemHtml += '<td class="del-btn"><img src="/Images/delete.png" alt="Remove data field" title="Remove data field" class="link remove-dataitem" /></td></tr>';
    var newDataItem = $(newDataItemHtml);
    if (!$('#usetimepoints').is(':checked')) {
        newDataItem.find('td.tp-check').css('visibility', 'hidden');
        newDataItem.find('td.tp-check input').prop('disabled', 'disabled');
    }
    $('#select-all-row input:checked').each(function () {
        var tpid = $(this).attr('id').split('-')[2];
        newDataItem.find('input.check[value=' + tpid + ']').prop('checked', true);
    });
    newDataItem.find('img.remove-dataitem').click(function () { removeDataItem(this); });
    $('table.data-items>tbody').append(newDataItem);
}

function initRemoveDataItemButtons() {
    $('table.data-items img.remove-dataitem').unbind('click');
    $('table.data-items img.remove-dataitem').click(function () {
        removeDataItem(this);
    });
}

function removeDataItem(button) {
    var item = $(button).closest('tr');
    if (null != item.attr('id')) {
        var dataItemId = item.attr('id').split('-')[1];
        updateTreeFromList(dataItemId, false);
    }
    item.remove();
    $('table.data-items>tbody>tr:first').addClass('first');
    if (0 == $('table.data-items>tbody>tr').length) $('#no-data-fields').show();
};

function getDataItemId(nodeId) {
    return nodeId.split('-')[2];
}

function selectEquivalentDataItem(node, flag) {
    var dataItemId = getDataItemId(node.data.key);
    $("#tree").dynatree("getRoot").visit(function (node) {
        if (dataItemId == getDataItemId(node.data.key)) {
            node.select(flag);
        }
    });
}

function updateListWithTreeDataItem(node, flag) {
    var dataItemId = getDataItemId(node.data.key);
    if (flag) {
        if (0 == $('table.data-items>tbody>tr#dataitem-' + dataItemId).length) {
            buildDataItemRow(node.data.tooltip, +dataItemId);
            $('#no-data-fields').hide();
        }
    } else {
        $('#dataitem-' + dataItemId).remove();
        if (0 == $('table.data-items>tbody>tr').length) $('#no-data-fields').show();
    }
}

function updateTreeFromList(dataItemId, flag) {
    $("#tree").dynatree("getTree").visit(function (node) {
        if (!node.data.isFolder && dataItemId == getDataItemId(node.data.key))
            node.select(flag);
    });
}

function setByTimePointStatus() {
    if ($('#usetimepoints').is(':checked')) {
        $('#time-point-row').show();
        $('#select-all-row').show();
        $('table.data-items td.tp-check').css('visibility', '');
        $('table.data-items td.tp-check input').removeAttr('disabled');
    } else {
        $('#time-point-row').hide();
        $('#select-all-row').hide();
        $('table.data-items td.tp-check').css('visibility', 'hidden');
        $('table.data-items td.tp-check input').prop('disabled', 'disabled');
    }
}
