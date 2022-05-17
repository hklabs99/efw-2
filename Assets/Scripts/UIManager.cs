using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText, _scoreTextDeath;
    public TextMeshProUGUI _coinText, _coinTextDeath;
    public TextMeshProUGUI _highScore, _highScoreDeath;

    [SerializeField] Sprite[] _livesSprites;

    [SerializeField] Image _livesImage;

    [Header ("On Screen Instructions for the Players")]
    public GameObject playerInstructions;

    // Start is called before the first frame update
    void Awake ()
    {
        _scoreText.text = "Score: " + 0;
        _scoreTextDeath.text = "Score: " + 0;

        _highScore.text = "Highscore: " + 0;
        _highScoreDeath.text = "Highscore: " + 0;

        _coinText.text = ": " + 0;
        _scoreTextDeath.text = ": " + 0;
    }

    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
            playerInstructions.SetActive (false);
    }

    public void UpdateCoins (int coinScore)
    {
        _coinText.text = ": " + coinScore.ToString ();
        _coinTextDeath.text = ": " + coinScore.ToString ();
    }

    public void UpdateScore (int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString ();
        _scoreTextDeath.text = "Score: " + playerScore.ToString ();
    }

    public void HighScore (int highScore)
    {
        _highScore.text = "Highscore: " + highScore.ToString ();
        _highScoreDeath.text = "Highscore: " + highScore.ToString ();
    }

    public void UpdateLives (int currentLives)
    {
        _livesImage.sprite = _livesSprites[currentLives];
    }
}