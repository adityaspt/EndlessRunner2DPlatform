using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSquare : MonoBehaviour
{
    //Audio
    public AudioSource jumpSound;
    public AudioSource deathSound;

    //Audio



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
    private float hInput=0f;
    bool jumpDevice = false;

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
        JumpAnimController(lastPosY);


        lastPosY = rb2D.position.y;

#if !UNITY_ANDROID && !UNITY_IPHONE
          horizontal = Input.GetAxis("Horizontal");
        //rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
         Move(horizontal);
#else
        Move(hInput);
#endif

        anim.SetFloat("Speed", speed);
        //if(rb2D.position.y <lastY && !Grounded)
        //{
        //    anim.SetTrigger("JumpDown");
        //}
        //else
        //{
        //    anim.SetTrigger("Jump");
        //}

        
        if ((Input.GetButtonDown("Jump") && Grounded) || (jumpDevice && Grounded))
        {
            JumpCount = 0;
            Jump(JumpPower);
            jumpDevice = false;
        }
        else if ((!Grounded && JumpCount < 2 && Input.GetButtonDown("Jump")) || (!Grounded && JumpCount < 2 && jumpDevice))
        {
            Jump(JumpPower);
            jumpDevice = false;
        }


        if (Grounded)
        {
            anim.SetBool("Grounded", true);
            isJumped = false;
        }
        else
            anim.SetBool("Grounded", false);




    }

    public void JumpDeviceButton()
    {
        jumpDevice = true;
        
    }


    public void StartMoving(float horizontalInput)
    {
        hInput = horizontalInput;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.name=="FireWall")
        //{
        //    deathSound.Play();
        //    GameManager.gameManagerInstance.RestartGame();
        //        print("GameOver");

        //}
        //else 
        if (collision.CompareTag("Kill"))
        {
            deathSound.Play();
            GameManager.gameManagerInstance.RestartGame();
            print("GameOver");

        }

    }
    public void Move(float horizontal)
    {

        transform.position = new Vector3(transform.position.x + horizontal * speed * Time.deltaTime, transform.position.y, transform.position.z);
        //Vector2 vel = rb2D.velocity;
        
        //vel.x = horizontal * speed;
        //rb2D.velocity = vel;
    }

    public void JumpAnimController(float lastY)
    {
        if (lastY < rb2D.position.y && !Grounded)
        {
            anim.SetTrigger("Jump");
        }
        else if (lastY > rb2D.position.y && !Grounded && isJumped)
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

        if (jumpSound.isPlaying)
        {
            jumpSound.Stop();
            jumpSound.Play();
        }
        else
        {
            jumpSound.Play();
        }
        jumpSound.Play();
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
