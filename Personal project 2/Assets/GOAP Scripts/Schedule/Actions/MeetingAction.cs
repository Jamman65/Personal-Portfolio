using UnityEngine;
using System.Collections;

namespace Scheduler {
    public class MeetingAction : Action {
        public MeetingAction(int weight, float minDuration, float maxDuration, string destinationTag)
            : base(weight, minDuration, maxDuration, destinationTag) {
        }

        public MeetingAction() { }

        public override void Initialise(GameObject agent) {
            base.Initialise(agent);
            Debug.Log("Initialise MeetingAction ");
            animator.SetBool("Meeting", true);
        }
    }

}