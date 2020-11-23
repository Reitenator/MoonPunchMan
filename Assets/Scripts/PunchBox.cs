using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchBox : MonoBehaviour {

    

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy") && !other.isTrigger) {
            AnimationToRagdoll enemy = other.GetComponent<AnimationToRagdoll>();
            enemy.ToggleRagdoll(false);
            enemy.Knockback(transform);
            enemy.RunCoroutine();
        } 
        
    }
}
