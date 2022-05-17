using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool _isGameOver;

    [SerializeField] GameObject _gameOverMenu;

    [SerializeField] GameObject _gameOverText;

    void Update()
    {
        if (Input.GetKeyDown (KeyCode.R) && _isGameOver == true)
            SceneManager.LoadScene (1);     //Current Game Scene

        else if (Input.GetKey (KeyCode.Escape))
        {
            print ("Quit game");
            Application.Quit ();
        }
    }

    public void GameOver ()
    {
        _isGameOver = true;
        Handheld.Vibrate ();
        Handheld.Vibrate ();
        Time.timeScale = 0.0f;
        _gameOverMenu.SetActive (true);
        StartCoroutine (FlickerText ());
    }

    IEnumerator FlickerText ()
    {
        while (true)
        {
            yield return new WaitForSeconds(.5f);
            _gameOverText.SetActive(false);

            yield return new WaitForSeconds(.5f);
            _gameOverText.SetActive(true);
        }
    }
}
