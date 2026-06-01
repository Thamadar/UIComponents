using ReactiveUI;

using System.Reactive;
using System.Threading.Tasks;

namespace Lib.Avalonia.Services.Dialogs;

/// <summary>
/// Интерфейс диалогового окна.
/// </summary>
public interface IDialog
{
	#region Properties 

	/// <summary>
	/// Затемнено ли окно?
	/// </summary>
	bool IsDark { get; } 

	/// <summary>
	/// Открыто ли диалоговое окно?
	/// </summary>
	bool IsOpen { get; } 

	/// <summary>
	/// Выдавать ли MessageBox типа yes/no предупреждения при закрытии? 
	/// </summary>
	bool HardCloseWarning { get; }

	/// <summary>
	/// Команда закрытия диалогового окна.
	/// </summary>
	IReactiveCommand OutCommand { get; }

	#endregion Properties

	#region Methods  

	/// <summary>
	/// Экстренное закрытие IDialog.
	/// </summary> 
	/// <param name="ignoreHardCloseWarning">игнорировать ли свойство HardCloseWarning, отвечающее за открытие MessageBox с
	/// уточнением "Уверены, что желаете закрыть?".</param> 
	Task HardClose(bool ignoreHardCloseWarning);

	/// <summary>
	/// Открытие MessageBox с уточнением "Уверены, что желаете закрыть?"
	/// </summary> 
	/// <param name="ignoreHardCloseWarning">игнорировать ли свойство HardCloseWarning, отвечающее за открытие MessageBox с 
	/// уточнением "Уверены, что желаете закрыть?".</param> 
	/// <returns>Результат MessageBox: true - Accept, false - Cancel</returns>
	Task<bool> OpenHardCloseWarning(bool ignoreHardCloseWarning);
	  
	/// <summary>
	/// Затемнение диалогового окна.
	/// </summary>
	void Shade();

	/// <summary>
	/// Разтемнение диалогового окна.
	/// </summary>
	void Unshade(); 

	#endregion Methods
} 
