using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
   
    // Start is called before the first frame update
    
    public void restartGame()
    {
        GameManager.gameManagerInstance.RestartGame();
    }
    public void QuitToMain()
    {
        SceneManager.LoadScene(0);
    }
    void Start()
    {
        //if (GameObject.Find("PauseB").activeSelf)
        //{
        //    GameObject.Find("PauseB").SetActive(false);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
