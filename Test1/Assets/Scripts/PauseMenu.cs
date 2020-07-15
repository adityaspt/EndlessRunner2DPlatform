using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
   // public static PauseMenu instance;
    public void Awake()
    {
      //  instance = this;
    }
    public  PauseMenu pauseScreen;
    // Start is called before the first frame update
    public void restartGame()
    {
        pauseScreen.gameObject.SetActive(false);
        Time.timeScale = 1f;
        GameManager.gameManagerInstance.ResetGamePlay();
    }
    public void QuitToMain()
    {
        pauseScreen.gameObject.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void ResumeGame()
    {
        pauseScreen.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public  void PauseGame()
    {

        pauseScreen.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

}
