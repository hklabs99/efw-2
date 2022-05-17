using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float _laserSpeed = 8f;
    bool _isEnemyLaser = false;

    // Update is called once per frame
    void Update ()
    {
        if (_isEnemyLaser == false)
            MoveUp ();

        else
            MoveDown ();
    }

    void MoveUp ()
    {
        //translate laser up
        Vector3 laser = new Vector3(0, 1, 0);
        transform.Translate(laser * _laserSpeed * Time.deltaTime);

        if (transform.position.y > 8f)
        {
            if (transform.parent != null)
                Destroy(transform.parent.gameObject);

            Destroy(this.gameObject);  //Destroys the gameobject this script is attached to.
        }
    }

    void MoveDown ()
    {
        //translate laser up
        Vector3 laser = new Vector3(0, -1, 0);
        transform.Translate(laser * _laserSpeed * Time.deltaTime);

        if (transform.position.y < -8f)
        {
            if (transform.parent != null)
                Destroy(transform.parent.gameObject);

            Destroy(this.gameObject);  //Destroys the gameobject this script is attached to.
        }
    }

    public void AssignEnemyLaser ()
    {
        _isEnemyLaser = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _isEnemyLaser == true)
        {
            Player player = other.GetComponent <Player> ();

            if (player != null)
                player.Damage ();
        }
    }
}