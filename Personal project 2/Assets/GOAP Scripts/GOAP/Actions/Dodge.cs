using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JO;

namespace GOAP {
    public class Dodge : Action {
        public Dodge(string name, int cost, StateDrivenBrain brain, StateDrivenBrain.GOAPStates moveToState) : base(name, cost, brain, moveToState) { }
        public override ActionStates Initialise() {
            Debug.Log("Start Action : Dodge");
            return ActionStates.Running;
        }
        public override ActionStates Update() {

            //Instructs the AI to perform a dodge action if the player is close enough and attacking the enemy 
            if (Vector3.Distance(destination.position, brain.transform.position) < 2f && brain.input.rb_Input)
            {
                brain.transform.rotation = Quaternion.RotateTowards(brain.transform.rotation, brain.RotationPoint.rotation, brain.rotationspeed * Time.deltaTime);

                brain.enemyanim.PlayTargetAnimation("Rolling", false, true);
                brain.RollCount -= 1; //Deducts one count off to indicate one action has been completed 
                
            }

            //Returns the action as a success when the rollcount is less than or equal to 0
            else if (brain.RollCount <= 0)
            {
                return ActionStates.Success;
            }
            return ActionStates.Running;


        }

   
        public override void CleanUp() {
            Debug.Log("End Action : Dodge");
        }
    }
}