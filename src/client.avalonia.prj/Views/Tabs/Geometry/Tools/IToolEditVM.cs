using Avalonia.Markup.Xaml.Templates;

namespace Client.Avalonia.Views.Tabs.Geometry.Tools
{
    public interface IToolEditVM
    {
        /// <summary>
        /// Получить иконку инструмента.
        /// </summary>
        ControlTemplate? GetIcon();
    }
}
