using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IntranetMobile.Core.Services;

namespace IntranetMobile.Core
{
    public class ReviewerService:IReviewerService
    {
        private readonly string _reviewrPath = "reviewr/api/v1/reviewrequest";
        private readonly string _actionTicketPath = "reviewr/api/v1/user/";

        private readonly RestClient _restClient;

        public ReviewerService(RestClient client)
        {
            _restClient = client;
        }

        public async Task<bool> AcceptUserReviewForTicketAsync(string userId, string ticketId)
        {
            return await _restClient.GetAsync(_actionTicketPath + userId + "/accept/" + ticketId);
        }

        public async Task<TicketsDto> CreateReviewTicketAsync(ReviewTicketRequestDto reviewTicketRequestDto)
        {
            return await _restClient.PostAsync<TicketsDto>(_reviewrPath, reviewTicketRequestDto);
        }

        public async Task<bool> DeclineuserReviewForTicketAsync(string userId, string ticketId)
        {
            return await _restClient.GetAsync(_actionTicketPath + userId + "/decline/" + ticketId);
        }

        public async Task<bool> DeleteTicketAsync()
        {
            return await _restClient.DeleteAsync(_reviewrPath + "/186");
        }

        public async Task<List<TicketsDto>> GetListOfMyTicketsAsync()
        {
            return await _restClient.GetAsync<List<TicketsDto>>(_reviewrPath + "/my");
        }

        public async Task<List<SubscribedTicketDto>> GetListOfSubscribedTicketsAsync()
        {
            return await _restClient.GetAsync<List<SubscribedTicketDto>>("reviewr/api/v1/myrequests");
        }

        public async Task<List<CommentTicketDto>> GetListOfTicketCommentsAsync(string ticketId)
        {
            return await _restClient.GetAsync<List<CommentTicketDto>>(_reviewrPath + "/" + ticketId + "/comment");
        }

        public async Task<List<TicketsDto>> GetListOfTicketsAsync()
        {
            return await _restClient.GetAsync<List<TicketsDto>>(_reviewrPath);
        }

        public async Task<List<TicketsDto>> GetListOfTicketsForConcreteGroupAsync(string groupId)
        {
            // Id = 1:PHP 2:JS 3:C#
            return await _restClient.GetAsync<List<TicketsDto>>(_reviewrPath + "/group/" + groupId);
        }

        public async Task<TicketsDto> GetTicketDetailsAsync(string ticketId)
        {
            return await _restClient.GetAsync<TicketsDto>(_reviewrPath + "/" + ticketId);
        }

        public async Task<bool> JoinTicketAsync(string userId, string ticketId)
        {
            return await _restClient.GetAsync(_actionTicketPath + userId + "/offeron/" + ticketId);
        }

        public async Task<bool> UndoJoinTicketAsync(string ticketId)
        {
            return await _restClient.GetAsync(_actionTicketPath + "offeroff/" + ticketId);
        }

        public Task<CommentTicketDto> WtiteCommentAsync(string ticketId, string text)
        {
            var commentTicketDto = new CommentTicketDto();
            commentTicketDto.text = text;
            commentTicketDto.created_at = DateTime.Now.ToString();
            commentTicketDto.formatted_created_at = "Invalid date";

            return _restClient.PostAsync<CommentTicketDto>(_reviewrPath + "/" + ticketId + "/comment");
        }
    }
}

