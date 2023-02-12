using UnityEngine;
using System.Collections;

namespace GOAP {
    public class JumpThroughWindow : Action {
        public JumpThroughWindow(string name, int cost, StateDrivenBrain brain, StateDrivenBrain.GOAPStates moveToState) : base(name, cost, brain, moveToState) { }
        public override ActionStates Initialise() {
            Debug.Log("Start Action : Jump Through Window");
            brain.GetComponent<Animator>().SetTrigger("JumpThroughWindow");
            // Orientate the agent to face window
            brain.transform.rotation = interactObject.transform.rotation;
            // Disable to avoid it colliding with the window
            brain.GetComponent<CharacterController>().enabled = false;
            brain.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            // The action will terminate when the animation ends.
            // This bing managed by the Animation State.
            return ActionStates.Running;
        }
        public override void CleanUp() {
            brain.GetComponent<CharacterController>().enabled = true;
            Debug.Log("End Action : Jump Through Window");
        }
    }
}