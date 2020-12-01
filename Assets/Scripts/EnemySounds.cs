using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour {

    [SerializeField]
    private AudioClip[] stepClips;

    

    public AudioSource audioSource;

    private void Awake() {
        //audioSource = GetComponent<AudioSource>();
    }

    private void Step() {
        AudioClip clip = GetRandomStepClip();
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(clip);
    }


    private AudioClip GetRandomStepClip() {
        return stepClips[Random.Range(0, stepClips.Length)];
    }

}
