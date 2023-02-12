using UnityEngine;
using System.Collections;

namespace Scheduler {

    public class DrinkBreakAction : Action {

        public DrinkBreakAction() { }

        public DrinkBreakAction(int weight,  float minDuration, float maxDuration, string destinationTag)
            : base(weight, minDuration, maxDuration, destinationTag) {
        }

        public override void Initialise(GameObject agent) {
            base.Initialise(agent);
            Debug.Log("Initialise DrinkBreakAction ");
            animator.SetFloat("Speed", 0f);
            animator.SetBool("Drink", true);
        }

    }

}