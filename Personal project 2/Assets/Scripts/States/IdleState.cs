using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{


    public class IdleState : State
    {
        public LayerMask detectionLayer;
        public PursueState pursueState;
        public override State Tick(EnemyController enemyController, EnemyStats enemyStats, EnemyAnimator enemyAnimator)
        {
            //Look for potential target
            //switch to pursue the target state if target is in range
            //if theres no target return back to this state and continue looking for a target
            //if (enemyController.isInteracting)
            //{
            //    return this;
            //}


            Collider[] colliders = Physics.OverlapSphere(transform.position, enemyController.detectionRadius, detectionLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();

                if (characterStats != null)
                {
                    //checks for an ai or player with character stats

                    Vector3 targetDirection = characterStats.transform.position - transform.position;
                    float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                    if (viewableAngle > enemyController.minimumDetectionAngle && viewableAngle < enemyController.maximumDetectionAngle)
                    {
                        enemyController.currentTarget = characterStats;
                        
                    }
                }
                
            }
            
            if(enemyController.currentTarget!= null)
            {
                return pursueState;
            }
            else
            {
                return this;
            }
        }

    }
}
