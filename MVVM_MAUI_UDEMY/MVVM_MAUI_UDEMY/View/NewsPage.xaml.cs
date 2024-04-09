using MVVM_MAUI_UDEMY.ViewModel;

namespace MVVM_MAUI_UDEMY.View;

public partial class NewsPage : ContentPage
{
	public NewsPage(NewsViewModel newsViewModel)
	{
		InitializeComponent();
		BindingContext = newsViewModel;
	}
}