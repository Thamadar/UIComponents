using Avalonia.Controls;
using Client.Avalonia.Views.Geometry.Shapes;
using DynamicData;

namespace Client.Avalonia.Services
{
    //TO DO: IDisposable привентить.
    /// <summary>
    /// Сервис управления геом. фигурами в проекте между VM.
    /// </summary>
    public interface IShapeService
    { 
        /// <summary> 
        /// Подключение к списку всех геом. фигур.
        /// </summary> 
        IObservable<IChangeSet<IShapeItem>> ConnectToTotalShapes();

        /// <summary>
        /// Отслеживание текущей выбранной геом. фигуры.
        /// </summary>
        IObservable<IShapeItem?> CurrentSelectedShapeObservable { get; }

        /// <summary>
        /// Получить текущую выделенную геом. фигуру.
        /// </summary> 
        public IShapeItem? GetCurrentSelectedShape();

        /// <summary>
        /// Добавить геом. фигуру
        /// </summary>
        public void AddShape(IShapeItem shapeItem);

        /// <summary>
        /// Добавление множество геом. фигур.
        /// </summary> 
        public void AddRangeShape(IEnumerable<IShapeItem> shapeItems);

        /// <summary>
        /// Удалить геом. фигуру.
        /// </summary> 
        public void RemoveShapeById(Guid guid);

        /// <summary>
        /// Удалить все геом. фигуры.
        /// </summary>
        public void RemoveAllShapes();

        /// <summary>
        /// Выбрать геом. фигуру по ID.
        /// </summary> 
        public void SelectShapeById(Guid? guid = null);
    }
}
