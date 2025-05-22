namespace SyncfusionMAUIApp1
{
    public partial class App : Application
    {
        public App()
        {
            //Register Syncfusion license https://help.syncfusion.com/common/essential-studio/licensing/how-to-generate
            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR LICENSE KEY");
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}
