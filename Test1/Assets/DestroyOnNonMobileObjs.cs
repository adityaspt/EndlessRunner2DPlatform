using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnNonMobileObjs : MonoBehaviour
{
    // Start is called before the first frame update


    void Start()
    {
#if !UNITY_ANDROID && !UNITY_IPHONE
        this.gameObject.SetActive(false);
    }
#else
        this.gameObject.SetActive(true);
    
    }

#endif
        // Update is called once per frame

    }
