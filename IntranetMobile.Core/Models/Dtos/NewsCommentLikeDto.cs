// ReSharper disable InconsistentNaming

namespace IntranetMobile.Core.Models.Dtos
{
    public class NewsCommentLikeDto : Persist
    {
        public string newsId { get; set; }
        public string commentId { get; set; }
    }
}