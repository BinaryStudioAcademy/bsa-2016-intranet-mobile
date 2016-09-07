using System;
using System.Collections.Generic;
using IntranetMobile.Core.Models.Dtos;

namespace IntranetMobile.Core.Models
{
    public class Ticket
    {
        public string TicketId { get; set; }
        public string UserServerId { get; set; }
        public DateTime DateReview { get; set; }
        public string ReviewText { get; set; }
        public string TitleName { get; set; }
        public string CategoryName { get; set; }
        public string GroupId { get; set; }
        public int OffersCount { get; set; }
        public List<string> ListOfUserIds { get; set; }
        public List<string> ListOfTagTitles { get; set; }

        public Ticket UpdateFromDto(TicketDto ticketDto)
        {
            TicketId = ticketDto.id;
            TitleName = ticketDto.title;
            CategoryName = ticketDto.group.title;
            ReviewText = ticketDto.details;
            DateReview = DateTime.Parse(ticketDto.date_review);
            UserServerId = ticketDto.user.binary_id;
            GroupId = ticketDto.group_id;
            OffersCount = ticketDto.offers_count;
            ListOfUserIds = new List<string>();
            ListOfTagTitles = new List<string>();

            return this;
        }
    }
}