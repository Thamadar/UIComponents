using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives; 
using Avalonia.Input; 
using Avalonia.Metadata;
using Avalonia.Rendering; 
using Avalonia.VisualTree; 
using Lib.Avalonia.Controls.Helpers;
using Lib.Avalonia.Extensions;
using Lib.Avalonia.Helpers;
using ReactiveUI;
using System.Collections.ObjectModel; 
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Input;

namespace Lib.Avalonia.Controls
{
    public class CustomContextMenu : TemplatedControl
    { 
        private ContextMenuControlGroupManager? _contextMenuGroupManager;

        private ColumnDefinitions? _columnDefinitions;

        public static readonly StyledProperty<object?> ContentProperty =
            AvaloniaProperty.Register<CustomContextMenu, object?>(nameof(Content));

        public static readonly StyledProperty<ObservableCollection<IMenuDataItem>> ItemsProperty =
            AvaloniaProperty.Register<CustomContextMenu, ObservableCollection<IMenuDataItem>>(nameof(Items));

        public static readonly StyledProperty<ColumnDefinitions?> ColumnDefinitionsProperty =
            AvaloniaProperty.Register<CustomMenuItem, ColumnDefinitions?>(nameof(ColumnDefinitions));

        public static readonly StyledProperty<ICommand?> CommandProperty =
            AvaloniaProperty.Register<CustomContextMenu, ICommand?>(nameof(Command));


        public static readonly StyledProperty<ICommand> CloseCommandProperty =
            AvaloniaProperty.Register<CustomMenuItem, ICommand>(nameof(CloseCommand));
         
        public static readonly StyledProperty<bool> IsDropDownOpenProperty =
            AvaloniaProperty.Register<CustomContextMenu, bool>(nameof(IsDropDownOpen), false);

        public static readonly StyledProperty<string?> GroupNameProperty =
            AvaloniaProperty.Register<CustomContextMenu, string?>(nameof(GroupName));

        public static readonly StyledProperty<double> MaxTextWidthProperty =
            AvaloniaProperty.Register<CustomContextMenu, double>(nameof(MaxTextWidth));

        public ObservableCollection<IMenuDataItem> Items
        {
            get => GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public ColumnDefinitions? ColumnDefinitions
        {
            get => GetValue(ColumnDefinitionsProperty);
            set => SetValue(ColumnDefinitionsProperty, value);
        }

        /// <summary>
        /// Command.
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

        [Content]
        public object? Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        /// <summary>
        /// Максимальная отображаемая ширина текста IMenuDataItem.Text.
        /// </summary>
        public double MaxTextWidth
        {
            get => GetValue(MaxTextWidthProperty);
            set => SetValue(MaxTextWidthProperty, value);
        }

        /// <summary>
        /// Наименование группы menu-кнопок. Используется, чтобы объединить их поведение. Работает по аналогии с RadioButton.
        /// </summary>
        public string? GroupName
        {
            get => GetValue(GroupNameProperty);
            set => SetValue(GroupNameProperty, value);
        }

        /// <summary>
        /// Расскрыто ли меню?
        /// </summary>
        public bool IsDropDownOpen
        {
            get => GetValue(IsDropDownOpenProperty);
            set => SetValue(IsDropDownOpenProperty, value);
        }   

        static CustomContextMenu()
        {
            AffectsMeasure<CustomContextMenu>(IsDropDownOpenProperty);  
        }   

        public CustomContextMenu()
        {
            CloseCommand = ReactiveCommand.Create(() => { IsDropDownOpen = false; });
        }
         
        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        { 
            _contextMenuGroupManager?.Remove(this, GroupName);
            EnsureGroupManager(e.Root);

            base.OnAttachedToVisualTree(e);
        }

        protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnDetachedFromVisualTree(e);
             
            _contextMenuGroupManager?.Remove(this, GroupName);
            _contextMenuGroupManager = null;
        }

        protected override void OnPointerEntered(PointerEventArgs e)
        {
            base.OnPointerEntered(e);
             
            _contextMenuGroupManager?.OnPointerEntered(this);
        }

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            if(Items.Count > 0)
            {
                IsDropDownOpen = !IsDropDownOpen;
            }
            else if(IsDropDownOpen)
            {
                IsDropDownOpen = false;
            }
            else
            { 
                Command?.Execute(null);
            } 
            
            e.Handled = true; 
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);

            if(change.Property == IsDropDownOpenProperty)
            {
                OnIsDropDownOpenChanged(change.GetNewValue<bool>());
            }
            else if(change.Property == GroupNameProperty)
            {
                var (oldValue, newValue) = change.GetOldAndNewValue<string?>();
                OnGroupNameChanged(oldValue, newValue);
            }
        } 

        private void OnGroupNameChanged(string? oldGroupName, string? newGroupName)
        {
            if(!string.IsNullOrEmpty(oldGroupName))
            {
                _contextMenuGroupManager?.Remove(this, oldGroupName);
            }
            if(!string.IsNullOrEmpty(newGroupName))
            {
                _contextMenuGroupManager?.Add(this);
            }
        }

        private void OnIsDropDownOpenChanged(bool value)
        {
            if(value)
            {
                SetMenuTextWidth(); 
                _contextMenuGroupManager?.OnIsDropDownOpenChanged(this);
            }
        }

        private void SetMenuTextWidth()
        {
            var columnDifs = CustomMenuItem.GetTextColumnDifs(Items, MaxTextWidth, FontSize, FontFamily, FontWeight, FontStyle);

            ColumnDefinitions = columnDifs;
        }

        [MemberNotNull(nameof(_contextMenuGroupManager))]
        private void EnsureGroupManager(IRenderRoot? root = null)
        {
            _contextMenuGroupManager = ContextMenuControlGroupManager.GetOrCreateForRoot(root ?? this.GetVisualRoot());
            _contextMenuGroupManager.Add(this);
        }  
    }
}
