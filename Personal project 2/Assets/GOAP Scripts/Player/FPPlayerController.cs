using UnityEngine;
using System.Collections;

public class FPPlayerController : MonoBehaviour {
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;
    public float runSpeed = 3f;
    public float walkSpeed = 1.5f;
    public float walkBackwardsSpeed = 1f;
    public float strafeSpeed = 1f;
    private const float GRAVITY = -9.81f;
    private Animator animator;





	void Start () {
        characterController = GetComponent<CharacterController>();
        animator = transform.Find("Camera/FPS_m16_01").GetComponent<Animator>();
	}
	

	void Update () {
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        //bool run = Input.GetButton("Run");
       // float verticalSpeed = GetVerticalSpeed(v, run);
       // float horizontalSpeed = GetHorizontalSpeed(h);

       // moveDirection = new Vector3(h, 0.0f, v);
        //moveDirection.x *= horizontalSpeed;
        //moveDirection.z *= verticalSpeed;

        // Transforms direction from local space to world space.
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = ApplyGravity(moveDirection);
        characterController.Move(moveDirection * Time.deltaTime);

       // animator.SetFloat("Speed", v);
       // animator.SetFloat("Direction", h);
        //animator.SetBool("Run", Input.GetButton("Run"));
       // animator.SetBool("Throw", Input.GetButton("Throw"));
        //animator.SetBool("Aim", Input.GetButton("Fire2"));

	}

    float GetVerticalSpeed(float vertical, bool run) {
        // Moving
        if (vertical != 0f) {
            // Moving forward
            if (vertical > 0f) {
                if (run) return runSpeed;
                else return walkSpeed;
            }
            // Moving backwards
            else {
                return walkBackwardsSpeed;
            }
        }
        else {
            return 0f;
        }
    }

    float GetHorizontalSpeed(float horizontal) {
        // Moving
        if (horizontal != 0f) {
            return strafeSpeed;
        }
        else {
            return 0;
        }
    }

    Vector3 ApplyGravity(Vector3 direction) {
        // Only apply gravity if not grounded
        if (characterController.isGrounded) {
            return direction;
        }
        else {
            // Force player down
            direction.y += (GRAVITY * Time.deltaTime);
            return direction;
        }
    }


}
