using Avalonia.Input;
using DynamicData;
using Lib.Avalonia;
using Lib.Avalonia.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Avalonia.Views
{
    public class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// Горячие клавиши окна.
        /// </summary>
        public ObservableCollection<IHotKey> HotKeys { get; } 

        public MainWindowViewModel()
        {
            HotKeys = new ObservableCollection<IHotKey>(); 

            LoadHotKeys();
        }

        /// <summary>
        /// Прогрузка горячих клавиш.
        /// TO DO: при смене вкладки надо менять клавиши, ибо другая среда - другие клавиши. Проверка на enum.
        /// </summary>
        public void LoadHotKeys()
        {
            HotKeys.Clear();

            //HotKeys.AddRange(GeometryViewModel.GetHotKeys());
        }
    }
}
