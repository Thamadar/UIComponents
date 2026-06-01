
using Avalonia.Input;
using Client.Avalonia.Services;
using Client.Avalonia.Services.Interfaces; 
using Lib.Avalonia;
using Lib.Avalonia.Helpers; 

namespace Client.Avalonia.Views.Geometry
{
    /// <summary>
    /// VM, отвечающая за вкладку "Графический редактор".
    /// //TO DO: переименовать класс. GeometryViewModel => GhaphicEditorViewModel.
    /// </summary>
    public sealed partial class GeometryViewModel : ViewModelBase, ITabVM
    {
        private readonly IShapeService _shapeService;

        public GeometryManagementPanelViewModel GeometryManagementPanelViewModel { get; }
        public GeometryCreateMenuViewModel GeometryCreateMenuViewModel { get; }
        public DisplayViewModel DisplayViewModel { get; }

        /// <inheritdoc/>
        public Guid Id { get; }

        /// <summary>
        /// Конструктор-заглушка, дабы Designer не падал.
        /// </summary>
        public GeometryViewModel()
            : this(Guid.NewGuid())
        {

        }

        public GeometryViewModel(Guid id)
        {
            _shapeService = ShapeService.Instance;

            Id = id;

            GeometryManagementPanelViewModel = new GeometryManagementPanelViewModel();
            GeometryCreateMenuViewModel      = new GeometryCreateMenuViewModel();
            DisplayViewModel                 = new DisplayViewModel(Commands.CreateShapeCommand);
        }

        /// <inheritdoc/>
        public IEnumerable<IHotKey> GetTabHotKeys()
        {
            var hotKeys = new List<IHotKey>
            {
                new HotKey(Key.Delete, Commands.RemoveShapeCommand),

                new HotKey(Key.N, GeometryManagementPanelViewModel.Commands.NewFileCommand, keyModifiers: KeyModifiers.Control),
                new HotKey(Key.O, GeometryManagementPanelViewModel.Commands.OpenFileCommand, keyModifiers: KeyModifiers.Control),
                new HotKey(Key.S, GeometryManagementPanelViewModel.Commands.SaveFileCommand, keyModifiers: KeyModifiers.Control),

                new HotKey(Key.Z, GeometryManagementPanelViewModel.Commands.StepBackEditCommand, keyModifiers:KeyModifiers.Control),
                new HotKey(Key.Y, GeometryManagementPanelViewModel.Commands.StepForwardEditCommand, keyModifiers:KeyModifiers.Control),
                new HotKey(Key.J, GeometryManagementPanelViewModel.Commands.CloneEditCommand, keyModifiers:KeyModifiers.Control),
            };

            return hotKeys;
        }

        /// <inheritdoc/>
        public Task DisposeTab()
        {
            //Какие-нибудь отписки, дочерние Dispose, await...

            Dispose();

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task LoadTab()
        {
            //Какие-нибудь подписки, await...

            return Task.CompletedTask;
        }
    }
}
