using UnityEngine;
using System.Collections;

public class AIGroupSoldier : MonoBehaviour {

    public Animator animator;


    void Awake() {
        animator = GetComponent<Animator>();
        SetAnimatorController("Controllers/DismountChopper");
    }


    public void SetAnimatorController(string animatorController) {
        animator.runtimeAnimatorController = Instantiate(Resources.Load(animatorController) as RuntimeAnimatorController);
    }

}
