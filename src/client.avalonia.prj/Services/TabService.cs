using Client.Avalonia.Services.Interfaces;
using Client.Avalonia.Views;
using Client.Avalonia.Views.Geometry.Shapes;
using DynamicData;
using Lib.Avalonia.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Client.Avalonia.Services 
{
    public class TabService : ITabService
    {
        #region Fields

        private static readonly Lazy<ITabService> _instance = new Lazy<ITabService>(() => new TabService());

        private readonly ISourceList<ITabMenu> _totalTabMenu;
        private readonly ISourceList<IHotKey> _totalHotKeys;
        private readonly Subject<ITabVM?> _currentSelectedTabVMSubject;

        //private IList<IDisposable> _disposables = new List<IDisposable>();
        private ITabVM? _currentSelectedTabVM;

        #endregion

        #region Properties

        /// <summary>
        /// Экземпляр.
        /// </summary>
        public static ITabService Instance => _instance.Value;

        /// <inheritdoc/>
        public IObservable<ITabVM?> CurrentSelectedTabVMObservable => _currentSelectedTabVMSubject.AsObservable();

        /// <summary>
        /// Текущая выбранная геом. фигура
        /// </summary>
        private ITabVM? CurrentSelectedTabVM
        {
            get => _currentSelectedTabVM;
            set
            {
                _currentSelectedTabVM = value;
                _currentSelectedTabVMSubject.OnNext(value);
            }
        }

        #endregion

        #region .ctor

        private TabService()
        {
            _totalTabMenu                = new SourceList<ITabMenu>();
            _totalHotKeys                = new SourceList<IHotKey>();
            _currentSelectedTabVMSubject = new Subject<ITabVM?>();
        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public IObservable<IChangeSet<ITabMenu>> ConnectToTotalTabMenu()
        {
            return _totalTabMenu.Connect();
        }

        /// <inheritdoc/>
        public IObservable<IChangeSet<IHotKey>> ConnectToCurrentTabHotKeys()
        {
            return _totalHotKeys.Connect();
        }

        /// <inheritdoc/>
        public ITabVM? GetCurrentSelectedTabVM() => CurrentSelectedTabVM;
          
        /// <inheritdoc/>
        public void LoadTotalTabMenuData()
        {
             //TO DO:
        }

        /// <inheritdoc/>
        public async Task SelectTabMenuById(Guid? guid)
        {
            var selectedTabMenu = _totalTabMenu.Items.FirstOrDefault(x => x.Id.Equals(guid));
            
            if(selectedTabMenu != null && !selectedTabMenu.Id.Equals(CurrentSelectedTabVM?.Id))
            { 
                if(CurrentSelectedTabVM != null)
                { 
                    selectedTabMenu.IsSelected = false;
                    await CurrentSelectedTabVM.DisposeTab();
                }  

                var tabVM = CreateTabVM(selectedTabMenu.TabCategory);

                CurrentSelectedTabVM = tabVM; 

                if(CurrentSelectedTabVM != null)
                {
                    //selectedTabMenu.IsSelected = true;
                    await CurrentSelectedTabVM.LoadTab();
                }  
            } 
        }
         
        /// <summary>
        /// Создание свежей вкладки, а именно VM, возвращая ITabVM.
        /// </summary> 
        private ITabVM? CreateTabVM(TabCategoryEnum tabCategoryEnum)
        { 
            return tabCategoryEnum switch
            {
                TabCategoryEnum.GraphicEditor => new GeometryViewModel(),
                TabCategoryEnum.Graphs        => new GraphsViewModel(),

                //TO DO: остальные типы...
                _ => throw new ArgumentOutOfRangeException("Tab create invalid")
            }; 
        }

        #endregion
    }
}
