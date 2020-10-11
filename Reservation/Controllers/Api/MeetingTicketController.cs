using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Reservation.Controllers.Api
{
    [Route("api/")]
    [ApiController]
    public class MeetingTicketController : ControllerBase
    {

        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public MeetingTicketController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        [Route("MeetingTicket/GetMeetingTikcetType")]
        public IActionResult GetMeetingTikcetType(long meetingId)
        {

            try
            {
                var res = _repository.MeetingTicket.FindByCondition(c => c.MeetingId == meetingId)
                     .ToList();

                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest("");
            }

        }

        [HttpGet]
        [Route("MeetingTicket/GetMeetingTikcetTypeById")]
        public IActionResult GetMeetingTikcetTypeById(long meetingTicketId)
        {

            try
            {
                var res = _repository.MeetingTicket.FindByCondition(c => c.MeetingTicketId == meetingTicketId)
                    .FirstOrDefault();

                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest("");
            }

        }
        [HttpGet]
        [Route("MeetingTicket/GetMeetingTikcetParams")]
        public IActionResult GetMeetingTikcetParams(long meetingTicketId)
        {

            try
            {
                var res = _repository.MeetingTicketParam.FindByCondition(c => c.MeetingTicketId == meetingTicketId)
                    .ToList();

                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest("");
            }

        }

    }
}
