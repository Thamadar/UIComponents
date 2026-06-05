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

    public class MovingTool : BaseTool
    {
        #region Fields
         

        #endregion

        #region Properties
         

        #endregion

        #region Constructors

        public MovingTool()
            : base(ToolTypeEnum.Moving)
        { 
            //CurrentToolEditVM = ShapeCircleEditViewModel;
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
        public override void OnPointerPressedCanvas(object? sender, PointerCanvasEventArgs e)
        {
            if(e.HitControl != null)
            { 

                base.OnPointerPressedCanvas(sender, e);
            }
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
