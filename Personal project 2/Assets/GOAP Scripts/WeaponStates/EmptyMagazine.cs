using UnityEngine;
using System.Collections;

public class EmptyMagazine<T> : State<T> {
    public EmptyMagazine(T stateName, WeaponController controller, float minDuration) : base(stateName, controller, minDuration) { }

    public override void OnEnter() {
        base.OnEnter();

    }

    public override void OnLeave() {
        base.OnLeave();
    }
}
