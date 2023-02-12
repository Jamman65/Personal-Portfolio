using UnityEngine;
using System.Collections;

namespace Scheduler {

    public class SitAtWorkstationAction : Action {

        public SitAtWorkstationAction() { }
        public SitAtWorkstationAction(int weight, float minDuration, float maxDuration, string destinationTag)
            : base(weight, minDuration, maxDuration, destinationTag) {
        }

        public override void Initialise(GameObject agent) {
            base.Initialise(agent);
           // Debug.Log("Initialise Sit At Workstation Action ");
            animator.SetBool("Sit", true);
        }



        public override void Terminate(GameObject agent) {
            //Debug.Log("Terminated Sit At Workstation Action ");
        }

    }

}