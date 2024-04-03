using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Sources :  https://www.youtube.com/watch?v=EciYWWDIgB8

public class SFXmanager : MonoBehaviour
{
    public AudioSource Audio;

    public AudioClip Click;

    public static SFXmanager SFXinstance;

    //private void Awake()
    //{
    //    if (SFXinstance == null && SFXinstance != this)
    //    {
    //        Destroy(this.gameObject);
    //        return;
    //    }

    //    SFXinstance = this;
    //    DontDestroyOnLoad(this);
    //}
}
