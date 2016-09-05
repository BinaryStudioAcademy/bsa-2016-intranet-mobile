namespace IntranetMobile.Core.Models
{
    // TODO: Create wrapper models! Why are we supposed to work with DTOs?

    public class Ticket
    {
        public string TicketId { get; set; }
        public string Author { get; set; }
        public string DateReview { get; set; }
        public string ReviewText { get; set; }
        public string TitleName { get; set; }
        public string AuthorImage { get; set; }
    }

    public class TicketGroup
    {
    }

    public class UserTicket
    {
        
    }

    public class TicketTag
    {
        
    }

    public class Pivot
    {
        
    }
}