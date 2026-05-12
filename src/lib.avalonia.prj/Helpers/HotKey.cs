using Avalonia.Input; 
using System.Windows.Input;

namespace Lib.Avalonia.Helpers
{
    public class HotKey : IHotKey
    {
        /// <inheritdoc/>
        public Key Key { get; }

        /// <inheritdoc/>
        public KeyModifiers KeyModifiers { get; }

        /// <inheritdoc/>
        public ICommand? Command { get; }

        /// <inheritdoc/>
        public object? CommandParameter { get; }

        public HotKey(
             Key key,
             ICommand? command,
             object? commandParameter = null,
             KeyModifiers? keyModifiers = null)
        {
            Key              = key;
            Command          = command;
            CommandParameter = commandParameter;
            KeyModifiers     = keyModifiers == null ? KeyModifiers.None : keyModifiers.Value;
        }
    }

}
