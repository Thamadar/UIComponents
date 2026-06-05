using Avalonia.Input;
using Client.Avalonia.Services;
using Client.Avalonia.Services.Interfaces;
using Client.Avalonia.Views.Geometry;
using Lib.Avalonia;
using Lib.Avalonia.Helpers;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Avalonia.Views.Graphs
{
    /// <summary>
    /// VM, отвечающая за вкладку "Графики". 
    /// </summary>
    public sealed partial class GraphsViewModel : ViewModelBase, ITabVM
    {

        #region Fields

        private IEnumerable<IHotKey> _defaultTabHotKeys = new IHotKey[0];

        #endregion 

        #region Properties

        /// <inheritdoc/>
        public Guid Id { get; }

        #endregion

        #region .ctor

        /// <summary>
        /// Конструктор-заглушка, дабы Designer не падал.
        /// </summary>
        public GraphsViewModel()
            : this(Guid.NewGuid())
        {

        }
        public GraphsViewModel(Guid id)
        { 
            Id = id;

            InitDefaultHotKeys();
        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public IEnumerable<IHotKey> GetTabHotKeys() => _defaultTabHotKeys;

        /// <inheritdoc/>
        public Task DisposeTab()
        {
            //Какие-нибудь отписки, дочерние Dispose, await...

            Dispose();

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task LoadTab()
        {
            //Какие-нибудь подписки, await...

            return Task.CompletedTask;
        }

        #endregion

        /// <summary>
        /// Инициализация горячих клавиш данной вкладки.
        /// </summary>
        private void InitDefaultHotKeys()
        {
            _defaultTabHotKeys = new List<IHotKey>
            {
                //Какие-то горячие клавиши...
            };
        }
    }
}
