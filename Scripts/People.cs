using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People : MonoBehaviour
{
    private float speed;
    public int damage;
    public float lifetime;
    private int souls;
    private int count;
    private Animator[] peasents;
    private GameObject[] skelets;

    void Start()
    {
        peasents = gameObject.GetComponentsInChildren<Animator>();
        skelets = GameObject.FindGameObjectsWithTag("Skelet");

        souls = Random.Range(5, 15);
        count = Random.Range(0, 3);
        if(count > 0)
        {
            for(int i=1;i <= count; i++)
            {
                Destroy(peasents[i].gameObject);
            }
        }
        if (mainCam.speed == 0.2)
        {
            lifetime = 10;
        }
        else if (mainCam.speed == 0.4)
        {
            lifetime = 5;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        speed = mainCam.speed + 0.1f;
        transform.Translate(speed * Vector2.left);
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
            if (skelets.Length < 1)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().TakeDamage(1);
            }
            else
            {

                for (int i = 0; i < peasents.Length; i++)
                {
                    if (peasents[i] != null)
                    {
                        peasents[i].GetComponent<Animator>().SetBool("dead", true);
                    }

                }
                speed = mainCam.speed;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Attack(peasents);
                Destroy(gameObject,2f);
                PlayerPrefs.SetInt("Souls", souls + PlayerPrefs.GetInt("Souls"));
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCol"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AttackFalse();
        }
    }


}
