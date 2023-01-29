using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balista : MonoBehaviour
{
    
    public float lifetime;   
    public GameObject arrow;
    private Animator anim;



    void Start()
    {
       anim = gameObject.GetComponent<Animator>();
       if(mainCam.speed == 0.2)
        {
            lifetime = 10;
        }
       else if(mainCam.speed == 0.4)
        {
            lifetime = 5;
        }
    }


    void FixedUpdate()
    {
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
            anim.SetBool("Visible",true);
        }
    }
    public void Fire()
    {
        Instantiate(arrow, transform.position, Quaternion.identity);
        anim.SetBool("Visible", false);
    }

    
}
