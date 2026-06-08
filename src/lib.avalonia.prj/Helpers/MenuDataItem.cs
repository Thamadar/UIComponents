using Avalonia.Markup.Xaml.Templates;
using DynamicData;
using ReactiveUI; 
using System.Collections.Generic;
using System.Collections.ObjectModel; 
using System.Windows.Input;

namespace Lib.Avalonia.Helpers
{
    public class MenuDataItem : ReactiveObject, IMenuDataItem
    {
        private ControlTemplate? _icon;

        /// <inheritdoc/>
        public ObservableCollection<IMenuDataItem>? Childs { get; }

        /// <inheritdoc/>
        public ControlTemplate? Icon
        {
            get => _icon;
            private set => this.RaiseAndSetIfChanged(ref _icon, value);
        }

        /// <inheritdoc/>
        public string? Key { get; set; }

        /// <inheritdoc/>
        public string? Text { get; set; }   

        /// <inheritdoc/>
        public ICommand? Command { get; }

        /// <inheritdoc/>
        public object? CommandParameter { get; set; }

        /// <inheritdoc/>
        public object? Tag { get; }

        /// <inheritdoc/>
        public bool IsSeparator { get; }  

        public MenuDataItem(
            string text = "", 
            ControlTemplate? icon = null,
            ICommand? command = null,
            object? commandParameter = null,
            object? tag = null,
            IEnumerable<IMenuDataItem>? childs = null, 
            string? key = null,
            bool isSeparator = false)
        {
            Childs = new ObservableCollection<IMenuDataItem>();

            Text                 = text;
            Icon                 = icon;
            Command              = command;
            CommandParameter     = commandParameter;
            Tag                  = tag;
            Key                  = key;
            IsSeparator          = isSeparator; 
            if(childs != null)
            {
                Childs.AddRange(childs);
            }
        }

        /// <inheritdoc/>
        public void ChildsAddRange(IEnumerable<IMenuDataItem> addChilds)
        {
            Childs?.AddRange(addChilds);
        }

        /// <inheritdoc/>
        public void ChildsRemoveRange(IEnumerable<IMenuDataItem> removeChilds)
        { 
            Childs?.RemoveMany(removeChilds);
        }

        /// <inheritdoc/>
        public void SetIcon(ControlTemplate? icon) => Icon = icon;
    }
}
