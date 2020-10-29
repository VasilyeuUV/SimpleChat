using System;
using System.Windows.Input;

namespace Vasilev.SimpleChat.WpfNetCore.Client.Infrastructure.Commands.Base
{
    /// <summary>
    /// Базовый класс команды
    /// </summary>
    internal abstract class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged    // событие, когда команда переходит из одного состояния в другое
        {                                              // реализуем событие явно для передачи этого события под управление WPF
            add => CommandManager.RequerySuggested += value;    // подписываемся на обработчик события
            remove => CommandManager.RequerySuggested -= value; // отписываемся
        }

        /// <summary>
        /// Можно или нет выполнить команду
        /// (включает/выключает визуальный элемент с командой)
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public abstract bool CanExecute(object parameter);          // реализацией займется наследник


        /// <summary>
        /// ТО, что должно быть выполнено командой (основная логика команды)
        /// </summary>
        /// <param name="parameter"></param>
        public abstract void Execute(object parameter);
    }
}
