using UnityEngine;
using System.Collections;

namespace GOAP {
    public class GetWeapon : Action {
        public GetWeapon(string name, int cost, StateDrivenBrain brain, StateDrivenBrain.GOAPStates moveToState) : base(name, cost, brain, moveToState) { }
        public override ActionStates Initialise() {
            Debug.Log("Start Action : Get Weapon");
            return ActionStates.Running;
        }
        public override ActionStates Update() {
            //This instructs the AI to find the weapon within the scene and returns success when the weapon is found 
            if (Vector3.Distance(brain.transform.position, destination.position) < 1.5f) {
                return ActionStates.Success;
            }
            else {
                return ActionStates.Running;
            }
        }
        //This equips the weapon that the AI has found into the AIs hand
        public override void CleanUp() {
            Debug.Log("End Action : Get Weapon");
            brain.weaponslot.rightHandWeapon = brain.Sword;
            brain.weapon.SetActive(true);
            brain.RightHandWeapon.SetActive(true);
            //brain.aiStateWeaponController.weaponObject = destination.gameObject;
            //brain.aiStateWeaponController.MountWeapon();
            //     Object.Destroy(destination.gameObject);
            destination.gameObject.SetActive(false);
            
        }
    }
}