using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

namespace Scheduler {

    // Encapsulates the agent's current behaviour
    public abstract class Action {
        public enum ActionStates { Success, Failed, Running }
        protected ActionStates status;
        [XmlAttribute("weight")]
        public int weight;
        [XmlAttribute("destinationTag")]
        public string destinationTag;
        [XmlAttribute("duration")]
        public float duration;
        private float startTime;
        protected int index;
        protected GameObject agent;
        protected Transform target;
        protected UnityEngine.AI.NavMeshAgent navMeshAgent = null;
        protected Animator animator;

        // Constructor invoked from XML
        public Action() {
            this.index = -1;
        }

        // Non XML approach to instantiation
        public Action(int weight, float minDuration, float maxDuration, string destinationTag) {
            this.weight = weight;
            this.destinationTag = destinationTag;
            this.index = -1;
        }

        // Invoked before calls to Update.
        public virtual void Initialise(GameObject agent) {
            startTime = Time.time;
            this.agent = agent;
            this.navMeshAgent = agent.GetComponent<UnityEngine.AI.NavMeshAgent>();
            this.animator = agent.GetComponent<Animator>();
            if (destinationTag != null) {
                target = GameObject.FindGameObjectWithTag(destinationTag).transform;
            }
            else {
                target = null;
            }
            status = ActionStates.Running;
        }

        public virtual void Terminate(GameObject agent) {
           // Debug.Log("Terminated action ");
        }

        public virtual ActionStates Update() {
            if (duration > 0f) {
                if (Time.time > (startTime + duration)) status = ActionStates.Success;
            }
            return status;
        }

        public virtual void OnTriggerEnter(Collider other) {
        }

        public virtual void OnAnimationEnd() {
           // Debug.Log("Animation End");
            status = ActionStates.Success;
        }
    }


}