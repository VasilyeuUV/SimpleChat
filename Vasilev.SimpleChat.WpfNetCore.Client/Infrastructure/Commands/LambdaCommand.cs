using System;
using Vasilev.SimpleChat.WpfNetCore.Client.Infrastructure.Commands.Base;

namespace Vasilev.SimpleChat.WpfNetCore.Client.Infrastructure.Commands
{
    /// <summary>
    /// Главный класс команды
    /// </summary>
    internal class LambdaCommand : CommandBase
    {
        private readonly Action<object> _execute;           // системный делегат, с ним сообщен метод, который будет выполняться когда будет срабатывать команда
        private readonly Func<object, bool> _canExecute;    // системный делегат, с ним сообщен метод, который будет выполнять проверку: может ли команды быть выполнена

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public LambdaCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }


        /// <summary>
        /// Проверка на возможность выполнения команды
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        /// <summary>
        /// Выполнение команды
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter) => _execute(parameter);
    }
}
