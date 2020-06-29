using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Transform SpeedMultiplier;
    private Vector3 LastPosSpeedMultiplier;
    private float DistanceToMove;
    void Start()
    {
        LastPosSpeedMultiplier = SpeedMultiplier.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DistanceToMove = SpeedMultiplier.position.x - LastPosSpeedMultiplier.x;

        transform.position = new Vector3(transform.position.x + DistanceToMove, transform.position.y, transform.position.z);

        LastPosSpeedMultiplier = SpeedMultiplier.position;

        // transform.position = new Vector3(MoveSquare.mainInstance.speed+transform.position.x, transform.position.y, transform.position.z);



    }
}
