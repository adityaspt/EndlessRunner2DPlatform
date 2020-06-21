using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfromGenerator : MonoBehaviour
{
   // public GameObject platformPrefab;
    //public GameObject[] platformPrefabsArray;
    private int platformSelector;
    public float[] PlatformWidthsArray;

    // Randomize Height
    public float minHeight;
    public float maxHeight;
    public Transform maxHeightPoint;
    public float maxHeightChange;
    public float heightChange;
    //

    public Transform GenerationPoint;
    
    private float DistanceBetween=3.8f;
    [SerializeField]
    private float DistanceBetweenMin = 2f;
    [SerializeField]
    private float DistanceBetweenMax = 6f;
    private float PlatformWidth;
    public ObjectPooler[] objectpools;
    
    //Coin Pool
    public float randomCointhreshold;
    private CoinGenerator coinGen;


    //Spikes Pool
    float temp;
    public float randomSpikesThreshold;
    public ObjectPooler spikePool;
    private bool SpikePlaced = false;

    //Enemy Pool
    public float enemySpawnThreshold;
    public ObjectPooler enemySpawnPool;
    public bool enemyPlaced=false;


    // Start is called before the first frame update
    public ObjectPooler powerUpPool;
    public float powerUpThreshold;
    public float powerUpHeight;
    void Start()
    {
        enemyPlaced = false;
        SpikePlaced = false;
        coinGen = FindObjectOfType<CoinGenerator>();
        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.transform.position.y;
        PlatformWidthsArray = new float[objectpools.Length];
        //PlatformWidth = platformPrefab.GetComponent<BoxCollider2D>().size.x;
        for(int i=0;i< objectpools.Length;i++)
        {
            PlatformWidthsArray[i]= objectpools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(transform.position.x<GenerationPoint.position.x)
        {
            platformSelector = UnityEngine.Random.Range(0, objectpools.Length);
            DistanceBetween = Convert.ToInt32(UnityEngine.Random.Range(DistanceBetweenMin, DistanceBetweenMax));
            heightChange = transform.position.y + Convert.ToInt32(UnityEngine.Random.Range(maxHeightChange, -maxHeightChange));
            
            if(heightChange>maxHeight)
            {
                heightChange = maxHeight;
            }
            else if (heightChange < minHeight)
            {
                heightChange = minHeight;
            }
            transform.position = new Vector3(transform.position.x + DistanceBetween + (PlatformWidthsArray[platformSelector]/2), heightChange, transform.position.z);

            //Instantiate(platformPrefabsArray[platformSelector],transform.position,transform.rotation);

            if(UnityEngine.Random.Range(0,100f)<powerUpThreshold)
            {
                GameObject newPowerUp = powerUpPool.GetPooledObjects();
                
                newPowerUp.transform.position = transform.position + new Vector3(DistanceBetween / 2,UnityEngine.Random.Range(powerUpHeight/2, powerUpHeight), 0f);
                newPowerUp.SetActive(true);
            }


            GameObject newPlatform = objectpools[platformSelector].GetPooledObjects();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);
            if (!SpikePlaced || enemyPlaced )
            {
                temp = Convert.ToInt32(UnityEngine.Random.Range(0f, 100f));
               // enemySpawnThreshold = 100 - temp;

                if (temp < randomSpikesThreshold)
                {
                    SpikePlaced = true;
                    enemyPlaced = false;
                    GameObject newSpike = spikePool.GetPooledObjects();
                    float spikeXPos = UnityEngine.Random.Range((-PlatformWidthsArray[platformSelector] / 2f) + 2.5f, (PlatformWidthsArray[platformSelector] / 2f) - 1f);
                    //float spikeXPos = UnityEngine.Random.Range(-PlatformWidthsArray[platformSelector] / 2, PlatformWidthsArray[platformSelector] / 2);
                    Vector3 spikePos = new Vector3(spikeXPos, 0.75f, 0f);
                    newSpike.transform.position = transform.position + spikePos;
                    newSpike.transform.rotation = transform.rotation;
                    newSpike.SetActive(true);

                }
            }
            else if(SpikePlaced || !enemyPlaced)
            {
                GameObject newEnemy = enemySpawnPool.GetPooledObjects();
                Vector3 enemySpawnPos = new Vector3((PlatformWidthsArray[platformSelector] ), 0.75f, transform.position.z);
                newEnemy.transform.position = transform.position + enemySpawnPos;
                newEnemy.transform.rotation = transform.rotation;
                newEnemy.SetActive(true);
                enemyPlaced = true;
                SpikePlaced = false;

            }
            //Coin gen script
            if (Convert.ToInt32(UnityEngine.Random.Range(0f, 100f)) < randomCointhreshold)
            // if(100f-test<randomCointhreshold)
            {
                //if (SpikePlaced)
                //{
                coinGen.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
                SpikePlaced = false;
                //}

            }

            transform.position = new Vector3(transform.position.x + (PlatformWidthsArray[platformSelector] / 2), transform.position.y, transform.position.z);

        }
    }
}
