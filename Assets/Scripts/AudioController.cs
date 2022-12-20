using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public Sound[] sounds;
    public float riseSpeed;
    private static AudioController _instance;
    private AudioSource mainSource;
    public static AudioController Instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        mainSource = GetComponent<AudioSource>();
        mainSource.volume = 0f;
        foreach (Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.playOnAwake = false;
            sound.audioSource.clip = sound.clip;
            sound.audioSource.volume = sound.volume;
        }
    }


    private void Update()
    {
        if (mainSource.volume <= 0.4f)
        {
            mainSource.volume += riseSpeed * Time.deltaTime;
        }
    }


    /// <summary>
    /// Plays an Audio Clip
    /// </summary>
    /// <param name="name"></param>
    public void Play(string name)
    {
        Sound sound = System.Array.Find(sounds, sObj => sObj.soundName == name);
        if (sound == null) 
            Debug.LogWarning("Sound " + name + "not found");
        sound.audioSource.UnPause();
        sound.audioSource.volume = sound.volume;
        if (sound.audioSource.isPlaying)
        {
            sound.audioSource.Stop();
        }
        sound.audioSource.Play();
    }

   
}
