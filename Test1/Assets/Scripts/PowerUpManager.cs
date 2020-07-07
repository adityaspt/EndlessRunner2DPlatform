using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PowerUpManager : MonoBehaviour
{
    public GameObject PlayerSprite;
    public static PowerUpManager instance;
    //private bool doublecoin;
    bool PlayerTransparent = false;
    private bool safeMode;
    private bool powerUpActive = false;
    public float powerUpLengthCounter;
    public GameObject Shield;
    public TextMeshProUGUI textImmune;
    GameObject[] arr;
    string[] KillTags = { "Kill", "Enemy","Ground" };
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
            textImmune.text =  Convert.ToInt32(powerUpLengthCounter).ToString() ;
            StartCoroutine(FadeCanvasGroup());
            if (powerUpLengthCounter <= 0)
            {
                Shield.gameObject.SetActive(false);
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
        else
        {
            if (!PlayerTransparent)
            {
                Color PlayerColor = new Color(255, 255, 255, 255);
                PlayerSprite.GetComponent<SpriteRenderer>().color = PlayerColor;
            }
        }
    }
    public IEnumerator FadeCanvasGroup()
    {

        Color PlayerColor;
        if (PlayerTransparent)
        {
            PlayerColor = new Color(255, 255, 255, 0);
            PlayerTransparent = false;
        }
        else
        {

            PlayerColor = new Color(255, 255, 255, 255);
            PlayerTransparent = true;
        }
        PlayerSprite.GetComponent<SpriteRenderer>().color = PlayerColor;

        yield return new WaitForSeconds(0.75f);
        //PlayerTransparent = !PlayerTransparent;
       
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
                    item.GetComponent<BoxCollider2D>().isTrigger = true;
                    item.gameObject.tag = "NoDamage";
                }
            }
        }
        Shield.gameObject.SetActive(true);
        textImmune.gameObject.SetActive(true);
        powerUpActive = true;
    }
}
