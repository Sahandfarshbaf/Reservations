﻿using System;
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

                _repository.Contributor.Create(contributor);
                var ticket = _repository.MeetingTicket.FindByCondition(c => c.MeetingTicketId == ticketTypeId && c.Count > 0).FirstOrDefault();
                if (ticket == null)
                {
                    return BadRequest("All Ticket By This Type Has been Reserverd!");
                }


                ContributorTicket contributorTicket = new ContributorTicket();
                contributorTicket.MeetingTicketId = ticket.MeetingTicketId;

                contributor.ContributorTicket = new List<ContributorTicket> { contributorTicket };

                ZarinPallRequest request = new ZarinPallRequest();
                request.amount = (int)(ticket.Price.Value);
                request.description = "order NO: " + contributor.NationalCode;
                Tools.ZarinPal.ZarinPal zarinPal = new Tools.ZarinPal.ZarinPal();
                var res = zarinPal.Request(request);

                ContributorPayment contributorPayment = new ContributorPayment();
                contributorPayment.Amount = ticket.Price.Value;
                contributorPayment.ContributorTicketId = contributorTicket.ContributorTicketId;
                contributorPayment.TransactionCode = res.authority;
                contributorTicket.ContributorPayment = new List<ContributorPayment> { contributorPayment };



                _repository.Save();
                return Ok("https://www.zarinpal.com/pg/StartPay/" + res.authority);


            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong inside FinalOrderInsert  To database: {e.Message}");
                return BadRequest("Internal server error");
            }
        }

        [HttpGet]
        [Route("BuyTicket/VerifyPayment")]
        public IActionResult VerifyPayment(string Authority, string Status)
        {
            try
            {
                var contributorPayment = _repository.ContributorPayment
                    .FindByCondition(c => c.TransactionCode == Authority).FirstOrDefault();



                ZarinPalVerifyRequest zarinPalVerifyRequest = new ZarinPalVerifyRequest();
                zarinPalVerifyRequest.authority = Authority;
                zarinPalVerifyRequest.amount = (int)contributorPayment.Amount.Value;

                Tools.ZarinPal.ZarinPal zarinPal = new Tools.ZarinPal.ZarinPal();
                var result = zarinPal.VerifyPayment(zarinPalVerifyRequest);
                if (result.code == 100 || result.code == 101)
                {


                    contributorPayment.RefId = result.ref_id;
                    contributorPayment.TransactionDate = DateTime.Now.Ticks;
                    contributorPayment.FinalStatusId = result.code;
                    _repository.ContributorPayment.Update(contributorPayment);

                    var meetingTicket = _repository.ContributorTicket.FindByCondition(c =>
                              c.ContributorTicketId == contributorPayment.ContributorTicketId)
                          .Include(c => c.MeetingTicket)
                          .Select(c => c.MeetingTicket).FirstOrDefault();

                    meetingTicket.Count = meetingTicket.Count - 1;
                    _repository.MeetingTicket.Update(meetingTicket);

                    _repository.Save();

                    return Ok("success");
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

    }
}
