//using MyMauiApp.Data.ClassHelpers;

namespace MyMauiApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //var x = new DatabaseHelper();
            //x.DeleteUsers();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}