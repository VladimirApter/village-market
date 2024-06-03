using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource audioSrc => GetComponent<AudioSource>();

    public void Play(AudioClip clip, float volume = 0.1f, bool destroyed = false, float p1 = 1f, bool loopFlag = false)
    {
        audioSrc.pitch = p1;

        if (destroyed)
            AudioSource.PlayClipAtPoint(clip, transform.position, volume);
        else
        {
            if (!loopFlag) audioSrc.PlayOneShot(clip, volume);
            else
            {
                audioSrc.loop = loopFlag;
                audioSrc.clip = clip;
                audioSrc.Play();
                audioSrc.volume = volume;
            }
        }
            
        
    }
}
