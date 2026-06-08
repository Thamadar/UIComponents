using Lib.Avalonia.Controls.Helpers;
using Lib.Avalonia.Helpers; 

namespace Client.Avalonia.Views.Tabs.Geometry.Tools
{
    public class TextTool : BaseTool
    {
        #region Fields


        #endregion

        #region Properties


        /// <summary>
        /// VM-Редактор для текста.
        /// </summary>
        public TextEditViewModel TextEditViewModel { get; }

        #endregion

        #region Constructors

        public TextTool()
            : base(ToolTypeEnum.Text)
        {
            TextEditViewModel = new TextEditViewModel();

            CurrentToolEditVM = TextEditViewModel;
        }

        #endregion

        #region Methods

        #region protected

        /// <inheritdoc/>
        public override void OnSelect()
        {
            base.OnSelect();

            //this.WhenAnyValue(x => x.SelectedShapeCreate)
            //    .Do(OnSelectedShapeCreate)
            //    .Subscribe()
            //    .AddTo(_observables);
        }

        /// <inheritdoc/>
        public override void OnPointerPressedCanvas(PointerHitInfo pointerHitInfo)
        {

        }

        #endregion


        /// <inheritdoc/>
        public override List<IHotKey> GetToolHotKeys()
        {
            return new List<IHotKey>()
            {

            };
        } 

        #endregion
    }
}
