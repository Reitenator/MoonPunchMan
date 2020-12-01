using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour{

    public Animator anim;
    public TMP_Text higScoreText; 

    private void Awake() {
        Cursor.visible = true;
        ZPlayerPrefs.Initialize("welcome", "toTheJungle");
        float highscore = ZPlayerPrefs.GetFloat("Highscore");
        higScoreText.text = highscore.ToString();

    }

    public void PlayButtonClicked() {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    public void ViewHighscore() {    
        anim.SetTrigger("ToHighscore");
    }

    public void ViewMainMenu() {
        anim.SetTrigger("ToMain");
    }

    public void ViewControls() {
        anim.SetTrigger("ToControls");
    }

    public void ViewHowToPlay() {
        anim.SetTrigger("ToHowToPlay");
    }

    public void ExitGame() {
        Debug.Log("Exiting game..");
        Application.Quit();
    }
  
}
