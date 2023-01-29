using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public GameObject[] objectsFirst;
    public GameObject[] objectsSecond;
    
    void Start()
    {
        if (SpawnBack.count == 0)
        {
            int rand = Random.Range(0, objectsFirst.Length);
            Instantiate(objectsFirst[rand], transform.position, Quaternion.identity);
        }

        if (SpawnBack.count == 1) 
        {
            int randSc = Random.Range(0, objectsSecond.Length);
            Instantiate(objectsSecond[randSc], transform.position, Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
