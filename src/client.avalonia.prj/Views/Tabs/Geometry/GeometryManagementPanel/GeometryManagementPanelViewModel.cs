using Avalonia;
using Client.Avalonia.Helpers;
using Lib.Avalonia;
using Lib.Avalonia.Extensions;
using Lib.Avalonia.Helpers;
using ReactiveUI; 
using System.Collections.ObjectModel;
using System.Diagnostics; 

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
            var fileChilds = new List<MenuDataItem>
            {
                new MenuDataItem(
                text: "Новый...", 
                command: Commands.NewFileCommand,
                key: "Ctrl+N"),

                new MenuDataItem(
                text: "Открыть...", 
                command: Commands.OpenFileCommand,
                key: "Ctrl+O"),
                 
                new MenuDataItem(
                text: "Последние файлы", 
                childs: new List<MenuDataItem>()
                {
                     new MenuDataItem(
                        text: "0 Project0.PSD", 
                        command: ReactiveCommand.Create(() => Debug.WriteLine("Project0.PSD"))),
                       new MenuDataItem(
                        text: "1 Project1.PSD",
                        command: ReactiveCommand.Create(() => Debug.WriteLine("Project1.PSD"))),
                         new MenuDataItem(
                        text: "2 Project2.PSD",
                        command: ReactiveCommand.Create(() => Debug.WriteLine("Project2.PSD"))),
                           new MenuDataItem(
                        text: "3 Project3.PSD",
                        command: ReactiveCommand.Create(() => Debug.WriteLine("Project3.PSD"))),
                          new MenuDataItem(
                        text: "4 Project4.PSD",
                        command: ReactiveCommand.Create(() => Debug.WriteLine("Project4.PSD"))),
                            new MenuDataItem(
                        text: "5 Project55555555_5555555555_5555555555.PSD",
                        command: ReactiveCommand.Create(() => Debug.WriteLine("Project5555555_5555555555_5555555555.PSD"))),
                              new MenuDataItem(
                        text: "6 Project6.PSD",
                        command: ReactiveCommand.Create(() => Debug.WriteLine("Project6.PSD"))),
                }),

                new MenuDataItem(isSeparator:true),

                new MenuDataItem(
                text: "Сохранить...",
                icon: Application.Current?.GetTemplateResource("SaveIcon"),
                command: Commands.SaveFileCommand,
                key: "Ctrl+S"),

                new MenuDataItem(isSeparator:true),

                new MenuDataItem(
                text: "Выход", 
                command: Commands.CloseFileCommand)
            };  
            var fileMenu = new MenuDataItem(
                text: "Файл",
                childs: fileChilds);  

            var editChilds = new List<MenuDataItem>
            { 
                new MenuDataItem(
                text: "Отменить",
                icon: Application.Current?.GetTemplateResource("UndoIcon"),
                key: "Ctrl+Z",
                command: Commands.StepBackEditCommand),

                new MenuDataItem(
                text: "Вернуть",
                icon: Application.Current?.GetTemplateResource("RedoIcon"),
                key: "Ctrl+Y",
                command: Commands.StepForwardEditCommand),

                new MenuDataItem(isSeparator:true),

                new MenuDataItem(
                text: "Дублировать",
                icon: Application.Current?.GetTemplateResource("DuplicateIcon"),
                key: "Ctrl+J",
                command: Commands.CloneEditCommand)
            }; 
            var editMenu = new MenuDataItem(
                text: "Редактировать", 
                childs: editChilds);

            var aboutMenu = new MenuDataItem(
                text: "Справка",
                command: Commands.ManualOpenCommand);

            MenuItems.Add(fileMenu);
            MenuItems.Add(editMenu);
            MenuItems.Add(aboutMenu);
        } 
    }
}
