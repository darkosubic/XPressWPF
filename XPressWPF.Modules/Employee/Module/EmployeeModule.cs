using Ninject;
using Ninject.Modules;
using XPressWPF.Modules.Employee.View;
using XPressWPF.Modules.Employee.ViewModel;
using XPressWPF.Shared;
using XPressWPF.Shared.Helpers;

namespace XPressWPF.Modules.Employee.Module
{
    public class EmployeeModule : NinjectModule
    {
        public override void Load()
        {
            SetBindings();
            AddUserControlToCollectionOfModules();
        }

        private void AddUserControlToCollectionOfModules()
        {
            IAvailableModules modules = this.Kernel.Get<IAvailableModules>();
            
            ModuleHelper.AddModuleToCollection<EmployeeView>(modules, Labels.MDL_Employees);
        }

        private void SetBindings()
        {
            this.Bind<EmployeeViewModel>().ToSelf();
            this.Bind<EmployeeView>()
                .ToMethod(context => 
                new EmployeeView
                {
                    DataContext = this.Kernel.Get<EmployeeViewModel>()
                });
        }
    }
}
