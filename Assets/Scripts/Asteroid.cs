using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] float _asteroidSpeed = 3.0f;

    Player _player;

    [SerializeField] GameObject _explosionPrefab;

    SpawnManager _spawnManager;

    [SerializeField] AudioClip _asteroidExplosionSound;

    AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find ("Player").GetComponent <Player> ();

        if (_player == null)
            Debug.LogError ("Player == null");

        _spawnManager = GameObject.Find ("Spawn_Manager").GetComponent <SpawnManager> ();

        if (_spawnManager == null)
            Debug.LogError ("Spawn_Manager == null");

        _audioSource = GetComponent <AudioSource> ();

        if (_audioSource == null)
            Debug.LogError ("Asteroid AudioSource == null");

        else
            _audioSource.clip = _asteroidExplosionSound;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 asteroidRotate = new Vector3 (0, 0, 2);
        transform.Rotate (_asteroidSpeed * asteroidRotate * Time.deltaTime);
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate (_explosionPrefab, transform.position, Quaternion.identity);
            _asteroidSpeed = 0;

            Destroy (other.gameObject);
            _spawnManager.StartSpawning ();
            Destroy (this.gameObject, .25f);

            _audioSource.Play();
        }
    }
}