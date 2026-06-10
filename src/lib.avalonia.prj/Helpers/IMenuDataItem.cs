using Avalonia.Markup.Xaml.Templates;
using System.Collections.Generic;
using System.Collections.ObjectModel; 
using System.Windows.Input;

namespace Lib.Avalonia.Helpers
{
    public interface IMenuDataItem
    { 
        /// <summary>
        /// Child'ы.
        /// </summary>
        public ObservableCollection<IMenuDataItem>? Childs { get; }

        /// <summary>
        /// Иконка Item'а. 
        /// Может быть null.
        /// </summary>
        public ControlTemplate? Icon { get; }

        /// <summary>
        /// Command.
        /// Может быть null.
        /// </summary>
        public ICommand? Command { get; }

        /// <summary>
        /// CommandParameter. 
        /// Может быть null.
        /// </summary>
        public object? CommandParameter { get; }

        /// <summary>
        /// Тег. 
        /// Может быть null.
        /// </summary>
        public object? Tag { get; }

        /// <summary>
        /// Подпись: "Комбинация горячих клавиш".
        /// Может быть null.
        /// //TO DO: Переименовать.
        /// </summary>
        public string? Key { get; set; }

        /// <summary>
        /// Отображаемый текст.
        /// Может быть null.
        /// </summary>
        public string? Text { get; set; }   
         
        /// <summary>
        /// Выбран ли данный Item? 
        /// Можно использовать в качестве отображения состояния меню в стилях.
        /// Может быть null.
        /// </summary>
        public bool? IsSelected { get; set; }

        /// <summary>
        /// Служит ли данный Item в качестве разделителя в списке.
        /// </summary>
        public bool IsSeparator { get; }

        /// <summary>
        /// Добавить список элементов IMenuDataItem в коллекцию Childs.
        /// </summary> 
        public void ChildsAddRange(IEnumerable<IMenuDataItem> addChilds);

        /// <summary>
        /// Удалить список элементов IMenuDataItem из коллекции Childs.
        /// </summary> 
        public void ChildsRemoveRange(IEnumerable<IMenuDataItem> removeChilds);
        
        /// <summary>
        /// Установка иконки.
        /// </summary>  
        public void SetIcon(ControlTemplate? icon);
    }
}
