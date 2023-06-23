using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JO
{


    public class PursueState : State
    {
        //public NavMeshAgent navmeshAgent;
        public CombatState combatState;
        public IdleState idleState;
        public RotateTowardsTargetState RotateTowardsTargetState;
        public override State Tick(EnemyController enemyController, EnemyStats enemyStats, EnemyAnimator enemyAnimator, EnemyManager enemy)
        {

            //chase the chosen target
            //switch to combat state when the enemy is in range 
            //if the target is out of range return to this state and continue to pursue the target
            if (enemyController.isInteracting)
            {
                return this;
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




            if (enemyController.distanceFromTarget <= enemyController.maximumAttackRange)
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
            //rotate manually
            if (enemyController.isPerformingAction)
            {
                Vector3 direction = enemyController.currentTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                enemyController.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemyController.rotationSpeed);
            }
            //rotate with navmesh
            else
            {
                Vector3 relativeDirection = transform.InverseTransformDirection(enemyController.navmeshAgent.desiredVelocity);
                Vector3 targetVelocity = enemyController.Enemyrb.velocity;

                enemyController.navmeshAgent.enabled = true;
                enemyController.navmeshAgent.SetDestination(enemyController.currentTarget.transform.position);
                enemyController.Enemyrb.velocity = targetVelocity;
                enemyController.transform.rotation = Quaternion.Slerp(enemyController.transform.rotation, enemyController.navmeshAgent.transform.rotation, 
                enemyController.rotationSpeed / Time.deltaTime);
            }
        }
    }
}