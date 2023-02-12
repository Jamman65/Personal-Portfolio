using UnityEngine;
using System.Collections;


namespace Scheduler {


    public class GoToDestinationAction:Action {
        

        public GoToDestinationAction() { }

        public GoToDestinationAction(int weight, float minDuration, float maxDuration, string destinationTag)
            : base(weight, minDuration, maxDuration, destinationTag) {   
        }

        public override void Initialise(GameObject agent) {
            base.Initialise(agent);
            animator.applyRootMotion = false;
            //Debug.Log("Initialise GoToDestinationAction ");
            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(target.position);
            animator.SetFloat("Speed", 1.0f);
        }

        public override ActionStates Update() {
            if (Vector3.Distance(navMeshAgent.destination, navMeshAgent.transform.position) <= 0.05f) {
                status = ActionStates.Success;
            }
            return status;
        }

        public override void Terminate(GameObject agent) {
            navMeshAgent.enabled = false;
           // Debug.Log("Reached Destination");
            animator.SetFloat("Speed", 0f);
            animator.applyRootMotion = true;
            if (target != null) {
                Vector3 lookRotation = Quaternion.LookRotation(target.transform.forward).eulerAngles;
                lookRotation.x = 0;
                lookRotation.z = 0;
                agent.transform.rotation = Quaternion.Euler(lookRotation);
            }
           // Debug.Log("Terminated action ");

        }

        public override void OnTriggerEnter(Collider other) {
           // Debug.Log("Step");
            if (other.tag == Tags.Step) {
                animator.SetBool("StepUp", true);
            }
        }

    }


}