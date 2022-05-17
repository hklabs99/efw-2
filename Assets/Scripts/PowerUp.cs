using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float _powerUpSpeed = 3f;
    [SerializeField]int powerupID;      //0 = triple shot, 1 = speed, 2 = shield

    [SerializeField] AudioClip _clip;

    // Update is called once per frame
    void Update ()
    {
        transform.Translate (Vector3.down * _powerUpSpeed * Time.deltaTime);

        if (transform.position.y < -6f)
            Destroy (this.gameObject);
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.transform.GetComponent <Player> ();

            AudioSource.PlayClipAtPoint (_clip, transform.position);

            if (player != null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive ();
                        break;

                    case 1:
                        player.SpeedActive ();
                        break;

                    case 2:
                        player.ShieldActive ();
                        break;

                    default:
                        print("Nothing to see here");
                        break;
                }
            }

            Destroy (this.gameObject);
        }
    }
}