using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    MeshRenderer mesh;
    float displacementAmount;
    public GameController gameController;
    Animator anim;

    private void Start() {
        mesh = GetComponent<MeshRenderer>();
        anim = GetComponent<Animator>();    
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

        }
    }

    IEnumerator ShootFireball() {
        anim.SetTrigger("ShootForMoon");
        gameController.powerBar.value = 0;
        gameController.powerBarFill.color = Color.green;
        yield return new WaitForSeconds(2.9f);
        gameController.MoonHit();
        
        gameController.KillFireball();
    }
    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PlayerMovement>().currentState != PlayerState.dead) {
            if(Input.GetButtonDown("Fire1")) {
                StartCoroutine(ShootFireball());
            }


        }
    }




}
