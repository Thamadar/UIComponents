using Client.Avalonia.Services.Interfaces;
using Client.Avalonia.Views.Tabs.Geometry.Tools; 
using DynamicData;
using System.Reactive.Linq;
using System.Reactive.Subjects; 

namespace Client.Avalonia.Services
{
    public class ToolService : IToolService
    {

        #region Fields

        private static readonly Lazy<IToolService> _instance = new Lazy<IToolService>(() => new ToolService());

        private readonly ISourceList<ITool> _totalTools;
        private readonly Subject<ITool> _currentSelectedToolSubject;

        //private IList<IDisposable> _disposables = new List<IDisposable>();
        private ITool _currentSelectedTool;

        #endregion

        #region Properties

        /// <summary>
        /// Экземпляр.
        /// </summary>
        public static IToolService Instance => _instance.Value;

        /// <inheritdoc/>
        public IObservable<ITool> CurrentSelectedToolObservable => _currentSelectedToolSubject.AsObservable();

        /// <summary>
        /// Текущий выбранный инструмент.
        /// </summary>
        private ITool CurrentSelectedTool
        {
            get => _currentSelectedTool;
            set
            {
                _currentSelectedTool = value;
                _currentSelectedToolSubject.OnNext(value);
            }
        }

        #endregion

        #region .ctor

        private ToolService()
        {
            _totalTools                 = new SourceList<ITool>(); 
            _currentSelectedToolSubject = new Subject<ITool>();

            InitTools();
        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public IObservable<IChangeSet<ITool>> ConnectToTotalTools()
        {
            return _totalTools.Connect();
        }

        /// <inheritdoc/>
        public ITool GetCurrentSelectedTool() => CurrentSelectedTool;

        /// <inheritdoc/>
        public T? GetTool<T>() 
            where T : ITool
        {
            var tool = _totalTools.Items.FirstOrDefault(x => x is T); 
            if(tool != null && tool is T toolResult)
            {
                return toolResult;
            }

            return default(T?);
        }

        /// <inheritdoc/>
        public void SelectTool(ToolTypeEnum toolType)
        {
            var selectedTool = _totalTools.Items.First(x => x.ToolType == toolType);

            CurrentSelectedTool?.OnDeselect();
            selectedTool.OnSelect();
            CurrentSelectedTool = selectedTool; 
        }

        private void InitTools()
        {
            var toolEnums = Enum.GetValues<ToolTypeEnum>();
            var resultTools = new List<ITool>();
            
            foreach(var toolEnum in toolEnums)
            {
                resultTools.Add(CreateTool(toolEnum));
            }

            _totalTools.AddRange(resultTools);
        }

        private ITool CreateTool(ToolTypeEnum toolType)
        {
            return toolType switch
            {
                ToolTypeEnum.Moving => new MovingTool(), 
                ToolTypeEnum.Brush  => new BrushTool(),
                ToolTypeEnum.Fill   => new FillTool(),
                ToolTypeEnum.Text   => new TextTool(),
                ToolTypeEnum.Shapes => new ShapesTool(),
                 
                _ => throw new ArgumentOutOfRangeException($"{nameof(CreateTool)}: {toolType} create invalid")
            };
        }

        #endregion
    }
}
