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
        private const string ActionTicketPath = "reviewr/api/v1/user/";
        private const string ReviewrPath = "reviewr/api/v1/reviewrequest";

        private readonly RestClient _restClient;

        public ReviewerService(RestClient client)
        {
            _restClient = client;
        }

        public Task<bool> AcceptUserReviewForTicketAsync(string userId, string ticketId)
        {
            return _restClient.GetAsync(ActionTicketPath + $"{userId}/accept/{ticketId}");
        }

        public async Task<bool> CreateReviewTicketAsync(ReviewTicketRequestDto reviewTicketRequestDto)
        {
            var result = await _restClient.PostAsync<bool>(ReviewrPath, reviewTicketRequestDto);

            return result;
        }

        public Task<bool> DeclineUserReviewForTicketAsync(string userId, string ticketId)
        {
            return _restClient.GetAsync(ActionTicketPath + $"{userId}/decline/{ticketId}");
        }

        public Task<bool> DeleteTicketAsync(string id)
        {
            return _restClient.DeleteAsync(ReviewrPath + $"/{id}");
        }

        public async Task<List<Ticket>> GetListOfMyTicketsAsync()
        {
            var listOfTicketsDto = await _restClient.GetAsync<List<TicketDto>>(ReviewrPath + "/my");
            var listOfTicket = GetListOfTickets(listOfTicketsDto);

            return listOfTicket;
        }

        public async Task<List<SubscribedTicket>> GetListOfSubscribedTicketsAsync()
        {
            var subscribedTicketDto = await _restClient.GetAsync<SubscribedTicketDto>("reviewr/api/v1/myrequests");

            return subscribedTicketDto.message.Select(s => new SubscribedTicket
            {
                Id = s.id,
                Details = s.details,
                DateReview = s.date_review,
                GroupId = s.group_id,
                OffersCount = s.offers_count,
                UserId = s.user_id,
                Title = s.title,
                Pivot = new SubscribedTicket.PivotTicket
                {
                    UserId = s.pivot.user_id,
                    ReviewRequestId = s.pivot.review_request_id,
                    IsAccepted = s.pivot.isAccepted,
                    Status = s.pivot.status
                }
            }).ToList();
        }

        public async Task<List<Comment>> GetListOfTicketCommentsAsync(string ticketId)
        {
            var commentsResponse =
                await _restClient.GetAsync<List<TicketCommentDto>>(ReviewrPath + $"/{ticketId}/comment");

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
            var listOfTicketsDto = await _restClient.GetAsync<List<TicketDto>>(ReviewrPath);
            var listOfTicket = GetListOfTickets(listOfTicketsDto);

            return listOfTicket;
        }

        public async Task<List<Ticket>> GetListOfTicketsForGroupAsync(ReviewerGroup group)
        {
            var groupId = (int) group;
            var dtos = await _restClient.GetAsync<List<TicketDto>>(ReviewrPath + $"/group/{groupId}");
            return dtos.Select(dto => new Ticket().UpdateFromDto(dto)).ToList();
        }

        public async Task<Ticket> GetTicketDetailsAsync(string ticketId)
        {
            var ticketDto = await _restClient.GetAsync<TicketDto>(ReviewrPath + $"/{ticketId}");
            var ticket = new Ticket().UpdateFromDto(ticketDto);

            return ticket;
        }

        public Task<bool> JoinTicketAsync(string userId, string ticketId)
        {
            return _restClient.GetAsync(ActionTicketPath + $"{userId}/offeron/{ticketId}");
        }

        public Task<bool> UndoJoinTicketAsync(string ticketId)
        {
            return _restClient.GetAsync(ActionTicketPath + $"offeroff/{ticketId}");
        }

        public async Task<bool> WtiteCommentAsync(string ticketId, string text)
        {
            var comment = new TicketCommentDto
            {
                text = text,
                created_at = DateTime.Now.ToString(),
                formatted_created_at = "Invalid date"
            };

            var result = await _restClient.PostAsync<bool>(ReviewrPath + $"/{ticketId}/comment", comment);

            return result;
        }

        public List<Ticket> GetListOfTickets(List<TicketDto> listOfTicketsDto)
        {
            return listOfTicketsDto.Select(ticketDto => new Ticket().UpdateFromDto(ticketDto)).ToList();
        }
    }
}