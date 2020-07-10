using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float velocityX = 6.5f;
    float velocityY = 0f;
    Rigidbody2D rb2;
    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2.velocity = new Vector2(velocityX, velocityY);
        Destroy(gameObject, 2f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy") || !collision.gameObject.CompareTag("Player") || !collision.gameObject.CompareTag("Bullet") || !collision.gameObject.CompareTag("PowerUp"))
        {
            Destroy(gameObject);

        }
        
    }

}
