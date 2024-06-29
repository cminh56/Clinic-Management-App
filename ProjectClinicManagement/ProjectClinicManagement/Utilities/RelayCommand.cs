using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectClinicManagement.Utilities
{
    public class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private Action<object> _Excute { get; set; }
        private readonly Func<object, bool> _canExecute;


        public RelayCommand(Action<object> ExcuteMethod, Func<object, bool> canExecute = null)
        {

            _Excute = ExcuteMethod;
            _canExecute = canExecute;

        }



        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
        public void Execute(object? parameter)
        {
            _Excute(parameter);
        }
    }
}
