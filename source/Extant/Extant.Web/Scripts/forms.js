function initDatepicker(obj) {
    obj.datepicker(
            {
                changeYear: true,
                yearRange: '-120:+0',
                showOn: 'both',
                buttonImage: '/Images/calendar.png',
                buttonImageOnly: true,
                dateFormat: 'dd-mm-yy',
                onSelect: function () {
                    $(this).closest('form').validate().element(this);
                }
            });
}
