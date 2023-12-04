namespace EventManagementSystemApi.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User user { get; set; }
        public Event Event { get; set; }
        public int EventId { get; set; }
    }
}
