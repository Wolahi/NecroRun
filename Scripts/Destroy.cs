using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private int count;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        if (collision.CompareTag("Loc"))
        {
            count++;
            if (count == 2)
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<mainCam>().ChangeSpeed(0.01f);
                count = 0;
            }
        }
        
    }
}
