using Avalonia.Markup.Xaml.Templates; 
using System.Threading.Tasks;

namespace Lib.Avalonia.Services.Dialogs.ViewDialog;
public interface IViewDialog : IDialog
{

	/// <summary>
	/// Игнорировать клики вне открытого окна.
	/// </summary>
	bool IgnoreClickOut { get; }
	 
	/// <summary>
	/// Скрыто ли диалоговое окно?
	/// </summary>
	bool IsHidden { get; }
	 
	/// <summary> 
	/// Иконка заголовка. Может быть пустым.
	/// </summary>
	ControlTemplate? HeaderIconControlTemplate { get; }

	/// <summary> 
	/// Отображаемая иконка в панеле. Может быть пустым.
	/// </summary>
	ControlTemplate? DisplayIconControlTemplate { get; }

	/// <summary> 
	/// Стечение времени по которому IViewDialog сам закроется. Может быть пустым.
	/// </summary>
	//TimeSpan? Timeout { get; }

	/// <summary>
	/// Скрыть диалоговое окно.
	/// </summary>
	void Hide();

	/// <summary>
	/// Раскрыть диалоговое окно.
	/// </summary>
	void Unhide();
}

public interface IViewDialog<P, R> : IViewDialog
		where P : ViewDialogParameters
		where R : ViewDialogDataResult
{
	#region Methods 

	/// <summary>
	/// Открыть диалоговое окно.
	/// </summary> 
	/// <param name="parameters">параметры IDialog.</param> 
	Task<R> Open(P parameters); 

	/// <summary>
	/// Закрыть диалоговое окно.
	/// </summary> 
	/// <param name="result">результат IDialog.</param> 
	Task Close(R result);

	#endregion Methods
}
