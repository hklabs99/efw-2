using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _enemySpeed = 4f;
    [SerializeField] GameObject _laserPrefab;

    Player _player;
    Animator _enemyDeathAnimation;
    [SerializeField] AudioClip _explosionSoundClip;
    AudioSource _audioSource;
    float _fireRate = 3.0f;
    float _canFire = -1;

    void Start ()
    {
        _player = GameObject.Find ("Player").GetComponent <Player> ();

        if (_player == null)
            Debug.LogError ("Player == null");

        _enemyDeathAnimation = GetComponent <Animator> ();

        if (_enemyDeathAnimation == null)
            Debug.LogError ("Animation == null");

        _audioSource = GetComponent <AudioSource> ();

        if (_audioSource == null)
            Debug.LogError ("Explosion AudioSource == null");

        else
            _audioSource.clip = _explosionSoundClip;
    }

    void Update ()
    {
        CalculateMovement ();

        if (Time.time > _canFire)
        {
            _fireRate = Random.Range (3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate (_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren <Laser> ();

            for (int i = 0; i < lasers.Length; i++)
                lasers[i].AssignEnemyLaser ();
        }
    }

    void CalculateMovement ()
    {
        //if bottom of screen, respawn at top with a new random x position

        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if (transform.position.y < -6)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
                player.Damage();

            _enemyDeathAnimation.SetTrigger ("OnEnemyDeath");
            other.transform.GetComponent <Player> ().Damage ();
            _enemySpeed = 0;
            _audioSource.Play ();
            Destroy(this.gameObject, 2.8f);            
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (_player != null)
                _player.AddScore (10);
            
            _enemyDeathAnimation.SetTrigger ("OnEnemyDeath");
            _enemySpeed = 0;
            _audioSource.PlayDelayed (.25f);
            Destroy (GetComponent <Collider2D> ());
            Destroy(this.gameObject, 2.8f);            
        }
    }
}