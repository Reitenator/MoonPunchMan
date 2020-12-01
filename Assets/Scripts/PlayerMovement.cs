using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
    moving,
    attacking,
    jumping,
    dead
}

public class PlayerMovement : MonoBehaviour {

    //CharacterController controller;
    private Rigidbody rb;
    Vector3 moveDir;

    public PlayerState currentState;

    public float walkSpeed = 5;
    public float runSpeed = 12;
    public Transform cam;
    public float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;
    [SerializeField] float currentSpeed;
    [SerializeField] float speed;
    [SerializeField] bool canPunch;
    [SerializeField] bool canDash;

    public Transform movementBounds;


    Vector3 actualMoveDir;

    public Animator anim;
    public Animator punchBoxAnim;

    private void Start() {
        canPunch = true;
        rb = GetComponent<Rigidbody>();
        currentState = PlayerState.moving;

    }

    private void FixedUpdate() {
        MovementInput();
        
    }
    private void Update() {
        if (Input.GetButton("Fire1") && canPunch) {
            StartCoroutine(Punch());
            
        }
    }


    private IEnumerator Punch() {
        canPunch = false;
        anim.SetTrigger("Punching");
        punchBoxAnim.SetTrigger("boxPunch");
        yield return new WaitForSeconds(0.3f);
        canPunch = true;
        canDash = true;
    }

    //void PunchAway(GameObject otherObject) {
    //    if (otherObject.CompareTag("Enemy") && !otherObject.GetComponent<Collider>().isTrigger) ;

    //}
    private void LimitBounds() {
        float radius = 49;
        Vector3 centerPosition = movementBounds.transform.localPosition;

        float distance = Vector3.Distance(transform.position, centerPosition);
        if (distance > radius) {
            Vector3 fromOriginToObject = transform.position - centerPosition;
            fromOriginToObject *= radius / distance;
            transform.position = centerPosition + fromOriginToObject;
        }

    }
    

    private void MovementInput () {

        currentSpeed = walkSpeed;
       

        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        moveDir = new Vector3(horizontal, 0f, vertical).normalized;



        if (moveDir.magnitude >= 0.1f && currentState != PlayerState.dead) {
            LimitBounds();
            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
            actualMoveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            
            //controller.Move(actualMoveDir.normalized * currentSpeed * Time.deltaTime);
        }
        float dashPower = 0;
        if (moveDir.magnitude >= 0.1f && currentState != PlayerState.dead) {
            anim.SetFloat("speed", 0.5f);
            dashPower = 10;
            if (Input.GetAxisRaw("run") > 0.1 || Input.GetKey(KeyCode.LeftShift)) {
                dashPower = 20;
                anim.SetFloat("speed", 1f);
                currentSpeed = runSpeed;

            }
            if (Input.GetButton("Dash") && currentState != PlayerState.attacking && canDash) {
                StartCoroutine(DashAttack(dashPower));
            }
            rb.MovePosition(transform.position + actualMoveDir * currentSpeed * Time.fixedDeltaTime);
        } else {
            anim.SetFloat("speed", 0.0f);
        }



    }

    IEnumerator DashAttack(float dashPower) {
        canDash = false;
        currentState = PlayerState.attacking;
        anim.SetBool("Dashing", true);
        punchBoxAnim.SetTrigger("boxDash");
        rb.AddRelativeForce(0, 0, dashPower, ForceMode.Impulse);
        yield return new WaitForSeconds(0.3f);
        currentState = PlayerState.moving;
        anim.SetBool("Dashing", false);
        yield return new WaitForSeconds(1);
        canDash = true;
    }


}
