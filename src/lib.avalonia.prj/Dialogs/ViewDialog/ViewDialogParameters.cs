using Avalonia.Markup.Xaml.Templates;
using System;
using System.Reactive.Linq;

namespace Lib.Avalonia.Services.Dialogs.ViewDialog;
public class ViewDialogParameters : ViewModelBase
{

	#region Properties

	/// <summary>
	/// Заголовок MessageBox.
	/// </summary>
	public IObservable<string> Header { get; set; }

	/// <summary>
	/// Сообщение MessageBox.
	/// </summary>
	public IObservable<string> Message { get; set; }

	/// <summary>
	/// Игнорировать ли клики вне открытого окна? 
	/// </summary>
	public bool IgnoreClickOut { get; }

	/// <summary>
	/// Выдавать ли MessageBox типа yes/no предупреждения при закрытии? 
	/// </summary>
	public bool HardCloseWarning { get; }

	/// <summary> 
	/// Иконка заголовка. Может быть пустым.
	/// </summary>
	public ControlTemplate? HeaderIconControlTemplate { get; }

	/// <summary> 
	/// Отображаемая иконка в панеле. Может быть пустым.
	/// </summary>
	public ControlTemplate? DisplayIconControlTemplate { get; }

	/// <summary> 
	/// Стечение времени по которому MessageBox сам закроется. Может быть пустым.
	/// </summary>
	public TimeSpan? Timeout { get; }

	#endregion

	#region Constructor

	public ViewDialogParameters(
		string header,
		string? message = null,
		bool ignoreClickOut = false,
		bool hardCloseWarning = false,
		ControlTemplate? headerIconControlTemplate = null,
		ControlTemplate? displayIconControlTemplate = null) 
	{
		Header                     = Observable.Return(header);
		Message                    = Observable.Return(message ?? "");

		IgnoreClickOut             = ignoreClickOut;
		HardCloseWarning           = hardCloseWarning;
		HeaderIconControlTemplate  = headerIconControlTemplate;
		DisplayIconControlTemplate = displayIconControlTemplate; 
	}

	#endregion Constructor
}
