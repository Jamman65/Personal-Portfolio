using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JO;

namespace GOAP
{
    public class Block : Action
    {
        public Block(string name, int cost, StateDrivenBrain brain, StateDrivenBrain.GOAPStates moveToState) : base(name, cost, brain, moveToState) { }
        public override ActionStates Initialise()
        {
            Debug.Log("Start Action : Block");
            return ActionStates.Running;
        }
        public override ActionStates Update()
        {
            //Instructs the AI to perform a blocking action if the player is performing a heavy attack near the enemy
            if (Vector3.Distance(destination.position, brain.transform.position) < 2f && brain.input.rt_Input)
            {


                brain.blocking = true;
                brain.StartCoroutine(Wait());
                
            }

            //Returns the action as a success if the AI is no longer blocking
            else if (brain.IsBlock == false)
            {
                return ActionStates.Success;
            }
            return ActionStates.Running;


        }

        //This coroutine controls the blocking action and the time of the animation to instruct the AI when to block
        IEnumerator Wait()
        {
            if (brain.blocking)
            {
                brain.enemyanim.PlayTargetAnimation("Block", false, true);
                brain.blocking = false;
                yield return new WaitForSeconds(2.5f);
                //brain.enemyanim.PlayTargetAnimation("Empty", false, true);
                brain.IsBlock = false;
            }
         
           
           
        }

        
        public override void CleanUp()
        {
            Debug.Log("End Action : Block");
        }
    }
}