using Avalonia;
using Lib.Avalonia;
using Lib.Avalonia.Extensions;
using Lib.Avalonia.Helpers; 
using System.Collections.ObjectModel; 

namespace Client.Avalonia.Views.Tabs.Geometry.Tools
{
    public sealed partial class ToolsPanelViewModel : ViewModelBase
    { 
        /// <summary>
        /// Список меню инструментов.
        /// </summary>
        public ObservableCollection<IMenuDataItem> ToolMenuItems { get; }

        public ToolsPanelViewModel()
        {
            ToolMenuItems = new ObservableCollection<IMenuDataItem>();

            LoadToolsMenuItems();
        }

        private void LoadToolsMenuItems()
        {
            var movingTool = new MenuDataItem(
                icon: Application.Current?.GetTemplateResource("MovingIcon"),
                command: Commands.SelectMovingToolCommand);

            var brushTool = new MenuDataItem(
                icon: Application.Current?.GetTemplateResource("BrushIcon"),
                command: Commands.SelectBrushToolCommand);

            var fillTool = new MenuDataItem(
                icon: Application.Current?.GetTemplateResource("BasketPaintIcon"),
                command: Commands.SelectFillToolCommand);

            var textTool = new MenuDataItem(
                icon: Application.Current?.GetTemplateResource("TIcon"),
                command: Commands.SelectTextToolCommand); 

            var shapesToolChilds = new List<IMenuDataItem>()
            {
                new MenuDataItem(
                text: "Инструмент \"Прямоугольник\"",
                icon: Application.Current?.GetTemplateResource("RectIcon"),
                command: Commands.SelectShapesToolCommand,
                commandParameter: ShapeCreateEnum.Rect,
                key: "U"),
                new MenuDataItem(
                text: "Инструмент \"Окружность\"",
                icon: Application.Current?.GetTemplateResource("EllipseIcon"),
                command: Commands.SelectShapesToolCommand,
                commandParameter: ShapeCreateEnum.Circle,
                key: "U"),
            };

            var shapesTool = new MenuDataItem(
                icon: Application.Current?.GetTemplateResource("RectIcon"),
                childs: shapesToolChilds);

            ToolMenuItems.Add(movingTool);
            ToolMenuItems.Add(brushTool);
            ToolMenuItems.Add(fillTool);
            ToolMenuItems.Add(textTool);
            ToolMenuItems.Add(shapesTool);
        }
    }
}
