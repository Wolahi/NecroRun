using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    
    private float speedForWall;
    public int damage;
    public float lifetime;
    private Animator anim;
    
   
    
  
    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        if (mainCam.speed >= 0.2)
        {
            lifetime = 10;
        }
        else if (mainCam.speed >= 0.4)
        {
            lifetime = 5;
        }
    }

   
    void FixedUpdate()
    {
        speedForWall = mainCam.speed;
        transform.Translate(speedForWall * Vector2.left);
        if(lifetime < 0)
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
            if(anim != null)
            {
                anim.SetBool("Destroy", true);
            }
            GetComponent<BoxCollider2D>().isTrigger = false;
            Destroy(gameObject,0.8f);
        }
    }

   
}
