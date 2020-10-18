let NCode = "";
let Mobile = "";


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


function GetTicketLists() {

    let html = ``;
    $.ajax({
        type: "Get",
        url: `/api/BuyTicket/GetTicketLists?NationalCode=${NCode}&MobileNo=${Mobile}`,
        data: "",
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            jQuery.each(response, function (i, item) {
                let tikcet = '------';
                let status = 'پرداخت نشده';
                let pay = 0;

                if (item.finalStatusId === 100 || item.finalStatusId === 101) {

                    status = "پرداخت شده";
                    tikcet = item.ticketNo;
                    pay = 1;
                }


                html += ` <tr>
                            <td>${i + 1}</td>
                            <td>${item.meetingName}</td>
                            <td>${item.meetingTicketType}</td>
                            <td>${item.amount}</td>
                            <td>${status}</td>
                            <td>${tikcet}</td>`;
                if (pay === 0) {
                    html += ` <td><input type="button" class="btn btn-md btn-sm payment" value="پرداخت"  contributorPaymentId=${item.contributorPaymentId} /></td>
                          </tr>`;

                }
                else {

                    html += ` <td></td>
                          </tr>`;

                }




            });

            $("#TicketListTable").html(html);
        },
        error: function (response) {

            console.log(response);


        },
        complete: function () {

        }
    });
}

const BeginTransaction = (aa) => {

    $.ajax({
        type: "Get",
        url: `/api/BuyTicket/BeginTransaction?contributorPaymentId=${aa}`,
        data: "",
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "text",
        success: function (response) {

            if (response !== "") {
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

    $(".navigation-container").hide();

    NCode = getUrlParameter('Ncode');
    Mobile = getUrlParameter('Mobile');
    if (NCode !== undefined && NCode !== undefined) {
        $("#txtNationalCode").val(NCode);
        $("#txtMobile").val(Mobile);
        GetTicketLists();
    }


    $(document.body).on('click', '#GetTickets', () => {

        NCode = $("#txtNationalCode").val();
        Mobile = $("#txtMobile").val();

        if ($("#txtNationalCode").val().length === 0) {
            toastr.error('کدملی خود را وارد نمایید');
        } else if ($("#txtMobile").val().length === 0) {
            toastr.error('شماره موبایل خود را وارد نمایید');
        } else {

            GetTicketLists();
        }


    });

    $(document.body).on('click', '.payment', (e) => {

        let aa = parseInt($(e.currentTarget).attr('contributorPaymentId'));
        BeginTransaction(aa);

    });



});