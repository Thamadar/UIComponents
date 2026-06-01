using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Lib.Avalonia.Services.Dialogs.ViewDialog;
using ReactiveUI;

namespace Lib.Avalonia.Services.Dialogs.MessageBox;

public enum MessageBoxTypeEnum
{

	/// <summary>
	/// Ок.
	/// </summary>
	Ok,

	/// <summary>
	/// Да/нет.
	/// </summary>
	YesNo, 
}

public sealed partial class MessageBoxViewModel : BaseViewDialogViewModel<MessageBoxParameters, MessageBoxResult>
{

	#region Fields

	private MessageBoxTypeEnum _messageBoxType; 

	#endregion

	#region Properties

	/// <summary>
	/// Тип MessageBox.
	/// </summary>
	public MessageBoxTypeEnum MessageBoxType
	{
		get => _messageBoxType;
		private set => this.RaiseAndSetIfChanged(ref _messageBoxType, value);
	}

	#endregion

	#region Constructors

	public MessageBoxViewModel()
		  : base()
	{
		if(Design.IsDesignMode)
		{
			var messageBoxParameter = new MessageBoxParameters(
				MessageBoxTypeEnum.YesNo,
				"SomeTest",
				"Какой-то текст");
			 
			new Thread(() =>
			{
				_ = Open(messageBoxParameter);
			}).Start();
		}
	}

	#endregion

	#region Methods   

	/// <inheritdoc/>
	public override Task<MessageBoxResult> Open(MessageBoxParameters data)
	{  
		MessageBoxType = data.MessageBoxType;  

		return base.Open(data);
	}

	/// <inheritdoc/>
	public override async Task HardClose(bool ignoreHardCloseWarning = false)
	{
		if(!await OpenHardCloseWarning(ignoreHardCloseWarning))
		{
			return;
		} 

		await Close(new MessageBoxResult(DialogResultEnum.Close));
	}


	#endregion
}
