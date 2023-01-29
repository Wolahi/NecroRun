using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLoc : MonoBehaviour
{
    public GameObject[] spawnLocPrefabs;
    private int i = 0;
    

   
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Loc"))
        {
            Instantiate(spawnLocPrefabs[i], transform.position, Quaternion.identity);
            i++;
            if (i == spawnLocPrefabs.Length)
            {
               i = 0;
            }
        }
        
        

    }
}
