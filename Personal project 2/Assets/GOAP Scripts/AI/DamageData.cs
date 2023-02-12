using UnityEngine;
using System.Collections;


public class DamageData{
    public GameObject opponent;
    public Vector3 impactPoint;
    public Vector3 impactDirection;
    public float impactForce;
    public DamageData(GameObject opponent,
                      Vector3 impactPoint,
                      Vector3 impactDirection,
                      float impactForce){
        this.opponent = opponent;
        this.impactPoint = impactPoint;
        this.impactDirection = impactDirection;
        this.impactForce = impactForce;
    }
    public Vector3 GetImpactDirection(){
        return (impactPoint - opponent.transform.position);
    }
}
