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

    public class FillTool : BaseTool
    {
        #region Fields


        #endregion

        #region Properties

        /// <summary>
        /// VM-Редактор для заливки.
        /// </summary>
        public FillEditViewModel FillEditViewModel { get; }

        #endregion

        #region Constructors

        public FillTool()
            : base(ToolTypeEnum.Fill)
        {
            FillEditViewModel = new FillEditViewModel();

            CurrentToolEditVM = FillEditViewModel;
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
