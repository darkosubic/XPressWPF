using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XPressWPF.Main.ViewModel;
using XPressWPF.Shared.Services.WindowService;

namespace XPressWPF.Main.View
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : UserControl
    {
        public MenuView()
        {
            InitializeComponent();
        }
        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            PopUpMenu1.IsOpen = PopUpMenu1.IsOpen != true;
        }

        public bool PopUpMenu
        {
            get { return PopUpMenu1.IsOpen; }
            set { PopUpMenu1.IsOpen = value; }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            WindowService windowService = new WindowService();
            UserOptionsViewModel vm = new UserOptionsViewModel(windowService);
            windowService.ShowWindowAndLetWindowsDecideOnSize<EditStylesView>(vm);
            PopUpMenu1.IsOpen = false;
        }
    }
}
