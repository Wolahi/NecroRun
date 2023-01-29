using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainCam : MonoBehaviour
{
    public static float speed;
    public float maxSpeed;
    public float startTimeToChSpeed;
    private float timeBtwCh;
 
    void Start()
    {
        speed = 0.3f;
    }

    private void FixedUpdate()
    {
       
    }
    void Update()
    {
        if (Player.playerIsDead) 
        {
            speed = 0;
        }
        
    }

    public void ChangeSpeed(float dicreaseSpeed)
    {
        if (speed < maxSpeed)
        {
            speed += dicreaseSpeed;
        }
        
    }

    public void RestartSpeed()
    {
        speed = 0.15f;

    }
    public void BackToMain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        RestartSpeed();
        
    }
}
