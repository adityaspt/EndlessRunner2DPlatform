using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfromGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    //public GameObject[] platformPrefabsArray;
    private int platformSelector;
    public float[] PlatformWidthsArray;



    public Transform GenerationPoint;
    
    private float DistanceBetween=3.8f;
    [SerializeField]
    private float DistanceBetweenMin = 2f;
    [SerializeField]
    private float DistanceBetweenMax = 6f;
    private float PlatformWidth;
    public ObjectPooler[] objectpools;
    // Start is called before the first frame update
    
    void Start()
    {
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
            
            transform.position = new Vector3(transform.position.x + DistanceBetween + PlatformWidthsArray[platformSelector], transform.position.y, transform.position.z);

            //Instantiate(platformPrefabsArray[platformSelector],transform.position,transform.rotation);

            GameObject newPlatform = objectpools[platformSelector].GetPooledObjects();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);
        }
    }
}
