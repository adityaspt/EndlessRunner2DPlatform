using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXSound : MonoBehaviour
{
    static SFXSound instanceSFX;

    public static SFXSound Instance { get { return instanceSFX; } }
    public static AudioClip hitSound;

    static AudioSource audioSrc;
    void Start()
    {
        if (instanceSFX)
        {
            Destroy(this.gameObject);
            return;
        }
        instanceSFX = this;
        DontDestroyOnLoad(this.gameObject);
        audioSrc = GetComponent<AudioSource>();

    }

    public static void PlaySound(string clip)
    {
        if (PlayerPrefs.GetInt("SFXOn") == 1)
        {
            hitSound = Resources.Load<AudioClip>("Audio/" + clip);
            audioSrc.PlayOneShot(hitSound);
        }

    }
}
