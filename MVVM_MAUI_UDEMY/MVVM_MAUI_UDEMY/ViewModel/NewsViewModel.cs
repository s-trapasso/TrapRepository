using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MVVM_MAUI_UDEMY.Model;
using MVVM_MAUI_UDEMY.Services;
using MVVM_MAUI_UDEMY.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_MAUI_UDEMY.ViewModel
{
    public partial class NewsViewModel : ObservableObject
    {
        private MockNewsService newsService;
        public ObservableCollection<News> newsCollection { get; set; } = new ObservableCollection<News>();
        public NewsViewModel(MockNewsService newsService)
        {
            this.newsService = newsService;
            GetNewsList();
        }

        [ObservableProperty]
        News selectedNews;
        private void GetNewsList()
        {
             var news = newsService.GetNews();
            foreach(var item in news)
            {
                newsCollection.Add(item);
            }
        }

        [RelayCommand]
        void GoToDetails()
        {
            Shell.Current.GoToAsync($"{nameof(NewsDetailPage)}",
                new Dictionary<string,object>
            {
                { "News", SelectedNews}
            });
        }
    }

}
