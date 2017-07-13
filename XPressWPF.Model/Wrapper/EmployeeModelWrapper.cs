using System.Runtime.CompilerServices;
using XPressWPF.Shared;

namespace XPressWPF.Model.Wrapper
{
    public class EmployeeModelWrapper : ViewModelBase
    {
        private readonly EmployeeModel _employee;

        public EmployeeModelWrapper(EmployeeModel employee)
        {
            _employee = employee;
        }

        public EmployeeModel Model { get { return _employee; } }

        private bool _isChanged;
        public bool IsChanged
        {
            get { return _isChanged; }
            private set { _isChanged = value; OnPropertyChanged(); }
        }

        public void AcceptChanges()
        {
            IsChanged = false;
        }

        public int Id => Model.Id;

        public string FirstName
        {
            get { return Model.FirstName; }
            set
            {
                Model.FirstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return Model.LastName; }
            set
            {
                Model.LastName = value;
                OnPropertyChanged();
            }
        }

        public int? Age
        {
            get { return Model.Age; }
            set
            {
                Model.Age = value;
                OnPropertyChanged();
            }
        }

        public double? Salary
        {
            get { return Model.Salary; }
            set
            {
                Model.Salary = value;
                OnPropertyChanged();
            }
        }

        public int? DepartmentId
        {
            get { return Model.DepartmentId; }
            set
            {
                Model.DepartmentId = value;
                OnPropertyChanged();
            }
        }

        public string DepartmentName
        {
            get { return Model.DepartmentName; }
            set
            {
                Model.DepartmentName = value;
                OnPropertyChanged();
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName != nameof(IsChanged))
            {
                IsChanged = true;
            }
        }
    }
}
