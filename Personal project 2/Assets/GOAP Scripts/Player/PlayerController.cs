using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private HealthManager healthManager;


    void Start()
    {
        animator = GetComponent<Animator>();
        healthManager = GetComponent<HealthManager>();
    }

    void Update()
    {
        if (healthManager.IsDead())
        {
            //  healthManager.Die();
            animator.SetBool("Die", true);
            // Detach camera from dead player avatar
            Camera.main.GetComponent<ThirdPersonCamera>().target = null;
            GetComponent<FootstepManager>().enabled = false;
            GetComponent<AudioSource>().enabled = false;
            enabled = false;
            return;
        }

        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        //animator.SetFloat("Speed", v);
        //animator.SetFloat("Direction", h);

        //if (Input.GetButtonDown("ToggleCrouch")) {
        //    animator.SetBool("ToggleCrouch", true);
        //}
        //else if (Input.GetButtonDown("ToggleProne")) {
        //    animator.SetBool("ToggleProne", true);
        //}
        //else if (Input.GetButtonDown("Dive")) {
        //    animator.SetTrigger("Dive");
        //}


        //    //animator.SetBool("Run", Input.GetButton("Run"));
        //    //animator.SetFloat("Rotation", (Input.GetAxis("Mouse X") ));
        //    //animator.SetBool("Throw", Input.GetButton("Throw"));

        //    // The aim has just been activated so set the parameter that plays the rest to aim animation
        //    if (!animator.GetBool("Aim") && Input.GetButton("Fire2")) {
        //        animator.SetTrigger("ActivateAim");
        //    }
        //    animator.SetBool("Aim", Input.GetButton("Fire2"));
        //}

    }
}
