using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using XPressWPF.Main.View;
using XPressWPF.Shared;
using XPressWPF.Shared.Helpers;
using XPressWPF.Shared.Services.DialogService;
using XPressWPF.Shared.Services.WindowService;

namespace XPressWPF.Main.ViewModel
{
    public class MainMenuListViewModel : ViewModelBase
    {
        private readonly IMessageDialogService _messageDialogService;
        private readonly IWindowService _windowService;
        private readonly IKernel _kernel;

        public MainMenuListViewModel(IMessageDialogService messageDialogService, IWindowService windowService, IKernel kernel, IAvailableModules availableModules)
        {
            _messageDialogService = messageDialogService;
            _windowService = windowService;
            _kernel = kernel;
            
            ModulesCollection = new ObservableCollection<Module>();
        }



        private ObservableCollection<Module> _modulesCollection;
        public ObservableCollection<Module> ModulesCollection
        {
            get => _modulesCollection;
            private set
            {
                if (_modulesCollection != null && _modulesCollection == value) return;
                if (_modulesCollection == null)
                {
                    _modulesCollection = new ObservableCollection<Module>();
                    List<Module> modules = _kernel.Get<IAvailableModules>().ListOfModules;
                    _modulesCollection = modules.AddToNewObservableCollection();
                    CurrentModule = _modulesCollection.FirstOrDefault();

                    OnPropertyChanged();
                }
                else
                {
                    _modulesCollection = value;
                    OnPropertyChanged();
                }
            }
        }

        private Module _currentModule;
        public Module CurrentModule
        {
            get => _currentModule;
            set
            {
                if (_currentModule == value) return;
                _currentModule = value;

                OnPropertyChanged();
            }
        }

        private ObservableCollection<XPressWPF.Shared.TabItem> _tabsCollection;
        public ObservableCollection<XPressWPF.Shared.TabItem> TabsCollection
        {
            get => _tabsCollection;
            set
            {
                _tabsCollection = value;

                OnPropertyChanged();
            }
        }

        private XPressWPF.Shared.TabItem _currentTab;
        public XPressWPF.Shared.TabItem CurrentTab
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
            XPressWPF.Shared.TabItem tab = (XPressWPF.Shared.TabItem)obj;
            TabsCollection.Remove(TabsCollection.First(tc => tc.Header == tab.Header));
        }


        #region RefreshUserControl
        public ICommand RefreshUserControl => new CustomCommand(CanExecuteRefreshUserControl, ExecuteRefreshUserControl);
        private bool CanExecuteRefreshUserControl(object obj)
        {
            return !IsWorking;
        }
        
        private void ExecuteRefreshUserControl(object obj)
        {
            IsWorking = true;
            try
            {
                Module module = (Module)obj;

                if (TabsCollection == null || TabsCollection.Count == 0)
                {
                    TabsCollection = new ObservableCollection<XPressWPF.Shared.TabItem>();
                }
                if (TabsCollection.Any(tc => tc.Id == module.Id))
                {
                    CurrentTab = TabsCollection.First(tc => tc.Id == module.Id);
                    return;
                }

                Type view = Type.GetType(module.Namespace);
                
                Shared.TabItem newTab = new Shared.TabItem
                {
                    Id = module.Id,
                    Content = _kernel.Get(view),
                    Header = module.Name
                };
                
                TabsCollection.Add(newTab);
                CurrentTab = newTab;

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
}