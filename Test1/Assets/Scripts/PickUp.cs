using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour

{
    public AudioSource coinSound;
    public int ScoreToGive;

    // Start is called before the first frame update
    void Start()
    {
        coinSound = GameObject.Find("CoinSound").GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            print("Get coin");
            ScoreManager.instance.AddScore(ScoreToGive);
            if(coinSound.isPlaying)
            {
                coinSound.Stop();
                coinSound.Play();
            }
            else
            {
                coinSound.Play();
            }
            
            gameObject.SetActive(false);

        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
       transform.localScale =new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z) ;
    }
}
