using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    //public bool DoublePoints;
    public bool safeMode=true;
    public float powerupLength;
    public PowerUpManager thePowerUpManager;
    // Start is called before the first frame update
    void Start()
    {
        thePowerUpManager = FindObjectOfType<PowerUpManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name=="Player")
        {
            SFXSound.PlaySound("Powerup");
            thePowerUpManager.ActivatePowerUp(safeMode,powerupLength);
            gameObject.SetActive(false);
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
