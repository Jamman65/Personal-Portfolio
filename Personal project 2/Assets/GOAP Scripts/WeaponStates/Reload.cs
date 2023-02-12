using UnityEngine;
using System.Collections;

public class Reload<T> : State<T> {
    private WeaponController weaponController;
    public Reload(T stateName, WeaponController controller, float minDuration): base(stateName, controller, minDuration) {
        weaponController = controller;
    }
    public override void OnEnter() {
        base.OnEnter();
        // Override upper body animation
        weaponController.animator.SetLayerWeight(1, 1f);
        // WIP : assumes standing pose.
        weaponController.animator.SetTrigger("StandReload");
        weaponController.MountedWeapon.Reload();  
    }

    public override void OnLeave() {
        base.OnLeave();
        weaponController.animator.SetLayerWeight(1, 0f);
    }


}
