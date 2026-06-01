namespace Lib.Avalonia.Services.Dialogs.ViewDialog;

public enum DialogResultEnum
{
	Accept, //Принять
	Cancel, //Отмена
	Close,  //Закрыть
}

/// <summary>
/// Объект, хранящий итоговую информацию о работе диалогового окна.
/// </summary>
public class ViewDialogDataResult
{
	/// <summary>
	/// Результат работы диалогового окна.
	/// </summary>
	public DialogResultEnum DialogResult { get; }

	#region Constructor

	public ViewDialogDataResult(DialogResultEnum dialogResult)
	{
		DialogResult = dialogResult;
	}

	#endregion Constructor

}
