using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_click : MonoBehaviour
{
    public AudioSource myClick;
    public AudioClip myClip;

    public void ClickSound()
    {
        myClick.PlayOneShot(myClip, volumeScale: 0.1f);
    }
}
