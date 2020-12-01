using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {

    [SerializeField]
    private AudioClip[] stepClips;

    [SerializeField]
    private AudioClip punchClip;

    [SerializeField]
    private AudioClip oofClip;

    [SerializeField]
    private AudioClip smackClip;

    [SerializeField]
    private AudioClip dashClip;

    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        
    }

    private void Step() {
        AudioClip clip = GetRandomStepClip();
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(clip);
    }
    private void Punch() {
        AudioClip clip = punchClip;
        audioSource.pitch = 1.5f;
        audioSource.PlayOneShot(clip);
    }

    private void PunchRun() {
        AudioClip clip = punchClip;
        audioSource.pitch = 0.2f;
        audioSource.PlayOneShot(clip);
    }

    public void Oof() {
        audioSource.PlayOneShot(oofClip);
    }

    public void Smack() {
        audioSource.PlayOneShot(smackClip);
    }
    private void Dash() {
        audioSource.PlayOneShot(dashClip);
    }


    private AudioClip GetRandomStepClip() {
        return stepClips[UnityEngine.Random.Range(0, stepClips.Length)];
    }

}
