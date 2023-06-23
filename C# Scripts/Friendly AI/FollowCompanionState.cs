using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{
    public class FollowCompanionState : State
    {
        IdleCompanionState idleState;
        CombatCompanionState combatState;

        private void Awake()
        {
            idleState = GetComponent<IdleCompanionState>();
            combatState = GetComponent<CombatCompanionState>();
        }

        public override State Tick(EnemyController enemyController, EnemyStats enemyStats, EnemyAnimator enemyAnimator, EnemyManager enemy)
        {


            //chase the chosen target
            //switch to combat state when the enemy is in range 
            //if the target is out of range return to this state and continue to pursue the target
            if (enemyController.isInteracting)
            {
                return this;
            }

            if (enemy.isAiming == true)
            {
                enemyAnimator.anim.SetBool("IsAiming", false);
            }

            HandleRotation(enemyController);

            //if(viewableAngle > 65 || viewableAngle < -55)
            //{
            //    return RotateTowardsTargetState;
            //}

            if (enemyController.isPerformingAction)
            {
                enemyAnimator.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                return this;
            }



            //if (enemyController.isPerformingAction)
            //{
            //    enemyAnimator.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            //    enemyController.navmeshAgent.enabled = false;
            //}

            if (enemyController.distanceFromTarget > enemyController.maximumAttackRange)
            {
                enemyAnimator.anim.SetFloat("Vertical", 0.75f, 0.1f, Time.deltaTime);
            }
            else if (enemyController.distanceFromTarget <= enemyController.maximumAttackRange)
            {
                enemyAnimator.anim.SetFloat("Vertical", 0.5f, 1f, Time.deltaTime);
            }




            if (enemyController.distanceFromHost <= enemyController.maxDistanceFromHost)
            {
                return combatState;
            }
            //else if (distanceFromTarget > enemyController.detectionRadius)
            //{
            //    return idleState;
            //}
            else
            {
                return this;
            }


        }

        public void HandleRotation(EnemyController enemyController)
        {

            Vector3 relativeDirection = transform.InverseTransformDirection(enemyController.navmeshAgent.desiredVelocity);
            Vector3 targetVelocity = enemyController.Enemyrb.velocity;

            enemyController.navmeshAgent.enabled = true;
            enemyController.navmeshAgent.SetDestination(enemyController.companion.transform.position);
            enemyController.Enemyrb.velocity = targetVelocity;
            enemyController.transform.rotation = Quaternion.Slerp(enemyController.transform.rotation, enemyController.navmeshAgent.transform.rotation,
            enemyController.rotationSpeed / Time.deltaTime);
        }
    }
}
