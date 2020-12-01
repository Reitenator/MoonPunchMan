using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CountDownTimer : MonoBehaviour {

    public float timeRemaining = 90; //in seconds
    public float timeScore;

    public TMP_Text timerText;

    private void Start() {
        timeScore = 0;
    }

    private void Update() {

        if (GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().currentState != PlayerState.dead) {

            TimeSpan timeSpan = TimeSpan.FromSeconds(timeRemaining);
            string countdown = timeSpan.ToString(@"mm\:ss\.f");

            timerText.text = countdown;

            timeScore += Time.deltaTime;
            timeRemaining -= Time.deltaTime;
        }
        
    }

}
