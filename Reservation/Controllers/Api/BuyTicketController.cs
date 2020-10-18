using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reservation.Tools;
using Reservation.Tools.Zarinpal;

namespace Reservation.Controllers.Api
{
    [Route("api")]
    [ApiController]
    public class BuyTicketController : ControllerBase
    {


        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;


        public BuyTicketController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;

        }

        [HttpPost]
        [Route("BuyTicket/BuyTicketInsert")]
        public IActionResult BuyTicketInsert(Contributor contributor, long ticketTypeId)
        {
            try
            {
                var ticket = _repository.MeetingTicket.FindByCondition(c => c.MeetingTicketId == ticketTypeId && c.Count > 0).FirstOrDefault();
                if (ticket == null)
                {
                    return BadRequest("تمام تیکت های انتخاب شده رزرو شده اند!");
                }

                var ticketno = "10000";
                try
                {
                    var aa = _repository.ContributorPayment.FindAll()
                        .Select(c => c.TicketNo.Substring(0, 6)).OrderByDescending(c => c).FirstOrDefault();

                    ticketno = (Convert.ToInt32(aa) + 1).ToString();


                }
                catch (Exception e)
                {

                }


                var _contributor = _repository.Contributor.FindByCondition(c => c.NationalCode == contributor.NationalCode).FirstOrDefault();

                if (_contributor == null)
                {


                    ContributorTicket contributorTicket = new ContributorTicket();
                    contributorTicket.MeetingTicketId = ticket.MeetingTicketId;

                    contributor.ContributorTicket = new List<ContributorTicket> { contributorTicket };


                    if (ticket.Price > 500000000)
                    {
                        ContributorPayment contributorPayment = new ContributorPayment();
                        contributorPayment.TicketNo = ticketno;
                        contributorPayment.Amount = 500000000;
                        contributorPayment.ContributorTicketId = contributorTicket.ContributorTicketId;
                        contributorPayment.TransactionCode = "";

                        ContributorPayment contributorPayment1 = new ContributorPayment();
                        contributorPayment1.TicketNo = ticketno + "_1";
                        contributorPayment1.Amount = ticket.Price.Value - 500000000;
                        contributorPayment1.ContributorTicketId = contributorTicket.ContributorTicketId;
                        contributorPayment1.TransactionCode = "";
                        contributorTicket.ContributorPayment = new List<ContributorPayment> { contributorPayment, contributorPayment1 };
                    }
                    else
                    {
                        ContributorPayment contributorPayment = new ContributorPayment();
                        contributorPayment.TicketNo = ticketno;
                        contributorPayment.Amount = ticket.Price.Value;
                        contributorPayment.ContributorTicketId = contributorTicket.ContributorTicketId;
                        contributorPayment.TransactionCode = "";
                        contributorTicket.ContributorPayment = new List<ContributorPayment> { contributorPayment };
                    }
                    _repository.Contributor.Create(contributor);
                    _repository.Save();

                   
                    return Ok("");
                }
                else
                {

                    ContributorTicket contributorTicket = new ContributorTicket();
                    contributorTicket.MeetingTicketId = ticket.MeetingTicketId;
                    contributorTicket.ContributorId = _contributor.ContributorId;

                    contributor.ContributorTicket = new List<ContributorTicket> { contributorTicket };



                    if (ticket.Price > 500000000)
                    {
                        ContributorPayment contributorPayment = new ContributorPayment();
                        contributorPayment.TicketNo = ticketno;
                        contributorPayment.Amount = 500000000;
                        contributorPayment.ContributorTicketId = contributorTicket.ContributorTicketId;
                        contributorPayment.TransactionCode = "";

                        ContributorPayment contributorPayment1 = new ContributorPayment();
                        contributorPayment1.TicketNo = ticketno + "_1";
                        contributorPayment1.Amount = ticket.Price.Value - 500000000;
                        contributorPayment1.ContributorTicketId = contributorTicket.ContributorTicketId;
                        contributorPayment1.TransactionCode = "";
                        contributorTicket.ContributorPayment = new List<ContributorPayment> { contributorPayment, contributorPayment1 };
                    }
                    else
                    {
                        ContributorPayment contributorPayment = new ContributorPayment();
                        contributorPayment.TicketNo = ticketno;
                        contributorPayment.Amount = ticket.Price.Value;
                        contributorPayment.ContributorTicketId = contributorTicket.ContributorTicketId;
                        contributorPayment.TransactionCode = "";
                        contributorTicket.ContributorPayment = new List<ContributorPayment> { contributorPayment };
                    }

                    _contributor.MobileNumber = contributor.MobileNumber;
                    _contributor.Email = contributor.Email;
                    _contributor.FirstName = contributor.FirstName;
                    _contributor.LastName = contributor.LastName;


                    _contributor.ContributorTicket.Add(contributorTicket);

                    _repository.Contributor.Update(_contributor);
                    _repository.Save();
                    return Ok("");

                }



            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong inside FinalOrderInsert  To database: {e.Message}");
                return BadRequest("Internal server error");
            }
        }

        [HttpGet]
        [Route("BuyTicket/BeginTransaction")]
        public IActionResult GetTransactionCode(long contributorPaymentId)
        {

            var payment = _repository.ContributorPayment
                .FindByCondition(c => c.ContributorPaymentId == contributorPaymentId).FirstOrDefault();

            if (payment == null) return BadRequest("");

            ZarinPallRequest request = new ZarinPallRequest();
            request.amount = (int)(payment.Amount);
            request.description = "شماره سفارش: " + payment.TicketNo;
            Tools.ZarinPal.ZarinPal zarinPal = new Tools.ZarinPal.ZarinPal();
            var res = zarinPal.Request(request);

            payment.TransactionCode = res.authority;
            payment.TransactionDate = DateTime.Now.Ticks;
            _repository.ContributorPayment.Update(payment);
            _repository.Save();

            return Ok("https://www.zarinpal.com/pg/StartPay/" + res.authority);
        }


        [HttpGet]
        [Route("BuyTicket/VerifyPayment")]
        public IActionResult VerifyPayment(string Authority, string Status)
        {
            try
            {
                var contributorPayment = _repository.ContributorPayment
                    .FindByCondition(c => c.TransactionCode == Authority).FirstOrDefault();


                Contributor contributor = _repository.ContributorTicket.FindByCondition(c => c.ContributorTicketId == contributorPayment.ContributorTicketId).Include(c => c.Contributor).Select(c => c.Contributor).FirstOrDefault();
                ZarinPalVerifyRequest zarinPalVerifyRequest = new ZarinPalVerifyRequest();
                zarinPalVerifyRequest.authority = Authority;
                zarinPalVerifyRequest.amount = (int)contributorPayment.Amount.Value;

                Tools.ZarinPal.ZarinPal zarinPal = new Tools.ZarinPal.ZarinPal();
                var result = zarinPal.VerifyPayment(zarinPalVerifyRequest);
                if (result.code == 100 || result.code == 101)
                {


                    contributorPayment.RefId = result.ref_id;
                    contributorPayment.CardPan = result.card_pan;
                    contributorPayment.VerifyDate = DateTime.Now.Ticks;
                    contributorPayment.FinalStatusId = result.code;
                    _repository.ContributorPayment.Update(contributorPayment);

                    var meetingTicket = _repository.ContributorTicket.FindByCondition(c =>
                              c.ContributorTicketId == contributorPayment.ContributorTicketId)
                          .Include(c => c.MeetingTicket)
                          .Select(c => c.MeetingTicket).FirstOrDefault();

                    meetingTicket.Count = meetingTicket.Count - 1;
                    _repository.MeetingTicket.Update(meetingTicket);

                    _repository.Save();
                    SendEmail sendEmail = new SendEmail();
                    sendEmail.SendSuccessfullBuy(contributor.FirstName + " " + contributor.LastName, Authority, contributor.Email);


                    return Ok(contributorPayment.TicketNo);
                }
                else
                {
                    contributorPayment.TransactionDate = DateTime.Now.Ticks;
                    contributorPayment.FinalStatusId = result.code;

                    _repository.ContributorPayment.Update(contributorPayment);
                    _repository.Save();
                    return Ok("error");
                }


            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong inside GetCustomerOrderListByCustomerId: {e.Message}");
                return BadRequest("Internal server error");
            }
        }

        [HttpGet]
        [Route("BuyTicket/GetTicketLists")]
        public IActionResult GetTicketLists(string NationalCode, string MobileNo)
        {
            try
            {
                var cobtributor = _repository.Contributor
                    .FindByCondition(c => c.MobileNumber == MobileNo && c.NationalCode == NationalCode)
                    .FirstOrDefault();
                if (cobtributor == null)
                {
                    return BadRequest("NotFound!");
                }


                var ticketList = _repository.ContributorPayment
        .FindByCondition(c => c.ContributorTicket.ContributorId == cobtributor.ContributorId)
        .Include(c => c.ContributorTicket).ThenInclude(c => c.MeetingTicket).ThenInclude(c => c.Meeting)
        .Select(c => new
        {
            c.ContributorPaymentId,
            MeetingName = c.ContributorTicket.MeetingTicket.Meeting.MeetingTitle,
            MeetingTicketType = c.ContributorTicket.MeetingTicket.MeetingTicketType,
            Amount = c.Amount,
            TicketNo = c.TicketNo,
            c.FinalStatusId,
            c.TransactionCode
        }).ToList();
                return Ok(ticketList);
            }
            catch (Exception e)
            {
                return BadRequest("");
            }

        }

    }
}
