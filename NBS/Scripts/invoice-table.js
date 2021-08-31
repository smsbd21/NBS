$(document).ready(function () {

    $(".click").click(function () {

        var prodName = $("#ProdName").val();
        var prodBarcode = $("#Barcode").val();
        var prodExpiry = $("#ExpiryDate").val();
        var prodCarton = $("#Carton").val();
        var prodQuantity = $("#Quantity").val();
        var prodBuyPrice = $("#BuyingPrice").val();

        var prod = "<tr style='margin-left:auto;'><td><input type='checkbox' name='ckProd' /></td><td>" +
            prodName + "</td><td>" + prodBarcode + "</td><td>" + prodExpiry + "</td><td>" +
            prodCarton + "</td><td>" + prodQuantity + "</td><td>" + prodBuyPrice + "</td></tr>";

        $("table .tbody").append(prod);
    })
    $(".del").click(function () {
        $("table .tbody").find('input[name="ckProd"]').each(function () {
            if($(this).is(":checked")) {
                $(this).parents("tr").remove();
            }
        })
    });
});