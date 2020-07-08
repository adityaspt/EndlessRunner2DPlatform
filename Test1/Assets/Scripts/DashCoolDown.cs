using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DashCoolDown : MonoBehaviour
{
    public Image ChildImageCoolDown;
    public float CoolDown = 5;
   // public float CoolDownTimer = 5;
  //  public TextMeshProUGUI countDownText;
    public static bool CoolDownStarted = false;

    // Update is called once per frame
    // float timer;
    void Update()
    {
        if (!CoolDownStarted)
        {
            if (Input.GetKeyDown(MoveSquare.mainInstance.ButtonZ) || MoveSquare.mainInstance.DashDevice)
            {
                ChildImageCoolDown.fillAmount = 0;
                CoolDownStarted = true;
               // CoolDownTimer = 5;
               // countDownText.enabled = true;
            }
        }
        if (CoolDownStarted)
        {

            // countDownText.enabled = true;
           // CoolDownTimer -= Time.deltaTime;
          //  int seconds = (int)CoolDownTimer % 60;
            //if (seconds < 0)
            //    countDownText.enabled = false;
            //else
            //    countDownText.text = seconds.ToString();

            ChildImageCoolDown.fillAmount += 1 / CoolDown * Time.deltaTime;
            if (ChildImageCoolDown.fillAmount == 1)
            {
                CoolDownStarted = false;
               // ChildImageCoolDown.fillAmount = 1;
            }
        }
    }
    //IEnumerator descreaseTimer()
    //{
    //    if (CoolDownTimer >0)
    //    {
    //        CoolDownTimer -= 1;
    //        yield return new WaitForSeconds(1f);

    //        StartCoroutine("descreaseTimer");
    //    }
    //    else
    //    {
    //        countDownText.enabled = false;
    //        yield break;
    //    }
    //}
}
