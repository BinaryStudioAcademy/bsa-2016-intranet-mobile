using System.Collections.Generic;
using System.Threading.Tasks;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Models.Dtos;

namespace IntranetMobile.Core.Interfaces
{
    public interface IReviewerService
    {
        Task<List<TicketDto>> GetListOfTicketsAsync();
        Task<List<TicketDto>> GetListOfTicketsForConcreteGroupAsync(string groupId);
        Task<List<TicketDto>> GetListOfMyTicketsAsync();
        Task<bool> JoinTicketAsync(string userId, string ticketId);
        Task<bool> UndoJoinTicketAsync(string ticketId);
        Task<Ticket> GetTicketDetailsAsync(string ticketId);
        Task<TicketDto> CreateReviewTicketAsync(ReviewTicketRequestDto reviewTicketRequestDto);
        Task<List<SubscribedTicketDto>> GetListOfSubscribedTicketsAsync();
        Task<bool> DeleteTicketAsync(string id);
        Task<List<Comment>> GetListOfTicketCommentsAsync(string ticketId);
        Task<TicketCommentDto> WtiteCommentAsync(string ticketId, string text);
        Task<bool> AcceptUserReviewForTicketAsync(string userId, string ticketId);
        Task<bool> DeclineUserReviewForTicketAsync(string userId, string ticketId);
    }
}