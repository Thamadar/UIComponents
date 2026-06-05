using Client.Avalonia.Views.Tabs.Geometry.Tools;
using DynamicData; 

namespace Client.Avalonia.Services.Interfaces
{
    public interface IToolService
    {
        /// <summary> 
        /// Подключение к списку всех инструментов.
        /// </summary> 
        IObservable<IChangeSet<ITool>> ConnectToTotalTools();

        /// <summary>
        /// Отслеживание текущего инструмента.
        /// </summary>
        IObservable<ITool?> CurrentSelectedToolObservable { get; }

        /// <summary>
        /// Получить текущего выделенного инструмента.
        /// </summary> 
        ITool? GetCurrentSelectedTool();

        /// <summary>
        /// Выбрать инструмент.
        /// </summary>
        void SelectTool(ToolTypeEnum toolType);
    }
}
