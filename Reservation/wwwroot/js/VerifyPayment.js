
let Authority = '';
let Status = '';

var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
};





function VerifyPayment() {


    jQuery.ajax({
        type: "Get",
        url: `/api/BuyTicket/VerifyPayment?Authority=${Authority}&Status=${Status}`,
        data: "",
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "text",
        success: function (response) {
            $('#waitingpayment').hide();
   

            if (response === "error") {
                $('#successpayment').hide();
                $('#failurepayment').show();


            } else {
                $('#successpayment').show();
                $('#failurepayment').hide();


            }

        },
        error: function (response) {

            console.log(response);


        },
        complete: function () {

        }
    });
}




$(document).ready(() => {

    $('#successpayment').hide();
    $('#failurepayment').hide();
    Authority = getUrlParameter('Authority');
    Status = getUrlParameter('Status');
    VerifyPayment();


});