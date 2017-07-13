using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using XPressWPF.ApiService;
using XPressWPF.Main.View;
using XPressWPF.Modules.Department.View;
using XPressWPF.Modules.Department.ViewModel;
using XPressWPF.Modules.Employee.View;
using XPressWPF.Modules.Employee.ViewModel;
using XPressWPF.Shared;
using XPressWPF.Shared.Services.DialogService;
using XPressWPF.Shared.Services.WindowService;

namespace XPressWPF.Main.ViewModel
{
    public class MainMenuListViewModel : ViewModelBase
    {
        private readonly IMessageDialogService _messageDialogService;
        private readonly IWindowService _windowService;

        public MainMenuListViewModel(IMessageDialogService messageDialogService, IWindowService windowService)
        {
            _messageDialogService = messageDialogService;
            _windowService = windowService;

            EmployeeVM = new EmployeeViewModel(new EmployeeApi(), _messageDialogService);
            DepartmentVM = new DepartmentsViewModel(new DepartmentApi(), _messageDialogService);

            ModulesCollection = new ObservableCollection<ViewModelBase>() { EmployeeVM, DepartmentVM };
            CurrentModule = ModulesCollection.First();
        }

        public EmployeeViewModel EmployeeVM { get; set; }
        public DepartmentsViewModel DepartmentVM { get; set; }


        private ObservableCollection<ViewModelBase> _modulesCollection;
        public ObservableCollection<ViewModelBase> ModulesCollection
        {
            get => _modulesCollection;
            set
            {
                if (_modulesCollection != null && _modulesCollection == value) return;
                _modulesCollection = value;
                OnPropertyChanged();
            }
        }

        private ViewModelBase _currentModule;
        public ViewModelBase CurrentModule
        {
            get => _currentModule;
            set
            {
                if (_currentModule == value) return;
                _currentModule = value;

                OnPropertyChanged();
            }
        }

        private ObservableCollection<TabItem> _tabsCollection;
        public ObservableCollection<TabItem> TabsCollection
        {
            get => _tabsCollection;
            set
            {
                _tabsCollection = value;

                OnPropertyChanged();
            }
        }

        private TabItem _currentTab;
        public TabItem CurrentTab
        {
            get => _currentTab;
            set
            {
                if (_currentTab == value) return;

                _currentTab = value;

                OnPropertyChanged();
            }
        }
        
        // TODO Remove this to command to its own viewmodel once the ninject modules are done
        public ICommand CloseTabCommand => new CustomCommand(CanExecuteCloseTabCommand, ExecuteCloseTabCommand);
        private bool CanExecuteCloseTabCommand(object obj)
        {
            return !IsWorking;
        }

        private void ExecuteCloseTabCommand(object obj)
        {
            TabItem tab = (TabItem)obj;
            TabsCollection.Remove(TabsCollection.First(tc => tc.Header == tab.Header));
        }


        #region RefreshUserControl
        public ICommand RefreshUserControl => new CustomCommand(CanExecuteRefreshUserControl, ExecuteRefreshUserControl);
        private bool CanExecuteRefreshUserControl(object obj)
        {
            return !IsWorking;
        }

        // ViewModel should not know about a View
        // change implementation of RefreshUserControl
        private void ExecuteRefreshUserControl(object obj)
        {
            IsWorking = true;
            try
            {
                if (TabsCollection == null || TabsCollection.Count == 0)
                {
                    TabsCollection = new ObservableCollection<TabItem>();
                }

                TabItem newTab = null;
                string viewModelName = CurrentModule.GetType().Name;
                switch (viewModelName)
                {
                    case ("EmployeeViewModel"):
                        {
                            newTab = new TabItem
                            {
                                Content = new EmployeeView() { DataContext = new EmployeeViewModel(new EmployeeApi(), _messageDialogService) },
                                Header = "Employees"
                            };
                            if (TabsCollection.Any(tc => tc.Header == newTab.Header))
                            {
                                CurrentTab = TabsCollection.Where(tc => tc.Header == newTab.Header).First();
                                return;
                            }
                            TabsCollection.Add(newTab);
                            CurrentTab = newTab;
                        }
                        break;
                    case ("DepartmentsViewModel"):
                        {
                            newTab = new TabItem
                            {
                                Content = new DepartmentView() { DataContext = new DepartmentsViewModel(new DepartmentApi(), _messageDialogService) },
                                Header = "Departments"
                            };
                            if (TabsCollection.Any(tc => tc.Header == newTab.Header))
                            {
                                CurrentTab = TabsCollection.First(tc => tc.Header == newTab.Header);
                                return;
                            }
                            TabsCollection.Add(newTab);
                            CurrentTab = newTab;
                        }
                        break;
                }
                OnPropertyChanged();
            }
            finally
            {
                IsWorking = false;
            }
        }
        #endregion


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
    public class TabItem
    {
        public string Header { get; set; }
        public UserControl Content { get; set; }
    }

}