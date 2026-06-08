using Avalonia;
using Avalonia.Markup.Xaml.Templates;
using Lib.Avalonia;
using Lib.Avalonia.Extensions;


namespace Client.Avalonia.Views.Tabs.Geometry.Tools
{
    public class FillEditViewModel : ViewModelBase, IToolEditVM
    {
        #region Fields
         
        #endregion

        #region Properties
          
        #endregion

        #region Methods 

        /// <inheritdoc/>
        public ControlTemplate? GetIcon() => Application.Current?.GetTemplateResource($"MenuFillIcon");

        #endregion
    }
}
