using Avalonia.Input; 
using System.Windows.Input;

namespace Lib.Avalonia.Helpers
{ 
    public interface IHotKey
    {
        /// <summary>
        /// Горячая клавиша.
        /// </summary>
        public Key Key { get; }

        /// <summary>
        /// Горячая клавиша-модификатор. Может быть несколько, ибо это enum flags.
        /// Например: (KeyModifiers.Alt | KeyModifiers.Shift | ...), - если нужно больше одной доп.клавиши.
        /// </summary>
        public KeyModifiers KeyModifiers { get; }

        /// <summary>
        /// Команда, исполняемая при нажатии.
        /// </summary>
        public ICommand? Command { get; }

        /// <summary>
        /// Параметр команды.
        /// Может быть null.
        /// </summary>
        public object? CommandParameter { get; }
    } 
}
