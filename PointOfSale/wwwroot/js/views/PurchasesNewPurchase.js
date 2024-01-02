let TaxValue = 16;
let ProductsForPurchase = [];
let SelectedTax = 0;
let qty = 0;
let up = 0;
let categoryID = 0;
let categ = "";
let desccat = "";

$(document).ready(function () {

    fetch("/Purchases/ListTypeDocumentPurchase")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        }).then(responseJson => {

            //borrar los options de cboTipoDocumentoVenta
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    $("#cboTypeDocumentPurchase").append(
                        $("<option>").val(item.idTypeDocumentPurchase).text(item.description)
                    )
                });
            }
        })

    fetch("/Taxes/GetTaxesForProducts")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        }).then(responseJson => {
            if (responseJson.data.length > 0) {

                responseJson.data.forEach((item) => {
                    $("#cboTax").append(
                        $("<option>").val(item.idTax).text(item.description)
                    )
                    var cboTaxselectElement = document.getElementById("cboTax");
                    var ddlselectedText = cboTaxselectElement.options[cboTaxselectElement.selectedIndex].text;
                    if (ddlselectedText === "Fixed") {
                        SelectedTax = 16;
                    }
                    else {
                        SelectedTax = ddlselectedText;
                    }
                });
            }
        })
    fetch("/Payment/GetActivePaymentes")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        }).then(responseJson => {
            if (responseJson.data.length > 0) {

                responseJson.data.forEach((item) => {
                    $("#cboPaymentMethod").append(
                        $("<option>").val(item.idPayment).text(item.paymentMethod)
                    )
                    
                });
            }
        })
    document.getElementById("cboTax").onchange = function () {
        fetch("/Taxes/GetTaxesForProducts")
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response);
            }).then(responseJson => {
                if (responseJson.data.length > 0) {
                    responseJson.data.forEach((item) => {

                        var cboTaxselectElement = document.getElementById("cboTax");
                        var ddlselectedText = cboTaxselectElement.options[cboTaxselectElement.selectedIndex].text;
                        if (ddlselectedText === "Fixed") {
                            SelectedTax = 16;
                        }
                        else {
                            SelectedTax = ddlselectedText;
                        }
                    });

                }
            })
    };
    document.getElementById("txtDiscount").onchange = function () {
        showProducts_Prices();
    };

    //fetch("/Negocio/Obtener")
    //    .then(response => {
    //        return response.ok ? response.json() : Promise.reject(response);
    //    }).then(responseJson => {
    //        if (responseJson.estado) {

    //            const d = responseJson.objeto;

    //            $("#inputGroupSubTotal").text(`Sub Total - ${d.simboloMoneda}`)
    //            $("#inputGroupIGV").text(`IGV(${d.porcentajeImpuesto}%) - ${d.simboloMoneda}`)
    //            $("#inputGroupTotal").text(`Total - ${d.simboloMoneda}`)

    //            TaxValue = parseFloat(d.porcentajeImpuesto)

    //        } else {
    //            $("#inputGroupIGV").text(`IGV(0}%)`)
    //            TaxValue = 0
    //        }
    //    })

    $("#cboSearchProduct").select2({
        ajax: {
            url: "/Purchases/GetCategory",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            delay: 250,
            data: function (params) {
                return {
                    search: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: data.map((item) => (
                        {
                            id: item.idCategory,
                            text: item.description,
                            brand: item.description,
                            price: parseFloat(item.price),
                            quantity: item.quantity
                        }
                    ))
                };
            }
        },
        placeholder: 'Search product...',
        minimumInputLength: 1,
        templateResult: formatResults
    });


})

function formatResults(data) {

    if (data.loading)
        return data.text;

    var container = $(
        `<table width="100%">
            <tr>
                <td>
                    <p style="margin:2px">${data.text}</p>
                </td>
            </tr>
         </table>`
    );

    return container;
}


$(document).on('select2:open', () => {
    document.querySelector('.select2-search__field').focus();
});

$('#cboSearchProduct').on('select2:select', function (e) {
    var data = e.params.data;
    let product_found = ProductsForPurchase.filter(prod => prod.idProduct == data.id)
    categoryID = data.id;
    if (product_found.length > 0) {
        $("#cboSearchProduct").val("").trigger('change');
        toastr.warning("", "The Category has already been added");
        return false;
    }

    swal({
        title: data.brand,
        text: data.text,
        type: "input",
        showCancelButton: true,
        closeOnConfirm: false,
        inputPlaceholder: "Enter quantity",
        input: "number",
        inputAttributes: {
            step: "any",
            min: 0,
        },
    }, function (quantity) {
        if (quantity === null) return false;
        
        if (quantity <= 0 || isNaN(parseFloat(quantity))) {
            toastr.warning("", "Please enter a valid quantity");
            return false;
        }
        qty = quantity;
        swal({
            title: data.brand,
            text: data.text,
            type: "input",
            showCancelButton: true,
            closeOnConfirm: false,
            inputPlaceholder: "Enter unit price",
            input: "number",
            inputAttributes: {
                step: "any",
                min: 0,
            },
        }, function (unitPrice) {
            if (unitPrice === null) return false;

            if (unitPrice <= 0 || isNaN(parseFloat(unitPrice))) {
                toastr.warning("", "Please enter a valid unit price");
                return false;
            }
            up = unitPrice;
            let total = parseFloat(quantity) * parseFloat(unitPrice);

            let product = {
                idProduct: data.id,
                brandProduct: data.brand,
                descriptionProduct: data.text,
                categoryProducty: data.category,
                quantity: parseFloat(quantity),
                price: parseFloat(unitPrice).toString(),
                total: total.toString(),
            }

            ProductsForPurchase.push(product);
            categ = product.brandProduct;
            desccat = product.descriptionProduct;

            showProducts_Prices();

            $("#cboSearchProduct").val("").trigger('change');
            swal.close();
        });
    });
});

function showProducts_Prices() {

    let total = 0;
    let discount = $("#txtDiscount").val();
    let discountStr = parseFloat(discount).toFixed(2);
    let Ftotal = 0;
    let FtotalwithTax = 0;
    let tax = 0;
    let subtotal = 0;
    let percentage = SelectedTax / 100;
    if (isNaN(parseInt(discountStr))) {
        toastr.warning("", "You must enter a valid value");
        discountStr = 0;
        $("#txtDiscount").val(0);
        return false
    }
    if (discountStr < 0) {
        toastr.warning("", "You must enter a valid value");
        return false;
    }
    $("#tbProduct tbody").html("")

    ProductsForPurchase.forEach((item) => {

        total = total + parseFloat(item.total);

        $("#tbProduct tbody").append(
            $("<tr>").append(
                $("<td>").append(
                    $("<button>").addClass("btn btn-danger btn-delete btn-sm").append(
                        $("<i>").addClass("mdi mdi-trash-can")
                    ).data("idProduct", item.idProduct)
                ),
                $("<td>").text(item.descriptionProduct),
                $("<td>").text(item.quantity),
                $("<td>").text(item.price),
                $("<td>").text(item.total)
            )
        )
    })

    /*subtotal = total / (percentage);*/
    subtotal = total;
    tax = subtotal * percentage;
    Ftotal = subtotal + tax;
    FtotalwithTax = subtotal + tax - discountStr;
    var totl = $("#txtTotal").val();
    if (parseFloat(discountStr) > parseFloat(totl)) {
        toastr.warning("", "The discount cannot be higher than the total");

    }
    $("#txtSubTotal").val(subtotal.toFixed(2))
    $("#txtTotalTaxes").val(tax.toFixed(2))
    $("#txtTotal").val(FtotalwithTax.toFixed(2))
}

$(document).on("click", "button.btn-delete", function () {
    const _idproduct = $(this).data("idProduct")

    ProductsForPurchase = ProductsForPurchase.filter(p => p.idProduct != _idproduct)

    showProducts_Prices()
})

$("#btnFinalizePurchase").click(function () {
    let discount = $("#txtDiscount").val();
    let discountStr = parseFloat(discount).toFixed(2);
    var Total = $("#txtTotal").val();
    if (isNaN(parseInt(discountStr))) {
        toastr.warning("", "You must enter a valid value");
        return false
    }
    if (discountStr < 0) {
        toastr.warning("", "You must enter a valid value");
        discountStr = 0;
        $("#txtDiscount").val(0);
        return false;
    }
    if (ProductsForPurchase.length < 1) {
        toastr.warning("", "You must enter Category");
        return;
    }
    if (parseFloat(discountStr) > parseFloat(Total)) {
        toastr.warning("", "The discount cannot be higher than the total");
        return;
    }

    const vmDetailPurchase = ProductsForPurchase;

    const purchase = {
        idTypeDocumentPurchase: $("#cboTypeDocumentPurchase").val(),
        sellerDocument: $("#txtSellerDocument").val(),
        sellerName: $("#txtSellerName").val(),
        phoneNo: $("#txtPhoneNo").val(),
        address: $("#txtAddress").val(),
        IdPaymentMethod: $("#cboPaymentMethod").val(),
        note: $("#txtNote").val(),
        title: $("#txtTitle").val(),
        barCode: $("#txtBarCode").val(),
        description: $("#txtDescription").val(),
        subtotal: $("#txtSubTotal").val(),
        totalTaxes: $("#txtTotalTaxes").val(),
        total: $("#txtTotal").val(),
        quantity: qty,
        unitPrice: up,
        discount: $("#txtDiscount").val(),
        DetailPurchases: vmDetailPurchase,
        IdCategory: categoryID,
        Category: categ
    }

    $("#btnFinalizePurchase").closest("div.card-body").LoadingOverlay("show")

    fetch("/Purchases/RegisterPurchase", {
        method: "POST",
        headers: { 'Content-Type': 'application/json;charset=utf-8' },
        body: JSON.stringify(purchase)
    }).then(response => {

        $("#btnFinalizePurchase").closest("div.card-body").LoadingOverlay("hide")
        return response.ok ? response.json() : Promise.reject(response);
    }).then(responseJson => {

        if (responseJson.state) {

            ProductsForPurchase = [];
            showProducts_Prices();
            $("#txtSellerDocument").val("");
            $("#txtSellerName").val("");
            $("#cboTypeDocumentPurchase").val($("#cboTypeDocumentPurchase option:first").val());

            swal("Registered!", `Purchase Number : ${responseJson.object.purchaseNumber}`, "success");
            $("#txtDiscount").val(0.00);
            $("#txtTotal").val(0.00);

        } else {
            swal("We're sorry", "The Purchase could not be registered", "error");
        }
    }).catch((error) => {
        $("#btnFinalizePurchase").closest("div.card-body").LoadingOverlay("hide")
    })


})