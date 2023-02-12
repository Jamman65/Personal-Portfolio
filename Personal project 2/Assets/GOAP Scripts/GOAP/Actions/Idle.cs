using UnityEngine;
using System.Collections;


namespace GOAP {
    public class Idle: Action{
   public Idle(string name, int cost, StateDrivenBrain brain, StateDrivenBrain.GOAPStates moveToState) : base(name, cost, brain, moveToState) { }
        //This instructs the AI to play an idle animation when the plan is complete
        public override ActionStates Initialise() {
            Debug.Log("Start Action : Idle");
            Debug.Log("Goal Complete");
            Debug.Log("Plan Complete");
            brain.enemyanim.anim.SetFloat("Vertical", 0, 0.01f, Time.deltaTime);
            return ActionStates.Running;
        }
        public override ActionStates Update() {
            // Action still running
            
            return ActionStates.Running;
        }
        public override void CleanUp() {
            Debug.Log("End Action : Idle");
            
        }
    }
}
