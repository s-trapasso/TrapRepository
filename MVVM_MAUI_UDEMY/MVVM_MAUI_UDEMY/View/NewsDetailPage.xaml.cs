using MVVM_MAUI_UDEMY.ViewModel;

namespace MVVM_MAUI_UDEMY.View;

public partial class NewsDetailPage : ContentPage
{
	public NewsDetailPage(NewsDetailViewModel newsDetailViewModel)
	{
		InitializeComponent();
		BindingContext = newsDetailViewModel;
	}
}