using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfromGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public Transform GenerationPoint;
    public float DistanceBetween;
    private float PlatformWidth;
    // Start is called before the first frame update
    void Start()
    {
        PlatformWidth = platformPrefab.GetComponent<BoxCollider2D>().size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x<GenerationPoint.position.x)
        {
            transform.position = new Vector3(transform.position.x + DistanceBetween + PlatformWidth, transform.position.y, transform.position.z);
        
            Instantiate(platformPrefab,transform.position,transform.rotation);
                
        }
    }
}
