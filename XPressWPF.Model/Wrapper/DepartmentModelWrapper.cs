using System.Runtime.CompilerServices;
using XPressWPF.Shared;

namespace XPressWPF.Model.Wrapper
{
    public class DepartmentModelWrapper : ViewModelBase
    {
        private readonly DepartmentModel _department;

        public DepartmentModelWrapper(DepartmentModel department)
        {
            _department = department;
        }

        public DepartmentModel Model { get { return _department; } }

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

        public string Name
        {
            get { return Model.Name; }
            set
            {
                Model.Name = value;
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
