using System.Windows;

namespace XPressWPF.Shared.Services.DialogService
{
    public class MessageDialogService : IMessageDialogService
    {
        public MessageDialogService()
        {

        }

        public MessageDialogResult ShowYesNoDialog(string title, string message)
        {
            // reference PresentationFramework
            // here we are using WPF MessageBox
            // simply because we have access to MessageBoxResult easly
            return MessageBox.Show(message, title, MessageBoxButton.YesNo) == MessageBoxResult.Yes
                ? MessageDialogResult.Yes : MessageDialogResult.No;
        }

        public void ShowOkDialog(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK);
        }
    }
}
