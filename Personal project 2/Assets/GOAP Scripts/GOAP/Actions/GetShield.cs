using UnityEngine;
using System.Collections;

namespace GOAP {
    public class GetShield : Action {
        public GetShield(string name, int cost, StateDrivenBrain brain, StateDrivenBrain.GOAPStates moveToState) : base(name, cost, brain, moveToState) { }
        public override ActionStates Initialise() {
            Debug.Log("Start Action : Get Shield");
            return ActionStates.Running;
        }
        //This instructs the AI to find the shield within the scene and returns success when the shield is found 
        public override ActionStates Update() {
            if (Vector3.Distance(brain.transform.position, destination.position) < 1.5f) {
                CleanUp();
                return ActionStates.Success;
                
            }

           
            else {
                return ActionStates.Running;
            }
        }
        //This Equips the shield into the AIs hand
        public override void CleanUp() {
            Debug.Log("End Action : Get Shield");
            brain.weaponslot.leftHandWeapon = brain.Shield;
            brain.ShieldWeapon.SetActive(true);
            destination.gameObject.SetActive(false);
        }
    }
}