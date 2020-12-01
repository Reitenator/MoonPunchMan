using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchBox : MonoBehaviour {

    public GameObject smashEffect;
    public GameController gameController;
    public PlayerSounds playerSounds;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy") && !other.isTrigger) {
            playerSounds.Smack();
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
