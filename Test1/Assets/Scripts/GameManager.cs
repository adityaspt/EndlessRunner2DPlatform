using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        thePlayer.gameObject.SetActive(false);
        deathscreen.gameObject.SetActive(true);
        PowerUpManager.instance.powerUpLengthCounter = 0f;
        if (PowerUpManager.instance.textImmune.gameObject.activeSelf)
            PowerUpManager.instance.textImmune.gameObject.SetActive(false);
       // StartCoroutine("restartGameCall");
    }
    public void ResetGamePlay()
    {

        PowerUpManager.instance.powerUpLengthCounter = 0f;
        if (PowerUpManager.instance.textImmune.gameObject.activeSelf)
            PowerUpManager.instance.textImmune.gameObject.SetActive(false);


        deathscreen.gameObject.SetActive(false);
        platfromList = FindObjectsOfType<PlatformDestroy>();
        for (int i = 0; i < platfromList.Length; i++)
        {
            platfromList[i].gameObject.SetActive(false);
        }
        scoremanager.scoreCount = 0;

        thePlayer.transform.position = playerStartPoint;
        GameObject[] gobjs=GameObject.FindGameObjectsWithTag("Platform");
        foreach(GameObject g in gobjs)
        {
            Destroy(g);
        }
        platformGenerator.position = platformStartPoint;
        emptyObj.transform.position = emptyObjstartPoint;
        thePlayer.gameObject.SetActive(true);
        EmptyObjMove.emptyObjInstance.speed = EmptyObjMove.emptyObjInstance.speedStore;
        EmptyObjMove.emptyObjInstance.speedMilestoneTarget = EmptyObjMove.emptyObjInstance.speedMilestoneTargetStore;
        EmptyObjMove.emptyObjInstance.speedMilestoneCount = EmptyObjMove.emptyObjInstance.speedMilestoneTargetStore;

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
