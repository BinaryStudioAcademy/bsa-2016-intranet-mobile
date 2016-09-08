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
        public List<UserTicket> ListOfUsers { get; } = new List<UserTicket>();
        public List<string> ListOfTagTitles { get; } = new List<string>();

        public Ticket UpdateFromDto(TicketDto ticketDto)
        {
            TicketId = ticketDto.id;
            TitleName = ticketDto.title;
            CategoryName = ticketDto.group?.title;
            ReviewText = ticketDto.details;
            DateReview = !string.IsNullOrEmpty(ticketDto.date_review)
                ? DateTime.Parse(ticketDto.date_review)
                : default(DateTime);
            UserServerId = ticketDto.user?.binary_id;
            GroupId = ticketDto.group_id;
            OffersCount = ticketDto.offers_count;

            ListOfTagTitles.Clear();
            if (ticketDto.tags != null)
            {
                foreach (var ticketTagDto in ticketDto.tags)
                {
                    ListOfTagTitles.Add(ticketTagDto.title);
                }
            }

            ListOfUsers.Clear();
            if (ticketDto.users != null)
            {
                foreach (var userTicketDto in ticketDto.users)
                {
                    ListOfUsers.Add(new UserTicket().UpdateFromDto(userTicketDto));
                }
            }

            return this;
        }

        public class UserTicket
        {
            public string Id { get; set; }
            public string Bid { get; set; }
            public string BinaryId { get; set; }
            public int ReviewRequestId { get; set; }
            public int UserId { get; set; }
            public int IsAccepted { get; set; }
            public string Status { get; set; }

            public UserTicket UpdateFromDto(UserTicketDto userTicketDto)
            {
                Id = userTicketDto.id;
                Bid = userTicketDto.bid;
                BinaryId = userTicketDto.binary_id;
                ReviewRequestId = userTicketDto.pivot.review_request_id;
                UserId = userTicketDto.pivot.user_id;
                IsAccepted = userTicketDto.pivot.isAccepted;
                Status = userTicketDto.pivot.status;

                return this;
            }
        }
    }
}