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

        /// <summary>
        /// Реакция на выбранный инструмент.
        /// </summary> 
        private void OnSelectedTool(ITool selectedTool)
        {
            var selectedType = selectedTool.ToolType;

            var selectedMenu = ToolMenuItems.FirstOrDefault(x => x.Tag is ToolTypeEnum toolType && toolType == selectedType);
            if(selectedMenu != null)
            {
                selectedMenu.SetIcon(selectedTool.CurrentToolEditVM.GetIcon());
                ToolMenuItems.SetSelectedStateToMenu(selectedMenu);
            }
        } 

        /// <summary>
        /// Инициализация меню инструментов.
        /// </summary>
        private void LoadToolsMenuItems()
        {  
            var movingTool = MenuDataItemBuilder
                .Create("Перемещение")
                .WithIcon("MenuMovingIcon")
                .WithCommand(Commands.SelectMovingToolCommand) 
                .WithTag(ToolTypeEnum.Moving)
                .Build();

            var brushTool = MenuDataItemBuilder
                .Create("Кисть")
                .WithIcon("MenuBrushIcon")
                .WithCommand(Commands.SelectBrushToolCommand)
                .WithTag(ToolTypeEnum.Brush)
                .Build();

            var fillTool = MenuDataItemBuilder
                .Create("Заливка")
                .WithIcon("MenuFillIcon")
                .WithCommand(Commands.SelectFillToolCommand)
                .WithTag(ToolTypeEnum.Fill)
                .Build();

            var textTool = MenuDataItemBuilder
                .Create("Текст")
                .WithIcon("MenuTextIcon")
                .WithCommand(Commands.SelectTextToolCommand)
                .WithTag(ToolTypeEnum.Text)
                .Build(); 

            var shapesTool = MenuDataItemBuilder
                .Create("Геометрические фигура")
                .WithIcon("MenuRectIcon") 
                .WithTag(ToolTypeEnum.Shapes)
                .AddChild(MenuDataItemBuilder
                    .Create("Инструмент \"Прямоугольник\"")
                    .WithIcon("MenuRectIcon")
                    .WithCommand(Commands.SelectShapesToolCommand)
                    .WithCommandParameter(ShapeCreateEnum.Rect) 
                    .WithKey("U")
                    .Build())
                .AddChild(MenuDataItemBuilder
                    .Create("Инструмент \"Окружность\"")
                    .WithIcon("MenuCircleIcon")
                    .WithCommand(Commands.SelectShapesToolCommand)
                    .WithCommandParameter(ShapeCreateEnum.Circle)
                    .WithKey("U")
                    .Build())
                .Build(); 

            ToolMenuItems.Add(movingTool);
            ToolMenuItems.Add(brushTool);
            ToolMenuItems.Add(fillTool);
            ToolMenuItems.Add(textTool);
            ToolMenuItems.Add(shapesTool);
        }
    }
}
