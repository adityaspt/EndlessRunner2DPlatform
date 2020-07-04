using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("TutorialShown"))
        {
            PlayerPrefs.SetInt("TutorialShown", 0);
        }
    }
    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
