using EventManagementSystemApi.DtoMode;
using EventManagementSystemApi.Models;

namespace EventManagementSystemApi.Interface
{
    public interface IEventManagement
    {
        ResponseBooking BookEvent(string emailUser, string eventName);
        ResponseCreate CreatEvent(string emailUser, string eventName, string description, DateTime dateEvent, string location);
        ResponseDelet DeletEvent(string emailUser, string eventName);
        ResponseDelet DeletTicket(string emailUser, int ticketId);
        public List<EventDto> GetAll();
        List<TicketDto> GetMyTicket(string emailUser);
        List<EventDto> GetMyEvent(string emailUser);
        ResponseUpdate UpdateTicket(string emailUser, Ticket ticket);
        ResponseUpdate UpdateEvent(string emailUser, Event @event);

    }
}