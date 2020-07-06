using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public GameObject SFXButton;
    public GameObject MusicButton;
    public Sprite SFXOn; public Sprite SFXOff; public Sprite MusicOn; public Sprite MusicOff;
    public GameObject MusicObj;
    // Start is called before the first frame update
    private void Awake()
    {
        MusicObj = GameObject.Find("BackgroundSound");
    }
    void Start()
    {
        if (PlayerPrefs.GetInt("SFXOn") == 1)
        {
            SFXButton.GetComponent<Image>().sprite = SFXOn ;
            
        }
        else
        {
            SFXButton.GetComponent<Image>().sprite = SFXOff;
        }
        if (PlayerPrefs.GetInt("MusicOn") == 1)
        {
            MusicButton.GetComponent<Image>().sprite = MusicOn;
            MusicObj.GetComponent<AudioSource>().mute = false;

        }
        else
        {
            MusicButton.GetComponent<Image>().sprite = MusicOff;
            MusicObj.GetComponent<AudioSource>().mute = true;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void MuiscClicked()
    {
        SFXSound.PlaySound("GUISound");
        if (PlayerPrefs.GetInt("MusicOn") == 1)
        {
            MusicButton.GetComponent<Image>().sprite = MusicOff;
            PlayerPrefs.SetInt("MusicOn", 0);
            MusicObj.GetComponent<AudioSource>().mute = true;
        }
        else
        {
            MusicButton.GetComponent<Image>().sprite = MusicOn;
            PlayerPrefs.SetInt("MusicOn", 1);
            MusicObj.GetComponent<AudioSource>().mute = false;

        }

    }
    public void SFXClicked()
    {
        SFXSound.PlaySound("GUISound");
        if (PlayerPrefs.GetInt("SFXOn") == 1)
        {
            SFXButton.GetComponent<Image>().sprite = SFXOff;
            PlayerPrefs.SetInt("SFXOn", 0);
           // MusicObj.GetComponent<AudioSource>().mute = true;
        }
        else
        {
            SFXButton.GetComponent<Image>().sprite = SFXOn;
            PlayerPrefs.SetInt("SFXOn", 1);
           // MusicObj.GetComponent<AudioSource>().mute = false;

        }

    }
}
