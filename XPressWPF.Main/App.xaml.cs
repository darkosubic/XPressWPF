using Ninject;
using System.Threading.Tasks;
using System.Windows;
using XPressWPF.ApiService;
using XPressWPF.Main.ViewModel;
using XPressWPF.Shared.Services.DialogService;
using XPressWPF.Shared.Services.WindowService;

namespace XPressWPF.Main
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel _container;

        public App()
        {
        }

        protected async override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //TODO: create ninject modules

            ConfigureContainer();

            await OpenFacebookWindowAsync();

            //Todo : Create User table, Create Style table, Allow only registered users to access app, apply styling to user interface
            ComposeObjects();

            //Thread.Sleep(10000);

            Current.MainWindow.Show();
        }

        public TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

        private async Task OpenFacebookWindowAsync()
        {
            var facebookWindowInstance = new XPressWPF.Facebook.MainWindow(tcs);
            Current.MainWindow = facebookWindowInstance;
            Current.MainWindow.Show();
            await facebookWindowInstance._tcs.Task;
            Current.MainWindow.Close();
        }

        private void ConfigureContainer()
        {
            this._container = new StandardKernel();
            _container.Bind<IMessageDialogService>().To<MessageDialogService>().InTransientScope();
            _container.Bind<IWindowService>().To<WindowService>().InTransientScope();
            _container.Bind<IEmployeeApi>().To<EmployeeApi>().InTransientScope();
            _container.Bind<IDepartmentApi>().To<DepartmentApi>().InTransientScope();
            _container.Bind<MainWindow>().ToSelf();
        }

        private void ComposeObjects()
        {
            Current.MainWindow = _container.Get<MainWindow>();
            Current.MainWindow.DataContext = new MainMenuListViewModel(_container.Get<MessageDialogService>(), _container.Get<WindowService>());
            Current.MainWindow.Title = "Main Window";
        }
    }
}
