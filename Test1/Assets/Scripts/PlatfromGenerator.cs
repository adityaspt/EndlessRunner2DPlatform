using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfromGenerator : MonoBehaviour
{
    private int platformSelector;
    public float[] PlatformWidthsArray;

    //Platformer Mover
    public float movingPlatformThreshold;

    // Randomize Height
    public float minHeight;
    public float maxHeight;
    public Transform maxHeightPoint;
    public float maxHeightChange;
    public float heightChange;
    //

    public Transform GenerationPoint;

    private float DistanceBetween = 3.8f;
    [SerializeField]
    private float DistanceBetweenMin ;
    [SerializeField]
    private float DistanceBetweenMax ;
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
     float enemySpawnThreshold;
    public ObjectPooler enemySpawnPool;
    public bool enemyPlaced = false;


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
        for (int i = 0; i < objectpools.Length; i++)
        {
            PlatformWidthsArray[i] = objectpools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }
    }
    void Update()
    {

        if (transform.position.x < GenerationPoint.position.x)
        {

            //Platform spawner
            platformSelector = UnityEngine.Random.Range(0, objectpools.Length);
            DistanceBetween = Convert.ToInt32(UnityEngine.Random.Range(DistanceBetweenMin, DistanceBetweenMax));
            heightChange = transform.position.y + Convert.ToInt32(UnityEngine.Random.Range(maxHeightChange, -maxHeightChange));

            if (heightChange > maxHeight)
            {
                heightChange = maxHeight;
            }
            else if (heightChange < minHeight)
            {
                heightChange = minHeight;
            }
            transform.position = new Vector3(transform.position.x + DistanceBetween + (PlatformWidthsArray[platformSelector] / 2), heightChange, transform.position.z);

            float temp1 = Convert.ToInt32(UnityEngine.Random.Range(0f, 100f));



            GameObject newPlatform = objectpools[platformSelector].GetPooledObjects();
            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            if (temp1 < movingPlatformThreshold)
            {
                if (!newPlatform.GetComponent<PlatformMovement>())
                {
                    Destroy(newPlatform.GetComponent<PlatformMovement>());
                    newPlatform.AddComponent<PlatformMovement>();
                
                }
            }
            newPlatform.SetActive(true);
            //

            #region EnemyObjectsSpawn



            temp = Convert.ToInt32(UnityEngine.Random.Range(0f, 100f));


            if (temp < randomSpikesThreshold)
            {
                int tempEnemySelect = UnityEngine.Random.Range(1, 3);
                if (tempEnemySelect == 1)
                {
                    if (platformSelector > 0)
                    {
                        GameObject newSpike = spikePool.GetPooledObjects();
                        float spikeXPos = (PlatformWidthsArray[platformSelector] / 2) - 3;// UnityEngine.Random.Range((-PlatformWidthsArray[platformSelector] / 2f) + 3f, (PlatformWidthsArray[platformSelector] / 2f) - 4f);
                        Vector3 spikePos = new Vector3(spikeXPos, 0.75f, 0f);
                        // Vector3 spikePos = new Vector3(newSpike.transform.position.x, 0.75f, 0f);
                        newSpike.transform.position = transform.position + spikePos;
                        newSpike.transform.rotation = transform.rotation;
                        newSpike.SetActive(true);

                    }
                }
                else
                {
                    if (platformSelector > 1)
                    {
                        GameObject newEnemy = enemySpawnPool.GetPooledObjects();

                        float enemySpawnXPos = UnityEngine.Random.Range((-PlatformWidthsArray[platformSelector] / 2f) + 2.5f, (PlatformWidthsArray[platformSelector] / 2f) - 1f);
                        Vector3 enemySpawnPos = new Vector3(enemySpawnXPos, 0.75f, 0f);
                        newEnemy.transform.position = transform.position + enemySpawnPos;
                        newEnemy.transform.rotation = transform.rotation;
                        newEnemy.SetActive(true);
                    }
                }
            }




            #endregion

            //Coin gen script
            if (Convert.ToInt32(UnityEngine.Random.Range(0f, 100f)) < randomCointhreshold)
            {
                coinGen.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
                SpikePlaced = false;

            }
            //


            //Sheild Spawner
            if (UnityEngine.Random.Range(0, 100f) < powerUpThreshold)
            {
                GameObject newPowerUp = powerUpPool.GetPooledObjects();

                newPowerUp.transform.position = transform.position + new Vector3(DistanceBetween / 2, UnityEngine.Random.Range(powerUpHeight / 2, powerUpHeight), 0f);
                newPowerUp.SetActive(true);
            }
            //


            //Push the generator ahead by x axis
            transform.position = new Vector3(transform.position.x + (PlatformWidthsArray[platformSelector] / 2), transform.position.y, transform.position.z);

        }
    }
}
