using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public Transform forceField;
    public CountDownTimer countDownTimer;
    public GameObject projectileObject;
    public GameObject enemy;
    public GameObject player;
    public Canvas canHitCanvas;
    public Canvas gameOverCanvas;
    

    public Image life1;
    public Image life2;
    public Image life3;

    public Slider powerBar;
    public Image powerBarFill;
    public List<Image> lives;

    float killCountValue;
    public TMP_Text killCount;
    public TMP_Text theScore;
    public bool fireBallIsSpawned;
    public float powerAdder;
    public bool canLoseLife;
    public PlayerSounds playerSounds;




    float currentHighscore;

    int[] totEnemiesLevelAarray;
    float[] enemySpawnTimeLevelAarray;
    

    public float spawnTime;
    public float currentTime;
    [SerializeField] float slidervalue;

    private void Start() {
        Cursor.visible = false;
        spawnTime = 1.6f;
        currentTime = spawnTime;
        totEnemiesLevelAarray = new int[] {30,40,50,60,70};
        enemySpawnTimeLevelAarray = new float[] { 5, 5, 4, 4, 3 };
        powerBar.value = 0;
        powerAdder = 10;
        fireBallIsSpawned = false;
        canLoseLife = true;
        ZPlayerPrefs.Initialize("welcome", "toTheJungle");

        currentHighscore = ZPlayerPrefs.GetFloat("Highscore");
        
        

    }
    // Update is called once per frame
    void Update() {
        if (powerBar.value >= 100) {
            powerBar.value = 100;
            powerBarFill.color = Color.yellow;
            SpawnFireball();
        }


       
            currentTime -= Time.deltaTime;

        if (currentTime <= 0 && player.GetComponent<PlayerMovement>().currentState != PlayerState.dead) {
            currentTime = spawnTime;
            Spawn();
        } else if (currentTime <= 0) {
            player.GetComponent<PlayerMovement>().currentState = PlayerState.dead;
        }
        if (player.GetComponent<PlayerMovement>().currentState == PlayerState.dead) {
            if (killCountValue > currentHighscore) {
                ZPlayerPrefs.SetFloat("Highscore", killCountValue);
                ZPlayerPrefs.Save();
            }
            theScore.text = killCountValue.ToString();
            gameOverCanvas.gameObject.SetActive(true);
            StartCoroutine(GoToMainMenuScene());
            if (Input.GetKeyDown(KeyCode.Return)) {
                SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
            }
        }



    }
    IEnumerator GoToMainMenuScene() {
        yield return new WaitForSeconds(10);
        if (SceneManager.GetActiveScene().name != "MenuScene") SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);

    }


    public void EnemyPunched() {
        killCountValue++;
        killCount.text = killCountValue.ToString();
        powerBar.value += powerAdder;
        
    }


    private Vector3 RandomPointOnCircleEdge(float radius) {
        Vector2 randomPoint = Random.insideUnitCircle.normalized * radius;
        return new Vector3(randomPoint.x, 3, randomPoint.y);
    }

    void Spawn() {
        Instantiate(enemy, RandomPointOnCircleEdge(47), Quaternion.identity);
    }

    public void MoonHit() {
        countDownTimer.timeRemaining += 30;
        fireBallIsSpawned = false;
    }

    void SpawnFireball() {
        if (!fireBallIsSpawned) {
            fireBallIsSpawned = true;
            projectileObject.SetActive(true);
        }

    }
    public void KillFireball() {
        projectileObject.SetActive(false);
    }

    IEnumerator HurtTime() {
        canLoseLife = false;
        yield return new WaitForSeconds(1);
        canLoseLife = true;
    }

    public void LoseLife() {
        if (lives.Count > 0 && canLoseLife) {
            StartCoroutine(HurtTime());
            playerSounds.Oof();
            lives.RemoveAt(lives.Count - 1);
            
        }
        if (lives.Count == 2) {
            life3.enabled = false;
        }
        if (lives.Count == 1) {
            life2.enabled = false;
        }
        if (lives.Count <= 0 ) {
            life1.enabled = false;
            player.GetComponent<PlayerMovement>().currentState = PlayerState.dead;
            player.GetComponent<PlayerMovement>().anim.SetBool("Dead", true);
        }
    }



}
