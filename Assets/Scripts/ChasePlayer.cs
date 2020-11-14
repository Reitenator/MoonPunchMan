using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour {

    AnimationToRagdoll animToRagdoll;
    Animator anim;
    [SerializeField] bool chasePlayer;
    float chaseSpeed;
    [SerializeField] float maxChaseSpeed = 0.5f;
    
    Vector3 lookPos;
    [SerializeField] float acceleration = 1;
    [SerializeField] float blend;
    [SerializeField] float rotationSpeed = 1;
    private void Start() {
        anim = GetComponent<Animator>();
        animToRagdoll = GetComponent<AnimationToRagdoll>();
    }

    private void Update() {
        if (chasePlayer) {
            if (blend <= 0.6f) {
                blend += acceleration / 2 * Time.deltaTime;    
            }
            if (chaseSpeed <= maxChaseSpeed) {
                chaseSpeed += acceleration * Time.deltaTime;
            } else {
                chaseSpeed = maxChaseSpeed;
            }
            
            transform.position += lookPos * chaseSpeed * Time.deltaTime;

        } else {
            if (blend >= 0) {
                blend -= acceleration / 2 * Time.deltaTime;
            }
            if (chaseSpeed >= 0) {
                chaseSpeed -= acceleration * Time.deltaTime;
            } if (chaseSpeed != 0) {
                transform.position += lookPos * chaseSpeed * Time.deltaTime;
            }
        }


        anim.SetFloat("Blend", blend);
        
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player") && !animToRagdoll.isRagdoll) {
            chasePlayer = true;
            lookPos = other.transform.position - transform.position;
            lookPos.y = 0;
            if (maxChaseSpeed <= chaseSpeed) chaseSpeed += acceleration * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * rotationSpeed);
            
        } 
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {            
            chasePlayer = false;
            
        }
    }
}
