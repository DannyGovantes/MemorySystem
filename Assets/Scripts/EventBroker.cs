using System;
public class EventBroker
{


    public static Action<string, string> HUDController;
    public static Action WinController;


    public static void CallHUDController(string title, string score)
    {
        HUDController?.Invoke(title, score);

    }

    public static void CallWinController()
    {
        WinController?.Invoke();
    }

}
