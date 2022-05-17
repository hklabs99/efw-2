using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowflakes : MonoBehaviour
{
    [SerializeField] float _snowFlakeSpeed = 4.0f;

    // Update is called once per frame
    void Update ()
    {
        transform.Translate (Vector3.down * _snowFlakeSpeed * Time.deltaTime);

        if (transform.position.y < -4.5f)
            Destroy (this.gameObject, 1.0f);
    }
}