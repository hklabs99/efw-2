using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    #region Game_Variables

    [SerializeField] float _speed = 3.5f, _speedMultiplier = 2f, _fireRate = .5f, _tripleShotPowerDown = 5f, _speedPowerDown = 5f, _shieldPowerDown = 5f;
    float _canFire = -1f;
    [SerializeField] GameObject _laserPrefab, _tripleShotLaserPrefab, _shieldVisualiser;
    public int score, lives = 6, bronzeCoins, highScore;
    SpawnManager _spawnManager;
    bool _isTripleShotActive = false, _isSpeedActive = false, _isShieldActive = false;
    UIManager _uiManager;

    GameManager _gameManager;

    [SerializeField] GameObject _rightEngine, _leftEngine;

    [SerializeField] AudioClip _laserSoundClip, _explosionSoundClip;

    AudioSource _audioSourceLaser, _audioSourceExplosion;

    #endregion 

    // Start is called before the first frame update
    void Start ()
    {
        //take the current position = new position (0, 0, 0)
        transform.position = new Vector3 (0, 0, 0);     //Reassigns the position of the object to (0, 0, 0), no matter their location.

        #region Initialisation

        _spawnManager = GameObject.Find ("Spawn_Manager").GetComponent <SpawnManager> ();

        _uiManager = GameObject.Find ("Canvas").GetComponent <UIManager> ();

        if (_spawnManager == null)
            Debug.LogError ("The SpawnManager is NULL");

        _shieldVisualiser.SetActive (false);

        if (_uiManager == null)
            Debug.LogError ("The UIManager is null");

        _gameManager = GameObject.Find ("Game_Manager").GetComponent <GameManager> ();

        if (_gameManager == null)
            Debug.LogError ("GameManager == null");

        _audioSourceLaser = GetComponent <AudioSource> ();

        if (_audioSourceLaser == null)
            Debug.LogError ("AudioSource on the player == null");

        else
            _audioSourceLaser.clip = _laserSoundClip;

        if (_audioSourceExplosion == null)
            Debug.LogError ("Explosion AudioSource == null");

        else
            _audioSourceExplosion.clip = _explosionSoundClip;

        #endregion

        #region Saving Highscore

        highScore = PlayerPrefs.GetInt ("Highscore", 0);
        _uiManager._highScore.text = "Highscore: " + highScore.ToString ();
        _uiManager._highScoreDeath.text = "Highscore: " + highScore.ToString ();

        #endregion
    }

    void FixedUpdate ()
    {
        if (Input.GetKeyDown (KeyCode.E))
        { 
            lives = 0;
            _gameManager.GameOver ();
        }

        CalculateMovement ();

        #region SpawnLaser_&&_CoolDown

        #if UNITY_ANDROID

        if (CrossPlatformInputManager.GetButtonDown ("Fire_Button") && Time.time > _canFire)
            FireLaser ();

        #endif

        if ( (Input.GetKeyDown (KeyCode.Space)) && Time.time > _canFire)
            FireLaser ();

        #endregion

        _uiManager.UpdateLives (lives / 2);

        if (Time.timeScale == 0.0f)
            Time.timeScale += .2f;
    }

    public void FireLaser ()
    {
        _canFire = Time.time + _fireRate;

        if (_isTripleShotActive == true)
            Instantiate (_tripleShotLaserPrefab, transform.position, Quaternion.identity);

        else
            //This allows the spawning of objects on the player itself by an offset.
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);

        //play the laser audio clip
        _audioSourceLaser.Play ();
    }

    void CalculateMovement ()
    {
        float horizontalInput = CrossPlatformInputManager.GetAxis ("Horizontal");
        float verticalInput = CrossPlatformInputManager.GetAxis ("Vertical");

        Vector3 direction = new Vector3 (horizontalInput, verticalInput, 0);
        
        transform.Translate (direction * _speed * Time.deltaTime);

        #region Restricting_Player_Movement

        //if player position on y > 0, y position = 0
        //This clamps the movement, restricts it in other words
        transform.position = new Vector3 (transform.position.x, Mathf.Clamp (transform.position.y, -3.8f, 0), 0);

        if (transform.position.x > 11.3f)
            transform.position = new Vector3(-11.3f, transform.position.y, 0);

        else if (transform.position.x < -11.3f)
            transform.position = new Vector3(11.3f, transform.position.y, 0);

        #endregion
    }

    public void Damage ()
    {
        if (_isShieldActive == true)
        {
            lives++;
            _isShieldActive = false;
            _shieldVisualiser.SetActive (false);
            return;
        }

        lives--;
        Handheld.Vibrate ();

        if (lives == 4)
        {
            _leftEngine.gameObject.SetActive (true);
        }

        else if (lives == 2)
        {
            _rightEngine.gameObject.SetActive (true);
        }

        #region Game_Over_Section

        if (lives < 1)
        {
            _gameManager.GameOver ();
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
            HighScore ();
            _audioSourceExplosion.Play ();
        }
        #endregion
    }

    public void TripleShotActive ()
    {
        _isTripleShotActive = true;
        StartCoroutine (TripleShotPowerDownRoutine ());
    }

    IEnumerator TripleShotPowerDownRoutine ()
    {
        while (_isTripleShotActive == true)
        {
            yield return new WaitForSeconds(_tripleShotPowerDown);
            _isTripleShotActive = false;
        }
    }

    public void SpeedActive ()
    {
        _isSpeedActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine (SpeedPowerDownRoutine ());
    }

    IEnumerator SpeedPowerDownRoutine ()
    {
        while (_isSpeedActive == true)
        {
            yield return new WaitForSeconds (_speedPowerDown);
            _isSpeedActive = false;
            _speed /= _speedMultiplier;
        }
    }

    public void ShieldActive ()
    {
        _isShieldActive = true;
        _shieldVisualiser.SetActive (true);
        StartCoroutine (ShieldPowerDownRoutine ());
    }

    IEnumerator ShieldPowerDownRoutine ()
    {
        while (_isShieldActive == true)
        {
            yield return new WaitForSeconds (_shieldPowerDown);
            _isShieldActive = false;
            _shieldVisualiser.SetActive (false);
        }
    }

    public void AddScore (int points)
    {
        score += points;
        _uiManager.UpdateScore(score);
    }

    void HighScore ()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt ("Highscore", highScore);
            _uiManager.HighScore(highScore);
        }        
    }

    public void IncrementCoins ()
    {
        bronzeCoins++;
        PlayerPrefs.SetInt ("Bronze Coins", bronzeCoins);
        _uiManager.UpdateCoins (bronzeCoins);
    }
}