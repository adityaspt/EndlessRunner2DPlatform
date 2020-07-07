using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField]
    private Sprite[] framearray;
    private int currentFrame;
    private float timer;
    private float frameRate = .1f;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
        spriteRenderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentFrame < framearray.Length - 1)
        {
            timer += Time.deltaTime;

            if (timer >= frameRate)
            {
                timer -= frameRate;
                currentFrame++;
                // currentFrame=(currentFrame+1)% framearray.Length;
                gameObject.GetComponent<SpriteRenderer>().sprite = framearray[currentFrame];

            }
            if (currentFrame == framearray.Length - 1)
            {
                
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                spriteRenderer.sprite = framearray[0];
            }
        }
    }
}
