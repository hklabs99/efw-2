using UnityEngine;
using TMPro;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class PlayGamesController : MonoBehaviour
{
    [Header ("Display Text incase Login fails")]
    [Tooltip ("The Text is set via the Script, no need to do it through the editor")]
    public TextMeshProUGUI mainText;

    // Start is called before the first frame update
    void Start()
    {
        AuthenticateUser ();
    }

    void AuthenticateUser ()
    {
        //PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder ().Build ();
        //PlayGamesPlatform.InitializeInstance (config);
        //PlayGamesPlatform.Activate ();
        Social.localUser.Authenticate ((bool success) =>
        {
            if (success == true)
                print ("Logged into Google Play Games Service");

            else
            {
                Debug.LogError ("Unable to log into Google Play Games Services");
                mainText.text = "Could not log into  Google Play Games Serivces!!";
            }
        });
    }
}