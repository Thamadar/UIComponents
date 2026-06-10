using Lib.Avalonia;
using Lib.Avalonia.Helpers;
using System.Collections.ObjectModel;

namespace Client.Avalonia.Views.Geometry
{
    public sealed partial class GeometryManagementPanelViewModel : ViewModelBase
    {
        /// <summary>
        /// Список меню управления.
        /// </summary>
        public ObservableCollection<IMenuDataItem> MenuItems { get; }   

        public GeometryManagementPanelViewModel()
        {
            MenuItems = new ObservableCollection<IMenuDataItem>();

            LoadMenuItems();
        }
         
        private void LoadMenuItems()
        {
            var fileMenu = MenuDataItemBuilder
               .Create("Файл")
               .AddChild(MenuDataItemBuilder
                    .Create("Новый...") 
                    .WithCommand(Commands.NewFileCommand) 
                    .WithKey("Ctrl+N")
                    .Build())
               .AddChild(MenuDataItemBuilder
                    .Create("Открыть...")
                    .WithCommand(Commands.OpenFileCommand)
                    .WithKey("Ctrl+O")
                    .Build())
               .AddChild(MenuDataItemBuilder
                    .Create("Последние файлы")
                    .AddChild(MenuDataItemBuilder
                        .Create("0 Project0.PSD")
                        .WithCommand(Commands.OpenFileCommand)
                        .WithCommandParameter("0 Project0.PSD")
                        .Build())
                    .AddChild(MenuDataItemBuilder
                        .Create("1 Project111.PSD")
                        .WithCommand(Commands.OpenFileCommand)
                        .WithCommandParameter("1 Project111.PSD")
                        .Build())
                    .AddChild(MenuDataItemBuilder
                        .Create("2 Project22.PSD")
                        .WithCommand(Commands.OpenFileCommand)
                        .WithCommandParameter("2 Project22.PSD")
                        .Build())
                    .AddChild(MenuDataItemBuilder
                        .Create("3 Project35.PSD")
                        .WithCommand(Commands.OpenFileCommand)
                        .WithCommandParameter("3 Project35.PSD")
                        .Build())
                    .AddChild(MenuDataItemBuilder
                        .Create("4 Project454354.PSD")
                        .WithCommand(Commands.OpenFileCommand)
                        .WithCommandParameter("4 Project454354.PSD")
                        .Build())
                    .AddChild(MenuDataItemBuilder
                        .Create("5 ProjectTestLongFile_2546233415676547_LongName.PSD")
                        .WithCommand(Commands.OpenFileCommand)
                        .WithCommandParameter("5 ProjectTestLongFile_2546233415676547_LongName.PSD")
                        .Build())
                    .AddChild(MenuDataItemBuilder
                        .Create("6 Project6.PSD")
                        .WithCommand(Commands.OpenFileCommand)
                        .WithCommandParameter("6 Project6.PSD")
                        .Build()) 
                    .Build())
               .AddChild(MenuDataItemBuilder
                    .Create()
                    .WithSeparator()
                    .Build())
               .AddChild(MenuDataItemBuilder
                    .Create("Сохранить...")
                    .WithCommand(Commands.SaveFileCommand)
                    .WithIcon("SaveIcon")
                    .WithKey("Ctrl+S")
                    .Build())
               .AddChild(MenuDataItemBuilder
                    .Create("Выход")
                    .WithCommand(Commands.CloseFileCommand) 
                    .Build())
               .Build();

            var editMenu = MenuDataItemBuilder
                .Create("Редактировать")  
                .AddChild(MenuDataItemBuilder
                    .Create("Отменить")
                    .WithIcon("UndoIcon")
                    .WithCommand(Commands.StepBackEditCommand) 
                    .WithKey("Ctrl+Z")
                    .Build())
                .AddChild(MenuDataItemBuilder
                    .Create("Вернуть")
                    .WithIcon("RedoIcon")
                    .WithCommand(Commands.StepForwardEditCommand)
                    .WithKey("Ctrl+Y")
                    .Build())
                .AddChild(MenuDataItemBuilder
                    .Create()
                    .WithSeparator()
                    .Build())
                .AddChild(MenuDataItemBuilder
                    .Create("Дублировать")
                    .WithIcon("DuplicateIcon")
                    .WithCommand(Commands.CloneEditCommand)
                    .WithKey("Ctrl+J")
                    .Build())
                .Build();

            var aboutMenu = MenuDataItemBuilder
                    .Create("Справка")
                    .WithCommand(Commands.ManualOpenCommand)
                    .Build(); 

            MenuItems.Add(fileMenu);
            MenuItems.Add(editMenu);
            MenuItems.Add(aboutMenu);
        } 
    }
}
