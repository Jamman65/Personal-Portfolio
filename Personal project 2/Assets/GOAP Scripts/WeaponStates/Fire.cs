using UnityEngine;
using System.Collections;

public class Fire<T> : State<T> {
    private WeaponController weaponController;
    public Fire(T stateName, WeaponController controller, float minDuration): base(stateName, controller, minDuration) {
        weaponController = controller;
    }

    public override void OnEnter() {
        base.OnEnter();
        weaponController.animator.SetBool("Fire", true);
        weaponController.MountedWeapon.Fire();
    }

    public override void OnLeave() {
        base.OnLeave();
        weaponController.animator.SetBool("Fire", false);
    }
}

