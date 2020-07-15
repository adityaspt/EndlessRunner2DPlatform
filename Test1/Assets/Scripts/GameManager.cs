using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;
    public Transform platformGenerator;
    private Vector3 platformStartPoint;
    public MoveSquare thePlayer;
    private Vector3 playerStartPoint;
    public EmptyObjMove emptyObj;
    private Vector3 emptyObjstartPoint;
    public PlatformDestroy[] platfromList;
    private ScoreManager scoremanager;
    public DeathMenu deathscreen;
    public GameObject PauseB;
    public GameObject TutorialCanvas;
    public Button RespawnButton;
    public GameObject Lives;
    public GameObject WatchAdScreen;
    public GameObject PauseMenu;

    public void StartRewardAD()
    {
        UnityAdsManager.UnityAds.ShowRewardAd();
    }

    public void ClickRespawnButton()
    {
        if (PlayerPrefs.GetInt("Lifes") > 0)
        {
            PlayerPrefs.SetInt("Lifes", PlayerPrefs.GetInt("Lifes") - 1);
            UpdateForLives();
            deathscreen.gameObject.SetActive(false);
            //PowerUpManager.instance.powerUpLengthCounter = 0f;

            // PowerUpManager.instance.textImmune.gameObject.SetActive(true);
            MoveSquare.mainInstance.RespawnPlayer();
        }
        else
        {
            deathscreen.gameObject.SetActive(false);
            WatchAdScreen.SetActive(true);
        }

    }

    private void Awake()
    {
        if (PlayerPrefs.GetInt("TutorialShown") == 0)
        {
            emptyObj.GetComponent<EmptyObjMove>().enabled = false;
            PauseB.SetActive(false);
            TutorialCanvas.SetActive(true);

        }

        UpdateForLives();
    }
    public void UpdateForLives()
    {
        int numOfLifes = PlayerPrefs.GetInt("Lifes");
        if (numOfLifes > 0)
        {
            for (int i = 0; i < 3; i++)
            {
                if (i + 1 <= numOfLifes)
                    Lives.transform.GetChild(i).gameObject.SetActive(true);
                else
                    Lives.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else if (numOfLifes <= 0)
        {
            for (int i = 0; i < 3; i++)
            {
               
                   Lives.transform.GetChild(i).gameObject.SetActive(false);
                 
            }

                Lives.transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    public void SetPlayerPrefTutorialShown()
    {
        PlayerPrefs.SetInt("TutorialShown", 1);
    }
    // public PauseMenu pauseScreen;
    // Start is called before the first frame update
    void Start()
    {


        gameManagerInstance = this;
        platformStartPoint = platformGenerator.position;
        playerStartPoint = thePlayer.transform.position;
        emptyObjstartPoint = emptyObj.transform.position;
        scoremanager = FindObjectOfType<ScoreManager>();

    }
    public void RestartGame()
    {
        if (PauseB.activeSelf)
        {
            PauseB.SetActive(false);
        }
        thePlayer.gameObject.SetActive(false);
        UnityAdsManager.UnityAds.ShowInter();
        
        //GameManager.gameManagerInstance.UpdateForLives();
        deathscreen.gameObject.SetActive(true);
        //if (PlayerPrefs.GetInt("Lifes") > 0)
        //{
        //    deathscreen.transform.GetChild(4).gameObject.SetActive(false);
        //}
        PowerUpManager.instance.powerUpLengthCounter = 0f;
        if (PowerUpManager.instance.textImmune.gameObject.activeSelf)
            PowerUpManager.instance.textImmune.gameObject.SetActive(false);

        // StartCoroutine("restartGameCall");
    }
    public void PlayNormalSFX()
    {
        SFXSound.PlaySound("GUISound");
    }
    public void ResetGamePlay()
    {
        if (!PauseB.activeSelf)
        {
            PauseB.SetActive(true);
        }

        emptyObj.transform.position = emptyObjstartPoint;

        PowerUpManager.instance.powerUpLengthCounter = 0f;
        if (PowerUpManager.instance.textImmune.gameObject.activeSelf)
            PowerUpManager.instance.textImmune.gameObject.SetActive(false);


        deathscreen.gameObject.SetActive(false);
        GameObject[] gobjs = GameObject.FindGameObjectsWithTag("Platform");

        foreach (GameObject g in gobjs)
        {
            // Destroy(g);
            g.SetActive(false);
        }
        platformGenerator.position = platformStartPoint;
        platfromList = FindObjectsOfType<PlatformDestroy>();
        for (int i = 0; i < platfromList.Length; i++)
        {
            platfromList[i].gameObject.SetActive(false);
        }
        scoremanager.scoreCount = 0;

        thePlayer.transform.position = playerStartPoint;

        thePlayer.gameObject.SetActive(true);
        EmptyObjMove.emptyObjInstance.speed = EmptyObjMove.emptyObjInstance.speedStore;
        EmptyObjMove.emptyObjInstance.speedMilestoneTarget = EmptyObjMove.emptyObjInstance.speedMilestoneTargetStore;
        EmptyObjMove.emptyObjInstance.speedMilestoneCount = EmptyObjMove.emptyObjInstance.speedMilestoneTargetStore;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    //public IEnumerator restartGameCall()
    //{

    //    thePlayer.gameObject.SetActive(false);
    //    yield return new WaitForSeconds(1f);
    //    platfromList = FindObjectsOfType<PlatformDestroy>();
    //    for (int i=0; i < platfromList.Length;i++)
    //    {
    //        platfromList[i].gameObject.SetActive(false);
    //    }
    //    scoremanager.scoreCount = 0;

    //    thePlayer.transform.position = playerStartPoint;
    //    platformGenerator.position = platformStartPoint;
    //    emptyObj.transform.position = emptyObjstartPoint;
    //    thePlayer.gameObject.SetActive(true);
    //    EmptyObjMove.emptyObjInstance.speed = EmptyObjMove.emptyObjInstance.speedStore;
    //    EmptyObjMove.emptyObjInstance.speedMilestoneTarget = EmptyObjMove.emptyObjInstance.speedMilestoneTargetStore;
    //    EmptyObjMove.emptyObjInstance.speedMilestoneCount = EmptyObjMove.emptyObjInstance.speedMilestoneTargetStore;
    //}
    // Update is called once per frame
    void Update()
    {

    }
}
