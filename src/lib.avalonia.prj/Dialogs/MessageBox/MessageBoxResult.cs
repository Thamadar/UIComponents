using Lib.Avalonia.Services.Dialogs.ViewDialog;

namespace Lib.Avalonia.Services.Dialogs.MessageBox;

public class MessageBoxResult : ViewDialogDataResult
{ 
	public MessageBoxResult(DialogResultEnum dialogResult)
		: base(dialogResult)
	{

	} 
}
