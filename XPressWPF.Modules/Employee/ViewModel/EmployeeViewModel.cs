using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using XPressWPF.ApiService;
using XPressWPF.Model;
using XPressWPF.Model.Wrapper;
using XPressWPF.Shared;
using XPressWPF.Shared.Services.DialogService;

namespace XPressWPF.Modules.Employee.ViewModel
{
    public class EmployeeViewModel : ViewModelBase
    {
        private readonly IEmployeeApi _api;
        private IMessageDialogService _messageDialog;
        public EmployeeViewModel(IEmployeeApi api, IMessageDialogService messageDialog)
        {
            _api = api;
            _messageDialog = messageDialog;
            ExecuteRefresh(new object());
        }


        public string Title => Labels.MDL_Employees;

        private ObservableCollection<EmployeeModelWrapper> _employees;
        public ObservableCollection<EmployeeModelWrapper> Employees
        {
            get
            {
                if (_employees == null)
                    _employees = new ObservableCollection<EmployeeModelWrapper>();
                return _employees;
            }
            set
            {
                if (_employees != null && _employees == value) return;
                _employees = value;
            }
        }

        private EmployeeModelWrapper _currentEmployee;

        public EmployeeModelWrapper CurrentEmployee
        {
            get { return _currentEmployee; }
            set
            {
                if (_currentEmployee == value) return;
                _currentEmployee = value;
                OnPropertyChanged();
            }
        }

        #region RefreshEmployeesCommand
        public ICommand RefreshEmployeesCommand => new CustomCommand(CanExecuteRefresh, ExecuteRefresh);
        private bool CanExecuteRefresh(object obj)
        {
            return !IsWorking;
        }

        private async void ExecuteRefresh(object obj)
        {
            IsWorking = true;
            try
            {
                var employees = await _api.GetAllEmployees();

                if (Employees != null)
                    Employees.Clear();

                foreach (var employee in employees)
                {
                    Employees.Add(new EmployeeModelWrapper(employee));
                }

                CurrentEmployee = new EmployeeModelWrapper(new EmployeeModel());

            }
            finally
            {
                IsWorking = false;
                OnPropertyChanged();
            }
        }
        #endregion


        //TODO: create new view for adding a new employee
        #region AddNewEmployeeCommand
        public ICommand AddNewEmployeeCommand => new CustomCommand(CanAddNewEmployee, ExecuteAddNewEmployee);
        private bool CanAddNewEmployee(object obj)
        {
            return false;
        }

        private async void ExecuteAddNewEmployee(object obj)
        {
            IsWorking = true;
            try
            {
                var newEmployee = _messageDialog.ShowYesNoDialog("Comfirm new employee", "Do you wish to create a new Employee");
                if (newEmployee == MessageDialogResult.Yes)
                {
                    await _api.InsertEmployee(CurrentEmployee.Model);
                    Employees.Insert(0, CurrentEmployee);
                }
            }
            finally
            {
                IsWorking = false;
            }
        }
        #endregion

        #region UpdateEmployeeCommand
        public ICommand UpdateEmployeeCommand => new CustomCommand(CanUpdateEmployee, ExecuteUpdateEmployeee);

        private bool CanUpdateEmployee(object obj)
        {
            return !IsWorking && CurrentEmployee != null && CurrentEmployee?.Id != 0 && Employees.Any(x => x.Id == CurrentEmployee?.Id) && CurrentEmployee.IsChanged;
        }

        private async void ExecuteUpdateEmployeee(object obj)
        {
            IsWorking = true;
            try
            {
                var updateEmployee = _messageDialog.ShowYesNoDialog("Comfirm update", $"Do you wish to update Employee {CurrentEmployee.Id}");
                if (updateEmployee == MessageDialogResult.Yes)
                {
                    await _api.UpdateEmployee(CurrentEmployee.Model);
                }
            }
            finally
            {
                IsWorking = false;
            }
        }
        #endregion

        #region DeleteEmployeeCommand
        public ICommand DeleteEmployeeCommand => new CustomCommand(CanDeleteEmployee, ExecuteDeleteEmployeee);

        private bool CanDeleteEmployee(object obj)
        {
            return !IsWorking && CurrentEmployee != null && CurrentEmployee.Id != 0 && Employees.Any(x => x.Id == CurrentEmployee.Id);
        }

        private async void ExecuteDeleteEmployeee(object obj)
        {
            IsWorking = true;
            try
            {
                var deleteEmployee = _messageDialog.ShowYesNoDialog("Comfirm delete", $"Do you wish to delete Employee {CurrentEmployee.Id}");
                if (deleteEmployee == MessageDialogResult.Yes)
                {
                    await _api.DeleteEmployee(CurrentEmployee.Id);
                    EmployeeModelWrapper employeeToRemove = Employees.First(x => x.Id == CurrentEmployee.Id);

                    // Removing employee from the list and collection
                    Employees.Remove(employeeToRemove);

                    // there are no selected employees
                    CurrentEmployee = null;
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
