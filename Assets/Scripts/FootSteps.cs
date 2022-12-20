using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public AudioClip[] footStepClips;
    public AudioSource audioSource;
    // Start is called before the first frame update
    public void PlayFootStep() {
        audioSource.PlayOneShot(footStepClips[Random.Range(0, footStepClips.Length)]);
    }

}
