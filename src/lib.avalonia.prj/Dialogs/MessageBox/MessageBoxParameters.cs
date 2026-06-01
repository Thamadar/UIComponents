
using Avalonia.Markup.Xaml.Templates;
using Lib.Avalonia.Services.Dialogs.ViewDialog; 

namespace Lib.Avalonia.Services.Dialogs.MessageBox;

/// <summary>
/// Параметры окна MessageBox.
/// </summary>
public class MessageBoxParameters : ViewDialogParameters
{ 
	public MessageBoxTypeEnum MessageBoxType { get; }

	/// <summary>
	/// Параметры MessageBox
	/// </summary>
	/// <param name="messageBoxType">тип MessageBox'а.</param>
	/// <param name="header">заголовок.</param>
	/// <param name="message">сообщение. Может быть пустым.</param>
	/// <param name="ignoreClickOut">игнорировать ли нажатия вне области? По умолчанию true.</param>
	/// <param name="hardCloseWarning">включить ли функцию "Отображать предупреждение Yes/No при попытке закрыть?". По умолчанию false.</param> 
	public MessageBoxParameters(
		MessageBoxTypeEnum messageBoxType,
		string header,
		string? message = null,
		bool ignoreClickOut = true,
		bool hardCloseWarning = false) 
		: base(header, message, ignoreClickOut, hardCloseWarning)
	{
		MessageBoxType = messageBoxType;
	} 
}
