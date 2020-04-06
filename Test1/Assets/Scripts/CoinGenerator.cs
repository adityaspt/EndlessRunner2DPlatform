using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    public ObjectPooler objPooler;
    public float distanceBetweenCoins;
    // Start is called before the first frame update

    public void SpawnCoins(Vector3 startPos)
    {
        GameObject coin1 =objPooler.GetPooledObjects();
        coin1.transform.position = startPos;
        coin1.SetActive(true);

        GameObject coin2 = objPooler.GetPooledObjects();
        coin2.transform.position = new Vector3(startPos.x-distanceBetweenCoins,startPos.y,startPos.z);
        coin2.SetActive(true);

        GameObject coin3 = objPooler.GetPooledObjects();
        coin3.transform.position = new Vector3(startPos.x + distanceBetweenCoins, startPos.y, startPos.z);
        coin3.SetActive(true);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
