using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBack : MonoBehaviour
{
    public GameObject[] spawnBackPrefabs;
    public GameObject[] spawnBackPrefabsChange;
    public int scoreChange;
    private float countValue;
    public static int count;
    private int backsCount;
    void Start()
    {
        backsCount = 2;
        countValue = 1;
        count = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       if(scoreChange * countValue == Player.scoreValue)
        {
            count++;
            if (count >= backsCount)
            {
                count = 0;
            }
            countValue++;
        }
        Debug.Log(count);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (count) {
            case 0:
                if (other.CompareTag("Back"))
                {
                    Instantiate(spawnBackPrefabs[0], transform.position, Quaternion.identity);
                }
                if (other.CompareTag("Middle"))
                {
                    Instantiate(spawnBackPrefabs[1], transform.position, Quaternion.identity);
                }
                if (other.CompareTag("Top"))
                {
                    Instantiate(spawnBackPrefabs[2], transform.position, Quaternion.identity);
                }
                break;
            case 1:
                if (other.CompareTag("Back"))
                {
                    Instantiate(spawnBackPrefabsChange[0], transform.position, Quaternion.identity);
                }
                if (other.CompareTag("Middle"))
                {
                    Instantiate(spawnBackPrefabsChange[1], transform.position, Quaternion.identity);
                }
                if (other.CompareTag("Top"))
                {
                    var pos = new Vector2(transform.position.x, transform.position.y - 5.5f);
                    Instantiate(spawnBackPrefabsChange[2], pos, Quaternion.identity);
                }
                break;
        }
        
    }
}
