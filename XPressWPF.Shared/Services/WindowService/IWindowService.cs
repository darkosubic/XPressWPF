using System.Windows.Controls;

namespace XPressWPF.Shared.Services.WindowService
{
    public interface IWindowService
    {
        void ShowFixedSizeWindow<T>(object dataContext, string title = "Please input title") where T : UserControl, new();
        void ShowFullScreenWindow<T>(object dataContext, string title = "Please input title") where T : UserControl, new();
        void ShowWindowAndLetWindowsDecideOnSize<T>(object dataContext, string title = "Please input title") where T : UserControl, new();
    }
}
