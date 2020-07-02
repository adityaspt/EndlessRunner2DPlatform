using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSquare : MonoBehaviour
{

    public GameObject bullet;
    Vector2 BulletPos;
    public float fireRate = 10f;
    float nextFire = 0.0f;
    public bool pressedFire = false;





    //Audio
    public AudioSource jumpSound;
    public AudioSource deathSound;

    //Audio


    public bool isFacingRight = true;
    public static MoveSquare mainInstance;
    public float speed = 5f;
    float horizontal;
    public float JumpPower = 3f;
    private Rigidbody2D rb2D;
    public bool Grounded;
    private int JumpCount = 0;
    public LayerMask groundLayer;
    public LayerMask fireLayer;

    private Animator anim;
    private float lastPosY = 0.0f;
    private bool isJumped = false;


    public Transform GroundCheck;
    public float groundcheckRadius;

    private float hInput = 0f;
    bool jumpDevice = false;

    void Start()
    {
        mainInstance = this;
        isFacingRight = true;
        anim = GetComponent<Animator>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();

        lastPosY = rb2D.position.y;

    }


    void Update()
    {

        Grounded = Physics2D.OverlapCircle(GroundCheck.position, groundcheckRadius, groundLayer);
        JumpAnimController(lastPosY);


        lastPosY = rb2D.position.y;

#if UNITY_EDITOR 
        if (Input.GetAxis("Horizontal") != 0)
        {
            horizontal = Input.GetAxis("Horizontal");

            Move(horizontal);
            anim.SetFloat("Speed", speed);
        }
        else if (Input.GetAxis("Horizontal") == 0)
        {
            horizontal = Input.GetAxis("Horizontal");
            Move(horizontal);


            anim.SetFloat("Speed", 0);
        }
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
        
        Move(hInput);
#endif





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
#if UNITY_ANDROID && !UNITY_EDITOR
        if(pressedFire &&   Time.time>nextFire)
#endif
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(1) && Time.time>nextFire)
#endif

        {
            nextFire = Time.time + fireRate;
            shootBullet();
            pressedFire = false;
        }



    }

    void shootBullet()
    {
        BulletPos = transform.position;
        if (isFacingRight)
        {
            BulletPos += new Vector2(+1f, 0.45f);
            bullet.GetComponent<BulletScript>().velocityX = 6f;
            Instantiate(bullet, BulletPos, bullet.transform.rotation);
                    
        }
        else
        {
            BulletPos += new Vector2(-1f, 0.45f);
            bullet.GetComponent<BulletScript>().velocityX = -6f;
            Instantiate(bullet, BulletPos, transform.rotation * Quaternion.Euler(0f, 0f, 90f));
           // GameObject.Instantiate(prefab, transform.position, transform.rotation * Quaternion.Euler(0f, 180f, 0f));
        }
    }


    public void JumpDeviceButton()
    {
        jumpDevice = true;

    }
    public void FireBulletDeviceButton()
    {
        pressedFire = true;

    }


    public void StartMoving(float horizontalInput)
    {
        hInput = horizontalInput;


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Kill") || collision.CompareTag("Enemy") || collision.CompareTag("FireKill"))
        {
            deathSound.Play();
            GameManager.gameManagerInstance.RestartGame();
            print("GameOver");

        }

    }




    public void Move(float horizontal)
    {

#if UNITY_EDITOR
        if ((horizontal > 0 && !isFacingRight) || (horizontal < 0 && isFacingRight))
        {
            isFacingRight = !isFacingRight;


            Vector2 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

#endif
#if UNITY_ANDROID && !UNITY_EDITOR
        if (horizontal != 0)
        {
            anim.SetFloat("Speed", speed);
            //if (horizontal > 0)
            //{
            //    isFacingRight = true;
            //}
            //else
            //{
            //    isFacingRight = false;
            //}
            if ((horizontal > 0 && !isFacingRight) || (horizontal < 0 && isFacingRight))
            {
                isFacingRight = !isFacingRight;
         Vector2 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            }
        }
#endif
        if (horizontal==0)
        {
            Vector2 theScale = transform.localScale;
            theScale.x = 1;
            transform.localScale = theScale;
            isFacingRight = true;
            anim.SetFloat("Speed", 0);
        }






        transform.position = new Vector3(transform.position.x + horizontal * speed * Time.deltaTime, transform.position.y, transform.position.z);


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

    }

    public void Jump(float power)
    {
        rb2D.velocity = Vector2.up * power;
        //rb2D.AddForce(transform.up * power);
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
