﻿$('#select-list-route-types').on('change', function () {
    $('#hdn-route-type-name').val(this.options[this.selectedIndex].text);
});

$('#select-list-method-types').on('change', function () {
    $('#hdn-method-type-name').val(this.options[this.selectedIndex].text);
});

$('#select-list-application-types').on('change', function () {
    $('#hdn-application-type-name').val(this.options[this.selectedIndex].text);
});

function addHeaderRow() {
    var routeId = document.getElementById("Route_RouteId").value;
    var table = document.getElementById("table-route-headers").getElementsByTagName('tbody')[0];
    var latestRow = table.rows[table.rows.length - 1];
    if (latestRow == null) {
        lastIndex = 0;
    }
    else {
        var lastIndex = latestRow.rowIndex;
    }

    var selectOptions = $('.select-data-type')[0].innerHTML;

    var newRow = table.insertRow();
    newRow.id = `header-${lastIndex}`
    newRow.innerHTML = `
        <input data-val="true" data-val-required="The RouteHeaderId field is required." id="Route_RouteHeaders_${lastIndex}__RouteHeaderId" name="Route.RouteHeaders[${lastIndex}].RouteHeaderId" type="hidden" value="0">
        <input data-val="true" data-val-required="The RouteId field is required." id="Route_RouteHeaders_${lastIndex}__RouteId" name="Route.RouteHeaders[${lastIndex}].RouteId" type="hidden" value="${routeId}">
        <td><input data-val="true" data-val-required="The HeaderKey field is required." id="Route_RouteHeaders_${lastIndex}__HeaderKey" name="Route.RouteHeaders[${lastIndex}].HeaderKey" required="required" type="text" value=""></td>
        <td><input id="Route_RouteHeaders_${lastIndex}__HeaderValue" name="Route.RouteHeaders[${lastIndex}].HeaderValue" type="text" value=""></td>
        <td>
            <select class="form-control select-data-type" data-val="true" data-val-required="The DataTypeId field is required." id="Route_RouteHeaders_${lastIndex}__DataTypeId" name="Route.RouteHeaders[${lastIndex}].DataTypeId">
                ${selectOptions}
            </select>
        </td>
        <td><button type="button" class="btn btn-danger btn-sm" onclick="removeRow(this)">Remove</button></td>
    `;
}

function addParamRow() {
    var table = document.getElementById("table-route-params").getElementsByTagName('tbody')[0];
    var latestRow = table.rows[table.rows.length - 1];
    if (latestRow == null) {
        lastIndex = 0;
    }
    else {
        var lastIndex = latestRow.rowIndex;
    }

    var routeId = document.getElementById("Route_RouteId").value; // Assuming you have a RouteId input field in your form

    var selectOptions = $('.select-data-type')[0].innerHTML;

    var newRow = table.insertRow();
    newRow.classList.add('align-middle');
    newRow.id = `header-${lastIndex}`;

    newRow.innerHTML = `
        <input data-val="true" data-val-required="The RouteParameterId field is required." id="Route_RouteParameters_${lastIndex}__RouteParameterId" name="Route.RouteParameters[${lastIndex}].RouteParameterId" type="hidden" value="0">
        <input data-val="true" data-val-required="The RouteId field is required." id="Route_RouteParameters_${lastIndex}__RouteId" name="Route.RouteParameters[${lastIndex}].RouteId" type="hidden" value="${routeId}">
        <td><input data-val="true" data-val-required="The ParameterKey field is required." id="Route_RouteParameters_${lastIndex}__ParameterKey" name="Route.RouteParameters[${lastIndex}].ParameterKey" required="required" type="text" value=""></td>
        <td><input id="Route_RouteParameters_${lastIndex}__ParameterValue" name="Route.RouteParameters[${lastIndex}].ParameterValue" type="text" value=""></td>
        <td>
            <select class="form-control select-data-type" data-val="true" data-val-required="The DataTypeId field is required." id="Route_RouteParameters_${lastIndex}__DataTypeId" name="Route.RouteParameters[${lastIndex}].DataTypeId">
                ${selectOptions}
            </select>
        </td>
        <td><button type="button" class="btn btn-danger btn-sm" onclick="removeRow(this)">Remove</button></td>
    `;
}

function addBodyParamRow() {
    var routeBodyId = document.getElementById("Route_RouteBody_RouteBodyId").value;
    var table = document.getElementById("table-body-params").getElementsByTagName('tbody')[0];
    var latestRow = table.rows[table.rows.length - 1];
    var lastIndex = latestRow ? latestRow.rowIndex : 0;

    var selectOptions = $('.select-data-type')[0].innerHTML;

    var newRow = table.insertRow();
    newRow.classList.add('align-middle');

    newRow.innerHTML = `
        <input data-val="true" data-val-required="The RouteBodyId field is required." id="Route_RouteBody_RouteBodyParameters_${lastIndex}__RouteBodyId" name="Route.RouteBody.RouteBodyParameters[${lastIndex}].RouteBodyId" type="hidden" value="${routeBodyId}">
        <input data-val="true" data-val-required="The RouteBodyParameterId field is required." id="Route_RouteBody_RouteBodyParameters_${lastIndex}__RouteBodyParameterId" name="Route.RouteBody.RouteBodyParameters[${lastIndex}].RouteBodyParameterId" type="hidden" value="0">
        <td><input data-val="true" data-val-required="The BodyKey field is required." id="Route_RouteBody_RouteBodyParameters_${lastIndex}__BodyKey" name="Route.RouteBody.RouteBodyParameters[${lastIndex}].BodyKey" type="text" value=""></td>
        <td><input id="Route_RouteBody_RouteBodyParameters_${lastIndex}__BodyValue" name="Route.RouteBody.RouteBodyParameters[${lastIndex}].BodyValue" type="text" value=""></td>
        <td>
            <select class="form-control select-data-type" data-val="true" data-val-required="The DataTypeId field is required." id="Route_RouteBody_RouteBodyParameters_${lastIndex}__DataTypeId" name="Route.RouteBody.RouteBodyParameters[${lastIndex}].DataTypeId">
                ${selectOptions}
            </select>
        </td>
        <td><button type="button" class="btn btn-danger btn-sm" onclick="removeRow(this)">Remove</button></td>
    `;
}

//function addBodyParamRow() {
//    var routeBodyId = document.getElementById("Route_RouteBody_RouteBodyId").value;
//    var table = document.getElementById("table-body-params").getElementsByTagName('tbody')[0];
//    var latestRow = table.rows[table.rows.length - 1];
//    if (latestRow == null) {
//        lastIndex = 0;
//    }
//    else {
//        var lastIndex = latestRow.rowIndex;
//    }

//    var selectOptions = $('.select-data-type')[0].innerHTML;

//    var newRow = table.insertRow();
//    newRow.classList.add('align-middle');

//    var cell0 = newRow.insertCell(0);
//    var cell1 = newRow.insertCell(1);
//    var cell2 = newRow.insertCell(2);
//    var cell3 = newRow.insertCell(3);
//    var cell4 = newRow.insertCell(4);
//    var cell5 = newRow.insertCell(5);

    
//    cell0.innerHTML = `<input data-val="true" data-val-required="The RouteBodyId field is required." id="Route_RouteBody_RouteBodyParameters_${lastIndex}__RouteBodyId" name="Route.RouteBody.RouteBodyParameters[${lastIndex}].RouteBodyId" type="hidden" value="${routeBodyId}">`;
//    cell1.innerHTML = `<input data-val="true" data-val-required="The RouteBodyParameterId field is required." id="Route_RouteBody_RouteBodyParameters_${lastIndex}__RouteBodyParameterId" name="Route.RouteBody.RouteBodyParameters[${lastIndex}].RouteBodyParameterId" type="hidden" value="${routeBodyId}">`;
//    cell2.innerHTML = `<input data-val="true" data-val-required="The BodyKey field is required." id="Route_RouteBody_RouteBodyParameters_${lastIndex}__BodyKey" name="Route.RouteBody.RouteBodyParameters[${lastIndex}].BodyKey" type="text" value="">`;
//    cell3.innerHTML = `<input id="Route_RouteBody_RouteBodyParameters_1__BodyValue" name="Route.RouteBody.RouteBodyParameters[${lastIndex}].BodyValue" type="text" value="">`;
//    cell4.innerHTML = `<select class="form-control select-data-type" data-val="true" data-val-required="The DataTypeId field is required." id="Route_RouteBody_RouteBodyParameters_${lastIndex}__DataTypeId">${selectOptions}</select>`;
//    cell5.innerHTML = `<button type="button" class="btn btn-danger btn-sm" onclick="removeRow(this)">Remove</button>`;
//}

function removeRow(button) {
    var row = button.parentNode.parentNode;
    row.parentNode.removeChild(row);
}

function retrieveDataTypeOptions() {

}