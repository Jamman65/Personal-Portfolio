using UnityEngine;
using System.Collections;

namespace Scheduler {

    public class PickUpItemAction : Action {
        public PickUpItemAction() { }
        public PickUpItemAction(int weight, float minDuration, float maxDuration, string destinationTag)
            : base(weight, minDuration, maxDuration, destinationTag) {
        }

        public override void Initialise(GameObject agent) {
            base.Initialise(agent);
            Debug.Log("Initialise PickUpItemAction ");
            animator.SetBool("PickUp", true);
        }

    }

}