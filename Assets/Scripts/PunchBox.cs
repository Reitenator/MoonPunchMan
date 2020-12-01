using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchBox : MonoBehaviour {

    public GameObject smashEffect;
    public GameController gameController;


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy") && !other.isTrigger) {
            AnimationToRagdoll enemy = other.GetComponent<AnimationToRagdoll>();
            enemy.ToggleRagdoll(false);
            enemy.Knockback(transform);
            enemy.RunCoroutine();
            gameController.EnemyPunched();
            
            GameObject firework = Instantiate(smashEffect, transform.position, Quaternion.identity);
            firework.GetComponent<ParticleSystem>().Play();
        } 
        
    }
}
