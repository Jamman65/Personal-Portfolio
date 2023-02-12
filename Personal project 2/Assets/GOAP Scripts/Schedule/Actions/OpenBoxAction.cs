using UnityEngine;
using System.Collections;

namespace Scheduler {
    public class OpenBoxAction : Action {
        public OpenBoxAction() { }
        public OpenBoxAction(int weight, float minDuration, float maxDuration, string destinationTag)
            : base(weight, minDuration, maxDuration, destinationTag) {
        }

        public override void Initialise(GameObject agent) {
            base.Initialise(agent);
            Debug.Log("Initialise OpenBoxAction ");
            animator.SetBool("OpenBox", true);
        }

    }

}