using Ninject;
using Ninject.Modules;
using XPressWPF.Modules.Department.View;
using XPressWPF.Modules.Department.ViewModel;
using XPressWPF.Shared;
using XPressWPF.Shared.Helpers;

namespace XPressWPF.Modules.Department.Module
{
    public class DepartmentModule : NinjectModule
    {
        public override void Load()
        {
            SetBindings();
            AddUserControlToCollectionOfModules();
        }

        private void AddUserControlToCollectionOfModules()
        {
            IAvailableModules modules = this.Kernel.Get<IAvailableModules>();

            ModuleHelper.AddModuleToCollection<DepartmentView>(modules, Labels.MDL_Departments);
        }


        private void SetBindings()
        {
            this.Bind<DepartmentsViewModel>().ToSelf();
            this.Bind<DepartmentView>()
                .ToMethod(context => new DepartmentView
                {
                    DataContext = this.Kernel.Get<DepartmentsViewModel>()
                });
        }
    }
}
