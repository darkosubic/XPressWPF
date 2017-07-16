using System.Collections.Generic;

namespace XPressWPF.Shared.Helpers
{
    public interface IAvailableModules
    {
        List<Module> ListOfModules { get; set; }
    }
    public class AvailableModules : IAvailableModules
    {
        private List<Module> _listOfModules;
        public List<Module> ListOfModules
        {
            get => _listOfModules;
            set
            {
                if (_listOfModules != null && _listOfModules == value) return;
                if (_listOfModules == null) _listOfModules = new List<Module>();
                _listOfModules = value;
            }
        }
    }
}
