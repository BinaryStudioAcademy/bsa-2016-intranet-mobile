using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntranetMobile.Core.Interfaces
{
    public interface IReviewerService
    {
        Task<List<TicketsDto>> GetListOfTicketsAsync();
        Task<List<TicketsDto>> GetListOfTicketsForConcreteGroupAsync(string groupId);
        Task<List<TicketsDto>> GetListOfMyTicketsAsync();
        Task<bool> JoinTicketAsync(string userId, string ticketId);
        Task<bool> UndoJoinTicketAsync(string ticketId);
        Task<TicketsDto> GetTicketDetailsAsync(string ticketId);
        Task<TicketsDto> CreateReviewTicketAsync(ReviewTicketRequestDto reviewTicketRequestDto);
        Task<List<SubscribedTicketDto>> GetListOfSubscribedTicketsAsync();
        Task<bool> DeleteTicketAsync(string id);
        Task<List<CommentTicketDto>> GetListOfTicketCommentsAsync(string ticketId);
        Task<CommentTicketDto> WtiteCommentAsync(string ticketId, string text);
        Task<bool> AcceptUserReviewForTicketAsync(string userId, string ticketId);
        Task<bool> DeclineUserReviewForTicketAsync(string userId, string ticketId);
    }
}