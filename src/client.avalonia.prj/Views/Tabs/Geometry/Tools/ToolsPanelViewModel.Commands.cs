using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.Avalonia.Views.Tabs.Geometry.Tools
{
    public sealed partial class ToolsPanelViewModel
    {
        public sealed class ToolsPanelViewModelCommands
        {
            public ICommand SelectMovingToolCommand { get; }
            public ICommand SelectBrushToolCommand { get; }
            public ICommand SelectFillToolCommand { get; }
            public ICommand SelectTextToolCommand { get; }
            public ICommand SelectShapesToolCommand { get; }
            public ICommand SelectZoomToolCommand { get; }

            public ToolsPanelViewModelCommands(ToolsPanelViewModel vm)
            {
                SelectZoomToolCommand   = ReactiveCommand.Create(vm.SelectZoomTool);
                SelectBrushToolCommand  = ReactiveCommand.Create(vm.SelectBrushTool);
                SelectMovingToolCommand = ReactiveCommand.Create(vm.SelectMovingTool);
                SelectFillToolCommand   = ReactiveCommand.Create(vm.SelectFillTool);
                SelectTextToolCommand   = ReactiveCommand.Create(vm.SelectTextTool);
                SelectShapesToolCommand = ReactiveCommand.Create<ShapeCreateEnum>(vm.SelectShapesTool);
            }
        }

        private ToolsPanelViewModelCommands? _commands;
        public ToolsPanelViewModelCommands Commands => _commands ??= new(this);

        #region Methods
         
        /// <summary>
        /// Выбрать инструмент: Перемещение.
        /// </summary>
        public void SelectMovingTool()
        {
            _toolService.SelectTool(ToolTypeEnum.Moving);
        }

        /// <summary>
        /// Выбрать инструмент: Кисточка.
        /// </summary>
        public void SelectBrushTool()
        {
            _toolService.SelectTool(ToolTypeEnum.Brush);
        }

        /// <summary>
        /// Выбрать инструмент: Заливка.
        /// </summary>
        public void SelectFillTool()
        {
            _toolService.SelectTool(ToolTypeEnum.Fill);
        }
        /// <summary>
        /// Выбрать инструмент: Текст.
        /// </summary>
        public void SelectTextTool()
        {
            _toolService.SelectTool(ToolTypeEnum.Text);
        }
        /// <summary>
        /// Выбрать инструмент: Геом. фигура.
        /// </summary>
        public void SelectShapesTool(ShapeCreateEnum shapeCreateEnum)
        { 
            var shapesTool = _toolService.GetTool<ShapesTool>();  
            if(shapesTool != null)
            {
                shapesTool.SelectedShapeCreate = shapeCreateEnum;
                _toolService.SelectTool(ToolTypeEnum.Shapes);
            }
        }

        /// <summary>
        /// Выбрать инструмент: Лупа.
        /// </summary>
        public void SelectZoomTool()
        {

        }

        #endregion
    }
}
