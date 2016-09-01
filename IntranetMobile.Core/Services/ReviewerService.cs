using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IntranetMobile.Core.Services;

namespace IntranetMobile.Core
{
    public class ReviewerService:IReviewerService
    {
        private readonly RestClient _restClient;

        public ReviewerService(RestClient client)
        {
            _restClient = client;
        }

        public Task<bool> AcceptUserReviewRequestForTicketAsync(string userId, string tickedId)
        {
            throw new NotImplementedException();
        }

        public Task<ReviewTicktsResponseDto> CreateReviewTicketAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeclineuserReviewRequestForTicketAsync(string userId, string tickedId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTicketRequstAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<TicketsDto>> GetListOfMyTicketsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<SubscribedTicketDto>> GetListOfSubscribedTicketsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<CommentsTicketDto>> GetListOfTicketCommentsAsync(string tickedId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TicketsDto>> GetListOfTicketsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<TicketsDto>> GetListOfTicketsForConcreteGroupAsync(string groupId)
        {
            throw new NotImplementedException();
        }

        public Task<TicketsDto> GetTicketDetailsAsync(string tickedId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> JoinTicketAsync(string userId, string tickedId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UndoJoinTicketAsync(string tickedId)
        {
            throw new NotImplementedException();
        }

        public Task<CommentsTicketDto> WtiteCommentAsync(string ticketId)
        {
            throw new NotImplementedException();
        }
    }
}

