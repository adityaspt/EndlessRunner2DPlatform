using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    private bool movingRight = true;
    public Transform groundDetection;
    public float distance ;
    public  float health = 100f;
    public Transform RedHealthBar;
   public Vector3 HealthBarLocalScale;
    // Start is called before the first frame update
    void Start()
    {
        HealthBarLocalScale = RedHealthBar.localScale;
        health = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position,Vector2.down,distance);
        if (groundInfo.collider == false)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
              
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
               
                movingRight = true;

            }
        }

        //if (movingRight)
        //{
        //    transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
        //    RedHealthBar.GetComponent<SpriteRenderer>().flipX = false;
        //}
        //else
        //{
        //    transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
        //     RedHealthBar.GetComponent<SpriteRenderer>().flipX = true;
        //}

        if (hitByBullet)
        {//HealthBarLocalScale.x = health/10f;
            HealthBarLocalScale = new Vector3(health / 100f, HealthBarLocalScale.y, HealthBarLocalScale.z);
            RedHealthBar.localScale = HealthBarLocalScale;
            hitByBullet = false;
        }
    }
    bool hitByBullet=false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            if (health > 1f)
            {
                health -= 50f;
                if (health <= 0)
                {
                    gameObject.SetActive(false);
                    ScoreManager.instance.AddScore(100);
                    health = 100f;
                }
                hitByBullet = true;
            }
            //else
            //{
            //    ScoreManager.instance.AddScore(100);
            //    gameObject.SetActive(false);
            //    health = 100f;
            //}
        }
       
    }
}
