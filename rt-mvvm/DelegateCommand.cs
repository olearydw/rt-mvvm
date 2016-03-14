using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace rt_mvvm
{
    class DelegateCommand : System.Windows.Input.ICommand
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecuteFunc = null)
        {
            this.canExecute = canExecuteFunc;
            this.execute = executeAction;
        }

        public bool CanExecute(object parameter)
        {
            if (this.canExecute == null) { return true;  }
            return this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
