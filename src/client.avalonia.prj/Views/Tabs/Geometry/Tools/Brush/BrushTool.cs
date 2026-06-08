using Lib.Avalonia.Controls.Helpers;
using Lib.Avalonia.Helpers;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Avalonia.Views.Tabs.Geometry.Tools
{

    public class BrushTool : BaseTool
    {
        #region Fields


        #endregion

        #region Properties 

        /// <summary>
        /// VM-Редактор для кисточки.
        /// </summary>
        public BrushEditViewModel BrushEditViewModel { get; }

        #endregion

        #region Constructors

        public BrushTool()
            : base(ToolTypeEnum.Brush)
        {
            BrushEditViewModel = new BrushEditViewModel();

            CurrentToolEditVM = BrushEditViewModel;
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
