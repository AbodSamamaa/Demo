let tableData;
let rowSelected;

const BASIC_MODEL = {
    idTax: 0,
    percentage: "",
    description: "",
    isFixed: 1,
    isExternal: 1,
    isActive: 1
}


$(document).ready(function () {


    tableData = $("#tbData").DataTable({
        responsive: true,
        "ajax": {
            "url": "/Taxes/GetTaxes",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "idTax",
                "visible": false,
                "searchable": false
            },
           
            { "data": "percentage" },
            { "data": "description" },
            {
                "data": "isFixed", render: function (data) {
                    if (data == 1)
                        return '<span class="badge badge-info">True</span>';
                    else
                        return '<span class="badge badge-danger">False</span>';
                }
            },
            {
                "data": "isExternal", render: function (data) {
                    if (data == 1)
                        return '<span class="badge badge-info">True</span>';
                    else
                        return '<span class="badge badge-danger">False</span>';
                }
            },
            {
                "data": "isActive", render: function (data) {
                    if (data == 1)
                        return '<span class="badge badge-info">True</span>';
                    else
                        return '<span class="badge badge-danger">False</span>';
                }
            },
            {
                "defaultContent": '<button class="btn btn-primary btn-edit btn-sm mr-2"><i class="mdi mdi-pencil"></i></button>' +
                    '<button class="btn btn-danger btn-delete btn-sm"><i class="mdi mdi-trash-can"></i></button>',
                "orderable": false,
                "searchable": false,
                "width": "80px"
            }
        ],
        order: [[0, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Export Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Report Users',
                exportOptions: {
                    columns: [2, 3, 4, 5, 6]
                }
            }, 'pageLength'
        ]
    });
})

const openModal = (model = BASIC_MODEL) => {
    $("#txtId").val(model.idTax);
    $("#txtPercentage").val(model.percentage);
    $("#txtDesc").val(model.description);
    $("#cboFixed").val(model.isFixed);
    $("#cboExternal").val(model.isExternal);
    $("#cboActive").val(model.isActive);

    $("#modalData").modal("show")

}

$("#btnNewTax").on("click", function () {
    openModal()
})

$("#btnSave").on("click", function () {
    const inputs = $("input.input-validate").serializeArray();
    const inputs_without_value = inputs.filter((item) => item.value.trim() == "")

    if (inputs_without_value.length > 0) {
        const msg = `You must complete the field : "${inputs_without_value[0].name}"`;
        toastr.warning(msg, "");
        $(`input[name="${inputs_without_value[0].name}"]`).focus();
        return;
    }

    const model = structuredClone(BASIC_MODEL);
    model["idTax"] = parseInt($("#txtId").val());
    model["percentage"] = $("#txtPercentage").val();
    model["description"] = $("#txtDesc").val();
    model["isFixed"] = $("#cboFixed").val();
    model["isExternal"] = $("#cboExternal").val();
    model["isActive"] = $("#cboActive").val();

    const formData = new FormData();
    formData.append('model', JSON.stringify(model));

    $("#modalData").find("div.modal-content").LoadingOverlay("show")


    if (model.idTax == 0) {
        fetch("/Taxes/CreateTax", {
            method: "POST",
            body: formData
        }).then(response => {
            $("#modalData").find("div.modal-content").LoadingOverlay("hide")
            return response.ok ? response.json() : Promise.reject(response);
        }).then(responseJson => {

            if (responseJson.state) {

                tableData.row.add(responseJson.object).draw(false);
                $("#modalData").modal("hide");
                swal("Successful!", "The user was created", "success");

            } else {
                swal("We're sorry", responseJson.message, "error");
            }
        }).catch((error) => {
            $("#modalData").find("div.modal-content").LoadingOverlay("hide")
        })
    } else {

        fetch("/Taxes/UpdateTax", {
            method: "PUT",
            body: formData
        }).then(response => {
            $("#modalData").find("div.modal-content").LoadingOverlay("hide")
            return response.ok ? response.json() : Promise.reject(response);
        }).then(responseJson => {
            if (responseJson.state) {

                tableData.row(rowSelected).data(responseJson.object).draw(false);
                rowSelected = null;
                $("#modalData").modal("hide");
                swal("Successful!", "The user was modified", "success");

            } else {
                swal("We're sorry", responseJson.message, "error");
            }
        }).catch((error) => {
            $("#modalData").find("div.modal-content").LoadingOverlay("hide")
        })
    }

})

$("#tbData tbody").on("click", ".btn-edit", function () {

    if ($(this).closest('tr').hasClass('child')) {
        rowSelected = $(this).closest('tr').prev();
    } else {
        rowSelected = $(this).closest('tr');
    }

    const data = tableData.row(rowSelected).data();

    openModal(data);
})



$("#tbData tbody").on("click", ".btn-delete", function () {

    let row;

    if ($(this).closest('tr').hasClass('child')) {
        row = $(this).closest('tr').prev();
    } else {
        row = $(this).closest('tr');
    }
    const data = tableData.row(row).data();

    swal({
        title: "¿Are you sure?",
        text: `Delete Tax "${data.description}"`,
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Yes, delete",
        cancelButtonText: "No, cancel",
        closeOnConfirm: false,
        closeOnCancel: true
    },
        function (respuesta) {

            if (respuesta) {

                $(".showSweetAlert").LoadingOverlay("show")

                fetch(`/Taxes/DeleteTax?idTax=${data.idTax}`, {
                    method: "DELETE"
                }).then(response => {
                    $(".showSweetAlert").LoadingOverlay("hide")
                    return response.ok ? response.json() : Promise.reject(response);
                }).then(responseJson => {
                    if (responseJson.state) {

                        tableData.row(row).remove().draw();
                        swal("Successful!", "User was deleted", "success");

                    } else {
                        swal("We're sorry", responseJson.message, "error");
                    }
                })
                    .catch((error) => {
                        $(".showSweetAlert").LoadingOverlay("hide")
                    })
            }
        });
})