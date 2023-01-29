using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loc : MonoBehaviour
{
    private float speed;
   
    
    void Start()
    {

       
    }

    // Update is called once per frame
    private void Update()
    {
        speed = mainCam.speed;

    }
    void FixedUpdate()
    {
          transform.Translate(Vector2.left * speed);
       
        
    }

    
    
}
