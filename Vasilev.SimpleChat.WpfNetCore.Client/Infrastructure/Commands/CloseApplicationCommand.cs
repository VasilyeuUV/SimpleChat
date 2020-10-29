using System.Windows;
using Vasilev.SimpleChat.WpfNetCore.Client.Infrastructure.Commands.Base;

namespace Vasilev.SimpleChat.WpfNetCore.Client.Infrastructure.Commands
{

    /// <summary>
    /// Класс конкретной команды
    /// </summary>
    internal class CloseApplicationCommand : CommandBase
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter) => Application.Current.Shutdown();
    }
}
