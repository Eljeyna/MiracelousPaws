﻿using UnityEngine;

public enum MusicList
{
    
}

public class MusicDirector : MonoBehaviour
{
    public static MusicDirector Instance { get; private set; }
    public AudioDirector audioDirector;

    private void Awake()
    {
        if (ReferenceEquals(Instance, null))
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(int music)
    {
        audioDirector.Play(music);
    }

    public void StopMusic(int music)
    {
        audioDirector.Stop(music);
    }
}