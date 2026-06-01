using Lib.Avalonia.Services.Dialogs.ViewDialog;
using ReactiveUI;
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Lib.Avalonia.Services.Dialogs;

public class DialogData<P, R> : ViewModelBase
	where P : ViewDialogParameters
	where R : ViewDialogDataResult
{
	#region Fields

	private readonly Subject<IViewDialog<P, R>?> _currentDialogSubject = new Subject<IViewDialog<P, R>?>();

	private IViewDialog<P, R>? _currentDialog;

	#endregion Fields

	#region Properties

	public IObservable<IViewDialog<P, R>?> CurrentDialogObserve => _currentDialogSubject.AsObservable();

	/// <summary>
	/// Текущее диалоговое окно данного типа.
	/// </summary>
	public IViewDialog<P, R>? CurrentDialog
	{
		get => _currentDialog;
		set
		{
			_currentDialog = value;
			_currentDialogSubject.OnNext(value);
		}
	}

	#endregion Properties
}
