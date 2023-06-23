using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{
    public class AdvancedAttackState : State
    {
        public ItemBasedAttackAction currentAttack;
        public AdvancedCombatState combatState;
        public AdvancedPursueState pursueState;
        public CharacterManager charactermanager;
        public AdvancedIdleState idleState;
        public AdvancedRotateTowardsTargetState RotateTowardsTargetState;
        public bool iscomboing = false;
        public bool hasPerformedAttck = false;

        private void Awake()
        {
            combatState = GetComponent<AdvancedCombatState>();
            pursueState = GetComponent<AdvancedPursueState>();
            RotateTowardsTargetState = GetComponent<AdvancedRotateTowardsTargetState>();
            idleState = GetComponent<AdvancedIdleState>();
        }
        public override State Tick(EnemyController enemyController, EnemyStats enemyStats, EnemyAnimator enemyAnimator, EnemyManager enemy)
        {
            if(enemy.combatStyle == CombatStanceStyle.swordAndShield)
            {
                return ProcessSwordAndShieldStyle(enemyController, enemyStats, enemyAnimator, enemy);
            }
            else if(enemy.combatStyle == CombatStanceStyle.Archer)
            {
                return ProcessArcherStyle(enemyController, enemyStats, enemyAnimator, enemy);
            }

            else
            {
                return this;
            }
        }

        private State ProcessSwordAndShieldStyle(EnemyController enemyController, EnemyStats enemyStats, EnemyAnimator enemyAnimator, EnemyManager enemy)
        {

            //Select an attack based on the attack score
            //if the selected attack can not be performed choose a new attack
            //when the enemy attacks the movement will stop
            //set the recovery time after an attack
            //return to the combat state after an attack
           
            HandleRotation(enemyController);

            if (enemyController.distanceFromTarget > enemyController.maximumAttackRange)
            {
                return pursueState;
            }

            if (iscomboing && enemyController.canDoCombo)
            {
                //Attack with combo animation
                //Set a cool down time
                AttackTargetWithCombo(enemyAnimator, enemyController, enemy);

            }


            if (!hasPerformedAttck)
            {
                //Attack
                //Roll for a combo chance
                AttackTarget(enemyAnimator, enemyController, enemy);
                RollForComboChance(enemyController);
            }

            //if (iscomboing && hasPerformedAttck)
            //{
            //    return this; //Starts the state again to perform a combo
            //}

            ResetStateFlags(enemy);

            return RotateTowardsTargetState;

        }

        private State ProcessArcherStyle(EnemyController enemyController, EnemyStats enemyStats, EnemyAnimator enemyAnimator, EnemyManager enemy)
        {

            HandleRotation(enemyController);

         
            if (enemy.isInteracting)
            {
                return this;
            }

            if (!enemy.isAimingArrow)
            {
                ResetStateFlags(enemy);
                return combatState;
            }

            if (enemyController.currentTarget.characterstats.isDead)
            {
                ResetStateFlags(enemy);
                enemyController.currentTarget = null;
                return idleState;
            }

            if (enemyController.distanceFromTarget > enemyController.maximumAttackRange)
            {
                ResetStateFlags(enemy);
                return pursueState;
            }

            if (!hasPerformedAttck)
            {
                //fire arrow
               
                FireArrow(enemy);
                ResetStateFlags(enemy);
            }



            ResetStateFlags(enemy);
            return RotateTowardsTargetState;
        }

        private void AttackTarget(EnemyAnimator enemyAnimator, EnemyController enemyController, EnemyManager enemy)
        {
            currentAttack.PerformAttackAction(enemy);
            enemyController.currentRecoveryTime = currentAttack.recoveryTime;
            hasPerformedAttck = true;
        }

        private void AttackTargetWithCombo(EnemyAnimator enemyAnimator, EnemyController enemyController, EnemyManager enemy)
        {
            currentAttack.PerformAttackAction(enemy);
            iscomboing = false;
  
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

            if (enemyController.allowAiToCombo && combochance <= enemyController.comboLikelyHood)
            {
                if (currentAttack.actionCanCombo)
                {
                    iscomboing = true;
               
                }

                else
                {
                    iscomboing = false;
                    currentAttack = null;
                }

            }
        }

        private void ResetStateFlags(EnemyManager enemy)
        {
            iscomboing = false;
            hasPerformedAttck = false;
            //enemy.isAimingArrow = false;
        }

        private void FireArrow(EnemyManager enemy)
        {
            Debug.Log("Fire arrow");
            enemy.isAimingArrow = true;
            if (enemy.isAimingArrow)
            {
                hasPerformedAttck = true;
                enemy.characterinventorymanager.currentItemBeingUsed = enemy.characterinventorymanager.rightWeapon;
                enemy.characterinventorymanager.rightWeapon.tap_RB_Action.PerformAction(enemy);
            }
        }
    }
}
