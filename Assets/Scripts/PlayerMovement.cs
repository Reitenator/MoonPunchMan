using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //CharacterController controller;
    Rigidbody rb;
    Vector3 moveDir;

    public float walkSpeed = 5;
    public float runSpeed = 12;
    public Transform cam;
    public float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;
    private float currentSpeed;
    Vector3 actualMoveDir;

    private void Start() {
        //controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();

    }

    private void Update() {
        MovementInput();
        
    }

    private void FixedUpdate() {
        if (moveDir.magnitude >= 0.1f) {
            rb.MovePosition(transform.position + actualMoveDir * currentSpeed * Time.fixedDeltaTime);
        }
        
    }

    private void MovementInput () {
        currentSpeed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) {
            currentSpeed = runSpeed;
        }

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

    }



}
