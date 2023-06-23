using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{


    public class CombatState : State
    {
        public AttackState attackState;
        public PursueState pursueState;
        public EnemyAttackAction[] enemyAttacks;

        protected bool randomDestinationSet = false;
        protected float VerticalMovementValue = 0;
        protected float HorizontalMovementvalue = 0;

        public override State Tick(EnemyController enemyController, EnemyStats enemyStats, EnemyAnimator enemyAnimator, EnemyManager enemy)
        {
            //Check for attack range
            //if in attack range change state to attack
            //return the pursue state if the player moves out of the range or the attack is on cooldown

            
            enemyAnimator.anim.SetFloat("Vertical", VerticalMovementValue, 0.2f, Time.deltaTime);
            enemyAnimator.anim.SetFloat("Horizontal", HorizontalMovementvalue, 0.5f, Time.deltaTime);
            attackState.hasPerformedAttck = false;

            if (enemyController.isInteracting)
            {
                enemyAnimator.anim.SetFloat("Vertical", 0);
                enemyAnimator.anim.SetFloat("Horizontal", 0);
                return this;
            }

            if (enemyController.distanceFromTarget > enemyController.maximumAttackRange)
            {
                return pursueState;
            }

            if (!randomDestinationSet)
            {
                randomDestinationSet = true;
                DecideCirclingAction(enemyAnimator, enemyController);
            }





            HandleRotation(enemyController);

            //if (enemyController.isPerformingAction)
            //{
            //    enemyAnimator.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            //}

            if(enemyController.currentRecoveryTime <= 0 && attackState.currentAttack != null)
            {
                randomDestinationSet = false;
                return attackState;
            }

          
            else
            {
                GetNewAttack(enemyController);
                
            }
            return this;


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

        protected void DecideCirclingAction(EnemyAnimator enemyAnimator, EnemyController enemyController)
        {
            //Circle with only forward movement
            //Circle with running
            //Circle with Walking
            //Many different options for the enemy to circle the target

            WalkAroundTarget(enemyAnimator, enemyController);
        }

        protected void WalkAroundTarget(EnemyAnimator enemyAnimator, EnemyController enemyController)
        {
            //VerticalMovementValue = Random.Range(0, 1);
            float distanceFromTarget = Vector3.Distance(enemyController.currentTarget.transform.position, enemyController.transform.position);

            //VerticalMovementValue = 0.5f;
            

            //HorizontalMovementvalue = Random.Range(-1, 1);

            if(distanceFromTarget > enemyController.maximumAttackRange)
            {
                VerticalMovementValue = 1f;
            }

            else if(distanceFromTarget <= enemyController.maximumAttackRange)
            {
                VerticalMovementValue = 0.5f;
            }
        }


        protected virtual void GetNewAttack(EnemyController enemyController)
        {
         
            

            int maxScore = 0;

            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                EnemyAttackAction enemyAttackaction = enemyAttacks[i];

                if (enemyController.distanceFromTarget <= enemyAttackaction.maximumDistanceForAttack && enemyController.distanceFromTarget >= enemyAttackaction.minimumDistanceForAttack)
                {
                    if (enemyController.viewableAngle <= enemyAttackaction.maximumAttackAngle && enemyController.viewableAngle >= enemyAttackaction.minimumAttackAngle)
                    {
                        maxScore += enemyAttackaction.attackScore;
                    }


                }
            }

            int randomValue = Random.Range(0, maxScore);
            int temporaryScore = 0;

            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                EnemyAttackAction enemyAttackaction = enemyAttacks[i];

                if (enemyController.distanceFromTarget <= enemyAttackaction.maximumDistanceForAttack && enemyController.distanceFromTarget >= enemyAttackaction.minimumDistanceForAttack)
                {
                    if (enemyController.viewableAngle <= enemyAttackaction.maximumAttackAngle && enemyController.viewableAngle >= enemyAttackaction.minimumAttackAngle)
                    {
                        if (attackState.currentAttack != null)
                        {
                            return;
                        }

                        temporaryScore += enemyAttackaction.attackScore;

                        if (temporaryScore > randomValue)
                        {
                            attackState.currentAttack = enemyAttackaction;
                        }
                    }




                }
            }


        }


    }
}
