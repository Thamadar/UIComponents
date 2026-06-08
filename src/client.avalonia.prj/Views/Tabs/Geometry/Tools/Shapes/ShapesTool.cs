using Client.Avalonia.Services;
using Client.Avalonia.Services.Interfaces;
using Client.Avalonia.Views.Geometry.Shapes; 
using Lib.Avalonia.Controls.Helpers;
using Lib.Avalonia.Extensions;
using Lib.Avalonia.Helpers;
using ReactiveUI;
using System.Reactive.Linq;

namespace Client.Avalonia.Views.Tabs.Geometry.Tools
{
    public class ShapesTool : BaseTool
    { 
        #region Fields

        private readonly IShapeService _shapeService;

        private ShapeCreateEnum _selectedShapeCreate; 

        #endregion

        #region Properties

        /// <summary>
        /// Текущий выбранный тип создания элемента.
        /// </summary>
        public ShapeCreateEnum SelectedShapeCreate
        {
            get => _selectedShapeCreate;
            set => this.RaiseAndSetIfChanged(ref _selectedShapeCreate, value);
        } 

        /// <summary>
        /// VM-Редактор для окружности.
        /// </summary>
        /// 
        public ShapeCircleEditViewModel ShapeCircleEditViewModel { get; }
        /// <summary>
        /// VM-Редактор для прямоугольника.
        /// </summary>
        public ShapeRectEditViewModel ShapeRectEditViewModel { get; }

        #endregion

        #region Constructors

        public ShapesTool()
            : base(ToolTypeEnum.Shapes)
        {
            _shapeService = ShapeService.Instance;

            ShapeCircleEditViewModel = new ShapeCircleEditViewModel();
            ShapeRectEditViewModel   = new ShapeRectEditViewModel();

            CurrentToolEditVM = ShapeRectEditViewModel;
        }

        #endregion

        #region Methods

        #region protected

        /// <inheritdoc/>
        public override void OnSelect()
        {
            base.OnSelect();
             
            this.WhenAnyValue(x => x.SelectedShapeCreate)
                .Do(OnSelectedShapeCreate)
                .Subscribe()
                .AddTo(_observables); 
        }

        /// <inheritdoc/>
        public override void OnPointerPressedCanvas(PointerHitInfo pointerHitInfo)
        {   
            if(pointerHitInfo.HitControl != null)
            { 
                if(pointerHitInfo.HitControl.DataContext is IShapeItem shapeItem)
                {
                    ShapeService.Instance.SelectShapeById(shapeItem.Id);
                }
                else
                {
                    var currentToolEditVM = CurrentToolEditVM as IShapeCreator;
                    var newShape = currentToolEditVM?.Create(pointerHitInfo.Point.X, pointerHitInfo.Point.Y);
                    if(newShape != null)
                    {
                        _shapeService.AddShape(newShape);
                    }
                }

                base.OnPointerPressedCanvas(pointerHitInfo);
            }  
        }

        #endregion

        /// <inheritdoc/>
        public override List<IHotKey> GetToolHotKeys()
        {
            return new List<IHotKey>()
            {

            };
        } 

        /// <summary>
        /// Реакция на изменения значения "Текущий тип редактируемой фигуры".
        /// </summary> 
        private void OnSelectedShapeCreate(ShapeCreateEnum shapeCreateEnum)
        {
            switch(shapeCreateEnum)
            {
                case ShapeCreateEnum.Rect:
                    CurrentToolEditVM = ShapeRectEditViewModel;
                    break;
                case ShapeCreateEnum.Circle:
                    CurrentToolEditVM = ShapeCircleEditViewModel;
                    break;
            }
        }

        #endregion
    }
}
