using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private String currMusic = null;

    public String getCurrMusic()
    {
        return currMusic;
    }
    
    private void Awake()
    {
 
        // Codes by Zong
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);              // Make manager work across scenes -Zong

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }


        if (s.name == "MenuBGM")
        {
            currMusic = s.name;             // note music to be played as current
        }
                s.source.Play();                // Play the music
       
    }

    public void StopPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s==null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        //s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volume / 2f, s.volume));
        currMusic = null;
        s.source.Stop();
    }

    public void SwitchMusic(string oldM, string newM)
    {
        StopPlaying(oldM);
        Play(newM);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
