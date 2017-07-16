using Moq;
using XPressWPF.ApiService;
using XPressWPF.Model;
using XPressWPF.Model.Wrapper;
using XPressWPF.Modules.Employee.ViewModel;
using XPressWPF.Modules.Tests.Helpers;
using XPressWPF.Shared.Services.DialogService;
using Xunit;

namespace XPressWPF.Modules.Tests.ViewModel
{
    public class EmployeeViewModelTests
    {
        private Mock<IEmployeeApi> _apiMock;
        private Mock<IMessageDialogService> _messsageDialogMock;
        private EmployeeViewModel _vm;

        public EmployeeViewModelTests()
        {
            _apiMock = new Mock<IEmployeeApi>();
            _messsageDialogMock = new Mock<IMessageDialogService>();

            _vm = new EmployeeViewModel(_apiMock.Object, _messsageDialogMock.Object);
        }

        [Fact]
        public void ShouldBeAbleToExecuteRefresh()
        {
            _vm.IsWorking = false;
            bool canExecute = _vm.RefreshEmployeesCommand.CanExecute(null);

            Assert.True(canExecute);
        }

        [Fact]
        public void ShouldNotBeAbleToExecuteRefresh()
        {
            _vm.IsWorking = true;
            bool canExecute = _vm.RefreshEmployeesCommand.CanExecute(null);

            Assert.False(canExecute);
        }

        
        [Fact]
        public void ShouldSetIsWorkingFalseAtTheEndOfTheMethod()
        {
            _vm.RefreshEmployeesCommand.Execute(null);
            Assert.False(_vm.IsWorking);
        }

        [Fact]
        public void ShouldPopulateEmployeesAfterRefresh()
        {
            _vm.RefreshEmployeesCommand.Execute(null);
            Assert.NotNull(_vm.Employees);
        }

        [Fact]
        public void ShouldRaisePropertyChangedEventAfterRefreshingEmployees()
        {
            bool isRaises = _vm.IsPropertyChangedRaised(() =>
            {
                _vm.RefreshEmployeesCommand.Execute(null);
            }, nameof(_vm.CurrentEmployee));

            Assert.True(isRaises);
        }

        [Fact]
        public void ShouldCallApiServiceGetAllMethod()
        {
            _vm.RefreshEmployeesCommand.Execute(null);

            // Method is called twice because 
            // constuctor also calls this method
            _apiMock.Verify(ap => ap.GetAllEmployees(), Times.Exactly(2));
        }

        [Fact]
        public void ShouldRaisePropertyChangedEventSelectingDifferentEmployee()
        {
            bool isRaises = _vm.IsPropertyChangedRaised(() =>
            {
                _vm.CurrentEmployee = new EmployeeModelWrapper(new EmployeeModel());
            }, nameof(_vm.CurrentEmployee));

            Assert.True(isRaises);
        }

        

    }
}
