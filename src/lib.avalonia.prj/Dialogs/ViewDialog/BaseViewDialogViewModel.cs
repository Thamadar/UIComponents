using Avalonia.Controls;
using Avalonia.Markup.Xaml.Templates; 
using Lib.Avalonia.Extensions;
using Lib.Avalonia.Services.Dialogs.MessageBox; 
using ReactiveUI;

using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Lib.Avalonia.Services.Dialogs.ViewDialog;
public abstract class BaseViewDialogViewModel<P, R> : ViewModelBase, IViewDialog<P, R>, IDisposable
	 where P : ViewDialogParameters
	 where R : ViewDialogDataResult
{

	#region Fields 

	protected ObservableAsPropertyHelper<string>? _header;
	protected ObservableAsPropertyHelper<string>? _message;

	protected MessageBoxViewModel? _warningYesNoMessageBox;

	protected TaskCompletionSource<R> _dialogTask = new();

	protected ControlTemplate? _headerIconControlTemplate;
	protected ControlTemplate? _displayIconControlTemplate;

	private IReactiveCommand _outCommand;

	private bool _hardCloseWarning;
	private bool _ignoreClickOut;
	private bool _isDark;
	private bool _isHidden;
	private bool _isOpen; 

	#endregion

	#region Properties

	/// <summary>
	/// Заголовок диалогового окна.
	/// </summary>
	public string Header => _header?.Value ?? "";

	/// <summary>
	/// Текст диалогового окна.
	/// </summary>
	public string Message => _message?.Value ?? "";
	  
	/// <inheritdoc/>
	public bool IsDark
	{
		get => _isDark;
		protected set => this.RaiseAndSetIfChanged(ref _isDark, value);
	}

	/// <inheritdoc/>
	public bool IsHidden
	{
		get => _isHidden;
		protected set => this.RaiseAndSetIfChanged(ref _isHidden, value);
	}

	/// <inheritdoc/>
	public bool IsOpen
	{
		get => _isOpen;
		protected set => this.RaiseAndSetIfChanged(ref _isOpen, value);
	}

	/// <inheritdoc/>
	public bool IgnoreClickOut
	{
		get => _ignoreClickOut;
		protected set => this.RaiseAndSetIfChanged(ref _ignoreClickOut, value);
	}

	/// <inheritdoc/>
	public bool HardCloseWarning
	{
		get => _hardCloseWarning;
		protected set => this.RaiseAndSetIfChanged(ref _hardCloseWarning, value);
	}

	/// <inheritdoc/>
	//public TimeSpan? Timeout { get; private set; }

	/// <inheritdoc/>
	public ControlTemplate? HeaderIconControlTemplate
	{
		get => _headerIconControlTemplate;
		protected set => this.RaiseAndSetIfChanged(ref _headerIconControlTemplate, value);
	}

	/// <inheritdoc/>
	public ControlTemplate? DisplayIconControlTemplate
	{
		get => _displayIconControlTemplate;
		protected set => this.RaiseAndSetIfChanged(ref _displayIconControlTemplate, value);
	} 

	/// <inheritdoc/>
	public IReactiveCommand OutCommand
	{
		get => _outCommand;
		protected set => this.RaiseAndSetIfChanged(ref _outCommand, value);
	}

	#endregion

	#region Constructor

	public BaseViewDialogViewModel()
	{

	}

	#endregion

	#region Methods
	 
	/// <inheritdoc/>
	public virtual Task Close(R dialogResult)
	{
		IsOpen   = false;
		IsHidden = false;
		IsDark   = false;

		Dispose();

		_dialogTask?.TrySetResult(dialogResult); 

		return Task.CompletedTask;
	}

	/// <inheritdoc/>
	public virtual void Hide()
	{
		if(_dialogTask != null
			&& !_dialogTask.Task.IsCompleted)
		{
			IsHidden = true;
		}
	}

	/// <inheritdoc/>
	public virtual void Shade()
	{
		if(_dialogTask != null
			&& !_dialogTask.Task.IsCompleted)
		{
			IsDark = true;
		}
	}

	public virtual void Unshade()
	{
		if(_dialogTask != null
			&& !_dialogTask.Task.IsCompleted)
		{
			IsDark = false;
		}
	}

	/// <inheritdoc/>
	public virtual void Unhide()
	{
		if(_dialogTask != null
			&& !_dialogTask.Task.IsCompleted)
		{
			IsHidden = false;
		}
	}
	 
	/// <inheritdoc/>
	public virtual async Task<R> Open(P parameters)
	{
		if(_dialogTask.Task.IsCompleted)
			_dialogTask = new TaskCompletionSource<R>(); 

		OutCommand = ReactiveCommand.CreateFromTask(CloseCommandMethod);
		OutCommand.AddTo(_disposables);

		parameters.Header.ToProperty(this, x => x.Header, out _header);
		this.RaisePropertyChanged(nameof(Header));

		parameters.Message.ToProperty(this, x => x.Message, out _message);
		this.RaisePropertyChanged(nameof(Message));

		IgnoreClickOut             = parameters.IgnoreClickOut;
		HardCloseWarning           = parameters.HardCloseWarning;
		HeaderIconControlTemplate  = parameters.HeaderIconControlTemplate;
		DisplayIconControlTemplate = parameters.DisplayIconControlTemplate; 

		if(!Design.IsDesignMode)
		{
			IsOpen = true;
		}

		if(parameters.HardCloseWarning)
		{
			_warningYesNoMessageBox = new();
		}
		 
		var result = await _dialogTask.Task;   
		return result;
	}

	/// <summary>
	/// Метод для Command'а по закрытию диалогового окна.
	/// </summary> 
	protected virtual async Task CloseCommandMethod()
	{
		if(!IsHidden && IsOpen)
		{
			await HardClose(false);
		}
	}

	/// <inheritdoc/> 
	public abstract Task HardClose(bool ignoreHardCloseWarning = false);

	/// <inheritdoc/> 
	public virtual async Task<bool> OpenHardCloseWarning(bool ignoreHardCloseWarning = false)
	{
		if(HardCloseWarning && !ignoreHardCloseWarning)
		{
			var messageBoxParameter = new MessageBoxParameters(
			MessageBoxTypeEnum.YesNo,
			"Предупреждение",
			"Вы уверены, что желаете закрыть окно?");

			var dialogResult = await DialogSystem.OpenMessageBox(messageBoxParameter, _warningYesNoMessageBox);
			if(dialogResult.DialogResult == DialogResultEnum.Cancel ||
				dialogResult.DialogResult == DialogResultEnum.Close)
			{
				return false;
			}
		}

		if(_warningYesNoMessageBox != null)
		{
			await _warningYesNoMessageBox.HardClose();
		}

		return true;
	}

	#endregion

}
