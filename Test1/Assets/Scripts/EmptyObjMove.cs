using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyObjMove : MonoBehaviour
{
    public static EmptyObjMove emptyObjInstance;
    public float speed=2f;
    public float speedStore;
    public float speedMilestoneTargetStore;
    public float speedMilestoneTarget;
    public float speedMilestoneCount;
    public float speedMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        emptyObjInstance = this;
        //speed = MoveSquare.mainInstance.speed;
        speedMilestoneTargetStore = speedMilestoneTarget;
        speedMilestoneCount = speedMilestoneTargetStore;
        speedStore = speed;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x>speedMilestoneCount)
        {
            speedMilestoneCount += speedMilestoneTarget;
            speedMilestoneTarget = speedMilestoneTarget * speedMultiplier;
                speed = speed * speedMultiplier;
            speedMilestoneTarget += speedMilestoneTarget ;

            
        }
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
