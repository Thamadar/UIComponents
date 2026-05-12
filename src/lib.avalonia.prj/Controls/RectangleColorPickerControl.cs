using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Media;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Avalonia.Controls
{
    [TemplatePart("PART_MainBorder", typeof(Border))]
    [TemplatePart("PART_ItemsControl", typeof(ItemsControl))]
    [TemplatePart("PART_Popup", typeof(Popup))]
    public class RectangleColorPickerControl : TemplatedControl
    {

        private Border? _mainBorder;
        private ItemsControl? _itemsControl;
        private Popup? _popup;

        private ReactiveCommand<IBrush, Unit> _pickColorCommand;
        private bool _isPopupOpen;
        private bool _areControlsAvailable;


        private static readonly IEnumerable<IBrush> DefaultBrushes = new List<IBrush>
    { 
            new SolidColorBrush(Colors.Red),
            new SolidColorBrush(Colors.Green),
            new SolidColorBrush(Colors.Blue),
            new SolidColorBrush(Colors.Yellow),
            new SolidColorBrush(Colors.Purple),
            new SolidColorBrush(Colors.Orange),
            new SolidColorBrush(Colors.White),
    };

        public static readonly StyledProperty<IBrush?> SelectedBrushProperty =
            AvaloniaProperty.Register<RectangleColorPickerControl, IBrush?>(nameof(SelectedBrush), defaultBindingMode: BindingMode.TwoWay);

        public static readonly StyledProperty<IEnumerable<IBrush>> AvailableBrushesProperty =
            AvaloniaProperty.Register<RectangleColorPickerControl, IEnumerable<IBrush>>(nameof(AvailableBrushes),
                defaultValue: DefaultBrushes,
                defaultBindingMode: BindingMode.TwoWay);

        public static readonly StyledProperty<double> WidthButtonProperty =
            AvaloniaProperty.Register<RectangleColorPickerControl, double>(nameof(WidthButton));

        public static readonly StyledProperty<double> HeightButtonProperty =
            AvaloniaProperty.Register<RectangleColorPickerControl, double>(nameof(HeightButton));

        public static readonly StyledProperty<double> SpacingBrushesProperty =
            AvaloniaProperty.Register<RectangleColorPickerControl, double>(nameof(SpacingBrushes));

        public static readonly StyledProperty<bool> IsAvaivableOpenPopupProperty =
            AvaloniaProperty.Register<RectangleColorPickerControl, bool>(nameof(IsAvaivableOpenPopup));

        public static readonly DirectProperty<RectangleColorPickerControl, bool> IsPopupOpenProperty =
            AvaloniaProperty.RegisterDirect<RectangleColorPickerControl, bool>(nameof(IsPopupOpen),
            x => x.IsPopupOpen,
            (x, v) => x.IsPopupOpen = v);

        public static readonly DirectProperty<RectangleColorPickerControl, ReactiveCommand<IBrush, Unit>> PickColorCommandProperty =
            AvaloniaProperty.RegisterDirect<RectangleColorPickerControl, ReactiveCommand<IBrush, Unit>>(nameof(PickColorCommand),
            x => x.PickColorCommand,
            (x, v) => x.PickColorCommand = v);

        /// <summary>
        /// Список доступных цветов для выбора.
        /// </summary>
        public IEnumerable<IBrush> AvailableBrushes
        {
            get => GetValue(AvailableBrushesProperty);
            set => SetValue(AvailableBrushesProperty, value);
        }

        /// <summary>
        /// Выбранный цвет.
        /// </summary>
        public IBrush? SelectedBrush
        {
            get => GetValue(SelectedBrushProperty);
            set => SetValue(SelectedBrushProperty, value);
        }

        public ReactiveCommand<IBrush, Unit> PickColorCommand
        {
            get => _pickColorCommand;
            set => SetAndRaise(PickColorCommandProperty, ref _pickColorCommand, value);
        }

        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetAndRaise(IsPopupOpenProperty, ref _isPopupOpen, value);
        }

        /// <summary>
        /// Может ли открываться Popup?
        /// </summary>
        public bool IsAvaivableOpenPopup
        {
            get => GetValue(IsAvaivableOpenPopupProperty);
            set => SetValue(IsAvaivableOpenPopupProperty, value);
        }

        /// <summary>
        /// Ширина кнопки, по которой открывается список цветов.
        /// </summary>
        public double WidthButton
        {
            get => GetValue(WidthButtonProperty);
            set => SetValue(WidthButtonProperty, value);
        }

        /// <summary>
        /// Высота кнопки, по которой открывается список цветов.
        /// </summary>
        public double HeightButton
        {
            get => GetValue(HeightButtonProperty);
            set => SetValue(HeightButtonProperty, value);
        }

        /// <summary>
        /// Отступы между цветами в списке.
        /// </summary>
        public double SpacingBrushes
        {
            get => GetValue(SpacingBrushesProperty);
            set => SetValue(SpacingBrushesProperty, value);
        }

        public RectangleColorPickerControl()
        {
            PickColorCommand = ReactiveCommand.Create<IBrush>((brush) =>
            {
                PickColor(brush);
            }); 
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            _areControlsAvailable = false;

            base.OnApplyTemplate(e);

            var mainBorder = e.NameScope.Find<Border>("PART_MainBorder");
            var popup = e.NameScope.Find<Popup>("PART_Popup");
            var itemsControl = e.NameScope.Find<ItemsControl>("PART_ItemsControl");

            if(mainBorder != null)
            {
                _mainBorder = InitMainBorder(mainBorder);
            }
            if(popup != null)
            {
                _popup = InitPopup(popup);
            }
            if(itemsControl != null)
            {
                _itemsControl = InitItemsControl(itemsControl);
            }

            if(SelectedBrush == null)
            {
                SelectedBrush = DefaultBrushes.FirstOrDefault();
            }

            _areControlsAvailable = true;
        }

        private Border InitMainBorder(Border border)
        {
            var binding = new Binding(nameof(SelectedBrush))
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            border.Bind(Border.BackgroundProperty, binding);
            border.PointerPressed += OpenPopup;

            return border;
        }

        private Popup InitPopup(Popup popup)
        {
            var binding = new Binding(nameof(IsPopupOpen))
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            popup.Bind(Popup.IsOpenProperty, binding);

            return popup;
        }

        private ItemsControl InitItemsControl(ItemsControl itemsControl)
        {
            var binding = new Binding(nameof(AvailableBrushes))
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            itemsControl.Bind(ItemsControl.ItemsSourceProperty, binding);

            return itemsControl;
        }

        private void OpenPopup(object? sender, PointerPressedEventArgs e)
        {
            if(IsAvaivableOpenPopup)
            {
                IsPopupOpen = !IsPopupOpen;
            }
        }

        private void PickColor(IBrush brush)
        {
            SelectedBrush = brush;
            IsPopupOpen = false;
        }
    }
}
