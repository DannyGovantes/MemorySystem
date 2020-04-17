using System;

namespace Solution
{

    public class EventBroker
    {

        public static Action<string, string> HUDHandler;


        public static void CallHUDController(string title, string score)
        {
            HUDHandler?.Invoke(title, score);
        }

    }
}
