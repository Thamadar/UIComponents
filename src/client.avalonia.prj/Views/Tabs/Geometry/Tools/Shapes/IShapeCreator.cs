

using Client.Avalonia.Views.Geometry.Shapes;

namespace Client.Avalonia.Views.Tabs.Geometry.Tools
{
    /// <summary>
    /// VM-Редактор для геом. фигур.
    /// </summary>
    public interface IShapeCreator : IToolEditVM
    {
        /// <summary>
        /// Создать геом. фигуру
        /// </summary> 
        public IShapeItem Create(double x, double y);
    }
}
