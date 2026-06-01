

using Lib.Avalonia.Services.Dialogs;
using Lib.Avalonia.Services.Dialogs.MessageBox;

namespace Client.Avalonia.Helpers
{
    public static class MessageHelper
    {
        public static async Task<MessageBoxResult> OpenMessageBoxMessage(string title, string message)
        {
            var messageBoxParameter = new MessageBoxParameters(
                   MessageBoxTypeEnum.Ok,
                   title,
                   message);

            var result = await DialogSystem.OpenMessageBox(messageBoxParameter);

            return result;
        }
    }
}
