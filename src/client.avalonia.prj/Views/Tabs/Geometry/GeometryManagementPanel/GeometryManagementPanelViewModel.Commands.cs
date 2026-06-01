using Client.Avalonia.Helpers;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.Avalonia.Views.Geometry
{
    public sealed partial class GeometryManagementPanelViewModel 
    {
        public sealed class GeometryManagementPanelViewModelCommands
        {
            public ICommand NewFileCommand { get; }
            public ICommand OpenFileCommand { get; }
            public ICommand SaveFileCommand { get; }
            public ICommand CloseFileCommand { get; }

            public ICommand StepBackEditCommand { get; }
            public ICommand StepForwardEditCommand { get; }
            public ICommand CloneEditCommand { get; }

            public ICommand ManualOpenCommand { get; }

            public GeometryManagementPanelViewModelCommands(GeometryManagementPanelViewModel vm)
            {
                NewFileCommand  = ReactiveCommand.Create(vm.OnNewFile);
                OpenFileCommand = ReactiveCommand.Create(vm.OnOpenFile);
                SaveFileCommand = ReactiveCommand.Create(vm.OnSavefile);
                CloseFileCommand = ReactiveCommand.Create(vm.OnCloseFile);

                StepBackEditCommand    = ReactiveCommand.Create(vm.OnStepBackEdit);
                StepForwardEditCommand = ReactiveCommand.Create(vm.OnStepForwardEdit);
                CloneEditCommand       = ReactiveCommand.Create(vm.OnCloneEdit);

                ManualOpenCommand = ReactiveCommand.Create(vm.OnManualOpen);
            }
        }

        private GeometryManagementPanelViewModelCommands? _commands;

        public GeometryManagementPanelViewModelCommands Commands => _commands ??= new(this);

        private async void OnNewFile()
        {
            await MessageHelper.OpenMessageBoxMessage("Файл", "Новый файл успешно создан. (имитация действия)");
        }

        private async void OnOpenFile()
        {
            await MessageHelper.OpenMessageBoxMessage("Файл", "Файл успешно выбран. (имитация действия)");
        } 
        private async void OnCloseFile()
        {
            await MessageHelper.OpenMessageBoxMessage("Файл", "Файл успешно закрыт. (имитация действия)");
        }

        private async void OnSavefile()
        {
            await MessageHelper.OpenMessageBoxMessage("Файл", "Файл успешно сохранен. (имитация действия)");
        }

        private async void OnStepBackEdit()
        {
            await MessageHelper.OpenMessageBoxMessage("Редактирование", "Предыдущее действие было отменено. (имитация действия)");
        }

        private async void OnStepForwardEdit()
        {
            await MessageHelper.OpenMessageBoxMessage("Редактирование", "Предыдущее действие было восстановлено. (имитация действия)");
        }

        private async void OnCloneEdit()
        {
            await MessageHelper.OpenMessageBoxMessage("Редактирование", "Графический элемент успешно клонирован. (имитация действия)");
        }

        private async void OnManualOpen()
        {
            await MessageHelper.OpenMessageBoxMessage("Справка", "Открыт мануал приложения. (имитация действия)");
        }

    }
}
