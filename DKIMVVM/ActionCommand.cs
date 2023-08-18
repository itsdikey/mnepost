using System.Windows.Input;

namespace DKIMVVM
{
    public sealed class ActionCommand : ICommand
    {
        private readonly Action<object?> _action;

        public ActionCommand(Action<object?> execute)
        {
            _action = execute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _action?.Invoke(parameter);
        }
    }

    public sealed class ActionCommand<T> : ICommand where T : class 
    {
        private readonly Action<T?> _action;

        public ActionCommand(Action<T?> execute)
        {
            _action = execute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if(parameter == null)
            {
                _action?.Invoke(null);
            }
            else
            {
                _action?.Invoke(parameter as T);
            }
        }
    }

    public sealed class ActionCommandAsync : ICommand
    {
        private readonly Func<Task>? _action;

        public ActionCommandAsync(Func<Task>? execute)
        {
            _action = execute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            if (_action == null)
                return;
            await _action.Invoke();
        }
    }

    public sealed class ActionCommandAsync<T> : ICommand where T : class
    {
        private readonly Func<T?, Task>? _action;

        public ActionCommandAsync(Func<T?, Task>? execute)
        {
            _action = execute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            if (_action == null)
                return;
            if (parameter == null)
            {
                await _action.Invoke(null);
            }
            else
            {
                await _action.Invoke(parameter as T);
            }
        }
    }
}
