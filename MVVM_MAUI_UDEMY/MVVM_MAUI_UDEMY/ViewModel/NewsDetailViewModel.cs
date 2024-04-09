using CommunityToolkit.Mvvm.ComponentModel;
using MVVM_MAUI_UDEMY.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_MAUI_UDEMY.ViewModel
{
    [QueryProperty(nameof(News),"News")]
    public partial class NewsDetailViewModel : ObservableObject
    {
        [ObservableProperty]
        News news;
    }
}
