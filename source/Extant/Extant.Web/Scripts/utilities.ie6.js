//see http://stackoverflow.com/questions/2094618/changing-name-attr-of-cloned-input-element-in-jquery-doesnt-work-in-ie6-7
function setElementName(elems, name) {
    $(elems).each(function () {
        this.mergeAttributes(document.createElement("<input name='" + name + "'/>"), false);
    });
}
