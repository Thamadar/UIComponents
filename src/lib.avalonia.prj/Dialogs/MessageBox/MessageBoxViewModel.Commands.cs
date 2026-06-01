using Lib.Avalonia.Services.Dialogs.ViewDialog;
using ReactiveUI; 

namespace Lib.Avalonia.Services.Dialogs.MessageBox;
public sealed partial class MessageBoxViewModel
{
	public sealed class MessageBoxViewModelCommands
	{
		public IReactiveCommand AcceptCommand { get; } 
		public IReactiveCommand CancelCommand { get; } 

		public MessageBoxViewModelCommands(MessageBoxViewModel vm)
		{
			AcceptCommand = ReactiveCommand.Create(() => { vm.Close(new MessageBoxResult(DialogResultEnum.Accept)); });
			CancelCommand = ReactiveCommand.Create(() => { vm.Close(new MessageBoxResult(DialogResultEnum.Cancel)); }); 
		}
	}

	private MessageBoxViewModelCommands? _commands;

	public MessageBoxViewModelCommands Commands => _commands ??= new(this);
}
