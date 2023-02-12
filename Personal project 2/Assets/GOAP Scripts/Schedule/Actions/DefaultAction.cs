using UnityEngine;
using System.Collections;

namespace Scheduler {

    public class DefaultAction : Action {

        public DefaultAction() {
        }

        public override void Initialise(GameObject agent) {
            base.Initialise(agent);
           // Debug.Log("Initialise DefaultAction");
            status = ActionStates.Success;
        }

    }
}
