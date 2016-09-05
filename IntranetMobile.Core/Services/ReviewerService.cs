using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;
using IntranetMobile.Core.Interfaces;
using IntranetMobile.Core.Models.Dtos;

namespace IntranetMobile.Core.Services
{
    public class ReviewerService : IReviewerService
    {
        private readonly string _actionTicketPath = "reviewr/api/v1/user/";

        private readonly RestClient _restClient;
        private readonly string _reviewrPath = "reviewr/api/v1/reviewrequest";

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
            //TODO: WTF is this???????????????????????
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

        public async Task<List<TicketDto>> GetListOfTicketsForConcreteGroupAsync(string groupId)
        {
            // Id = 1:PHP 2:JS 3:C#
            return await _restClient.GetAsync<List<TicketDto>>(_reviewrPath + "/group/" + groupId);
        }

        public async Task<TicketDto> GetTicketDetailsAsync(string ticketId)
        {
            return await _restClient.GetAsync<TicketDto>(_reviewrPath + "/" + ticketId);
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