using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float MaxHeight = 2.6f;
    public float MinHeight = -2.85f;
    [SerializeField]
    private Vector3 newPos;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private Vector3 MaxPos;
    [SerializeField]
    private Vector3 MinPos;
    private Vector3 nextPos;
    public PlatformMovement instance;

    private void Awake()
    {
        instance = this;    
    }
    // Start is called before the first frame update
    void Start()
    {
        MaxPos = new Vector3(this.transform.position.x, MaxHeight, transform.position.z);
        MinPos = new Vector3(this.transform.position.x, MinHeight, transform.position.z);
        if (Vector3.Distance(transform.position, MaxPos) < Vector3.Distance(transform.position, MinPos))
        {
            nextPos = MaxPos;
        }
        else
        {
            nextPos = MinPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlatformMove();
    }

    private void PlatformMove()
    {

        transform.position = Vector3.MoveTowards(this.transform.position, nextPos, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, nextPos) <= 0.1f)
        {
            changeDestination();
        }
    }
    private void changeDestination()
    {
        nextPos = nextPos != MaxPos ? MaxPos : MinPos;
    }
}
