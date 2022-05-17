using UnityEngine;
using Unity.Notifications.Android;

public class NotificationManager : MonoBehaviour
{
    void Start ()
    {
        CreateNotificationChannel ();
        print ("Notification channel created");

        SendNotification ();
    }

    void CreateNotificationChannel ()
    {
        var c = new AndroidNotificationChannel ()
        {
            Id = "Notification_1",
            Name = "PlayReminder",
            Importance = Importance.Default,
            Description = "Remninds the player to play the game",
        };

        AndroidNotificationCenter.RegisterNotificationChannel (c);
    }

    void SendNotification ()
    {
        var notification = new AndroidNotification ();
        notification.Title = "See How Far You Can Venture!";
        notification.Text = "Come back to play";
        notification.FireTime = System.DateTime.Now.AddSeconds (5f);
        notification.LargeIcon = "efw2_icon_large";
        notification.SmallIcon = "efw2_icon_small";
    }
}