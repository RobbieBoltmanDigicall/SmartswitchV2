$('#select-list-route-types').on('change', function () {
    $('#hdn-route-type-name').val(this.options[this.selectedIndex].text);
});

$('#select-list-method-types').on('change', function () {
    $('#hdn-method-type-name').val(this.options[this.selectedIndex].text);
});

$('#select-list-application-types').on('change', function () {
    $('#hdn-application-type-name').val(this.options[this.selectedIndex].text);
});

function addHeaderRow() {
    var table = document.getElementById("table-route-headers").getElementsByTagName('tbody')[0];
    var lastIndex = table.rows[table.rows.length - 1].rowIndex;
    var newRow = table.insertRow();

    var cell0 = newRow.insertCell(0);
    var cell1 = newRow.insertCell(1);
    var cell2 = newRow.insertCell(2);
    var cell3 = newRow.insertCell(3);

    cell0.innerHTML = `<input data-val="true" data-val-required="The HeaderKey field is required." id="Route_RouteHeaders_${lastIndex}__HeaderKey" name="Route.RouteHeaders[${lastIndex}].HeaderKey" type="text" value="">`;
    cell1.innerHTML = `<input id="Route_RouteHeaders_${lastIndex}__HeaderValue" name="Route.RouteHeaders[${lastIndex}].HeaderValue" type="text" value="">`;
    cell2.innerHTML = `<select class="form-control" data-val="true" data-val-required="The DataTypeId field is required." id="Route_RouteHeaders_${lastIndex}__DataTypeId" name="Route.RouteHeaders[${lastIndex}].DataTypeId"></select>`;
    cell3.innerHTML = `<button type="button" class="btn btn-danger btn-sm" onclick="removeRow(this)">Remove</button>`;
}

function removeRow(button) {
    var row = button.parentNode.parentNode;
    row.parentNode.removeChild(row);
}