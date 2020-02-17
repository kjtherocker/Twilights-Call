using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    public AudioSource _audioSource;

    public AudioManager(GameObject gameObject)
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlaySoundRepeating(AudioClip audioClip)
    {
        if (audioClip == null)
        {
            return;
        }

        _audioSource.loop = true;
        _audioSource.clip = audioClip;
        _audioSource.Play();
        
    }

    public void PlaySoundOneShot(AudioClip audioClip)
    {
        if (audioClip == null)
        {
            return;
        }
        
        _audioSource.loop = false;
        _audioSource.PlayOneShot(audioClip);
    }

    public void PlaySoundDelayed(AudioClip audioClip, float delay)
    {
        if (audioClip == null)
        {
            return;
        }
        _audioSource.loop = false;
         _audioSource.clip = audioClip;
         _audioSource.PlayDelayed(delay);
        
    }

    public void PlaySoundScheduled(AudioClip audioClip, double time)
    {
        if (audioClip == null)
        {
            return;
        }
        _audioSource.loop = false;
         _audioSource.clip = audioClip;
         _audioSource.PlayScheduled(time);
        
    }

    public void StopSound()
    {
        _audioSource.Stop();
    }
}