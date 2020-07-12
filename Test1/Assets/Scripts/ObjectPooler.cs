using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject pooledObject;
    public int pooledAmount;
    List<GameObject> pooledObjects;
    // Start is called before the first frame update
    void Start()
    {
    pooledObjects = new List<GameObject> { };
    for (int i=0;i<pooledAmount;i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            if (obj.CompareTag("Kill"))
            {
                obj.transform.SetParent(PowerUpManager.instance.spikesPowerPoolObj.transform);
            }
            if (obj.CompareTag("Enemy"))
            {
                obj.transform.SetParent(PowerUpManager.instance.enemyPowerPoolObj.transform);
            }
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
    public GameObject GetPooledObjects()
    {
        for(int i=0;i<pooledObjects.Count;i++)
        {
            
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(pooledObject);
        //if (obj.CompareTag("PowerUp"))
        //{
        //    obj.transform.SetParent(PowerUpManager.instance.powerUpPoolerObj.transform);
        //    for (int j = 0; j < PowerUpManager.instance.powerUpPoolerObj.transform.childCount; j++)
        //    {
        //        PowerUpManager.instance.arr.Add(PowerUpManager.instance.powerUpPoolerObj.transform.GetChild(j).gameObject);
        //    }
        //}
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
