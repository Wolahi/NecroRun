using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    
    public GameObject pauseGameMenu;
    
    public void Resume()
    {
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;
       
    }

    public void Pause()
    {
        pauseGameMenu.SetActive(true);
        Time.timeScale = 0f;
     
    }

    public void LetsPlay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
