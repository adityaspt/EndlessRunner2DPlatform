using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSquare : MonoBehaviour
{

    public GameObject bullet;
    Vector2 BulletPos;
    public float fireRate;
    float nextFire = 0.0f;
    public bool pressedFire = false;





    //Audio
    //public AudioSource jumpSound;
    //public AudioSource deathSound;

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

    float fJumpPressedRemember;
    float fJumpPressedRememberTime = 0.2f;

    float fGroundedRemember;
    float fGroundedRememberTime = 0.4f;

    void Start()
    {

        mainInstance = this;
        isFacingRight = true;
        anim = GetComponent<Animator>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();

        lastPosY = rb2D.position.y;

    }

    private void MoveEndlessly()
    {
        rb2D.velocity = new Vector2(GameManager.gameManagerInstance.emptyObj.speed, rb2D.velocity.y);
        anim.SetFloat("Speed", speed);

    }

    void Update()
    {
        if (PlayerPrefs.GetInt("TutorialShown") == 1)
            MoveEndlessly();
        Grounded = Physics2D.OverlapCircle(GroundCheck.position, groundcheckRadius, groundLayer);
        JumpAnimController(lastPosY);


        lastPosY = rb2D.position.y;

#if UNITY_EDITOR 
        if (Input.GetAxis("Horizontal") != 0)
        {
            horizontal = Input.GetAxis("Horizontal");

            Move(horizontal);
            //  anim.SetFloat("Speed", speed);
        }
        else if (Input.GetAxis("Horizontal") == 0)
        {
            horizontal = Input.GetAxis("Horizontal");
            Move(horizontal);


            //  anim.SetFloat("Speed", 0);
        }
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
        
        Move(hInput);
#endif


        fJumpPressedRemember = -Time.deltaTime;
        fGroundedRemember = -Time.deltaTime;
        if (Grounded || canJumpAfterDash)
        {
            fGroundedRemember = fGroundedRememberTime;
        }

        if ((Input.GetButtonDown("Jump") || jumpDevice ))
        {
            fJumpPressedRemember = fJumpPressedRememberTime;
        }

        if ((fGroundedRemember > 0 && fJumpPressedRemember > 0))//|| (fGroundedRemember > 0 && fJumpPressedRemember>0))
        {
            fGroundedRemember = 0;
            fJumpPressedRemember = 0;
            JumpCount = 0;
            Jump(JumpPower);
            jumpDevice = false;
        }
        else if (((!Grounded ) && JumpCount < 2 && Input.GetButtonDown("Jump")) || ((!Grounded ) && JumpCount < 2 && jumpDevice))
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
        if (Input.GetMouseButtonDown(1) && Time.time > nextFire)
#endif

        {
            nextFire = Time.time + fireRate;

            shootBullet();
            pressedFire = false;
        }
        if (Input.GetKeyDown(ButtonZ) || DashDevice)
        {
            Debug.Log("Pressed z");

            if (!CoolDownStarted)
            {
                Debug.Log("Came under dash, not colldownstart");

                if (Grounded)
                {
                    canJumpAfterDash = true;
                    print("dash and grounded");
                }
                //anim.SetTrigger("DashAnim");
                Dash();
                StartCoroutine("StopTrailAfter2Secs");
                ChildImageCoolDown.fillAmount = 0;
                CoolDownStarted = true;
                DashDevice = false;
                // canDashNow = false;
            }
            else
            {
                DashDevice = false;

            }

        }

        if (CoolDownStarted)
        {



            ChildImageCoolDown.fillAmount += 1 / CoolDown * Time.deltaTime;
            if (ChildImageCoolDown.fillAmount == 1)
            {
                CoolDownStarted = false;


            }
        }


    }
    [SerializeField]
    private TrailRenderer PlayerTrailRenderer;
    float thrust = 100f;
    public Transform dashTrailPrefab;
    void Dash()
    {

        Vector3 beforeDashPos = transform.position;
        //Transform dashEffectTransform = Instantiate(dashTrailPrefab, beforeDashPos, Quaternion.identity);
        //dashEffectTransform.eulerAngles = new Vector3(0, 0);
        //  float dashEffectWidth = 2009f;
        PlayerTrailRenderer.GetComponent<TrailRenderer>().enabled = true;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, thrust);

        if (hit.collider == null)
        {
            //rb2D.AddForce(Vector2.right * thrust);
            transform.position += new Vector3(thrust * Time.deltaTime, 0f, 0.0f);
            //Transform dashEffectTransform = Instantiate(dashTrailPrefab, new Vector2(beforeDashPos.x, transform.position.y + 0.2f), Quaternion.identity);
            //dashEffectTransform.eulerAngles = new Vector3(0, 0);
            //dashEffectTransform.localScale = new Vector3(thrust / dashEffectWidth, .1079f, 1f);
        }
        else
        {
            transform.position += new Vector3(hit.distance * Time.deltaTime, 0f, 0.0f);
            //Transform dashEffectTransform = Instantiate(dashTrailPrefab, new Vector2(beforeDashPos.x, transform.position.y + 0.2f), Quaternion.identity);
            //dashEffectTransform.eulerAngles = new Vector3(0, 0);
            //dashEffectTransform.localScale = new Vector3(hit.distance / dashEffectWidth, .1079f, 1f);
        }
        if (Grounded)
        {
            canJumpAfterDash = false;
        }
        
    }
    IEnumerator StopTrailAfter2Secs()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerTrailRenderer.GetComponent<TrailRenderer>().enabled = false;

    }
    bool canJumpAfterDash = false;

   // public bool canDashNow = false;
    public Image ChildImageCoolDown;
    public float CoolDown = 5;
    public  bool CoolDownStarted = false;
    private void FixedUpdate()
    {

     



    }
    
    public KeyCode ButtonZ;
    float bulletVelocity = 6.5f;
    void shootBullet()
    {
        SFXSound.PlaySound("Laser2");
        BulletPos = transform.position;
        if (isFacingRight)
        {
            BulletPos += new Vector2(+1f, 0.45f);
            bullet.GetComponent<BulletScript>().velocityX = 6.5f;
            Instantiate(bullet, BulletPos, bullet.transform.rotation);

        }
        else
        {
            BulletPos += new Vector2(-1f, 0.45f);
            bullet.GetComponent<BulletScript>().velocityX = -6.5f;
            Instantiate(bullet, BulletPos, transform.rotation * Quaternion.Euler(0f, 0f, 90f));
            // GameObject.Instantiate(prefab, transform.position, transform.rotation * Quaternion.Euler(0f, 180f, 0f));
        }

    }

    public bool DashDevice = false;
    public void DashDeviceButton()
    {
        DashDevice = true;
    }

    public void JumpDeviceButtonDown()
    {

        jumpDevice = true;

    }
    public void JumpDeviceButtonUp()
    {

        jumpDevice = false;

    }
    public void FireBulletDeviceButton()
    {
        pressedFire = true;

    }


    public void StartMoving(float horizontalInput)
    {
        hInput = horizontalInput;


    }
    public Vector2 wherePlayerDiedPos;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Kill") || collision.CompareTag("Enemy") || collision.CompareTag("FireKill") || collision.CompareTag("Ground"))
        {
            wherePlayerDiedPos = transform.position;
            SFXSound.PlaySound("Death");
            // deathSound.Play();
            GameManager.gameManagerInstance.RestartGame();
            print("GameOver");

        }

    }

    public void RespawnPlayer()
    {

        if (wherePlayerDiedPos.x > 7.5f)
        {
            GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
            float MinDistance = Mathf.Infinity;
            //float temp;
            GameObject closestPlatform = null;
            if (platforms != null)
            {
                foreach (GameObject obj in platforms)
                {


                    Vector3 diff = obj.transform.position - transform.position;
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance < MinDistance)
                    {
                        closestPlatform = obj;
                        MinDistance = curDistance;

                    }

                }
                print("The closest platform name is " + closestPlatform.name);
                //  transform.position = closestPlatform.transform.position+(new Vector2((closestPlatform.GetComponent<BoxCollider2D>().size.x / 2) - 3, 0.75f));
                float newXPos = closestPlatform.transform.position.x-(closestPlatform.GetComponent<BoxCollider2D>().size.x / 2) +1 ;
                float newYPos = closestPlatform.transform.position.y + 0.75f;
                //Vector3 newPos = new Vector3(newXPos, 0.75f, 0f);
                // Vector3 spikePos = new Vector3(newSpike.transform.position.x, 0.75f, 0f);
                transform.position = new Vector2(newXPos,newYPos);//closestPlatform.transform.position + newPos;
                gameObject.SetActive(true);
                PowerUpManager.instance.ActivatePowerUp(true, 5);

            }
            else
            {
                print("No platforms formed");
                return;
            }
        }
        else
        {
            GameManager.gameManagerInstance.ResetGamePlay();
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
        if (horizontal == 0)
        {
            Vector2 theScale = transform.localScale;
            theScale.x = 1;
            transform.localScale = theScale;
            isFacingRight = true;
            // anim.SetFloat("Speed", 0);
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

        //if (jumpSound.isPlaying)
        //{
        //    jumpSound.Stop();
        //    jumpSound.Play();
        //}
        //else
        //{
        //    jumpSound.Play();
        //}
        //jumpSound.Play();
        SFXSound.PlaySound("Jump");

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

    //public GameObject ClosestEnemy;
    //public void FindNearestObstacle()
    //{
    //    float closestDistance = Mathf.Infinity;
    //    ClosestEnemy = null;
    //    List<GameObject> items = new List<GameObject>(GameObject.FindGameObjectsWithTag("tag1"));
    //    items.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("tag2")));
    //    //   GameObject[] enemyList=GameObject.FindGameObjectsWithTag
    //}

}
