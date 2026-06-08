using Avalonia;
using Client.Avalonia.Services;
using Client.Avalonia.Services.Interfaces;
using Lib.Avalonia;
using Lib.Avalonia.Extensions;
using Lib.Avalonia.Helpers;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace Client.Avalonia.Views.Tabs.Geometry.Tools
{
    public sealed partial class ToolsPanelViewModel : ViewModelBase
    { 
        private readonly IToolService _toolService; 
        /// <summary>
        /// Список меню инструментов.
        /// </summary>
        public ObservableCollection<IMenuDataItem> ToolMenuItems { get; }
         
        public ToolsPanelViewModel()
        {
            _toolService = ToolService.Instance;

            ToolMenuItems = new ObservableCollection<IMenuDataItem>();

            _toolService
                .CurrentSelectedToolObservable
                .Do(OnSelectedTool)
                .Subscribe()
                .AddTo(_disposables);

            LoadToolsMenuItems();
        }

        private void OnSelectedTool(ITool selectedTool)
        {
            var selectedType = selectedTool.ToolType;

            var selectedMenu = ToolMenuItems.FirstOrDefault(x => x.Tag is ToolTypeEnum toolType && toolType == selectedType);
            if(selectedMenu != null)
            {
                selectedMenu.SetIcon(selectedTool.CurrentToolEditVM.GetIcon());
            }
        }

        private void LoadToolsMenuItems()
        {
            var movingTool = new MenuDataItem(
                icon: Application.Current?.GetTemplateResource("MenuMovingIcon"),
                tag: ToolTypeEnum.Moving,
                command: Commands.SelectMovingToolCommand);

            var brushTool = new MenuDataItem(
                icon: Application.Current?.GetTemplateResource("MenuBrushIcon"),
                tag: ToolTypeEnum.Brush,
                command: Commands.SelectBrushToolCommand);

            var fillTool = new MenuDataItem(
                icon: Application.Current?.GetTemplateResource("MenuFillIcon"),
                tag: ToolTypeEnum.Fill,
                command: Commands.SelectFillToolCommand);

            var textTool = new MenuDataItem(
                icon: Application.Current?.GetTemplateResource("MenuTextIcon"),
                tag: ToolTypeEnum.Text,
                command: Commands.SelectTextToolCommand); 

            var shapesToolChilds = new List<IMenuDataItem>()
            {
                new MenuDataItem(
                text: "Инструмент \"Прямоугольник\"",
                icon: Application.Current?.GetTemplateResource("MenuRectIcon"),
                command: Commands.SelectShapesToolCommand,
                commandParameter: ShapeCreateEnum.Rect,
                key: "U"),
                new MenuDataItem(
                text: "Инструмент \"Окружность\"",
                icon: Application.Current?.GetTemplateResource("MenuCircleIcon"),
                command: Commands.SelectShapesToolCommand,
                commandParameter: ShapeCreateEnum.Circle,
                key: "U"),
            };

            var shapesTool = new MenuDataItem(
                icon: Application.Current?.GetTemplateResource("MenuRectIcon"),
                tag: ToolTypeEnum.Shapes,
                childs: shapesToolChilds);

            ToolMenuItems.Add(movingTool);
            ToolMenuItems.Add(brushTool);
            ToolMenuItems.Add(fillTool);
            ToolMenuItems.Add(textTool);
            ToolMenuItems.Add(shapesTool);
        }
    }
}
