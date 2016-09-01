using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntranetMobile.Core
{
    public interface IReviewerService
    {
        Task<List<TicketsDto>> GetListOfTicketsAsync();
        Task<List<TicketsDto>> GetListOfTicketsForConcreteGroupAsync(string groupId);
        Task<List<TicketsDto>> GetListOfMyTicketsAsync();
        Task<bool> JoinTicketAsync(string userId, string tickedId);
        Task<bool> UndoJoinTicketAsync(string tickedId);
        Task<TicketsDto> GetTicketDetailsAsync(string tickedId);
        Task<ReviewTicktsResponseDto> CreateReviewTicketAsync();
        Task<List<SubscribedTicketDto>> GetListOfSubscribedTicketsAsync();
        Task<bool> DeleteTicketRequstAsync();
        Task<List<CommentsTicketDto>> GetListOfTicketCommentsAsync(string tickedId);
        Task<CommentsTicketDto> WtiteCommentAsync(string ticketId);
        Task<bool> AcceptUserReviewRequestForTicketAsync(string userId, string tickedId);
        Task<bool> DeclineuserReviewRequestForTicketAsync(string userId, string tickedId);
    }
}

