using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{


    public class AttackState : State
    {
        //public EnemyAttackAction[] enemyAttacks;
        public EnemyAttackAction currentAttack;
        public CombatState combatState;
        public PursueState pursueState;
        public CharacterManager charactermanager;
        public RotateTowardsTargetState RotateTowardsTargetState;
        public bool iscomboing = false;
        public bool hasPerformedAttck = false;
        public override State Tick(EnemyController enemyController, EnemyStats enemyStats, EnemyAnimator enemyAnimator)
        {
            //Select an attack based on the attack score
            //if the selected attack can not be performed choose a new attack
            //when the enemy attacks the movement will stop
            //set the recovery time after an attack
            //return to the combat state after an attack
            float distanceFromTarget = Vector3.Distance(enemyController.currentTarget.transform.position, enemyController.transform.position);
            HandleRotation(enemyController);

            if(distanceFromTarget > enemyController.maximumAttackRange)
            {
                return pursueState;
            }

            if(iscomboing && enemyController.canDoCombo)
            {
                //Attack with combo animation
                //Set a cool down time
                AttackTargetWithCombo(enemyAnimator, enemyController);
                
            }


            if (!hasPerformedAttck)
            {
                //Attack
                //Roll for a combo chance
                AttackTarget(enemyAnimator, enemyController);
                RollForComboChance(enemyController);
            }

            if(iscomboing && hasPerformedAttck)
            {
                return this; //Starts the state again to perform a combo
            }

            return RotateTowardsTargetState;
        }

        private void AttackTarget(EnemyAnimator enemyAnimator, EnemyController enemyController)
        {
            enemyAnimator.PlayTargetAnimation(currentAttack.actionAnimation, true);
            enemyController.currentRecoveryTime = currentAttack.recoveryTime;
            hasPerformedAttck = true;
        }

        private void AttackTargetWithCombo(EnemyAnimator enemyAnimator, EnemyController enemyController)
        {
            iscomboing = false;
            enemyAnimator.PlayTargetAnimation(currentAttack.actionAnimation, true);
            enemyController.currentRecoveryTime = currentAttack.recoveryTime;
            currentAttack = null;
        }


        public void HandleRotation(EnemyController enemyController)
        {
            //rotate manually
            if (enemyController.canRotate && enemyController.isInteracting)
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
           
        }

        private void RollForComboChance(EnemyController enemyController)
        {
            float combochance = Random.Range(0, 100);
            Debug.Log(combochance);

            if(enemyController.allowAiToCombo && combochance <= enemyController.comboLikelyHood)
            {
                if(currentAttack.comboAction != null)
                {
                    iscomboing = true;
                    currentAttack = currentAttack.comboAction;
                }

                else
                {
                    iscomboing = false;
                    currentAttack = null;
                }
                
            }
        }

    }
}
