﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Awake()
    {
        if (PlayerPrefs.GetInt("TutorialShown") == 0)
        {
            emptyObj.GetComponent<EmptyObjMove>().enabled = false;
            PauseB.SetActive(false);
            TutorialCanvas.SetActive(true);
            PlayerPrefs.SetInt("TutorialShown", 1);
        }
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
        deathscreen.gameObject.SetActive(true);
        PowerUpManager.instance.powerUpLengthCounter = 0f;
        if (PowerUpManager.instance.textImmune.gameObject.activeSelf)
            PowerUpManager.instance.textImmune.gameObject.SetActive(false);
       
        // StartCoroutine("restartGameCall");
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
