

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

    $(document.body).on('click', '#BuyTicket', function () {


        Payment();
    });




});