using UnityEngine;
using System.Collections;

public class IKWeaponGrip : MonoBehaviour {
    private Animator animator;
    private WeaponController weaponController;

    void Awake() {
        animator = GetComponent<Animator>();
        weaponController = GetComponent<WeaponController>();
        
    }

    void OnAnimatorIK(int layerIndex) {
        if (weaponController.HasMountedWeapon) {
            Transform leftHandTrans = weaponController.MountedWeapon.IKHandPosition;
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTrans.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTrans.rotation);
        }
        else {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);   
        }
    }

}
