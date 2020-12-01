using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    MeshRenderer mesh;
    float displacementAmount;
    public GameController gameController;
    Animator anim;
    public AudioClip projectileFire;
    AudioSource audioSource;
    GameObject player;
    private void Start() {
        mesh = GetComponent<MeshRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Awake() {
        Spawn();
    }


    private void Spawn() {
        displacementAmount = -1;
    }

    // Update is called once per frame
    void Update() {
        displacementAmount = Mathf.Lerp(displacementAmount, 0.1f, Time.deltaTime);
        mesh.material.SetFloat("_Amount", displacementAmount);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PlayerMovement>().currentState != PlayerState.dead) {
            Debug.Log("Player within");
            gameController.canHitCanvas.gameObject.SetActive(true);
          

        } 
    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PlayerMovement>().currentState != PlayerState.dead) {
            Debug.Log("Player out");
            gameController.canHitCanvas.gameObject.SetActive(false);
            player.GetComponent<PlayerMovement>().canPowerPunch = false;
        }
    }

    IEnumerator ShootFireball() {
        
        player.GetComponent<PlayerMovement>().currentState = PlayerState.powerPunching;
        yield return new WaitForSeconds(0.6f);
        audioSource.PlayOneShot(projectileFire);
        player.GetComponent<PlayerMovement>().currentState = PlayerState.moving;
        anim.SetTrigger("ShootForMoon");
        gameController.powerBar.value = 0;
        gameController.powerBarFill.color = Color.green;
        //gameController.spawnTime -= 0.5f;
        //gameController.currentTime = gameController.spawnTime;
        yield return new WaitForSeconds(2.9f);
        gameController.MoonHit();
        
        gameController.KillFireball();
    }

    

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PlayerMovement>().currentState != PlayerState.dead) {
            
            player.GetComponent<PlayerMovement>().canPowerPunch = true;
            if (Input.GetButton("Fire2")) {
                StartCoroutine(ShootFireball());
                
            }


        }
    }




}
