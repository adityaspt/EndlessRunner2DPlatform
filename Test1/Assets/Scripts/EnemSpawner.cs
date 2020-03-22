using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemSpawner : MonoBehaviour
{
    public Transform EnemyPrefab;
    int c = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (c < 5) { 
        Instantiate(EnemyPrefab, new Vector3(-6f, 0, 0), Quaternion.identity);
        c++; }
    }
}
