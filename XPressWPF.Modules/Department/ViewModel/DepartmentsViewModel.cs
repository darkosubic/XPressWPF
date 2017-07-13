using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using XPressWPF.ApiService;
using XPressWPF.Model;
using XPressWPF.Model.Wrapper;
using XPressWPF.Shared;
using XPressWPF.Shared.Services.DialogService;

namespace XPressWPF.Modules.Department.ViewModel
{
    public class DepartmentsViewModel : ViewModelBase
    {
        private readonly IDepartmentApi _api;
        private readonly IMessageDialogService _messageDialogService;

        public DepartmentsViewModel(IDepartmentApi api, IMessageDialogService messageDialogService)
        {
            _api = api;
            _messageDialogService = messageDialogService;

            ExecuteRefreshDepartments(new object());
        }

        public string Title => Labels.MDL_Departments;

        private ObservableCollection<DepartmentModelWrapper> _departments;

        public ObservableCollection<DepartmentModelWrapper> Departments
        {
            get
            {
                if (_departments == null)
                    _departments = new ObservableCollection<DepartmentModelWrapper>();
                return _departments;
            }
            set
            {
                if (_departments != null && _departments == value) return;
                _departments = value;
            }
        }

        private DepartmentModelWrapper _currentDepartment;
        public DepartmentModelWrapper CurrentDepartment
        {
            get { return _currentDepartment; }
            set
            {
                if (_currentDepartment == value) return;
                _currentDepartment = value;

                OnPropertyChanged();
            }
        }

        #region RefreshDepartmentsCommand

        public ICommand RefreshDepartmentsCommand => new CustomCommand(CanExecuteRefreshDepartments, ExecuteRefreshDepartments);

        private bool CanExecuteRefreshDepartments(object obj)
        {
            return !IsWorking;
        }

        private async void ExecuteRefreshDepartments(object obj)
        {
            IsWorking = true;
            try
            {
                var departmens = await _api.GetAllDepartments();

                if (Departments != null)
                    Departments.Clear();

                foreach (var department in departmens)
                {
                    Departments.Add(new DepartmentModelWrapper(department));
                }

                CurrentDepartment = new DepartmentModelWrapper(new DepartmentModel());
                OnPropertyChanged();
            }
            finally
            {
                IsWorking = false;
            }
        }

        #endregion

        // TODO: add new view to add a new department 
        #region AddNewDepartmentCommand

        public ICommand AddNewDepartmentCommand => new CustomCommand(CanAddNewDepartment, ExecuteAddNewDepartment);

        private bool CanAddNewDepartment(object obj)
        {
            //if (Departments.Any(x => x.Id == CurrentDepartment?.Id) == true)
            //    return false;
            //return !IsWorking && CurrentDepartment != null && CurrentDepartment?.Id != 0 &&
            //       Departments.Any(x => CurrentDepartment?.Id == x.Id) == false;
            return false;
        }

        private async void ExecuteAddNewDepartment(object obj)
        {
            IsWorking = true;
            try
            {
                var newDepartment = _messageDialogService.ShowYesNoDialog("Comfirm new Department", "Do you wish to create a new Department");
                if (newDepartment == MessageDialogResult.Yes)
                {                    
                    await _api.InsertDepartment(CurrentDepartment.Model);

                    Departments.Insert(0, CurrentDepartment);
                }
            }
            finally
            {
                IsWorking = false;
            }
        }

        #endregion

        #region UpdateDepartmentCommand

        public ICommand UpdateDepartmentCommand => new CustomCommand(CanUpdateDepartment, ExecuteUpdateDepartment);

        private bool CanUpdateDepartment(object obj)
        {
            return !IsWorking && CurrentDepartment != null && CurrentDepartment?.Id != 0 &&
                   Departments.Any(x => x.Id == CurrentDepartment?.Id);
        }

        private async void ExecuteUpdateDepartment(object obj)
        {
            IsWorking = true;
            try
            {
                var updateEDepartment = _messageDialogService.ShowYesNoDialog("Comfirm update Department", $"Do you wish to update Department {CurrentDepartment.Id}");
                if (updateEDepartment == MessageDialogResult.Yes)
                {
                    await _api.UpdateDepartment(CurrentDepartment.Model);

                    // Retrieving Department who will be updated
                    DepartmentModelWrapper departmentForUpdate = Departments.FirstOrDefault(x => x.Id == CurrentDepartment?.Id);

                    // Removing Department from the Grid
                    Departments.Remove(departmentForUpdate);

                    // Assigning new values to the old Department
                    CurrentDepartment = departmentForUpdate;

                    // Inserting updated Department 
                    // to the first plece in the Grid
                    Departments.Insert(0, departmentForUpdate);
                }
            }
            finally
            {
                IsWorking = false;
            }

        }

        #endregion

        #region DeleteDepartmentCommand

        public ICommand DeleteDepartmentCommand => new CustomCommand(CanDeleteDepartment, ExecuteDeleteDepartment);

        private bool CanDeleteDepartment(object obj)
        {
            return !IsWorking && CurrentDepartment != null && CurrentDepartment.Id != 0 &&
                   Departments.Any(x => x.Id == CurrentDepartment.Id);
        }

        private async void ExecuteDeleteDepartment(object obj)
        {
            IsWorking = true;
            try
            {
                var deleteEDepartment = _messageDialogService.ShowYesNoDialog("Comfirm delete Department", $"Do you wish to delete Department {CurrentDepartment.Id}");
                if (deleteEDepartment == MessageDialogResult.Yes)
                {
                    await _api.DeleteDepartment(CurrentDepartment.Id);
                    DepartmentModelWrapper departmentToRemove = Departments.FirstOrDefault(x => x.Id == CurrentDepartment.Id);

                    // Removing department from the list and collection
                    Departments.Remove(departmentToRemove);

                    CurrentDepartment = Departments.FirstOrDefault();
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
