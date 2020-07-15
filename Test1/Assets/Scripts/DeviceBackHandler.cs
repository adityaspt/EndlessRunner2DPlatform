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
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WindowsEditor)
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {

                if (!BackBtnDevice)
                {
                    BackBtnDevice = true;

                    if (SceneManager.GetActiveScene().name == "Main")
                    {
                        SFXSound.PlaySound("GUISound");


                        if (!GameManager.gameManagerInstance.PauseMenu.activeSelf)
                        {
                            if (GameManager.gameManagerInstance.PauseB.activeSelf)
                                GameManager.gameManagerInstance.PauseB.SetActive(false);
                            Time.timeScale = 0f;
                            GameManager.gameManagerInstance.PauseMenu.gameObject.SetActive(true);
                            // PauseMenu.instance.PauseGame();
                        }
                        else
                        {
                            GameManager.gameManagerInstance.PauseB.SetActive(true);
                            Time.timeScale = 1f;
                            GameManager.gameManagerInstance.PauseMenu.gameObject.SetActive(false);
                            //SceneManager.LoadScene(0);
                        }

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
                BackBtnDevice = false;
            }
            return;
        }
    }
}
