using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///PLays a blink sound effect
/// </summary>
public class Blink : MonoBehaviour
{
    public AudioClip[] blinkAudioClips;
    public AudioSource audioSource;
    // Start is called before the first frame update
    public void PlayBlinkSound() {
        audioSource.PlayOneShot(blinkAudioClips[Random.Range(0, blinkAudioClips.Length)]);
    }
}
