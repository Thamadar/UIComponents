using Avalonia;
using Avalonia.Controls; 
using Avalonia.Media;
using Client.Avalonia.Services;
using Client.Avalonia.Services.Interfaces;
using Client.Avalonia.Views.Geometry.Shapes;
using Client.Avalonia.Views.Tabs.Geometry.Tools;
using DynamicData;
using Lib.Avalonia;
using Lib.Avalonia.Extensions;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;

namespace Client.Avalonia.Views.Geometry
{
    public class DisplayViewModel : ViewModelBase
    {
        #region Fields
         
        private ReadOnlyObservableCollection<IShapeItem> _totalShapes = new(new());
        private readonly IShapeService _shapeService;
        private readonly IToolService _toolService;

        private IShapeItem? _selectedShape;
        private ITool _currentSelectedTool;

        #endregion

        #region Properties 
         
        /// <summary>
        /// Геометрические фигуры.
        /// </summary>
        public ReadOnlyObservableCollection<IShapeItem> TotalShapes => _totalShapes;
        public ReactiveCommand<Point, Unit> CreateShapeCommand { get; }

        /// <summary>
        /// Текущая выбранная геом. фигура.
        /// </summary>
        public IShapeItem? SelectedShape
        {
            get => _selectedShape;
            set => this.RaiseAndSetIfChanged(ref _selectedShape, value);
        } 

        /// <summary>
        /// Текущий выбранный инструмент (заливка, текст, перемещение, ...) .
        /// </summary>
        public ITool CurrentSelectedTool
        {
            get => _currentSelectedTool;
            set => this.RaiseAndSetIfChanged(ref _currentSelectedTool, value);
        }
        #endregion

        #region .ctor 

        public DisplayViewModel()
        {
            _shapeService = ShapeService.Instance;
            _toolService  = ToolService.Instance;

            _shapeService
                .ConnectToTotalShapes() 
                .Bind(out _totalShapes)
                .Subscribe()
                .AddTo(_disposables);

            _shapeService
                .CurrentSelectedShapeObservable 
                .BindTo(this, x => x.SelectedShape)
                .AddTo(_disposables);
            
            _toolService
                .CurrentSelectedToolObservable
                .BindTo(this, x => x.CurrentSelectedTool)
                .AddTo(_disposables);

            LoadDefaultShapes();
        }

        #endregion

        #region Methods  

        /// <summary>
        /// Загрузка базовых геом. фигур (для теста).
        /// </summary>
        private void LoadDefaultShapes()
        {
            _shapeService.RemoveAllShapes();

            var shapes = new List<IShapeItem>()
            {
                new CircleItem(100, 200, 50, 0, 1, SolidColorBrush.Parse("#006cb5")),
                new CircleItem(300, 300, 30, 4, 0.5, SolidColorBrush.Parse("#00645f"), SolidColorBrush.Parse("#e85222")),
                new RectItem(500, 600, 30, 30, 4, 0.7, SolidColorBrush.Parse("#00645f"), SolidColorBrush.Parse("#e85222")),
                new RectItem(0, 0, 100, 20, 1, 1, SolidColorBrush.Parse("#00645f"), SolidColorBrush.Parse("#e85222")),
            };

            _shapeService.AddRangeShape(shapes);
        }

        #endregion
    }
}
