using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    const int max = 3;
    public GameObject[] varint;
    public GameObject[] bafs;
    public GameObject[] souls;
    public float startBtwSpawn;
    private float timeToSpawn;
    public float startBtwSpawnBafs;
    private float timeToSpawnBafs;
    public float startBtwSouls;
    private float timeToSpawnSouls;
    public float minTime;
    public float dicreaseTime;
    private bool spawned;
    private bool vamp;
    
   
    
    // Start is called before the first frame update
    void Start()
    {
        spawned = false;
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        vamp = GameObject.Find("Player").GetComponent<Player>().isVamp;

        if (timeToSpawn < 0)
        {
            int rand = Random.Range(0, varint.Length);
            Instantiate(varint[rand], transform.position , Quaternion.identity);
            Instantiate(souls[rand], transform.position, Quaternion.identity);
            timeToSpawnSouls = startBtwSouls;
            timeToSpawn = startBtwSpawn;
            if (startBtwSpawn > minTime)
            {
                startBtwSpawn -= dicreaseTime;
            }
            spawned = true;

        }
        else
        {
            spawned = false;
            
            Destroy(GameObject.FindGameObjectWithTag("Variant"));
            timeToSpawn -= Time.deltaTime;
        }

        if (timeToSpawnBafs < 0)
        {
            int rand = Random.Range(0, varint.Length);
            Instantiate(bafs[rand], transform.position, Quaternion.identity);
            timeToSpawnBafs = startBtwSpawnBafs;          


        }
        else
        {
            timeToSpawnBafs -= Time.deltaTime;
        }

        if (timeToSpawnSouls < 0 && !spawned)
        {
            int rand = Random.Range(0, souls.Length);
            timeToSpawnSouls = startBtwSouls;
            
            if (vamp)
            {
                Vector3 pos = new(transform.position.x, transform.position.y + 10f, transform.position.z);
                Instantiate(souls[0], pos, Quaternion.identity);
                Instantiate(souls[1], pos, Quaternion.identity);
                Instantiate(souls[2], pos, Quaternion.identity);
            }
            else
            {
                Instantiate(souls[rand], transform.position, Quaternion.identity);
                
            }

            if (startBtwSouls > minTime - 0.1f)
            {
                startBtwSouls -= dicreaseTime;
            }
        }
        else
        {
            timeToSpawnSouls -= Time.deltaTime;
        }





    }
}
