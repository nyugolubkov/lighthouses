using System;
using System.Windows.Input;

namespace Маяки.GameWindow
{
    /// <summary>
    /// Класс, реализующий интерфейс ICommand
    /// </summary>
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Func<object, bool> _canExecute;
        private readonly Action<object> _onExecute;

        /// <summary>
        /// Конструктор, инициализирующий компоненты класса
        /// </summary>
        public RelayCommand(Action<object> execute, 
            Func<object, bool> canExecute = null)
        {
            _onExecute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Определяет, может ли данная команда выполняться в ее текущем состоянии
        /// </summary>
        public bool CanExecute(object parameter)
             => _canExecute == null ? true : _canExecute.Invoke(parameter);

        /// <summary>
        /// Вызывает метод, приписанной к данной комманде
        /// </summary>
        public void Execute(object parameter) => _onExecute?.Invoke(parameter);
    }
}
