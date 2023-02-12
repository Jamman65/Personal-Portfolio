using UnityEngine;
using System.Collections;


namespace Scheduler {
    public class TalkAction : Action {
        public TalkAction(int weight, float minDuration, float maxDuration, string destinationTag)
            : base(weight, minDuration, maxDuration, destinationTag) {
        }

        public TalkAction() { }

        public override void Initialise(GameObject agent) {
            base.Initialise(agent);
            Debug.Log("Initialise Talk Action ");
            animator.SetBool("Talk", true);
        }
    }

}