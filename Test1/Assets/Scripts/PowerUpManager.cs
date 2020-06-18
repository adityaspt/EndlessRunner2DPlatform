using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager instance;
    //private bool doublecoin;
    private bool safeMode;
    private bool powerUpActive = false;
    public float powerUpLengthCounter;
    public TextMeshProUGUI textImmune;
    GameObject[] arr;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        powerUpLengthCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (powerUpActive)
        {
            powerUpLengthCounter -= Time.deltaTime;
            textImmune.text = "Immunity for " + Convert.ToInt32(powerUpLengthCounter) + " seconds";

            if (powerUpLengthCounter <= 0)
            {
                textImmune.gameObject.SetActive(false);
                //GameObject[] arr;
                //arr = GameObject.FindGameObjectsWithTag("Kill");
                foreach (GameObject item in arr)
                {
                    item.gameObject.tag = "Kill";
                    if (item.name == "TestGround")
                        item.GetComponent<BoxCollider2D>().isTrigger = true;

                    else
                        item.GetComponent<BoxCollider2D>().isTrigger = true;

                }
                powerUpActive = false;
            }
        }
    }
    public void ActivatePowerUp(bool safe, float time)
    {
        //doubleCoin = doublecoin;
        safeMode = safe;
        powerUpLengthCounter = time;
        
        arr = GameObject.FindGameObjectsWithTag("Kill");
        foreach (GameObject item in arr)
        {
            if (item.name == "TestGround")
            {
                item.GetComponent<BoxCollider2D>().isTrigger = true;
                continue;
            }
            else
                item.GetComponent<BoxCollider2D>().isTrigger = false;
            item.gameObject.tag = "Untagged";

        }
        textImmune.gameObject.SetActive(true);
        powerUpActive = true;
    }
}
