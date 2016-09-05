using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;
using IntranetMobile.Core.Interfaces;
using IntranetMobile.Core.Models.Dtos;

namespace IntranetMobile.Core.Services
{
    public enum ReviewerGroup
    {
        none = 0,
        JavaScript = 1,
        Php = 2,
        DotNet = 3
    }

    public class ReviewerService : IReviewerService
    {
        private readonly string _actionTicketPath = "reviewr/api/v1/user/";
        private readonly string _reviewrPath = "reviewr/api/v1/reviewrequest";

        private readonly RestClient _restClient;

        public ReviewerService(RestClient client)
        {
            _restClient = client;
        }

        public async Task<bool> AcceptUserReviewForTicketAsync(string userId, string ticketId)
        {
            return await _restClient.GetAsync(_actionTicketPath + userId + "/accept/" + ticketId);
        }

        public async Task<TicketDto> CreateReviewTicketAsync(ReviewTicketRequestDto reviewTicketRequestDto)
        {
            return await _restClient.PostAsync<TicketDto>(_reviewrPath, reviewTicketRequestDto);
        }

        public async Task<bool> DeclineUserReviewForTicketAsync(string userId, string ticketId)
        {
            return await _restClient.GetAsync(_actionTicketPath + userId + "/decline/" + ticketId);
        }

        public async Task<bool> DeleteTicketAsync(string id)
        {
            return await _restClient.DeleteAsync(_reviewrPath + $"/{id}");
        }

        public async Task<List<TicketDto>> GetListOfMyTicketsAsync()
        {
            return await _restClient.GetAsync<List<TicketDto>>(_reviewrPath + "/my");
        }

        public async Task<List<SubscribedTicketDto>> GetListOfSubscribedTicketsAsync()
        {
            return await _restClient.GetAsync<List<SubscribedTicketDto>>("reviewr/api/v1/myrequests");
        }

        public async Task<List<Comment>> GetListOfTicketCommentsAsync(string ticketId)
        {
            var comments = new List<Comment>();
            var commentsResponse = await _restClient.GetAsync<List<TicketCommentDto>>(_reviewrPath + "/" + ticketId + "/comment");

            foreach (var c in commentsResponse)
            {
                var comment = new Comment();

                comment.AuthorId = c.user.binary_id;
                comment.Body = c.text;
                comment.CommentId = c.id;
                comment.Date = c.created_at;

                comments.Add(comment);
            }

            return comments;
        }

        public async Task<List<TicketDto>> GetListOfTicketsAsync()
        {
            return await _restClient.GetAsync<List<TicketDto>>(_reviewrPath);
        }

        public Task<List<TicketDto>> GetListOfTicketsForGroupAsync(ReviewerGroup group)
        {
            var groupId = (int)group;
            return _restClient.GetAsync<List<TicketDto>>(_reviewrPath + "/group/" + groupId);
        }

        public async Task<Ticket> GetTicketDetailsAsync(string ticketId)
        {
            var ticket = new Ticket();
            var ticketDto = await _restClient.GetAsync<TicketDto>(_reviewrPath + "/" + ticketId);

            ticket.TicketId = ticketDto.id;
            ticket.Author = ticketDto.user.first_name + " " + ticketDto.user.last_name;
            ticket.DateReview = ticketDto.date_review;
            ticket.ReviewText = ticketDto.details;
            ticket.TitleName = ticketDto.title;
            ticket.AuthorImage = ticketDto.user.avatar;
            ticket.CategoryName = ticketDto.group.title;
            ticket.ListOfUsersId = new List<string>();
            ticket.ListOfTagTitle = new List<string>();

            foreach (var id in ticketDto.users)
            {
                ticket.ListOfUsersId.Add(id.binary_id);
            }

            foreach (var tag in ticketDto.tags)
            {
                ticket.ListOfTagTitle.Add(tag.title);
            }

            return ticket;
        }

        public async Task<bool> JoinTicketAsync(string userId, string ticketId)
        {
            return await _restClient.GetAsync(_actionTicketPath + userId + "/offeron/" + ticketId);
        }

        public async Task<bool> UndoJoinTicketAsync(string ticketId)
        {
            return await _restClient.GetAsync(_actionTicketPath + "offeroff/" + ticketId);
        }

        public Task<TicketCommentDto> WtiteCommentAsync(string ticketId, string text)
        {
            var commentTicketDto = new TicketCommentDto();
            commentTicketDto.text = text;
            commentTicketDto.created_at = DateTime.Now.ToString();
            commentTicketDto.formatted_created_at = "Invalid date";

            return _restClient.PostAsync<TicketCommentDto>(_reviewrPath + "/" + ticketId + "/comment");
        }
    }
}