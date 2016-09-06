using System.Collections.Generic;
using System.Threading.Tasks;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Models.Dtos;
using IntranetMobile.Core.Services;

namespace IntranetMobile.Core.Interfaces
{
    public interface IReviewerService
    {
        Task<List<Ticket>> GetListOfTicketsAsync();
        Task<List<Ticket>> GetListOfTicketsForGroupAsync(ReviewerGroup group);
        Task<List<Ticket>> GetListOfMyTicketsAsync();
        Task<bool> JoinTicketAsync(string userId, string ticketId);
        Task<bool> UndoJoinTicketAsync(string ticketId);
        Task<Ticket> GetTicketDetailsAsync(string ticketId);
        Task<bool> CreateReviewTicketAsync(ReviewTicketRequestDto reviewTicketRequestDto);
        Task<List<SubscribedTicket>> GetListOfSubscribedTicketsAsync();
        Task<bool> DeleteTicketAsync(string id);
        Task<List<Comment>> GetListOfTicketCommentsAsync(string ticketId);
        Task<bool> WtiteCommentAsync(string ticketId, string text);
        Task<bool> AcceptUserReviewForTicketAsync(string userId, string ticketId);
        Task<bool> DeclineUserReviewForTicketAsync(string userId, string ticketId);
    }
}