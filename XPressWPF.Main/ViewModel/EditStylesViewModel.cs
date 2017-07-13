using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using XPressWPF.ApiService;
using XPressWPF.Model;
using XPressWPF.Shared;
using XPressWPF.Shared.Services.DialogService;

namespace XPressWPF.Main.ViewModel
{
    public class EditStylesViewModel : ViewModelBase
    {
        private readonly IUserApi _api;
        private readonly IMessageDialogService _dialogService;

        public EditStylesViewModel(IUserApi api, IMessageDialogService dialogService)
        {
            _api = api;
            _dialogService = dialogService;
        }

        private ObservableCollection<AppStyles> _stylesCollection;
        public ObservableCollection<AppStyles> StylesCollection
        {
            get => _stylesCollection;
            set
            {
                if (_stylesCollection == null) _stylesCollection = new ObservableCollection<AppStyles>();
                _stylesCollection = value;
                OnPropertyChanged();
            }
        }

        private AppStyles _currentStyle;
        public AppStyles CurrentStyle
        {
            get => _currentStyle;
            set
            {
                if (_currentStyle == null) _currentStyle = new AppStyles();
                _currentStyle = value;
                OnPropertyChanged();
            }
        }

        private AppStyles _moddifiedStyle;
        public AppStyles ModdifiedStyle
        {
            get => _moddifiedStyle;
            set
            {
                if (_moddifiedStyle == null) _moddifiedStyle = new AppStyles();
                _moddifiedStyle = value;
                OnPropertyChanged();
            }
        }

        #region LoadCommand
        public ICommand LoadCommand => new CustomCommand(CanExecuteLoad, ExecuteLoad);
        private bool CanExecuteLoad(object obj)
        {
            return !IsWorking;
        }

        private async void ExecuteLoad(object obj)
        {
            IsWorking = true;
            try
            {
                IEnumerable<AppStyles> styles = await _api.GetAllStyles(User.ID);
                if (styles != null && styles.Any(st => st.ID == User.StyleId))
                {
                    CurrentStyle = styles.First(st => st.ID == User.StyleId);
                }
                foreach (var style in styles)
                {
                    StylesCollection.Add(style);
                }
            }

            finally
            {
                IsWorking = false;
            }
        }
        #endregion

        #region SaveChangesCommand
        public ICommand SaveChangesCommand => new CustomCommand(CanSaveChanges, ExecuteSaveChanges);
        private bool CanSaveChanges(object obj)
        {
            return !IsWorking && ModdifiedStyle != null;
        }
        private async void ExecuteSaveChanges(object obj)
        {
            IsWorking = true;
            try
            {
                if (StylesCollection.Any(st => st.StyleName == ModdifiedStyle.StyleName))
                    _dialogService.ShowOkDialog("Invalid style name", "You must specify unique style name.");
                else
                {
                    if(IsNew)
                        await _api.InsertNewStyleAsync(User.ID, CurrentStyle);
                    else
                        await _api.UpdateCurrentStyle(User.ID, CurrentStyle);

                    CurrentStyle = ModdifiedStyle;
                }
            }

            finally
            {
                IsWorking = false;
            }
        }
        #endregion
    }
}
