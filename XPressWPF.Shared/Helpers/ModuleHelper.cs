using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace XPressWPF.Shared.Helpers
{
    public static class ModuleHelper
    {

        public static void AddModuleToCollection<T>(IAvailableModules modules, string userControlName) where T : UserControl, new()
        {
            string userControlQualifiedName = typeof(T).AssemblyQualifiedName;

            if (modules.ListOfModules == null)
                modules.ListOfModules = new List<Module>();

            int? userControlId = modules.ListOfModules.Max(u => (int?)u.Id);
            if (userControlId == null || userControlId == 0)
            {
                userControlId = 1;

                modules.ListOfModules.Add(new Module()
                {
                    Id = userControlId.Value,
                    Name = userControlName,
                    Namespace = userControlQualifiedName
                });
            }
            else
            {
                modules.ListOfModules.Add(new Module()
                {
                    Id = userControlId.Value + 1,
                    Name = userControlName,
                    Namespace = userControlQualifiedName
                });
            }
        }
    }
}
