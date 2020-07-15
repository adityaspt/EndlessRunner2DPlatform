using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
//using DG.Tweening;

public class DailyRewardHandler : MonoBehaviour
{
    public static DailyRewardHandler Instance;
    string currStr;
    //public GameObject[] daytext;
    //public Button[] buttons;
    //public TextMeshProUGUI[] coinText;


    int currDate, prevDate;


   // public bool isSpin = false;
    //public RectTransform SpinWheel;
   // public Button spinB;
    private void Awake()
    {
        Instance = this;

        //daytext = GameObject.FindGameObjectsWithTag("day");
        //coinText = new TextMeshProUGUI[buttons.Length];
        //for (int i = 0; i < daytext.Length; i++)
        //{
        //    daytext[i].GetComponent<TextMeshProUGUI>().text = "Day " + (i + 1);
        //}
        //for (int i = 0; i < daytext.Length; i++)
        //{
        //    coinText[i] = buttons[i].transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        //}

        if (PlayerPrefs.HasKey("GameDateCounter"))
        {
            currDate = PlayerPrefs.GetInt("GameDateCounter");
            print("gAMEDATE " + currDate);
        }
        else
        {
            currDate = PlayerPrefs.GetInt("GameDateCounter", 1);
        }
        if (PlayerPrefs.HasKey("lastDate"))
        {
            print("Last Date " + PlayerPrefs.GetString("lastDate"));
        }
        else
        {
            PlayerPrefs.SetString("lastDate", System.DateTime.Now.ToString());
            string str = PlayerPrefs.GetString("lastDate");
            PlayerPrefs.Save();
            print("Last str DATE " + str);
        }
     //   if (currDate > 1)
     //   {
            print("currentDate"+currDate);
            CheckLastCurrEntDate();
            CheckDateDiff();
     //   }
     //   else
       // {
         
            
            //  CheckDateCounter();


      //  }


        // CheckTick();



    }
    // Start is called before the first frame update
    //void CheckTick()
    //{
    //    if (currDate != 1)
    //    {
    //        for (int i = 0; i < daytext.Length; i++)
    //        {
    //            if (i < currDate - 1)
    //            {

    //                buttons[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
    //                buttons[i].transform.GetChild(0).GetComponent<Image>().color = Color.green;
    //                buttons[i].transform.GetChild(2).GetComponent<Image>().color = Color.green;
    //                buttons[i].transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.black;
    //                //165519
    //            }
    //            else
    //            {
    //                buttons[i].transform.GetChild(0).GetComponent<Image>().enabled = false;

    //            }
    //        }
    //    }
    //    else
    //    {
    //        for (int i = 0; i < daytext.Length; i++)
    //        {
    //            buttons[i].transform.GetChild(0).GetComponent<Image>().enabled = false;

    //        }
    //    }
    //}

    //void CheckDateCounter()
    //{
    //    for (int i = 0; i < daytext.Length; i++)
    //    {
    //        if (i <= currDate - 1)
    //        {
    //            buttons[i].interactable = true;
    //        }
    //        else
    //            buttons[i].interactable = false;
    //    }
    //    CheckTick();
    //}

    void CheckLastCurrEntDate()
    {
        if(currDate>30)
        {
            currDate = 1;
            PlayerPrefs.SetString("lastDate", System.DateTime.Now.ToString());
            PlayerPrefs.SetInt("GameDateCounter", currDate);
          //  CheckDateCounter();
        }
    }
    //void disableallButtons()
    //{
    //    for (int i = 0; i < daytext.Length; i++)
    //    {
    //        if (i < currDate - 1)
    //            buttons[i].interactable = true;
    //        else
    //            buttons[i].interactable = false;
    //    }
    //    CheckTick();
    //}
    public void CheckDateDiff()
    {
       
        TimeSpan TS = checkTS();

        Debug.Log("LastDay: " + TS.Days);
        if (TS.Days == 1)
        {
            if (PlayerPrefs.GetInt("Lifes") < 3)
            {
                PlayerPrefs.SetInt("Lifes", PlayerPrefs.GetInt("Lifes") + 1);
            }
            PlayerPrefs.SetString("lastDate", System.DateTime.Now.ToString());
            PlayerPrefs.SetInt("GameDateCounter", currDate);
            //   CheckDateCounter();
            //  CheckTick();
        }
        else if(TS.Days ==0)
        {
            print(" disableallButtons();");
        //    disableallButtons();
            
        }
        else if(TS.Days<0)
        {
        //    CheckDateCounter();
        }
        else if (TS.Days > 1)
        {
            currDate = 1;
            PlayerPrefs.SetString("lastDate", System.DateTime.Now.ToString());
            PlayerPrefs.SetInt("GameDateCounter", currDate);
         //   CheckDateCounter();
            // CheckTick();
        }
       
        
    }
    //void CheckDate()
    //{
    //    if (currDate == 1)
    //    {
    //     //   GetReward();
    //        currDate++;
    //        PlayerPrefs.SetString("lastDate", System.DateTime.Now.ToString());
    //        PlayerPrefs.SetInt("GameDateCounter", currDate);
    //     //   disableallButtons();
    //    }
       
    //    TimeSpan TS = checkTS();

    //    Debug.Log("LastDay: " + TS.Days);
    //    if (TS.Days == 1)
    //    {
    //     //   GetReward();
    //        currDate++;
    //        PlayerPrefs.SetString("lastDate", System.DateTime.Now.ToString());
    //        PlayerPrefs.SetInt("GameDateCounter", currDate);
    //      //  disableallButtons();
    //    }
    //    else if (TS.Days < 0)
    //    {
    //      //  CheckDateCounter();
    //    }
    //    else if (TS.Days == 0)
    //    {
    //      //  disableallButtons();
            
    //    }


    //}


    TimeSpan checkTS()
    {
        string Get_currDate = System.DateTime.Now.ToString();
        //string oldDate=
        string stringDate = PlayerPrefs.GetString("lastDate");
        DateTime oldDate = Convert.ToDateTime(stringDate);
        DateTime newDate = Convert.ToDateTime(Get_currDate);
        TimeSpan TS = newDate.Subtract(oldDate);

        return TS;
    }


    //private void GetReward()
    //{
    //    string str = coinText[currDate - 1].GetComponent<TextMeshProUGUI>().text;
    //    currStr = str;
    //    print("Last Curr" + str);




    //    if (str.Contains("coins"))
    //    {
    //        GiveCoinReward();
    //    }
    //    else if (str.Contains("hint"))
    //    {
    //        GiveHintReward();
    //    }
    //    else if(str.Contains("spin"))
    //    {
    //        OpenSpinWheel(SpinWheel);
    //    }
    //}

    //public void OpenSpinWheel(RectTransform panelname)
    //{

    //    print("LevelCompletString " + MainGamePlayController.MainInstance.GetCurrLevelComp(PlayerPrefs.GetString("LevelCompletString")));

    //    if (!isSpin)
    //    {
    //        panelname.DOMoveY(0, 1.5f);
    //        spinB.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "SPIN !";
    //    }
    //    isSpin = true;

    //}
    //public void CloseSpinWheel(RectTransform panelname)
    //{
    //    //Aditya
    //    //if (GameObject.Find("NextLoadPopUp") != null)
    //    //    GameObject.Find("NextLoadPopUp").gameObject.SetActive(false);
    //    //Aditya
    //    print("Close the spin wheel pressed");
    //    if (isSpin)
    //        panelname.DOMoveY(Screen.height / 2, 1.5f);
    //    //WordflowXmlLoader.parseXmlFile();
    //    //TransitionManager.instance.LoadNextScene(2);

    //}
    //private void GiveHintReward()
    //{
    //    string hint = "";
    //    print("Last Curr");


         
    //    for (int i = 0; i < currStr.Length; i++)
    //    {
    //        if (Char.IsDigit(currStr[i]))
    //            hint += currStr[i];
    //    }



    //    print("Last hint " + hint);
    //    int rewardhint = Convert.ToInt32(hint);
    //    int currhint = PlayerPrefs.GetInt("HintCount");
    //    currhint += rewardhint;
    //    PlayerPrefs.SetInt("HintCount", currhint);
    //    PlayerPrefs.Save();
    //    print("Last All hint " + currhint);



    //}

    //private void GiveCoinReward()
    //{
    //    string coin = "";
    //    print("Last Curr");



    //    for (int i = 0; i < currStr.Length; i++)
    //    {
    //        if (Char.IsDigit(currStr[i]))
    //            coin += currStr[i];
    //    }



    //    print("Last Coin " + coin);
    //    int rewardCoin = Convert.ToInt32(coin);
    //    int currCoin = PlayerPrefs.GetInt("CoinCounter");
    //    currCoin += rewardCoin;
    //    PlayerPrefs.SetInt("CoinCounter", currCoin);
    //    PlayerPrefs.Save();
    //    print("Last All Coin " + currCoin);

    //}

    //public void OnButtonClick(GameObject tick)
    //{
    //    if (tick.GetComponent<Image>().enabled == true)
    //        return;

    //    CheckDateCounter();
    //    CheckDate();

    //}

}
