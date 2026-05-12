
using Avalonia.Input;
using Client.Avalonia.Services;
using Lib.Avalonia;
using Lib.Avalonia.Helpers;
using ReactiveUI;

namespace Client.Avalonia.Views
{
    public sealed partial class GeometryViewModel : ViewModelBase
    {
        private readonly IShapeService _shapeService;
        public GeometryCreateMenuViewModel GeometryCreateMenuViewModel { get; }
        public DisplayViewModel DisplayViewModel { get; }

        public GeometryViewModel()
        {
            _shapeService = ShapeService.Instance;

            GeometryCreateMenuViewModel = new GeometryCreateMenuViewModel();
            DisplayViewModel            = new DisplayViewModel(Commands.CreateShapeCommand);
        } 

        /// <summary>
        /// Получение базовых горячих клавиш с данной вкладки.
        /// </summary> 
        public IEnumerable<IHotKey> GetHotKeys()
        {
            var hotKeys = new List<IHotKey>();

            hotKeys.Add(new HotKey(Key.Delete, Commands.RemoveShapeCommand));

            return hotKeys;
        }  
    }
}
