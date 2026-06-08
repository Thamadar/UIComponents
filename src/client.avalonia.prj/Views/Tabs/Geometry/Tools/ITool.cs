using Lib.Avalonia.Controls.Helpers;
using Lib.Avalonia.Helpers;
using ReactiveUI; 
using System.Reactive; 

namespace Client.Avalonia.Views.Tabs.Geometry.Tools
{
    public enum ToolTypeEnum
    {
        /// <summary>
        /// Перемещение.
        /// </summary>
        Moving,
        /// <summary>
        /// Кисточка.
        /// </summary>
        Brush,
        /// <summary>
        /// Заливка.
        /// </summary>
        Fill,
        /// <summary>
        /// Текст.
        /// </summary>
        Text,
        /// <summary>
        /// Фигуры.
        /// </summary>
        Shapes
    }

    public interface ITool
    {
        ToolTypeEnum ToolType { get; }
        IToolEditVM CurrentToolEditVM { get; set; }

        /// <summary>
        /// Зажатие мыши по Canvas.
        /// </summary> 
        ReactiveCommand<PointerHitInfo, Unit> PointerPressedCanvas { get; }

        /// <summary>
        /// Отжатие мыши по Canvas.
        /// </summary> 
        ReactiveCommand<PointerHitInfo, Unit> PointerReleasedCanvas { get; }

        List<IHotKey> GetToolHotKeys();

        /// <summary>
        /// Реакция на "Инструмент выбран".
        /// </summary>
        public void OnSelect();

        /// <summary>
        /// Реакция на "Инструмент больше не выбран".
        /// </summary>
        public void OnDeselect();

    }
}
