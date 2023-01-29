using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float speedForArrow;
    public int damage;
    public float lifetime;
    
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        speedForArrow = mainCam.speed + 0.2f;
        transform.Translate(speedForArrow * Vector2.left);
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
            other.GetComponentInParent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}
