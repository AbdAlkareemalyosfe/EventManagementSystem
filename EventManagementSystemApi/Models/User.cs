namespace EventManagementSystemApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public List<Event> Eventes { get; set; }
        public int EventId { get; set; }
        public List<Ticket> Ticketes { get; set; }
        public int TicketId { get; set; }

    }
}
