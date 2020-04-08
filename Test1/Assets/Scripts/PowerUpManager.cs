using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PowerUpManager : MonoBehaviour
{
    //private bool doublecoin;
    private bool safeMode;
    private bool powerUpActive=false;
    private float powerUpLengthCounter;
    public TextMeshProUGUI textImmune;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(powerUpActive)
        { powerUpLengthCounter -= Time.deltaTime;
            textImmune.text = "Immunity for " + Convert.ToInt32 (powerUpLengthCounter) + " seconds";

        if (powerUpLengthCounter<=0)
            {
                textImmune.gameObject.SetActive(false);
                GameObject[] arr;
                arr = GameObject.FindGameObjectsWithTag("Kill");
                foreach (GameObject item in arr)
                {
                    item.GetComponent<BoxCollider2D>().enabled = true;
                }
                powerUpActive = false;
            }
        }
    }
    public void ActivatePowerUp(bool safe,float time)
    {
        //doubleCoin = doublecoin;
        safeMode = safe;
        powerUpLengthCounter = time;
        GameObject[] arr;
        arr = GameObject.FindGameObjectsWithTag("Kill");
        foreach (GameObject item in arr)
        {
            item.GetComponent<BoxCollider2D>().enabled = false;
        }
        textImmune.gameObject.SetActive(true);
        powerUpActive = true;
    }
}
