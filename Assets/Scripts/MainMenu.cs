using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header ("Items to be Disappeared After Buttons are Clicked")]
    public GameObject mainMenu;

    [Header ("Scene Selection Menu")]
    [Tooltip ("This will Appear After the PlayButton has been Clicked and Everything has Disappeared")]
    public GameObject playButtonClickedEvent;

    ADManager _adManager;

    void Awake ()
    {
        _adManager = GameObject.Find ("AdManager").GetComponent <ADManager> ();
    }

    #region Forever Visible

    /// <summary>
    /// This will be forever visible in all menu subsections
    /// </summary>
    public void QuitGame ()
    {
        Application.Quit ();
        print ("QUIT GAME");
    }

    #endregion

    #region Play Game Button Pressed Event

    public void PlayButtonPressed ()
    {
        mainMenu.SetActive (false);
        
        ///This will happen after the Main Menu has disappeared
        playButtonClickedEvent.SetActive (true);
    }

    public void BackToMainMenu ()
    {
        playButtonClickedEvent.SetActive (false);
        mainMenu.SetActive (true);
    }

    #region Scene Loading Methods

    public void LoadDefaultScene ()
    {
        _adManager.PlayInterstitialAd ();
        SceneManager.LoadScene ("Default_Scene");
        Time.timeScale = 1.0f;
    }

    public void LoadSunnyMountainsScene ()
    {
        _adManager.PlayInterstitialAd ();
        SceneManager.LoadScene ("SunnyMountains");
        Time.timeScale = 1.0f;
    }

    public void LoadPinkyMoonScene ()
    {
        _adManager.PlayInterstitialAd ();
        SceneManager.LoadScene ("PinkyMoon");
        Time.timeScale = 1.0f;
    }

    public void LoadSnowyScene ()
    {
        _adManager.PlayInterstitialAd ();
        SceneManager.LoadScene ("Snowy");
        Time.timeScale = 1.0f;
    }

    #endregion

    #endregion
}