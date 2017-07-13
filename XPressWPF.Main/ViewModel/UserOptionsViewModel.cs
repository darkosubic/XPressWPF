using System.Collections.ObjectModel;
using System.Windows.Controls;
using XPressWPF.Main.View;
using XPressWPF.Shared;
using XPressWPF.Shared.Services.WindowService;

namespace XPressWPF.Main.ViewModel
{
    public class UserOptionsViewModel : ViewModelBase
    {
        private readonly IWindowService _windowService;

        public UserOptionsViewModel(IWindowService windowService)
        {
            _windowService = windowService;
        }

        private ObservableCollection<ListBoxItem> _userOptions;

        public ObservableCollection<ListBoxItem> UserOptions
        {
            get => _userOptions;
            set
            {
                if (_userOptions != null && _userOptions == value) return;
                _userOptions = value;
                OnPropertyChanged();
            }
        }

        private ListBoxItem _selectedOption;
        public ListBoxItem SelectedOption
        {
            get => _selectedOption;
            set
            {
                if (_selectedOption == value) return;
                _selectedOption = value;

                OpenNewView();
            }
        }

        private void OpenNewView()
        {
            _windowService.ShowFullScreenWindow<EditStylesView>(this);
        }
    }
}
