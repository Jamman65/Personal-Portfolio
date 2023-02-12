using UnityEngine;
using System.Collections;

public class DryFire<T> : State<T> {
    private WeaponController weaponController;
    public DryFire(T stateName, WeaponController controller, float minDuration): base(stateName, controller, minDuration) {
        weaponController = controller;
    }

    public override void OnEnter() {
        base.OnEnter();
        AudioSource audio = weaponController.MountedWeapon.GetComponent<AudioSource>();
        audio.PlayOneShot(weaponController.MountedWeapon.dryFireSound, 0.75f);
    }

    public override void OnLeave() {
        base.OnLeave();
    }
}
