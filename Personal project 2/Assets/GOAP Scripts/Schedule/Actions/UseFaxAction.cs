using UnityEngine;
using System.Collections;

namespace Scheduler {

    public class UseFaxAction : Action {

        public UseFaxAction() { }
        public UseFaxAction(int weight, float minDuration, float maxDuration, string destinationTag)
            : base(weight, minDuration, maxDuration, destinationTag) {
        }

        public override void Initialise(GameObject agent) {
            base.Initialise(agent);
            Debug.Log("Initialise UsePrinterAction ");
            animator.SetBool("UseFax", true);
        }

    }

}