namespace StudentSQLite
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            Window window = base.CreateWindow(activationState);

            const int newWidth = 550;
            const int newHeight = 950;

            window.Width = newWidth;
            window.Height = newHeight;

            return window;
        }
    }
}