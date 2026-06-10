using Avalonia;
using Avalonia.Markup.Xaml.Templates;
using Lib.Avalonia.Extensions;
using System.Collections.Generic; 
using System.Windows.Input;

namespace Lib.Avalonia.Helpers
{
    /// <summary>
    /// Builder для MenuDataItem.
    /// </summary>
    public class MenuDataItemBuilder
    { 
        private string _text = "";
        private ControlTemplate? _icon;
        private ICommand? _command;
        private object? _commandParameter;
        private object? _tag;
        private List<IMenuDataItem>? _childs;
        private string? _key;
        private bool _isSeparator;

        public MenuDataItemBuilder WithText(string text)
        {
            _text = text;
            return this;
        }

        public MenuDataItemBuilder WithIcon(ControlTemplate icon)
        {
            _icon = icon;
            return this;
        }
         
        public MenuDataItemBuilder WithIcon(string iconTemplateName)
        {
            _icon = Application.Current?.GetTemplateResource(iconTemplateName);
            return this;
        }

        public MenuDataItemBuilder WithCommand(ICommand command)
        {
            _command = command;
            return this;
        }

        public MenuDataItemBuilder WithCommandParameter(object commandParameter)
        {
            _commandParameter = commandParameter;
            return this;
        }

        public MenuDataItemBuilder WithTag(object tag)
        {
            _tag = tag;
            return this;
        }

        public MenuDataItemBuilder WithKey(string key)
        {
            _key = key;
            return this;
        }

        public MenuDataItemBuilder WithSeparator(bool isSeparator = true)
        {
            _isSeparator = isSeparator;
            return this;
        }

        public MenuDataItemBuilder WithChilds(IEnumerable<IMenuDataItem> childs)
        {
            _childs = new List<IMenuDataItem>(childs);
            return this;
        }

        public MenuDataItemBuilder AddChild(IMenuDataItem child)
        {
            if(_childs == null)
                _childs = new List<IMenuDataItem>();

            _childs.Add(child);
            return this;
        }

        public IMenuDataItem Build()
        {
            return new MenuDataItem(
                text: _text,
                icon: _icon,
                command: _command,
                commandParameter: _commandParameter,
                tag: _tag,
                childs: _childs,
                key: _key,
                isSeparator: _isSeparator
            );
        }

        public static MenuDataItemBuilder Create() => new MenuDataItemBuilder();

        public static MenuDataItemBuilder Create(string text) => new MenuDataItemBuilder().WithText(text);
    }
}
