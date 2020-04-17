using System;
public class EventBroker
{


    public static Action<string, string> HUDController;


    public static void CallHUDController(string title, string score)
    {
        // if (HUDController != null)
        // {
        //     HUDController(title, score);
        // }

        HUDController?.Invoke(title, score);

    }

}
