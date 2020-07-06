using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSound : MonoBehaviour
{
   // static Sound instanceHome;

    public static BGSound Instance; //{ get { return instanceHome; } }
   // private static Sound instance;
   // public AudioSource _audioSource;
    private void Awake()
    {
        if (Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        // DontDestroyOnLoad(transform.gameObject);
      //  _audioSource = GetComponent<AudioSource>();
    }

    //public void PlayMusic()
    //{
    //    if (_audioSource.isPlaying) return;
    //    _audioSource.Play();
    //}

    //public void StopMusic()
    //{
    //    _audioSource.Stop();
    //}

}
