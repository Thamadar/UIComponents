using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Media;
using Avalonia.Rendering;
using Avalonia.Threading;
using Avalonia.VisualTree;

using Lib.Avalonia.Controls.Helpers;
using Lib.Avalonia.Extensions;
using Lib.Avalonia.Helpers;
using ReactiveUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;

namespace Lib.Avalonia.Controls
{
    [TemplatePart("PART_TextContainerGrid", typeof(Grid))]
    public class CustomMenuItem : TemplatedControl
    {
        private List<IDisposable> _disposables = new List<IDisposable>();

        private Grid? _textContainerGrid;

        private CustomMenuItem? _itemParent; 
        private CustomMenuItemControlManager? _customMenuItemControlManager;

        public static readonly StyledProperty<ObservableCollection<IMenuDataItem>> ItemsProperty =
            AvaloniaProperty.Register<CustomMenuItem, ObservableCollection<IMenuDataItem>>(nameof(Items));

        public static readonly StyledProperty<ControlTemplate?> IconProperty =
            AvaloniaProperty.Register<CustomMenuItem, ControlTemplate?>(nameof(Icon));
          
        public static readonly StyledProperty<ICommand?> CommandProperty =
            AvaloniaProperty.Register<CustomMenuItem, ICommand?>(nameof(Command));

        public static readonly StyledProperty<ICommand> CloseCommandProperty =
            AvaloniaProperty.Register<CustomMenuItem, ICommand>(nameof(CloseCommand));

        public static readonly StyledProperty<string?> KeyProperty =
            AvaloniaProperty.Register<CustomMenuItem, string?>(nameof(Key));

        public static readonly StyledProperty<string> TextProperty =
            AvaloniaProperty.Register<CustomMenuItem, string>(nameof(Text)); 

        public static readonly StyledProperty<bool> IsSubMenuOpenProperty =
            AvaloniaProperty.Register<CustomMenuItem, bool>(nameof(IsSubMenuOpen));

        public static readonly StyledProperty<bool> IsSeparatorProperty =
            AvaloniaProperty.Register<CustomMenuItem, bool>(nameof(IsSeparator));

        public static readonly StyledProperty<double> MaxTextWidthProperty =
            AvaloniaProperty.Register<CustomMenuItem, double>(nameof(MaxTextWidth));

        public static readonly StyledProperty<ColumnDefinitions?> ColumnDefinitionsProperty =
            AvaloniaProperty.Register<CustomMenuItem, ColumnDefinitions?>(nameof(ColumnDefinitions));

        public static readonly StyledProperty<ColumnDefinitions?> ChildsColumnDefinitionsProperty =
            AvaloniaProperty.Register<CustomMenuItem, ColumnDefinitions?>(nameof(ChildsColumnDefinitions)); 

        public static readonly DirectProperty<CustomMenuItem, CustomMenuItem?> ItemParentProperty =
            AvaloniaProperty.RegisterDirect<CustomMenuItem, CustomMenuItem?>(
            nameof(ItemParent),
            x => x.ItemParent,
            (x, v) => x.ItemParent = v); 

        /// <summary>
        /// Список меню. Может быть пустым.
        /// </summary>
        public ObservableCollection<IMenuDataItem> Items
        {
            get => GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        /// <summary>
        /// Иконка объекта меню. Может быть пустым.
        /// </summary>
        public ControlTemplate? Icon
        {
            get => GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        /// <summary>
        /// Колоны для текста: Text + Key. Распространяется на остальные меню данной группы.
        /// </summary>
        public ColumnDefinitions? ColumnDefinitions
        {
            get => GetValue(ColumnDefinitionsProperty);
            set => SetValue(ColumnDefinitionsProperty, value);
        }

        /// <summary>
        /// Колоны для текста дочернего меню: Text + Key. Распространяется на остальные меню дочерней группы.
        /// </summary>
        public ColumnDefinitions? ChildsColumnDefinitions
        {
            get => GetValue(ChildsColumnDefinitionsProperty);
            set => SetValue(ChildsColumnDefinitionsProperty, value);
        } 

        /// <summary>
        /// Родитель данного item'а. Может быть пустым.
        /// Устанавливается автоматически.
        /// </summary>
        public CustomMenuItem? ItemParent
        {
            get => _itemParent;
            set => SetAndRaise(ItemParentProperty, ref _itemParent, value);
        } 

        /// <summary>
        /// Command, выполняемый по нажатию на элемент.
        /// </summary>
        public ICommand? Command
        {
            get => GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Command завершения работы данного menu (закрывающий popup).
        /// </summary>
        public ICommand CloseCommand
        {
            get => GetValue(CloseCommandProperty);
            set => SetValue(CloseCommandProperty, value);
        }

        /// <summary>
        /// Горячие клавиши. Может быть пустым
        /// </summary>
        public string? Key
        {
            get => GetValue(KeyProperty);
            set => SetValue(KeyProperty, value);
        } 

        /// <summary>
        /// Выводимый текст.
        /// </summary>
        public string Text
        {
            get => GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        } 
         
        /// <summary>
        /// Открыта ли панель-меню данного Item'а?
        /// </summary>
        public bool IsSubMenuOpen
        {
            get => GetValue(IsSubMenuOpenProperty);
            set => SetValue(IsSubMenuOpenProperty, value);
        } 
         
        /// <summary>
        /// Существует ли данный Item в качестве разделителя в списке меню?
        /// </summary>
        public bool IsSeparator
        {
            get => GetValue(IsSeparatorProperty);
            set => SetValue(IsSeparatorProperty, value);
        }

        /// <summary>
        /// Максимальная отображаемая ширина текста IMenuDataItem.Text.
        /// </summary>
        public double MaxTextWidth
        {
            get => GetValue(MaxTextWidthProperty);
            set => SetValue(MaxTextWidthProperty, value);
        }  

        public CustomMenuItem()
        { 
            CloseCommand = ReactiveCommand.Create(() => { IsSubMenuOpen = false; });
        }

        /// <summary>
        /// Высчитывание необходимо размера текста.
        /// </summary> 
        public static ColumnDefinitions GetTextColumnDifs(
            IEnumerable<IMenuDataItem> menuItems, 
            double maxTextWidth,
            double fontSize,
            FontFamily fontFamily, 
            FontWeight fontWeight,
            FontStyle fontStyle)
        {
            var longTextString = menuItems.MaxBy(x => x.Text.Length)?.Text;
            var longKeyString  = menuItems.MaxBy(x => x.Key?.Length)?.Key;

            var calculatedTextWidth = longTextString?.GetTextWidth(fontSize, fontFamily, fontWeight, fontStyle);
            var calculatedKeyWidth  = longKeyString?.GetTextWidth(fontSize, fontFamily, fontWeight, fontStyle);

            var resultTextWidth = ((calculatedTextWidth > maxTextWidth ? maxTextWidth : calculatedTextWidth) ?? 1) + 32; // +отступ cбоку.
            var resultKeyWidth  = ((calculatedKeyWidth + 4 > 80 ? 80 : calculatedKeyWidth) ?? 0); 

            var columnDifs = new ColumnDefinitions();

            var textDefinition = new ColumnDefinition();
            textDefinition.Width = new GridLength(resultTextWidth, GridUnitType.Pixel);
            var keyDefinition = new ColumnDefinition();
            keyDefinition.Width = new GridLength(resultKeyWidth, GridUnitType.Pixel);

            columnDifs.Add(textDefinition);
            columnDifs.Add(keyDefinition);

            return columnDifs;
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
             
            _textContainerGrid = e.NameScope.Find<Grid>("PART_TextContainerGrid");

            UpdateColumns();
        } 

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        { 
            base.OnPointerPressed(e);

            if(!IsSeparator)
            {
                _customMenuItemControlManager?.OnPointedPressed(this); 
                if(Items.Count() == 0)
                {
                    Close();
                    Command?.Execute(null);
                } 
            }

            e.Handled = true;
        }

        protected override void OnPointerEntered(PointerEventArgs e)
        {
            base.OnPointerEntered(e);
             
            if(!IsSeparator)
            {
                _customMenuItemControlManager?.OnPointerEntered(this);
            }

            e.Handled = true;
        }
         
        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            InitObservables();

            ItemParent = this.GetParent<CustomMenuItem>(); 

            _customMenuItemControlManager?.Remove(this);
            EnsureMenuManager(e.Root);

            base.OnAttachedToVisualTree(e);
        }

        protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnDetachedFromVisualTree(e);

            _disposables.DisposeAll();

            _customMenuItemControlManager?.Remove(this);
            _customMenuItemControlManager = null;
        }
         
        protected void Close()
        {
            if(ItemParent != null)
            {
                ItemParent.Close();
            }
            else
            {
                CloseCommand.Execute(null);
            }
        }

        private void InitObservables()
        { 
            this.GetObservable(ColumnDefinitionsProperty)
                .Do(x => UpdateColumns())
                .Subscribe()
                .AddTo(_disposables);


            this.GetObservable(IsSubMenuOpenProperty)
                .Do(x => SetMenuTextWidth())
                .Subscribe()
                .AddTo(_disposables);
        }

        [MemberNotNull(nameof(_customMenuItemControlManager))]
        private void EnsureMenuManager(IRenderRoot? root = null)
        {
            _customMenuItemControlManager = CustomMenuItemControlManager.GetOrCreateForRoot(root ?? this.GetVisualRoot());
            _customMenuItemControlManager.Add(this);
        }
          
        private void UpdateColumns()
        { 
            if(_textContainerGrid != null)
            {
                _textContainerGrid.ColumnDefinitions = ColumnDefinitions?.Clone() ?? new ColumnDefinitions();
            }
        }

        private void SetMenuTextWidth()
        {
            if(Items != null)
            {
                var columnDifs = CustomMenuItem.GetTextColumnDifs(Items, MaxTextWidth, FontSize, FontFamily, FontWeight, FontStyle);

                ChildsColumnDefinitions = columnDifs;
            } 
        }
    }
}
