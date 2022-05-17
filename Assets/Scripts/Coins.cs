using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] float _coinSpeed = 3.0f;

    [SerializeField] AudioClip _coinCollect;

    public int bronzeCoins, silverCoins, goldCoins;

    Player _player;

    void Awake ()
    {
        _player = GameObject.Find ("Player").GetComponent <Player> ();
        CoinConversion ();
    }

    public void CoinConversion ()
    {
        bronzeCoins = _player.bronzeCoins;
        silverCoins = 10 * bronzeCoins;
        goldCoins = 10 * silverCoins;
    }

    // Update is called once per frame
    void Update ()
    {
        transform.Translate (Vector3.down * _coinSpeed * Time.deltaTime);

        if (transform.position.y < -4.5f)
        {
            float randomX = Random.Range (-8f, 8f);
            transform.position = new Vector3 (randomX, 7, 0);
        }
    }

    #region Bronze Coin Logic

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint (_coinCollect, transform.position);

            _player.IncrementCoins ();
            Destroy (this.gameObject);
        }
    }

    #endregion
}