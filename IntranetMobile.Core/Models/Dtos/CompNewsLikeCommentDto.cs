// ReSharper disable InconsistentNaming

namespace IntranetMobile.Core.Models.Dtos
{
    public class CompNewsLikeCommentDto : Persist
    {
        public string newsId { get; set; }
        public string commentId { get; set; }
    }
}