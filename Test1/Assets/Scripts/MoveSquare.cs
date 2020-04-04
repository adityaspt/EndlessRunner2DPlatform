using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSquare : MonoBehaviour
{
    public static MoveSquare mainInstance;
    public float speed = 5f;
    float horizontal;
    public float JumpPower = 3f;
    private Rigidbody2D rb2D;
    public bool Grounded;
    private int JumpCount = 0;
    public LayerMask groundLayer;
    public LayerMask fireLayer;
   // private BoxCollider2D BCol2d;
    private Animator anim;
    private float lastPosY = 0.0f;
    private bool isJumped = false;


    public Transform GroundCheck;
    public float groundcheckRadius;
   // private bool caughtOnFire = false; 


    void Start()
    {
        mainInstance = this;

        anim = GetComponent<Animator>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        //BCol2d = GetComponent<BoxCollider2D>();
        lastPosY = rb2D.position.y;
       //isJumped = false;
    }


    void Update()
    {
        //caughtOnFire= Physics2D.IsTouchingLayers(BCol2d, fireLayer);

        // Grounded = Physics2D.IsTouchingLayers(BCol2d, groundLayer);
        Grounded = Physics2D.OverlapCircle(GroundCheck.position, groundcheckRadius, groundLayer);

        horizontal = Input.GetAxis("Horizontal");
        //rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
        JumpAnimController(lastPosY);


        lastPosY = rb2D.position.y;

        Move(horizontal);
        anim.SetFloat("Speed", speed);
        //if(rb2D.position.y <lastY && !Grounded)
        //{
        //    anim.SetTrigger("JumpDown");
        //}
        //else
        //{
        //    anim.SetTrigger("Jump");
        //}


        if (Input.GetButtonDown("Jump") && Grounded)
        {
            JumpCount = 0;
            Jump(JumpPower);
            
        }
        else if (!Grounded && JumpCount < 2 && Input.GetButtonDown("Jump"))
        {
            Jump(JumpPower);
        }

        
        if (Grounded)
        {
            anim.SetBool("Grounded", true);
            isJumped = false;
        }
        else
            anim.SetBool("Grounded", false);
        
        


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name=="Fire")
        {
            GameManager.gameManagerInstance.RestartGame();
                print("GameOver");
           
        }
    }
    public void Move(float horizontal)
    {

        transform.position = new Vector3(transform.position.x + horizontal * speed * Time.deltaTime, transform.position.y, transform.position.z);
    }

    public void JumpAnimController(float lastY)
    {
        if(lastY<rb2D.position.y && !Grounded)
        {
            anim.SetTrigger("Jump");
        }
        else if(lastY > rb2D.position.y && !Grounded && isJumped)
        {
            anim.SetTrigger("JumpDown");
        }
        else if (lastY > rb2D.position.y && !Grounded && !isJumped)
        {
            anim.SetTrigger("JumpDown");
        }
        //else if(lastY > rb2D.position.y && !Grounded)
    }

    public void Jump(float power)
    {
        
        rb2D.AddForce(transform.up * power);
        JumpCount++;
        isJumped = true;
        //print(JumpCount);
        //anim.SetTrigger("JumpDown");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            Grounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            Grounded = false;
        }
    }
}
