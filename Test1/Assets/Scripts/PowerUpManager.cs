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
    string[] KillTags = { "Kill", "Enemy" };
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

                arr = GameObject.FindGameObjectsWithTag("NoDamage");
                foreach (GameObject item in arr)
                {


                    if (item.name == "TestGround")
                        item.GetComponent<BoxCollider2D>().isTrigger = true;

                    else
                    {
                        if (item.gameObject.name.Contains("Spikes"))
                        {
                            print("Deactivate power spike");
                            item.gameObject.tag = "Kill";
                            
                        }
                        if (item.gameObject.name.Contains("Enemy"))
                        {
                            print("Deactivate power Enemmy");
                            item.gameObject.tag = "Enemy";
                           
                        }
                        item.GetComponent<BoxCollider2D>().isTrigger = true;


                    }

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

        for (int i = 0; i < KillTags.Length; i++)
        {
            arr = GameObject.FindGameObjectsWithTag(KillTags[i]);
            foreach (GameObject item in arr)
            {
                if (item.name == "TestGround")
                {
                    item.GetComponent<BoxCollider2D>().isTrigger = true;
                    continue;
                }
                else
                {
                    item.GetComponent<BoxCollider2D>().isTrigger = false;
                    item.gameObject.tag = "NoDamage";
                }
            }
        }
        textImmune.gameObject.SetActive(true);
        powerUpActive = true;
    }
}
