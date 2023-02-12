using UnityEngine;
using System.Collections;

namespace GOAP {
    public class FindPlayer : Action {
        public FindPlayer(string name, int cost, StateDrivenBrain brain, StateDrivenBrain.GOAPStates moveToState) : base(name, cost, brain, moveToState) { }
        public override ActionStates Initialise() {
            Debug.Log("Start Action : FindPlayer");
            return ActionStates.Running;
        }
        public override ActionStates Update() {
            //Instructs the AI to find the player within the scene and returns success if they are close enough to the destination
            if (Vector3.Distance(brain.transform.position, destination.position) < 5f) {
                return ActionStates.Success;
            }
            else {
                return ActionStates.Running;
            }
        }
        public override void CleanUp() {
            Debug.Log("End Action : FindPlayer");
            brain.navmeshAgent.enabled = false;
        }
    }
}