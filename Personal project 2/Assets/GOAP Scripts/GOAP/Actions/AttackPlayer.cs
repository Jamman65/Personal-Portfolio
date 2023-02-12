using UnityEngine;
using System.Collections;

namespace GOAP {
    public class AttackPlayer : Action {
        public AttackPlayer(string name, int cost, StateDrivenBrain brain, StateDrivenBrain.GOAPStates moveToState) : base(name, cost, brain, moveToState) {
            angleOffset = -20f;
        }
        public override ActionStates Initialise()
        {
            Debug.Log("Start Action : Attack Player");
            return ActionStates.Running;
            
        }
        public override ActionStates Update() {

            //Instructs the AI to perform an attack against the player if they are in range 
            if (Vector3.Distance(destination.position, brain.transform.position) < 1f && !brain.playerstats.isDead) 
            {
                destination = GameObject.FindGameObjectWithTag(Tags.Player).transform;
                brain.enemyanim.PlayTargetAnimation("OH_Light_Attack_1", false, true);
            }
            //Returns the action as a success if the player has been defeated
            else if (brain.playerstats.isDead)
            {
                return ActionStates.Success;
            }
                
            
            // Action still running
            return ActionStates.Running;
        }
        public override void CleanUp() {
            Debug.Log("End Action : Attack Player");
           
        }


    }
}