using Lib.Avalonia.Controls.Helpers;
using Lib.Avalonia.Extensions;
using Lib.Avalonia.Helpers;

using ReactiveUI;
using System.Reactive;

namespace Client.Avalonia.Views.Tabs.Geometry.Tools
{
    public abstract class BaseTool : ReactiveObject, ITool
    {
        #region Fields

        protected List<IDisposable> _observables = new List<IDisposable>();
        protected IToolEditVM _currentToolEditVM; 

        #endregion

        #region Properties

        /// <summary>
        /// Текущая VM-редактора инструмента.
        /// </summary>
        public IToolEditVM CurrentToolEditVM 
        {
            get => _currentToolEditVM;
            set => this.RaiseAndSetIfChanged(ref _currentToolEditVM, value);
        }

        /// <inheritdoc/>
        public ToolTypeEnum ToolType { get; }

        /// <inheritdoc/>
        public ReactiveCommand<PointerHitInfo, Unit> PointerPressedCanvas { get; }

        /// <inheritdoc/>
        public ReactiveCommand<PointerHitInfo, Unit> PointerReleasedCanvas { get; } 
        #endregion

        #region Constructors

        protected BaseTool(
            ToolTypeEnum toolType)
        {
            ToolType = toolType;

            PointerPressedCanvas  = ReactiveCommand.Create<PointerHitInfo>(OnPointerPressedCanvas);
            PointerReleasedCanvas = ReactiveCommand.Create<PointerHitInfo>(OnPointerReleasedCanvas);
        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public virtual void OnSelect()
        {

        }

        /// <inheritdoc/>
        public virtual void OnDeselect()
        {  
            _observables.DisposeAll();
        } 

        /// <summary>
        /// Реакция на зажатие мыши по Canvas.
        /// </summary> 
        public virtual void OnPointerPressedCanvas(PointerHitInfo pointerHitInfo)
        {
            
        }

        /// <summary>
        /// Реакция на отжатие мыши по Canvas.
        /// </summary> 
        public virtual void OnPointerReleasedCanvas(PointerHitInfo pointerHitInfo)
        {

        }

        /// <inheritdoc/>
        public abstract List<IHotKey> GetToolHotKeys();

        #endregion
    }
}
