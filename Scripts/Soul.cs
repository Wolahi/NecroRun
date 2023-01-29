using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    private float speed;
    public int soulCost;
    public float lifetime;
    void Start()
    {
        if (mainCam.speed >= 0.2)
        {
            lifetime = 10;
        }
        else if (mainCam.speed >= 0.4)
        {
            lifetime = 5;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        speed = mainCam.speed;
        transform.Translate(Vector2.left * speed);
        if (lifetime < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            lifetime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerColSouls"))
        {
            PlayerPrefs.SetInt("Souls", soulCost + PlayerPrefs.GetInt("Souls"));
            Destroy(gameObject);
        }
    }
}
