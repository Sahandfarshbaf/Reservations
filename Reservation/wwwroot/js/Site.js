let innerhtml = '';

function Payment() {

    let contributor = {


        firstName: $('#first_name').val(),
        lastName: $('#last_name').val(),
        nationalCode: $('#NationalCode').val(),
        mobileNumber: $('#mobile').val(),
        email: $('#email').val(),
    }


    debugger 


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
            debugger 
            console.log(response);


        },
        complete: function () {

        }
    });

}



function GetMeetingTikcetType() {


    let Html = ``;


    jQuery.ajax({
        type: "Get",
        url: "/api/MeetingTicket/GetMeetingTikcetType?meetingId=1",
        data: "",
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            debugger;

            $.each(response, function (i, item) {
                GetMeetingTikcetParams(item.meetingTicketId);
                Html += `<div class="col-sm-3">
                            <div class="package-column" >
                            <h6 class="package-title">${item.meetingTicketType}</h6>
                                <div class="package-price">
                                <span class="currency">میلیون تومان</span>${item.price / 10000000}
                                </div>
                            <div class="package-detail" style="min-height: 300px;">
                            <ul>`;


                Html += innerhtml;
                Html += ` </ul>
                        </div>
                        <a href="/home/Ticketdetails?meetingTicketId=${item.meetingTicketId}" class="btn btn-lg btn-outline-clr">جزئیات و رزرو</a>
                    </div>
                </div>`;
            });



            $('#MeetingTicketTypeDiv').html(Html);

        },
        error: function (response) {

            console.log(response);

        },
        complete: function () {

        }
    });
}

function GetMeetingTikcetParams(meetingTicketId) {





    jQuery.ajax({
        type: "Get",
        url: `/api/MeetingTicket/GetMeetingTikcetParams?meetingTicketId=${meetingTicketId}`,
        data: "",
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {



            innerhtml = `           <li style="direction: rtl; float: right;text-align: justify;"><span class="fa fa-check check-icon"></span>${response[0].description}</li>
                                <li style="direction: rtl; float: right;text-align: justify;"><span class="fa fa-check check-icon"></span>${response[1].description}</li>
                                <li style="direction: rtl; float: right;text-align: justify;"><span class="fa fa-check check-icon"></span>${response[2].description}</li>`;




        },
        error: function (response) {

            console.log(response);

        },
        complete: function () {

        }
    });
}









$(document).ready(() => {

    GetMeetingTikcetType();

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