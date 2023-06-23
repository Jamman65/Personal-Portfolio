using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{
    public class IdleCompanionState : State
    {
        public LayerMask detectionLayer;
        public LayerMask LayersToIgnore;
        PursueCompanionState pursueState;
        FollowCompanionState followHostState;
        public float detectionRadius = 2;

        private void Awake()
        {
            pursueState = GetComponent<PursueCompanionState>();
            followHostState = GetComponent<FollowCompanionState>();
        }

        public override State Tick(EnemyController enemyController, EnemyStats enemyStats, EnemyAnimator enemyAnimator, EnemyManager enemy)
        {
            enemyAnimator.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);

            //Look for potential target
            //switch to pursue the target state if target is in range
            //if theres no target return back to this state and continue looking for a target
            //if (enemyController.isInteracting)
            //{
            //    return this;
            //}


            if(enemyController.distanceFromHost > enemyController.maxDistanceFromHost)
            {
                return followHostState;
            }


            Collider[] colliders = Physics.OverlapSphere(enemyController.transform.position, detectionRadius, detectionLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterManager characterStats = colliders[i].transform.GetComponent<CharacterManager>();

                if (characterStats != null)
                {
                    Vector3 targetsDirection = characterStats.transform.position - enemyController.transform.position;
                    float viewableAngle = Vector3.Angle(targetsDirection, enemyController.transform.forward);

                    if (viewableAngle > enemyController.minimumDetectionAngle &&
                        viewableAngle < enemyController.maximumDetectionAngle)
                    {
                        //If the target has an obstacle between the ai and the player then it will return
                        if (Physics.Linecast(enemy.lockOnTransform.position, characterStats.lockOnTransform.position, LayersToIgnore))
                        {
                            return this;
                        }
                        else
                        {
                            enemyController.currentTarget = characterStats;
                        }

                        //isSleeping = false;
                        //enemyAnimator.PlayTargetAnimation(wakeAnimation, true);
                    }
                }
            }

            //if (enemyController.currentTarget != null)
            //{
            //    return pursueState;
            //}
            return this;



        }
    }
}
