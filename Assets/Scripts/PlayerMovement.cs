using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
    walking,
    attacking,
    jumping
}

public class PlayerMovement : MonoBehaviour {

    //CharacterController controller;
    private Rigidbody rb;
    Vector3 moveDir;

    [SerializeField] PlayerState currentState;

    public float walkSpeed = 5;
    public float runSpeed = 12;
    public Transform cam;
    public float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;
    [SerializeField] float currentSpeed;
    [SerializeField] float speed;
    [SerializeField] bool canPunch;             


    Vector3 actualMoveDir;

    public Animator anim;
    public Animator punchBoxAnim;

    private void Start() {
        canPunch = true;
        rb = GetComponent<Rigidbody>();


    }

    private void FixedUpdate() {
        MovementInput();
        
    }
    private void Update() {
        if (Input.GetButtonDown("Fire1") && canPunch) {
            StartCoroutine(Punch());
            
        }
    }


    private IEnumerator Punch() {
        canPunch = false;
        anim.SetTrigger("Punching");
        punchBoxAnim.SetTrigger("boxPunch");
        yield return new WaitForSeconds(0.3f);
        canPunch = true;
    }

    //void PunchAway(GameObject otherObject) {
    //    if (otherObject.CompareTag("Enemy") && !otherObject.GetComponent<Collider>().isTrigger) ;

    //}

    

    private void MovementInput () {

        currentSpeed = walkSpeed;
       

        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        moveDir = new Vector3(horizontal, 0f, vertical).normalized;



        if (moveDir.magnitude >= 0.1f) {

            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
            actualMoveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            
            //controller.Move(actualMoveDir.normalized * currentSpeed * Time.deltaTime);
        }
        
        if (moveDir.magnitude >= 0.1f) {
            anim.SetFloat("speed", 0.5f);
            if (Input.GetAxisRaw("run") > 0.1) {
                anim.SetFloat("speed", 1f);
                currentSpeed = runSpeed;
            }
            rb.MovePosition(transform.position + actualMoveDir * currentSpeed * Time.fixedDeltaTime);
        } else {
            anim.SetFloat("speed", 0.0f);
        }



    }



}
