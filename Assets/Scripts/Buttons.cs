using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] GameObject _pauseMenu, _pauseButton;

    Player _player;

    bool _canFire;

    // Start is called before the first frame update
    void Start ()
    {
        _player = GetComponent <Player> ();

        _pauseMenu.SetActive (false);
    }

    // Update is called once per frame
    void Update ()
    {
        if (_canFire)
        {
            _player.FireLaser ();
            print ("FIRE LASER");
        }
    }

    public void Fire ()
    {
        _canFire = true;
    }

    public void StopFiring ()
    {
        _canFire = false;
    }

    public void PauseGame ()
    {
        _pauseMenu.SetActive (true);
        print ("PAUSE GAME");
        Time.timeScale = 0;
        _pauseButton.SetActive (false);
    }

    public void ResumeGame ()
    {
        _pauseMenu.SetActive (false);
        print ("RESUME GAME");
        Time.timeScale = 1;
        _pauseButton.SetActive (true);
    }

    public void QuitGame ()
    {
        Application.Quit ();
        print ("QUIT GAME");
    }

    public void MainMenu ()
    {
        SceneManager.LoadScene (0);
    }

    public void ReloadGame ()
    {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
        Time.timeScale = 1.0f;
    }
}