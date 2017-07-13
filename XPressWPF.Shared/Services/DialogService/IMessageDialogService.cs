namespace XPressWPF.Shared.Services.DialogService
{
    public interface IMessageDialogService
    {
        MessageDialogResult ShowYesNoDialog(string title, string message);
        void ShowOkDialog(string title, string message);
    }

    public enum MessageDialogResult
    {
        Yes,
        No
    }
}
