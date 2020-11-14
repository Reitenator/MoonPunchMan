using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToRagdoll : MonoBehaviour {
    private Collider col;
    private Rigidbody rb;
    [SerializeField] float respawnTime = 10f;
    [SerializeField] float forceAmount = 100;
    [SerializeField] float upwardsForce = 200;
    Rigidbody[] rigidbodies;
    public bool isRagdoll = false;

    void Start() {
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        ToggleRagdoll(true);
        
    }

 

    private void OnCollisionEnter(Collision other) {
        
        
        if (!isRagdoll && other.gameObject.CompareTag("Player")) {
            col.gameObject.transform.parent = transform;
            ToggleRagdoll(false);
            Knockback(other.transform);
            StartCoroutine(GetBackUp());
        }
    }

    private void Knockback(Transform other) {
        foreach (Rigidbody rbBone in rigidbodies) {
            Vector3 forceDir = transform.position - other.position;
            
            rbBone.AddForce( (forceDir * forceAmount + Vector3.up * upwardsForce));
        }
    }

    private void ToggleRagdoll(bool isAnimating) {
        isRagdoll = !isAnimating;
        
        col.enabled = isAnimating;
        
        foreach (Rigidbody ragdollBone in rigidbodies) {
            ragdollBone.isKinematic = isAnimating;
        }
        
        GetComponent<Animator>().enabled = isAnimating;
        if(isAnimating) {
            RandomAnimation();
        }

    }

    private IEnumerator GetBackUp() {
        yield return new WaitForSeconds(respawnTime);
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        Destroy(gameObject);
        //ToggleRagdoll(true);
    }

    void RandomAnimation() {
        //int randomNum = Random.Range(0, 2);

        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("angry");
        //if (randomNum == 0) {
        //    animator.SetTrigger("dead");
        //} else {
        //    animator.SetTrigger("angry");
        //}

    }
}
