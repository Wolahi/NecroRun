using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private float speedForGhost;
    public float lifetime;

    void Start()
    {


    }


    void FixedUpdate()
    {
        speedForGhost = mainCam.speed;
        transform.Translate(speedForGhost * Vector2.left);
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
            other.GetComponentInParent<Player>().OnGhost();
            Destroy(gameObject);
        }
    }
}
