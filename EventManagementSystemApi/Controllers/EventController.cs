using EventManagementSystemApi.DtoMode;
using EventManagementSystemApi.Interface;
using EventManagementSystemApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventManagement _eventManagement;
        public EventController(IEventManagement eventManagement)
        {
            _eventManagement = eventManagement;
        }

        // GET: api/<EventController>
        [HttpGet("GetAllEventes")]
        [Authorize]
        public List<EventDto> GetAll()
        {
            return _eventManagement.GetAll();
        }

        // GET api/<EventController>/5
        [HttpPost("CreatEvent")]
        [Authorize]
        public ResponseCreate CreatEvent(string emailUser, string eventName, string description, DateTime dateEvent, string location)
        {
            return _eventManagement.CreatEvent(emailUser, eventName, description, dateEvent, location);

        }

        // POST api/<EventController>
        [HttpPost("BookTicket")]
        [Authorize]
        public ResponseBooking BookTicket([FromQuery] string emailUser, string eventName)
        {
            return _eventManagement.BookEvent(emailUser, eventName);
        }

        // PUT api/<EventController>/5
        [HttpDelete("DeletEvent")]
        [Authorize]
        public ResponseDelet DeletEvent(string emailUser, string eventName)
        {
            return _eventManagement.DeletEvent(emailUser, eventName);
        }


        [HttpPut("UpdateEvent")]
        [Authorize]
        public ResponseUpdate UpdateEvent(string emailUser, Event @event)
        {

            return _eventManagement.UpdateEvent(emailUser, @event);
        }
        [HttpPut("UpdateTicket")]
        [Authorize]
        public ResponseUpdate UpdateTicket(string emailUser, Ticket ticket)
        {
            return _eventManagement.UpdateTicket(emailUser, ticket);
        }
        [HttpGet("GetMyEvent")]
        [Authorize]
        public List<EventDto> GetMyEvent(string emailUser)
        {
            return _eventManagement.GetMyEvent(emailUser);
        }
        [HttpGet("GetMyTicket")]
        [Authorize]
        public List<TicketDto> GetMyTicket(string emailUser)
        {
            return _eventManagement.GetMyTicket(emailUser);
        }
    }
}
