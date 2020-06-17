using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPos;
    public GameObject cam;
    public float parallaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        print(length + " " + gameObject.name);
        if(gameObject.name== "Mountain_1" || gameObject.name == "Mountain_2" || gameObject.name == "Mountain_3" || gameObject.name == "Mountain_4")
        {
            length = 20f;
        }
    }

    private void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float distance = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPos+distance,transform.position.y,transform.position.z);
        if (temp > startPos + length) startPos += length;
        else if (temp < startPos - length) startPos -= length;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
