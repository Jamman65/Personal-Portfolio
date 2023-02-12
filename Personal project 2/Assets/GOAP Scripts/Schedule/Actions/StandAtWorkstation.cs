using UnityEngine;
using System.Collections;

namespace Scheduler {

    public class StandAtWorkstationAction : Action {

        public StandAtWorkstationAction() { }
        public StandAtWorkstationAction(int weight, float minDuration, float maxDuration, string destinationTag)
            : base(weight, minDuration, maxDuration, destinationTag) {
        }

        public override void Initialise(GameObject agent) {
            base.Initialise(agent);
            //Debug.Log("Initialise Stand At Workstation Action ");
            animator.SetBool("Sit", false);
        }



        public override void Terminate(GameObject agent) {
            //Debug.Log("Terminated Stand At Workstation Action ");
        }

    }

}