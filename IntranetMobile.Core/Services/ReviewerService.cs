using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntranetMobile.Core.Interfaces;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Models.Dtos;

namespace IntranetMobile.Core.Services
{
    public enum ReviewerGroup
    {
        None = 0,
        Php = 1,
        JavaScript = 2,
        DotNet = 3
    }

    public class ReviewerService : IReviewerService
    {
        private readonly string _actionTicketPath = "reviewr/api/v1/user/";

        private readonly RestClient _restClient;
        private readonly string _reviewrPath = "reviewr/api/v1/reviewrequest";

        public ReviewerService(RestClient client)
        {
            _restClient = client;
        }

        public Task<bool> AcceptUserReviewForTicketAsync(string userId, string ticketId)
        {
            return _restClient.GetAsync(_actionTicketPath + $"{userId}/accept/{ticketId}");
        }

        public async Task<bool> CreateReviewTicketAsync(Ticket ticket)
        {
            var ticketDto = new ReviewTicketRequestDto();

            ticketDto.title = ticket.TitleName;
            ticketDto.details = ticket.ReviewText;
            ticketDto.date_review = ticket.DateReview.ToString();
            ticketDto.formatted_date_review = ticket.DateReview.ToString();
            ticketDto.tags = ticket.ListOfTagTitles;
            ticketDto.group = "";
            ticketDto.created_at = "";
            ticketDto.formatted_created_at = "Invalid Date";
            ticketDto.group_id = ticket.GroupId;

            var result = await _restClient.PostAsync<bool>(_reviewrPath, ticketDto);

            return result;
        }

        public Task<bool> DeclineUserReviewForTicketAsync(string userId, string ticketId)
        {
            return _restClient.GetAsync(_actionTicketPath + $"{userId}/decline/{ticketId}");
        }

        public Task<bool> DeleteTicketAsync(string id)
        {
            return _restClient.DeleteAsync(_reviewrPath + $"/{id}");
        }

        public async Task<List<Ticket>> GetListOfMyTicketsAsync()
        {
            var listOfTicketsDto = await _restClient.GetAsync<List<TicketDto>>(_reviewrPath + "/my");
            var listOfTicket = GetListOfTickets(listOfTicketsDto);

            return listOfTicket;
        }

        public async Task<List<SubscribedTicket>> GetListOfSubscribedTicketsAsync()
        {
            var subscribedTicketDto = await _restClient.GetAsync<SubscribedTicketDto>("reviewr/api/v1/myrequests");
            var listOfSubscribedTicket = new List<SubscribedTicket>();

            foreach (var s in subscribedTicketDto.message)
            {
                var subTicket = new SubscribedTicket();

                subTicket.Id = s.id;
                subTicket.Details = s.details;
                subTicket.DateReview = s.date_review;
                subTicket.GroupId = s.group_id;
                subTicket.OffersCount = s.offers_count;
                subTicket.UserId = s.user_id;
                subTicket.Title = s.title;

                subTicket.Pivot = new SubscribedTicket.PivotTicket();
                subTicket.Pivot.UserId = s.pivot.user_id;
                subTicket.Pivot.ReviewRequestId = s.pivot.review_request_id;
                subTicket.Pivot.IsAccepted = s.pivot.isAccepted;
                subTicket.Pivot.Status = s.pivot.status;

                listOfSubscribedTicket.Add(subTicket);
            }

            return listOfSubscribedTicket;
        }

        public async Task<List<Comment>> GetListOfTicketCommentsAsync(string ticketId)
        {
            var commentsResponse =
                await _restClient.GetAsync<List<TicketCommentDto>>(_reviewrPath + $"/{ticketId}/comment");

            return commentsResponse.Select(c => new Comment
            {
                AuthorId = c.user.binary_id,
                Body = c.text,
                CommentId = c.id,
                Date = string.IsNullOrWhiteSpace(c.created_at)
                             ? DateTime.MinValue
                             : DateTime.Parse(c.created_at)
            }).ToList();
        }

        public async Task<List<Ticket>> GetListOfTicketsAsync()
        {
            var listOfTicketsDto = await _restClient.GetAsync<List<TicketDto>>(_reviewrPath);
            var listOfTicket = GetListOfTickets(listOfTicketsDto);

            return listOfTicket;
        }

        public async Task<List<Ticket>> GetListOfTicketsForGroupAsync(ReviewerGroup group)
        {
            var groupId = (int) group;
            var dtos = await _restClient.GetAsync<List<TicketDto>>(_reviewrPath + $"/group/{groupId}");
            return dtos.Select(dto => new Ticket().UpdateFromDto(dto)).ToList();
        }

        public async Task<Ticket> GetTicketDetailsAsync(string ticketId)
        {
            var ticketDto = await _restClient.GetAsync<TicketDto>(_reviewrPath + $"/{ticketId}");
            var ticket = new Ticket().UpdateFromDto(ticketDto);

            foreach (var id in ticketDto.users)
            {
                ticket.ListOfUserIds.Add(id.binary_id);
            }

            foreach (var tag in ticketDto.tags)
            {
                ticket.ListOfTagTitles.Add(tag.title);
            }

            return ticket;
        }

        public Task<bool> JoinTicketAsync(string userId, string ticketId)
        {
            return _restClient.GetAsync(_actionTicketPath + $"{userId}/offeron/{ticketId}");
        }

        public Task<bool> UndoJoinTicketAsync(string ticketId)
        {
            return _restClient.GetAsync(_actionTicketPath + $"offeroff/{ticketId}");
        }

        public async Task<bool> WtiteCommentAsync(string ticketId, string text)
        {
            var comment = new TicketCommentDto();
            comment.text = text;
            comment.created_at = DateTime.Now.ToString();
            comment.formatted_created_at = "Invalid date";

            var result = await _restClient.PostAsync<bool>(_reviewrPath + $"/{ticketId}/comment", comment);

            return result;
        }

        public List<Ticket> GetListOfTickets(List<TicketDto> listOfTicketsDto)
        {
            var result = new List<Ticket>();

            foreach (var ticketDto in listOfTicketsDto)
            {
                var ticket = new Ticket().UpdateFromDto(ticketDto);

                foreach (var id in ticketDto.users)
                {
                    ticket.ListOfUserIds.Add(id.binary_id);
                }

                foreach (var tag in ticketDto.tags)
                {
                    ticket.ListOfTagTitles.Add(tag.title);
                }

                result.Add(ticket);
            }

            return result;
        }
    }
}