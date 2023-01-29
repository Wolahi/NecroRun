using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{

    private float parallaxEffect;
    
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        

        if (mainCam.speed > 0)
        {
            if (CompareTag("Back"))
            {
                parallaxEffect = mainCam.speed - 0.08f;
            }
            if (CompareTag("Middle"))
            {
                parallaxEffect = mainCam.speed - 0.05f;
            }
            if (CompareTag("Top"))
            {
                parallaxEffect = mainCam.speed - 0.02f;
            }
        }
        else
        {
            parallaxEffect = 0;
        }

     
        transform.Translate(Vector2.left * parallaxEffect);
    }
}
