using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Transform Player;
    private Vector3 LastPosPlayer;
    private float DistanceToMove;
    void Start()
    {
        LastPosPlayer = Player.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DistanceToMove = Player.position.x - LastPosPlayer.x;

        transform.position = new Vector3(transform.position.x + DistanceToMove, transform.position.y, transform.position.z);

        LastPosPlayer = Player.position;

        // transform.position = new Vector3(MoveSquare.mainInstance.speed+transform.position.x, transform.position.y, transform.position.z);



    }
}
