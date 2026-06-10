using Lib.Avalonia.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Avalonia.Extensions
{
    public static class MenuDataItemExtension
    {
        /// <summary>
        /// Установка состояния "IsSelected = false" у всей коллекции, а у указанного в параметре IMenuDataItem "IsSelected = true".  
        /// Если не передан IMenuDataItem, то вся коллекция IsSelected = false.
        /// </summary>
        /// <param name="menuItems">коллекция, по которой нужно пройтись</param>
        /// <param name="selectedMenuItem">item, которому выставить IsSelected = true</param>
        public static void SetSelectedStateToMenu(this IEnumerable<IMenuDataItem> menuItems, IMenuDataItem? selectedMenuItem = null)
        {
            foreach(var menu in menuItems)
            {
                menu.IsSelected = selectedMenuItem != null && menu == selectedMenuItem ?
                                  true :
                                  false;
            }
        }
    }
}
