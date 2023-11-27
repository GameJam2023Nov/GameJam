using System;

internal interface IMessages
{
    void ShowMessage(string message, float delay);
    void ShowRestartOrGoToHome(string title, Action action, Action action1);
    void ShowGameCompleted(string title, Action action);
}