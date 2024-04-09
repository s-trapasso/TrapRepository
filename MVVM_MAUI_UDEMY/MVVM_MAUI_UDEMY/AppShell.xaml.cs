using MVVM_MAUI_UDEMY.View;

namespace MVVM_MAUI_UDEMY
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NewsDetailPage),typeof(NewsDetailPage));
        }
    }
}
