using EventManagementSystemApi.DbContext;
using EventManagementSystemApi.DtoMode;
using EventManagementSystemApi.Interface;
using EventManagementSystemApi.Models;
using System.Data;

namespace EventManagementSystemApi.Repository
{
    public class EventManagement : IEventManagement
    {

        private IUserManager _userManager;
        private ApplicationDbContext _dbContext;


        public EventManagement(IUserManager userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }
        public List<EventDto> GetAll()
        {
            var x = _dbContext.Eventes.Select(x => new EventDto
            { Name = x.Name, Description = x.Description, Location = x.Location, DateTimeEvent = x.DateTimeEvent }).AsEnumerable().ToList();
            return x;
        }
        public ResponseCreate CreatEvent(string emailUser, string eventName, string description, DateTime dateEvent, string location)
        {
            var user = _userManager.GetUserByEmail(emailUser);
            if (user != null)
            {
                Event @event = new Event() { UserId = user.Id, Name = eventName, Description = description, DateTimeEvent = dateEvent, Location = location };
                _dbContext.Eventes.Add(@event);
                _dbContext.SaveChanges();
                return new ResponseCreate { IsSuccess = true };
            }
            return new ResponseCreate { IsSuccess = false };
        }
        public ResponseBooking BookEvent(string emailUser, string eventName)
        {
            var user = _userManager.GetUserByEmail(emailUser);
            var @event = _dbContext.Eventes.FirstOrDefault(x => x.Name == eventName);
            Ticket ticket = new() { UserId = user.Id, EventId = @event.Id };
            _dbContext.Tickets.Add(ticket);
            _dbContext.SaveChanges();
            return new ResponseBooking { ticketId = ticket.Id };
        }
        public ResponseDelet DeletTicket(string emailUser, int ticketId)
        {
            var ticket = _dbContext.Tickets.FirstOrDefault(x => x.Id == ticketId);
            var user = _userManager.GetUserByEmail(emailUser);
            if (ticket != null && ticket.Id == ticketId)
            {
                _dbContext.Tickets.Remove(ticket);
                return new ResponseDelet { IsSuccess = true };
            }
            return new ResponseDelet { IsSuccess = false };
        }
        public ResponseDelet DeletEvent(string emailUser, string eventName)
        {
            var @event = _dbContext.Eventes.FirstOrDefault(x => x.Name == eventName);
            var user = _userManager.GetUserByEmail(emailUser);
            if (@event != null && @event.UserId == user.Id)
            {
                _dbContext.Eventes.Remove(@event);
                return new ResponseDelet { IsSuccess = true };
            }
            return new ResponseDelet { IsSuccess = false };
        }
        public ResponseUpdate UpdateEvent(string emailUser, Event @event)
        {
            var data = _dbContext.Eventes.FirstOrDefault(x => x.Id == @event.Id);
            var user = _userManager.GetUserByEmail(emailUser);
            if (data != null && user != null)
            {
                _dbContext.Eventes.Update(@event);
                return new ResponseUpdate { IsSuccess = true };
            }
            return new ResponseUpdate { IsSuccess = false };
        }
        public ResponseUpdate UpdateTicket(string emailUser, Ticket ticket)
        {
            var data = _dbContext.Tickets.FirstOrDefault(x => x.Id == ticket.Id);
            var user = _userManager.GetUserByEmail(emailUser);
            if (data != null && user != null)
            {
                _dbContext.Tickets.Update(ticket);
                return new ResponseUpdate { IsSuccess = true };
            }
            return new ResponseUpdate { IsSuccess = false };
        }
        public List<EventDto> GetMyEvent(string emailUser)
        {
            var user = _userManager.GetUserByEmail(emailUser);
            return _dbContext.Eventes.Where(x => x.UserId == user.Id).Select(x => new EventDto
            { Name = x.Name, Description = x.Description, Location = x.Location, DateTimeEvent = x.DateTimeEvent }).AsEnumerable().ToList();
        }
        public List<TicketDto> GetMyTicket(string emailUser)
        {
            var user = _userManager.GetUserByEmail(emailUser);
            return _dbContext.Tickets.Where(x => x.UserId == user.Id).Select(x => new TicketDto
            { EventId = x.EventId, Id = x.Id }).ToList();
        }


    }
}
