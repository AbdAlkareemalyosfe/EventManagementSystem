namespace EventManagementSystemApi.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime DateTimeEvent { get; set; }
        public int AvailableTickets { get; set; }
        public int Balance { get; set; }
        public int? UserId { get; set; }
        public User user { get; set; }
        public List<Ticket> Ticket { get; set; }
        public int ticketId { get; set; }

    }
}