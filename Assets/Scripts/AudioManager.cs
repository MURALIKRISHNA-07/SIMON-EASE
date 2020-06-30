using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioSource Bgm;
    
    public static AudioManager instance;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        int i=0;
        foreach (Sound s in sounds)
        {
            
            i++;
            s.number=s.number+i;
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.thisclip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            
        }
        Bgm.Play();
    }
  
    public void Play(int number)
    {
        Sound s = Array.Find(sounds, sound => sound.number == number);
        if (s == null)
            return;
        
        s.source.Play();
    }
    public void Stop(int number)
    {
        Sound s = Array.Find(sounds, sound => sound.number == number);
        if (s == null)
            return;
        
        s.source.Stop();
    }
}

[System.Serializable]
public class Sound
{
    [SerializeField]
    public int number;
    
    public AudioClip thisclip;

    [Range(0f, 1f)] public float volume;
    [Range(0f, 1f)] public float pitch;

    public bool loop;
    [HideInInspector]public  AudioSource source;

}
