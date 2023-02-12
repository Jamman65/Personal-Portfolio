using UnityEngine;
using System.Collections;

namespace GOAP {
    public class LocateWindow : Action {
        public LocateWindow(string name, int cost, StateDrivenBrain brain, StateDrivenBrain.GOAPStates moveToState) : base(name, cost, brain, moveToState) { }
        public override ActionStates Initialise() {
            Debug.Log("Start Action : Locate Window");
            return ActionStates.Running;
        }
        public override ActionStates Update() {
            if (Vector3.Distance(brain.transform.position, destination.position) < 0.50f) {
                return ActionStates.Success;
            }
            else {
                return ActionStates.Running;
            }
        }
        public override void CleanUp() {
            Debug.Log("End Action : Locate Window");

        }
    }
}
