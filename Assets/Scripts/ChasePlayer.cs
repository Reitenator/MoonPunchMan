using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour {

    AnimationToRagdoll animToRagdoll;
    Animator anim;
    Rigidbody rb;

    public bool chasePlayer;
    float chaseSpeed;
    [SerializeField] float maxChaseSpeed = 0.5f;
    
    Vector3 lookPos;
    [SerializeField] float acceleration = 1;
    [SerializeField] float blend;
    [SerializeField] float rotationSpeed = 1;
    bool statusIsHappy = false;

    GameObject player;


    private void Start() {
        anim = GetComponent<Animator>();
        animToRagdoll = GetComponent<AnimationToRagdoll>();
        rb = GetComponent<Rigidbody>();
        chasePlayer = true;
    }
    

    private void Update() {
        //if (other.gameObject.CompareTag("Player") && !animToRagdoll.isRagdoll) {
        //    chasePlayer = true;
        //    lookPos = other.transform.position - transform.position;
        //    lookPos.y = 0;
        //    if (maxChaseSpeed <= chaseSpeed) chaseSpeed += acceleration * Time.deltaTime;
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * rotationSpeed);


        //}

        GameObject player = GameObject.FindWithTag("Player");
        


        if (!animToRagdoll.isRagdoll && player.GetComponent<PlayerMovement>().currentState != PlayerState.dead) {
            Transform other = GameObject.FindWithTag("Player").transform;
            lookPos = other.transform.position - transform.position;
            lookPos.y = 0;
            lookPos = lookPos.normalized;
            if (maxChaseSpeed <= chaseSpeed) chaseSpeed += acceleration * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * rotationSpeed);
            if (blend <= 0.6f) {
                blend += acceleration / 2 * Time.deltaTime;    
            }
            if (chaseSpeed <= maxChaseSpeed) {
                chaseSpeed += acceleration * Time.deltaTime;
            } else {
                chaseSpeed = maxChaseSpeed;
            }

            //transform.position += lookPos * chaseSpeed * Time.deltaTime;
            rb.MovePosition(transform.position + lookPos * chaseSpeed * Time.deltaTime);

        } else {
            blend = 0;
            if (!statusIsHappy) {
                statusIsHappy = true;
                anim.SetTrigger("happy");
            }

        }
        //else {
        //    if (blend >= 0) {
        //        blend -= acceleration / 2 * Time.deltaTime;
        //    }
        //    if (chaseSpeed >= 0) {
        //        chaseSpeed -= acceleration * Time.deltaTime;
        //    } if (chaseSpeed != 0) {
        //        transform.position += lookPos * chaseSpeed * Time.deltaTime;
        //    }
        //}


        anim.SetFloat("Blend", blend);
        
    }

    //private void OnTriggerStay(Collider other) {
    //    //if (other.gameObject.CompareTag("Player") && !animToRagdoll.isRagdoll) {
    //    //    chasePlayer = true;
    //    //    lookPos = other.transform.position - transform.position;
    //    //    lookPos.y = 0;
    //    //    if (maxChaseSpeed <= chaseSpeed) chaseSpeed += acceleration * Time.deltaTime;
    //    //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * rotationSpeed);

    //    //} 
    //}

    //private void OnTriggerExit(Collider other) {
    //    if (other.gameObject.CompareTag("Player")) {            
    //        chasePlayer = false;

    //    }
    //}

    private void OnCollisionEnter(Collision other) {
        if (other.collider.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PlayerMovement>().currentState != PlayerState.attacking) {
            Vector3 forceDir = other.transform.position - transform.position;
            GameObject gameController = GameObject.FindWithTag("GameController").gameObject;
            gameController.GetComponent<GameController>().LoseLife();
            other.gameObject.GetComponent<Rigidbody>().AddForce(forceDir * 900 + Vector3.up *100);
        }
    }
}
