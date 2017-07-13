using System.Windows;
using System.Windows.Controls;

namespace XPressWPF.Shared.Services.WindowService
{
    public class WindowService : IWindowService
    {
        public void ShowFixedSizeWindow<T>(object dataContext, string title ) where T : UserControl, new()
        {
            var window = PrepareWindow<T>(dataContext, title);
            window.Width = Settings.Default.DefalutWindowWidth;
            window.Height = Settings.Default.DefalutWindowHeight;
            window.Show();
        }

        public void ShowFullScreenWindow<T>(object dataContext, string title = "Please input title") where T : UserControl, new()
        {
            var window = PrepareWindow<T>(dataContext, title);

            // TODO: find a better sollution
            // that includes multi screen solution 
            window.WindowState = WindowState.Maximized;
            window.Show();
        }

        // Window width and height are read from user control
        public void ShowWindowAndLetWindowsDecideOnSize<T>(object dataContext, string title = "Please input title") where T : UserControl, new()
        {
            //var window = PrepareWindow<T>(dataContext);
            var window = PrepareWindow<T>(dataContext, title);
            window.Show();
        }

        private static Window PrepareWindow<T>(object dataContext, string title) where T : UserControl, new()
        {
            Window window = new Window()
            {
                Title = title,
                Content = new T(),
                DataContext = dataContext
            };
            return window;
        }

        //private T PrepareWindow<T>(object dataContext) where T : UserControl, new()
        //{
        //    Window window = new Window()
        //    {
        //        Title = "My User Control Dialog",
        //        Content = new T(),
        //        DataContext = dataContext
        //    };
        //    return window;
        //    //return new T() { DataContext = dataContext };
        //}
    }
}
