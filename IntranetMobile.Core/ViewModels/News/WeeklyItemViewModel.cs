using System;
using System.Threading.Tasks;
using System.Windows.Input;
using IntranetMobile.Core.Extensions;
using IntranetMobile.Core.Models;
using IntranetMobile.Core.Services;

namespace IntranetMobile.Core.ViewModels.News
{
    public class WeeklyItemViewModel : BaseViewModel
    {
        private string _authorId;
        private DateTime _date;
        private string _previewImageUri;

        public string WeeklyId { get; set; }
        
        public string AuthorId
        {
            get { return _authorId; }
            set
            {
                _authorId = value;
                GetAuthor();
            }
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                RaisePropertyChanged(() => Date);
                RaisePropertyChanged(() => Subtitle);
            }
        }

        public string PreviewImageUri
        {
            get { return _previewImageUri; }
            set
            {
                _previewImageUri = value;
                RaisePropertyChanged(() => PreviewImageUri);
            }
        }

        public ICommand ClickLikeCommand { get; private set; }
        public ICommand ClickCommentCommand { get; private set; }

        private void GetAuthor()
        {
            Task.Run(async () =>
            {
                var author = await ServiceBus.UserService.GetUserInfoById(AuthorId) ?? new UserInfo();
                Subtitle = $"{author.FullName}     {Date.ToString("dd-MM-yyyy HH:mm")}";

                InvokeOnMainThread(() => RaisePropertyChanged(() => Subtitle));
            });
        }

        public static async Task<WeeklyItemViewModel> FromModel(WeeklyNews news)
        {
            var firstNews = await ServiceBus.NewsService.GetNewsByIdAsync(news.FullNews[0]);

            // TODO: Why do we pass values, not WeeklyNews reference with encapsulated properties?

            return new WeeklyItemViewModel
            {
                PreviewImageUri = firstNews.Body != null ? firstNews.Body.GetFirstImageUri() : "",
                WeeklyId = news.WeeklyId,
                Title = news.Title,
                AuthorId = news.AuthorId,
                Date = news.Date
            };
        }
    }
}