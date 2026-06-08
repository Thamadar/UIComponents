using Avalonia;
using Client.Avalonia.Views.Tabs.Geometry.Tools;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.Avalonia.Views.Geometry
{
    public sealed partial class GeometryViewModel
    {
        public sealed class GeometryViewModelCommands
        { 
            public ICommand RemoveShapeCommand { get; }

            public ICommand SelectShapesToolCommand { get; }

            public GeometryViewModelCommands(GeometryViewModel vm)
            { 
                RemoveShapeCommand      = ReactiveCommand.Create(vm.RemoveSelectedShape);
                SelectShapesToolCommand = ReactiveCommand.Create(vm.SelectShapesTool);
            }
        }

        private GeometryViewModelCommands? _commands;

        public GeometryViewModelCommands Commands => _commands ??= new(this);

        #region Methods 

        /// <summary>
        /// Удаление текущей выбранной геом. фигуры.
        /// </summary>
        private void RemoveSelectedShape()
        {
            var currentShape = _shapeService.GetCurrentSelectedShape();
            if(currentShape != null)
            { 
                _shapeService.RemoveShapeById(currentShape.Id);
            }
        }

        /// <summary>
        /// Выбрать инструмента Shapes.
        /// </summary>
        private void SelectShapesTool() =>_toolService.SelectTool(ToolTypeEnum.Shapes);

        #endregion
    }
}
