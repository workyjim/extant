//Custom validation methods for jquery.validate plugin
$.validator.addMethod(
    "required-unaccessed",
    function (value, element) {
        return this.getLength(value, element) > 0 && !$(element).hasClass('unaccessed');
    },
    "This field is required.");

$.validator.addMethod(
    "integer",
    function (value, element) {
        return this.optional(element) ||
            /^-?\d+$/.test(value);
    },
    "Please enter a valid integer.");