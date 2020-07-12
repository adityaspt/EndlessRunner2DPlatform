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
        if (!PlayerPrefs.HasKey("SFXOn"))
        {
            PlayerPrefs.SetInt("SFXOn", 1);
        }
        if (!PlayerPrefs.HasKey("MusicOn"))
        {
            PlayerPrefs.SetInt("MusicOn", 1);
        }
        if (!PlayerPrefs.HasKey("Lifes"))
        {
            PlayerPrefs.SetInt("Lifes", 3);
        }
    }
    // Start is called before the first frame update
    public void PlayGame()
    {
        SFXSound.PlaySound("GUISound");
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        SFXSound.PlaySound("GUISound");
        Application.Quit();
    }


}
