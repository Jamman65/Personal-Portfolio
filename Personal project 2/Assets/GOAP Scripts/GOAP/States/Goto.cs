using UnityEngine;
using System.Collections;
using JO;

    // Manages an Action that moves the agent to a location specified by the Action's destination transform.
    public class Destination<T> : AIState<T> {
    
        public Destination(T stateName, StateDrivenBrain controller, float minDuration) : base(stateName, controller, minDuration) { }
        public override void OnEnter() {
            base.OnEnter();
        // Use NavMesh to navigate to destination
            
            brain.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            brain.navmeshAgent.SetDestination(brain.currentAction.destination.position);
            brain.enemyanim.anim.SetFloat("Vertical", 2, 0.1f, Time.deltaTime);
            //brain.animator.SetFloat("Speed", 2f);
            brain.animator.applyRootMotion = false;
            // Sets destination for action
            actionStatus = brain.currentAction.Initialise();
        }
        public override void Act() {
            // Exit state when destination reached
            actionStatus = brain.currentAction.Update();
            if (actionStatus != GOAP.ActionStates.Running) {
                // Force OnLeave to be invoked.
                stateFinished = true;
               
            }
        }
        public override void OnLeave() {
            base.OnLeave();
            brain.enemyanim.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            brain.animator.applyRootMotion = true;
            brain.navmeshAgent.enabled = false;
            brain.currentAction.CleanUp();
            // On successful completion of the task the effects are applied to the agent's WS
            if (actionStatus == GOAP.ActionStates.Success) {
                brain.startWS.ApplyEffects(brain.currentAction.effects);
            }
            brain.currentAction = brain.plan.Pop();
        }
    }

