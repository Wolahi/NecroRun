using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    
    public float distance;
    public float speed;
    private Vector2 targetPos;
    private Vector2 startPos;
    private Vector3 touchPos;
    private Vector3 targetCam;
    private Vector3 startCamPos;
    private float camStartSize;
    private GameObject coll;
    public GameObject deathTab;
    public GameObject Swipe;
    private float direct;
    public float minHeight;
    public float maxHeight;
    public Text score;
    public Text HighScore;
    public Text Souls;
    public static int scoreValue;
    public int health = 5;
    public float startTimeToScore;
    private float timeToScore;
    private GameObject cam;
    private Animator necro;
    public float startTimeToCreat;
    private float startTimeToDeath = 0.8f;
    private float timeToCreat;
    private float timeToDeath;
    public bool isVamp;
    private  bool isGhost;
    private bool isDead;
    public static bool playerIsDead;
    private int scorePlus;
    float min, max;
    private GameObject[] skelets;
    public int num;
    public int count;
    public int damages = 0;

    private void OnSwipe(Vector2 direction)
    {
        var dir = direction;
        direct = dir.y;
    
        targetPos = new Vector3(transform.position.x + (direct * distance/2), transform.position.y + (direct * distance), transform.position.z);
        ChangeZ(direct);
    }

    void Start()
    {

        cam = GameObject.FindGameObjectWithTag("MainCamera");
        necro = GameObject.FindGameObjectWithTag("Necro").GetComponent<Animator>();
        Swipe.GetComponent<SwipeDetection>().SwipeEvent += OnSwipe;
        camStartSize = cam.GetComponent<Camera>().orthographicSize;
        coll = GameObject.FindGameObjectWithTag("PlayerCol");
        startCamPos = cam.transform.position;
        scoreValue = 0;    
        min = minHeight;
        max = maxHeight;
        timeToCreat = startTimeToCreat;
        isVamp = false;
        isGhost = false;
        scorePlus = 1;
        timeToDeath = startTimeToDeath;
        isDead = false;
        playerIsDead = false;        
    }

    

    private void FixedUpdate()
    {
        skelets = GameObject.FindGameObjectsWithTag("Skelet");

        health = skelets.Length;
        if (timeToScore < 0)
        {
            scoreValue  += scorePlus;
            score.text = scoreValue.ToString();
            timeToScore = startTimeToScore;
            if(startTimeToScore > 0.2f)
            {
                startTimeToScore -= mainCam.speed * Time.deltaTime;
            }           
           
        }
        else
        {
            timeToScore -= Time.deltaTime;
        }
        HighScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        Souls.text = PlayerPrefs.GetInt("Souls").ToString();
       
        MoveUp();
        CreateUp();
        CamReset();
        DeadTransform();

        
        
    }

   

    private void MoveUp()
    {
        if (transform.position.y > minHeight + 0.1f && direct < 0)
        {
            transform.position =Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            
            
        }
        else if (transform.position.y < maxHeight - 0.1f && direct > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        }
        
    }

    private void ChangeZ(float dir)
    {
        var trans = GameObject.FindGameObjectWithTag("Skelets").transform.position;
        if (trans.z > -3.1f && dir < 0)
        {
            
            trans = new Vector3(trans.x, trans.y, trans.z + (dir * 1f));
            if(trans.z > -1.1f)
            {
                trans = new Vector3(trans.x, trans.y, -1.1f);
            }
            if(trans.z < -3.1f)
            {
                trans = new Vector3(trans.x, trans.y, -3.1f);
            }
            GameObject.FindGameObjectWithTag("Skelets").transform.position = trans;

        }
        else if (trans.z < -1.1f && dir > 0)
        {
            trans = new Vector3(trans.x, trans.y, trans.z + (dir * 1f));
            GameObject.FindGameObjectWithTag("Skelets").transform.position = trans;

        }
    }

    private void CreateUp()
    {
        if (isVamp && timeToCreat < 0)
        {
            ResetCreat();
        }
        else if (isVamp)
        {
            if (cam.GetComponent<Camera>().orthographicSize < 16)
            {
                cam.GetComponent<Camera>().orthographicSize += 5f * Time.deltaTime;

            }
            for (int i = 0; i < skelets.Length; i++)
            {
                skelets[i].GetComponent<Animator>().SetFloat("Fly", timeToCreat);
            }
            necro.SetFloat("Fly", timeToCreat);
            timeToCreat -= Time.deltaTime;
        }

        if (isGhost && timeToCreat < 0)
        {
            ResetCreat();
        }
        else if (isGhost)
        {
            if (cam.GetComponent<Camera>().orthographicSize > 7)
            {
                cam.GetComponent<Camera>().orthographicSize -= 2f * Time.deltaTime;
                cam.GetComponent<Camera>().transform.position = Vector3.MoveTowards(cam.transform.position, targetCam, 10f * Time.deltaTime);
            }
            for (int i = 0; i < skelets.Length; i++)
            {
                skelets[i].GetComponent<Animator>().SetFloat("GhostFly", timeToCreat);
            }
            necro.SetFloat("GhostFly", timeToCreat);
            timeToCreat -= Time.deltaTime;
        }

    }
    private void CamReset()
    {
        if (cam.GetComponent<Camera>().orthographicSize > camStartSize + 0.001f && !isVamp)
        {
            cam.GetComponent<Camera>().orthographicSize -= 5f * Time.deltaTime;
        }
        else if (cam.GetComponent<Camera>().orthographicSize < camStartSize - 0.001f && !isGhost)
        {
            cam.GetComponent<Camera>().orthographicSize += 2f * Time.deltaTime;
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, startCamPos, 10f * Time.deltaTime);
        }
    }

    private void DeadTransform()
    {
        if (isDead && timeToDeath > 0)
        {
            if (damages - 1 >= 0)
            {
                
                skelets[damages - 1].transform.Translate(Vector2.left * mainCam.speed);
            }
            timeToDeath -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        float sizeColl;

        if(health >= 1)
        {
            sizeColl = coll.GetComponent<BoxCollider2D>().size.x;
            if(sizeColl > 0.5f)
            {
                sizeColl -= 0.5f;
                coll.GetComponent<BoxCollider2D>().size = new Vector2(sizeColl, 1f);
            }    
            isDead = true;
            timeToDeath = startTimeToDeath;
            DeathSkul(skelets[damages]);
            damages++;
        }    
        if (health <= 1)
        {
            necro.SetBool("Dead", true);
            deathTab.SetActive(true);
            Destroy(gameObject, 0.8f);
            playerIsDead = true;
            if (PlayerPrefs.GetInt("HighScore") < scoreValue)
            {
                        PlayerPrefs.SetInt("HighScore", scoreValue);
            }
        }
           
    }

    public void Attack(Animator[] peasents)
    {
        for(int i = 0; i<skelets.Length; i++)
        {
            skelets[i].GetComponent<Animator>().SetBool("Attack", true);
        }
        if(peasents.Length > skelets.Length)
        {
            TakeDamage(1);
        }
        
    }

    public void AttackFalse()
    {
        for (int i = 0; i < skelets.Length; i++)
        {
            skelets[i].GetComponent<Animator>().SetBool("Attack", false);
        }
    }
            

    

    public void Vamp()
    {
        transform.position = new(-7f, 6f, transform.position.z);
        targetPos = new(-7, 6f);
        mainCam.speed += 0.3f;
        minHeight = 6;
        maxHeight = 12;
        isVamp = true;
        startTimeToScore -= 0.3f;
    }
    public void OnVampire()
    {
        for (int i = 0; i < skelets.Length; i++)
        {
            skelets[i].GetComponent<Animator>().SetBool("Vampire", true);
        }
        necro.SetBool("Vampire", true);
        for (int i = 0; i < skelets.Length; i++)
        {
            skelets[i].GetComponent<Animator>().SetBool("Tp", true);
        }
        necro.SetBool("Tp", true);
        coll.SetActive(false);
        StartCoroutine(WaitVamp(1f));                
    }
    
    public void OnGhost()
    {
        for (int i = 0; i < skelets.Length; i++)
        {
            skelets[i].GetComponent<Animator>().SetBool("Ghost", true);
        }
        necro.SetBool("Ghost", true);
        coll.SetActive(false);
        StartCoroutine(WaitGhost(0.5f));
    }
    public void Ghost()
    {
        targetCam = new Vector3(-5f, -1f, cam.transform.position.z);
        mainCam.speed += 0.3f;
        transform.position = new(transform.position.x + 3f, transform.position.y, transform.position.z);
        startTimeToScore -= 0.3f;
        isGhost = true;
    }
    public void ResetCreat()
    {
        for (int i = 0; i < skelets.Length; i++)
        {
            skelets[i].GetComponent<Animator>().SetBool("Vampire", false);
        }
        necro.SetBool("Vampire", false);
        for (int i = 0; i < skelets.Length; i++)
        {
            skelets[i].GetComponent<Animator>().SetBool("Tp", false);
        }
        necro.SetBool("Tp", false);
        if (isGhost)
        {
            for (int i = 0; i < skelets.Length; i++)
            {
                skelets[i].GetComponent<Animator>().SetBool("Ghost", false);
            }
            
            isGhost = false;
            necro.SetBool("Ghost", false);
        }
        if (isVamp)
        {
            Vector3 pos = new(transform.position.x, transform.position.y - 11f, transform.position.z);
            transform.position = pos;
            targetPos = pos;
            isVamp = false;
        }
        coll.SetActive(true);
        mainCam.speed -= 0.3f;
        timeToCreat = startTimeToCreat;
        minHeight = min;
        maxHeight = max;
        startTimeToScore += 0.3f;


    }

    public IEnumerator Wait(float seconds, GameObject skul)
    {

        yield return new WaitForSeconds(seconds); // таймер, через n секунд
        Destroy(skul);
        isDead = false;
        damages = 0; // выполнится эта строка
    }

    public void DeathSkul(GameObject skelet)
    {
        skelet.GetComponent<Animator>().SetBool("Dead", true);
        StartCoroutine(Wait(0.8f, skelet));
    }

    public IEnumerator WaitVamp(float seconds)
    {
        yield return new WaitForSeconds(seconds); // таймер, через n секунд
        Vamp();
    }
    public IEnumerator WaitGhost(float seconds)
    {
        yield return new WaitForSeconds(seconds); // таймер, через n секунд
        Ghost();
    }

    

}   
