using System.Collections.Generic;
using IntranetMobile.Core.Models.Dtos;

namespace IntranetMobile.Core.Models
{
    public class Ticket
    {
        public string TicketId { get; set; }
        public string UserServerId { get; set; }
        public string DateReview { get; set; }
        public string ReviewText { get; set; }
        public string TitleName { get; set; } 
        public string CategoryName { get; set; }
        public string GroupId { get; set; }
        public int OffersCount { get; set; }

        public List<string> ListOfUserIds { get; set; }
        public List<string> ListOfTagTitles { get; set; }

        public static Ticket TicketFromDto(TicketDto dto)
        {
            return new Ticket
            {
                TicketId = dto.id,       
                TitleName = dto.title,
                CategoryName = dto.group.title,
                ReviewText = dto.details,
                DateReview = dto.date_review,
                UserServerId = dto.user.binary_id,
                GroupId = dto.group_id,
                OffersCount = dto.offers_count,        
                ListOfUserIds = new List<string>(),
                ListOfTagTitles = new List<string>()
            };
        }
    }
}