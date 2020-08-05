using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : Singleton<AudioManager>
{

    public enum Soundtypes
    {
        Ambient,
        Dialogue,
        Music,
        SoundEffects
    }


    public AudioSource m_AmbientAudioSource;
    public AudioSource m_DialogueAudioSource;
    public AudioSource m_MusicAudioSource;
    public AudioSource m_SoundEffectsAudioSource;
    public Dictionary<int, AudioSource> m_AudioSources = new Dictionary<int, AudioSource>();
    public AudioManager(GameObject gameObject)
    {
        AudioSource[] audioSources = gameObject.GetComponents<AudioSource>();
        m_AmbientAudioSource = audioSources[0];
        m_AudioSources.Add((int)Soundtypes.Ambient,m_AmbientAudioSource );
        
        m_DialogueAudioSource = audioSources[1];
        m_AudioSources.Add((int)Soundtypes.Ambient,m_DialogueAudioSource );
        
        m_MusicAudioSource = audioSources[2];
        m_AudioSources.Add((int)Soundtypes.Ambient,m_MusicAudioSource );
        
        m_SoundEffectsAudioSource = audioSources[3];
        m_AudioSources.Add((int)Soundtypes.Ambient,m_SoundEffectsAudioSource );
        

    }

    public void PlaySoundRepeating(AudioClip audioClip,Soundtypes aSoundType)
    {
        
        AudioSource TempAudioSource = m_AudioSources[(int)aSoundType];

        if (TempAudioSource == null)
        {
            Debug.Log("Audio source: " + aSoundType + " is Null");
            return;
        }
        
        if (audioClip == null)
        {
            return;
        }

  
        TempAudioSource.loop = true;
        TempAudioSource.clip = audioClip;
        TempAudioSource.Play();
        
    }

    public void PlaySoundOneShot(AudioClip audioClip,Soundtypes aSoundType)
    {
        AudioSource TempAudioSource = m_AudioSources[(int)aSoundType];

        if (TempAudioSource == null)
        {
            Debug.Log("Audio source: " + aSoundType + " is Null");
            return;
        }
        
        if (audioClip == null)
        {
            return;
        }
        
        TempAudioSource.loop = false;
        TempAudioSource.PlayOneShot(audioClip);
    }

    public void PlaySoundDelayed(AudioClip audioClip, float delay,Soundtypes aSoundType)
    {
        
        AudioSource TempAudioSource = m_AudioSources[(int)aSoundType];

        if (TempAudioSource == null)
        {
            Debug.Log("Audio source: " + aSoundType + " is Null");
            return;
        }

        if (audioClip == null)
        {
            return;
        }
        TempAudioSource.loop = false;
        TempAudioSource.clip = audioClip;
         TempAudioSource.PlayDelayed(delay);
        
    }

    public void PlaySoundScheduled(AudioClip audioClip, double time,Soundtypes aSoundType)
    {
        AudioSource TempAudioSource = m_AudioSources[(int)aSoundType];

        if (TempAudioSource == null)
        {
            Debug.Log("Audio source: " + aSoundType + " is Null");
            return;
        }

        if (audioClip == null)
        {
            return;
        }
        TempAudioSource.loop = false;
        TempAudioSource.clip = audioClip;
         TempAudioSource.PlayScheduled(time);
        
    }

    public void StopSound(Soundtypes aSoundType)
    {
        AudioSource TempAudioSource = m_AudioSources[(int)aSoundType];

        if (TempAudioSource == null)
        {
            Debug.Log("Audio source: " + aSoundType + " is Null");
            return;
        }
        TempAudioSource.Stop();
    }
}