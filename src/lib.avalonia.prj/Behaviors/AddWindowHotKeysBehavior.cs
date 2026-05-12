using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Xaml.Interactivity;
using Lib.Avalonia.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq; 

namespace Lib.Avalonia.Behaviors
{

    /// <summary>
    /// Behavior, с помощью которого можно подключить горячие клавиши к Window,
    /// используя свойство HotKeys.
    /// </summary>
    public class AddWindowHotKeysBehavior : Behavior<Window>
    {
        public static readonly StyledProperty<ObservableCollection<IHotKey>> HotKeysProperty =
            AvaloniaProperty.Register<AddWindowHotKeysBehavior, ObservableCollection<IHotKey>>(nameof(HotKeys));

        /// <summary>
        /// Список горячих клавиш интерфейса IHotKey.
        /// </summary>
        public ObservableCollection<IHotKey> HotKeys
        {
            get => GetValue(HotKeysProperty);
            set => SetValue(HotKeysProperty, value);
        }

        protected override void OnAttached()
        {
            if(AssociatedObject is null) return;

            AssociatedObject.AddHandler(Window.KeyDownEvent, OnKeyDown, RoutingStrategies.Tunnel);
        }

        protected override void OnDetaching()
        {
            if(AssociatedObject is null) return;

            AssociatedObject.RemoveHandler(Window.KeyDownEvent, OnKeyDown);
        }

        /// <summary>
        /// Исполнение события нажатия на клавишу. Поиск совпадений в списке горячих клавиш и их выполнение.
        /// </summary> 
        private void OnKeyDown(object? sender, KeyEventArgs e)
        {
            var textBox = e.Source as TextBox;
            if(HotKeys == null || 
                HotKeys.Count() == 0 ||
                textBox != null)
                return;
             
            var tempKeyBindingItems = new List<IHotKey>();
            foreach(var item in HotKeys)
            {
                if(item.Key == e.Key &&
                   (e.KeyModifiers == item.KeyModifiers))
                    tempKeyBindingItems.Add(item);
            } 

            foreach(var keyBinding in tempKeyBindingItems)
            {
                keyBinding.Command?.Execute(keyBinding.CommandParameter);
                e.Handled = true;
            }
        }
    }
}
