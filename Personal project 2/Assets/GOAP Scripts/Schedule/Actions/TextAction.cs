using UnityEngine;
using System.Collections;

namespace Scheduler {

    public class TextAction : Action {

        public TextAction() { }

        public TextAction(int weight, float minDuration, float maxDuration, string destinationTag)
            : base(weight, minDuration, maxDuration, destinationTag) {
        }

        public override void Initialise(GameObject agent) {
            base.Initialise(agent);
            Debug.Log("Initialise Text Action ");
            animator.SetFloat("Speed", 0f);
            animator.SetBool("Text", true);
        }

    }

}