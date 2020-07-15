using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeviceBackHandler : MonoBehaviour
{

    public static bool BackBtnDevice = false;

    // public static DeviceBackHandler DeviceBackHandlerInstance;


  
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {

                if (!BackBtnDevice)
                {
                    BackBtnDevice=true;
                   
                    if (SceneManager.GetActiveScene().name == "Main")
                    {
                        SFXSound.PlaySound("GUISound");
                        SceneManager.LoadScene(0);
                        // MainGamePlayController.MainInstance.BackToLevel();
                    }
                    if (SceneManager.GetActiveScene().name == "Menu")
                    {
                        SFXSound.PlaySound("GUISound");
                        Application.Quit();
                    }
                }
              
            }
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                BackBtnDevice=false;
            }
             return;
        }
    }
}
