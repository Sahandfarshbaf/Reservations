
let meetingTicketId = 0;

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




function GetMeetingTikcetParams() {


    let html = ``;

    jQuery.ajax({
        type: "Get",
        url: `/api/MeetingTicket/GetMeetingTikcetParams?meetingTicketId=${meetingTicketId}`,
        data: "",
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            $.each(response, function (i, item) {

                html +=
                    `       <li ><span class="fa fa-check check-icon"></span>${item.description}</li><br/>`;


            });
            $('#DivDetailes').html(html);

        },
        error: function (response) {

            console.log(response);

        },
        complete: function () {

        }
    });
}


function GetMeetingTikcetTypeById() {




    jQuery.ajax({
        type: "Get",
        url: `/api/MeetingTicket/GetMeetingTikcetTypeById?meetingTicketId=${meetingTicketId}`,
        data: "",
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {


            $("#typeName").html(response.meetingTicketType);
            $("#price").html(response.price / 10000);
            $("#Cmbtikcet").html(`<option style="float: right" value="${meetingTicketId}">${response.meetingTicketType}</option>`)


        },
        error: function (response) {

            console.log(response);

        },
        complete: function () {

        }
    });
}

function Payment() {

    let contributor = {


        firstName: $('#first_name').val(),
        lastName: $('#last_name').val(),
        nationalCode: $('#NationalCode').val(),
        mobileNumber: $('#mobile').val(),
        email: $('#email').val(),
    }





    jQuery.ajax({
        type: "Post",
        url: `/api/BuyTicket/BuyTicketInsert?ticketTypeId=${$("#Cmbtikcet").val()}`,
        data: JSON.stringify(contributor),
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "text",
        success: function (response) {

            if (response != "") {
                window.location = response;
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


    meetingTicketId = getUrlParameter('meetingTicketId');

    GetMeetingTikcetParams();
    GetMeetingTikcetTypeById();

    $(document.body).on('click', '#BuyTicket', function () {

        if ($("#first_name").val().length == 0) {
            toastr.error('نام خود را وارد نمایید');
        } else
        if ($("#last_name").val().length == 0) {
            toastr.error('نام خانوادگی خود را وارد نمایید');
        } else
        if ($("#NationalCode").val().length < 10) {
            toastr.error('کدملی خود را وارد نمایید');
        }
        else
        if ($("#mobile").val().length == 0) {
            toastr.error('موبایل خود را وارد نمایید');
        } else
        if ($("#email").val().length < 10) {
            toastr.error('ایمیل خود را وارد نمایید');
        }
        else {
            Command: toastr["info"]("درحال انتقال به درگاه پرداخت")

            toastr.options = {
                "closeButton": false,
                "debug": false,
                "newestOnTop": false,
                "progressBar": true,
                "positionClass": "toast-top-right",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": "500",
                "hideDuration": "1000",
                "timeOut": "5000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }
            Payment();
        }

    });


});