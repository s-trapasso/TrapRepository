using Serilog;

namespace CarDesk
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                Log.Fatal(e.ExceptionObject as Exception, "UNHANDLED EXCEPTION");
            };

            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                Log.Fatal(e.Exception, "UNOBSERVED TASK EXCEPTION");
                e.SetObserved();
            };
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new MainPage()) { Title = "CarDesk" };
        }
    }
}
