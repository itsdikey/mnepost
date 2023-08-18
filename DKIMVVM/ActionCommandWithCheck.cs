using System.Windows.Input;

namespace DKIMVVM
{
    public sealed class ActionCommandWithCheck : ICommand
    {
        private readonly Action<object?> _action;
        private readonly Func<object?, bool> _canExecute;

        public ActionCommandWithCheck(Action<object?> execute, Func<object?, bool> canExecute)
        {
            _action = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            if (_canExecute == null)
                return true;
            return _canExecute.Invoke(parameter);
        }

        public void Execute(object? parameter)
        {
            _action?.Invoke(parameter);
        }
    }

    public sealed class ActionCommandWithCheck<T> : ICommand where T : class
    {
        private readonly Action<T?> _action;
        private readonly Func<object?, bool> _canExecute;

        public ActionCommandWithCheck(Action<T?> execute, Func<object?, bool> canExecute)
        {
            _action = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            if (_canExecute == null)
                return true;
            return _canExecute.Invoke(parameter);
        }

        public void Execute(object? parameter)
        {
            if (parameter == null)
            {
                _action?.Invoke(null);
            }
            else
            {
                _action?.Invoke(parameter as T);
            }
        }
    }

    public sealed class ActionCommandWithCheckAsync : ICommand
    {
        private readonly Func<Task>? _action;
        private readonly Func<object?, bool> _canExecute;

        public ActionCommandWithCheckAsync(Func<Task>? execute, Func<object?, bool> canExecute)
        {
            _action = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            if (_canExecute == null)
                return true;
            return _canExecute.Invoke(parameter);
        }

        public async void Execute(object? parameter)
        {
            if (_action == null)
                return;
            await _action.Invoke();
        }
    }

    public sealed class ActionCommandWithCheckAsync<T> : ICommand where T : class
    {
        private readonly Func<T?, Task>? _action;
        private readonly Func<object?, bool> _canExecute;

        public ActionCommandWithCheckAsync(Func<T?, Task>? execute, Func<object?, bool> canExecute)
        {
            _action = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            if (_canExecute == null)
                return true;
            return _canExecute.Invoke(parameter);
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
