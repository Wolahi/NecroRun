using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire : MonoBehaviour
{
    private float speedForVamp;
    public float lifetime;

    void Start()
    {
        if (mainCam.speed == 0.2)
        {
            lifetime = 10;
        }
        else if (mainCam.speed == 0.4)
        {
            lifetime = 5;
        }

    }


    void FixedUpdate()
    {
        speedForVamp = mainCam.speed;
        transform.Translate(speedForVamp * Vector2.left);
        if (lifetime < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            lifetime -= Time.deltaTime;
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCol"))
        {
            other.GetComponentInParent<Player>().OnVampire();
            Destroy(gameObject);
        }
    }
}
