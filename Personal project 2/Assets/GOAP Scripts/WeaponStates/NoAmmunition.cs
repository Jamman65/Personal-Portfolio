using UnityEngine;
using System.Collections;

public class NoAmmunition<T> : State<T> {
    public NoAmmunition(T stateName, WeaponController controller, float minDuration) : base(stateName,controller, minDuration) { }

    public override void OnEnter() {
        base.OnEnter();
    }

    public override void OnLeave() {
        base.OnLeave();
    }
}