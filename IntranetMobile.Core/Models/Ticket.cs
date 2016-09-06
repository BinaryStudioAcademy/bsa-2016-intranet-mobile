using System;
using System.Collections.Generic;
using IntranetMobile.Core.Models.Dtos;

namespace IntranetMobile.Core.Models
{
    // TODO: Create wrapper models! Why are we supposed to work with DTOs?

    public class Ticket
    {
        public string TicketId { get; set; }
        public string AuthorName { get; set; }
        public string UserServerId { get; set; }
        public DateTime DateReview { get; set; }
        public string ReviewText { get; set; }
        public string TitleName { get; set; }
        public string AuthorImage { get; set; }
        public string CategoryName { get; set; }
        public List<string> ListOfUserIds { get; set; }
        public List<string> ListOfTagTitles { get; set; }

        public static Ticket FromDto(TicketDto dto)
        {
            return new Ticket
            {
                TicketId = dto.id,
                AuthorName = $"{dto.user.first_name} {dto.user.last_name}",
                TitleName = dto.title,
                AuthorImage = dto.user.avatar,
                CategoryName = dto.group.title,
                ReviewText = dto.details,
                DateReview = DateTime.Parse(dto.date_review),
                UserServerId = dto.user.binary_id
            };
        }
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