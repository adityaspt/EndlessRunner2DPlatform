using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSquare : MonoBehaviour
{
    public float speed = 5f;
    float horizontal;
    public float JumpPower = 3f;
    private Rigidbody2D rb2D;
    public bool Grounded;
    private int JumpCount = 0;
    public LayerMask groundLayer;
    private BoxCollider2D BCol2d;
    private Animator anim;
    private float lastPosY = 0.0f;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        BCol2d = GetComponent<BoxCollider2D>();
        lastPosY = rb2D.position.y;
    }


    void Update()
    {
        Grounded = Physics2D.IsTouchingLayers(BCol2d, groundLayer);
        horizontal = Input.GetAxis("Horizontal");
        JumpAnimController(lastPosY);


        lastPosY = rb2D.position.y;

        Move(horizontal);
        anim.SetFloat("Speed", horizontal);
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
        }
        else
            anim.SetBool("Grounded", false);
        
        


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
        else if(lastY > rb2D.position.y && !Grounded)
        {
            anim.SetTrigger("JumpDown");
        }
    }

    public void Jump(float power)
    {
        
        rb2D.AddForce(transform.up * power);
        JumpCount++;
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
