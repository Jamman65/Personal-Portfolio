using UnityEngine;
using System.Collections;

namespace Scheduler {

    public class SitAction : Action{

        public SitAction() { }
        public SitAction(int weight, float minDuration, float maxDuration, string destinationTag)
            : base(weight, minDuration, maxDuration, destinationTag) {
        }

        public override void Initialise(GameObject agent) {
            base.Initialise(agent);
            Debug.Log("Initialise SitAction ");
            animator.SetBool("Sit", true);
        }

        public override void Terminate(GameObject agent) {
            Debug.Log("Terminated sit action ");
            animator.SetBool("Sit", false);
        }

}

}