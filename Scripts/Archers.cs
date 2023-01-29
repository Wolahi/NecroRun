using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archers : MonoBehaviour
{
    private float speedForArch;
    public int damage;
    public float lifetime;
    private Animator[] archers;
    private GameObject player;




    void Start()
    {
        archers = gameObject.GetComponentsInChildren<Animator>();
        player = GameObject.Find("Player");
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
        speedForArch = mainCam.speed;
        transform.Translate(speedForArch * Vector2.left);
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
            for (int i = 0; i < archers.Length; i++)
            {
                archers[i].GetComponent<Animator>().SetBool("Destroy", true);
            }
            GetComponent<BoxCollider2D>().isTrigger = false;
            Destroy(gameObject, 0.8f);
        }
    }
}
