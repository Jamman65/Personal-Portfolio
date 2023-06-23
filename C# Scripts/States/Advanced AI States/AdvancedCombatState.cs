using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{
    public class AdvancedCombatState : State
    {
        public AdvancedAttackState attackState;
        public AdvancedPursueState pursueState;
        public EnemyController enemyController;
        public ItemBasedAttackAction[] enemyAttacks;

        protected bool randomDestinationSet = false;
        protected float VerticalMovementValue = 0;
        protected float HorizontalMovementvalue = 0;

        [Header("State Flags")]
        bool willPerformBlock = false;
        bool willPerformDodge = false;
        bool willPerformParry = false;

        bool hasPerformedDodge = false;
        public bool hasPerformedParry = false;
        bool hasRandomDodgeDirection = false;
        bool hasAmmoLoaded = false;
        Quaternion targetDodgeDirection;

        private void Awake()
        {
            attackState = GetComponent<AdvancedAttackState>();
            pursueState = GetComponent<AdvancedPursueState>();
           
        }

        public override State Tick(EnemyController enemyController, EnemyStats enemyStats, EnemyAnimator enemyAnimator, EnemyManager enemy)
        {
            //Check for attack range
            //if in attack range change state to attack
            //return the pursue state if the player moves out of the range or the attack is on cooldown

            if(enemy.combatStyle == CombatStanceStyle.swordAndShield)
            {
                return ProcessSwordAndShieldStyle(enemyController, enemyStats, enemyAnimator, enemy);
            }

            else if (enemy.combatStyle == CombatStanceStyle.Archer)
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

            enemyAnimator.anim.SetFloat("Vertical", VerticalMovementValue, 0.2f, Time.deltaTime);
            enemyAnimator.anim.SetFloat("Horizontal", HorizontalMovementvalue, 0.5f, Time.deltaTime);
     

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

            //randomises the walking pattern of the AI
            if (!randomDestinationSet)
            {
                randomDestinationSet = true;
                DecideCirclingAction(enemyAnimator, enemyController);
            }

            if (enemy.allowAiToParry)
            {
                if (enemyController.currentTarget.canBeRiposted)
                {
                    CheckForRiposte(enemy, enemyController);
                    return this;
                }
            }

            if (enemy.allowAiToBlock)
            {
                //roll for a block chance
                RollForBlockChance(enemy);
            }

            if (enemy.allowAiToDodge)
            {
                //roll for a block chance
                RollForDodgeChance(enemy);
            }

            if (enemy.allowAiToParry)
            {
                //roll for a block chance
                RollForParryChance(enemy);
            }

            if (willPerformBlock)
            {
                //block using off hand
                BlockUsingOffHand(enemy);

            }

            if (willPerformDodge && enemyController.currentTarget.isAttacking)
            {
                Dodge(enemy);
            }

            if (enemyController.currentTarget.isAttacking)
            {
                if (willPerformParry && !hasPerformedParry)
                {
                    ParryCurrentTarget(enemy, enemyController);
                    return this;
                }
            }

           

       





            HandleRotation(enemyController);

            //if (enemyController.isPerformingAction)
            //{
            //    enemyAnimator.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            //}

            if (enemyController.currentRecoveryTime <= 0 && attackState.currentAttack != null)
            {
                ResetStateFlags();
                return attackState;
            }


            else
            {
                GetNewAttack(enemyController);
                Debug.Log("Attack has been performed");

            }
            CheckIfTooClose(enemy, enemyController, enemyAnimator);

            return this;

        }

        private State ProcessArcherStyle(EnemyController enemyController, EnemyStats enemyStats, EnemyAnimator enemyAnimator, EnemyManager enemy)
        {
            enemyAnimator.anim.SetFloat("Vertical", VerticalMovementValue, 0.2f, Time.deltaTime);
            enemyAnimator.anim.SetFloat("Horizontal", HorizontalMovementvalue, 0.5f, Time.deltaTime);


            if (enemyController.isInteracting)
            {
                enemyAnimator.anim.SetFloat("Vertical", 0);
                enemyAnimator.anim.SetFloat("Horizontal", 0);
                return this;
            }
    


            if (enemyController.distanceFromTarget > enemyController.maximumAttackRange)
            {
                ResetStateFlags();
                enemy.isAiming = false;
                return pursueState;
            }

            //randomises the walking pattern of the AI
            if (!randomDestinationSet)
            {
                randomDestinationSet = true;
                DecideCirclingAction(enemyAnimator, enemyController);
            }

            if (enemy.allowAiToDodge)
            {
                //roll for a block chance
                RollForDodgeChance(enemy);
            }

            if (willPerformDodge && enemyController.currentTarget.isAttacking)
            {
                Dodge(enemy);
            }


           





            HandleRotation(enemyController);

            if (!hasAmmoLoaded)
            {
                //draw arrow
                //aim at the target before firing
                DrawArrow(enemy);
                AimAtTarget(enemy, enemyController);
            }

            //if (enemyController.isPerformingAction)
            //{
            //    enemyAnimator.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            //}

            if (enemyController.currentRecoveryTime <= 0.5 && hasAmmoLoaded)
            {
                ResetStateFlags();
                return attackState;
            }

            if (enemyController.isStationaryArcher)
            {
                enemyAnimator.anim.SetFloat("Vertical", 0, 0.2f, Time.deltaTime);
                enemyAnimator.anim.SetFloat("Horizontal", 0, 0.5f, Time.deltaTime);
            }
            else
            {
                CheckIfTooClose(enemy, enemyController, enemyAnimator);
            }


          

            return this;
        }

        public void HandleRotation(EnemyController enemyController)
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

            if (distanceFromTarget > enemyController.maximumAttackRange)
            {
                VerticalMovementValue = 1f;
            }

            else if (distanceFromTarget <= enemyController.maximumAttackRange)
            {
                VerticalMovementValue = 0.5f;
            }
        }


        protected virtual void GetNewAttack(EnemyController enemyController)
        {



            int maxScore = 0;

            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                ItemBasedAttackAction enemyAttackaction = enemyAttacks[i];

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
                ItemBasedAttackAction enemyAttackaction = enemyAttacks[i];

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

        private void RollForBlockChance(EnemyManager enemy)
        {
            int blockchance = Random.Range(0, 100);

            if(blockchance <= enemy.blockLikelyHood)
            {
                willPerformBlock = true;
            }
            else
            {
                willPerformBlock = false;
            }
        }

        private void RollForDodgeChance(EnemyManager enemy)
        {
            int dodgeChance = Random.Range(0, 100);

            if (dodgeChance <= enemy.blockLikelyHood)
            {
                willPerformDodge = true;
            }
            else
            {
                willPerformDodge = false;
            }
        }

        private void RollForParryChance(EnemyManager enemy)
        {
            int parryChance = Random.Range(0, 100);

            if (parryChance <= enemy.blockLikelyHood)
            {
                willPerformParry = true;
            }
            else
            {
                willPerformParry = false;
            }
        }

        private void ParryCurrentTarget(EnemyManager enemy, EnemyController enemyController)
        {
            if (enemyController.currentTarget.canBeParried)
            {
                if(enemyController.distanceFromTarget <= 1)
                {
                    hasPerformedParry = true;
                    enemy.isParrying = true;
                    enemy.animatormanager.PlayTargetAnimation("Parry", true);
                   
                }
            }
            hasPerformedParry = false;
        
        }

        private void CheckForRiposte(EnemyManager enemy, EnemyController enemyController)
        {
            if (enemy.isInteracting)
            {
                enemy.animatormanager.anim.SetFloat("Horizontal", 0, 0.2f, Time.deltaTime);
                enemy.animatormanager.anim.SetFloat("Vertical", 0, 0.2f, Time.deltaTime);
                return;
            }

            if(enemyController.distanceFromTarget >= 1)
            {
                HandleRotation(enemyController);
                enemy.animatormanager.anim.SetFloat("Horizontal", 0, 0.2f, Time.deltaTime);
                enemy.animatormanager.anim.SetFloat("Vertical", 1, 0.2f, Time.deltaTime);
            }

            if (enemy.allowAiToParry)
            {
                Debug.Log("Allow Riposte");
                enemy.isBlocking = false;
                if(!enemy.isInteracting && !enemyController.currentTarget.isRiposted && !enemyController.currentTarget.isBackstabbed)
                {
                   
                    enemyController.Enemyrb.velocity = Vector3.zero;
                    enemy.animatormanager.anim.SetFloat("Vertical", 0);
                    enemy.characterCombatManager.AttemptBackStabOrRiposte();
                    Debug.Log("Riposted");
                }

                enemy.isParrying = false;

            }
        }

        private void ResetStateFlags()
        {

            hasPerformedParry = false;
            hasAmmoLoaded = false;
            hasRandomDodgeDirection = false;
            hasPerformedDodge = false;
            randomDestinationSet = false;
            willPerformBlock = false;
            willPerformDodge = false;
            willPerformParry = false;
        }

        //Ai Actions

        private void BlockUsingOffHand(EnemyManager enemy)
        {
            if(enemy.isBlocking == false)
            {
                if (enemy.allowAiToBlock)
                {
                    enemy.isBlocking = true;
                    enemy.characterinventorymanager.currentItemBeingUsed = enemy.characterinventorymanager.leftWeapon;

                    
                }
            }
        }

        private void Dodge(EnemyManager enemy)
        {
            if (!hasPerformedDodge)
            {
                if (!hasRandomDodgeDirection)
                {
                    float randomDodgeDirection;
                 

                    hasRandomDodgeDirection = true;
                    randomDodgeDirection = Random.Range(0, 360);
                    targetDodgeDirection = Quaternion.Euler(enemy.transform.eulerAngles.x, randomDodgeDirection, enemy.transform.eulerAngles.z);
                }

                if(enemy.transform.rotation != targetDodgeDirection)
                {
                    Quaternion targetRotation = Quaternion.Slerp(enemy.transform.rotation, targetDodgeDirection, 1f);
                    enemy.transform.rotation = targetRotation;

                    float targetYRotation = targetDodgeDirection.eulerAngles.y;
                    float currentYRotation = enemy.transform.eulerAngles.y;
                    float rotationDifference = Mathf.Abs(targetYRotation - currentYRotation);

                    if(rotationDifference <= 5)
                    {
                        hasPerformedDodge = true;
                        enemy.transform.rotation = targetDodgeDirection;
                        enemy.animatormanager.PlayTargetAnimation("Rolling", true);
                    }
                }
            }
        }

        private void DrawArrow(EnemyManager enemy)
        {

                hasAmmoLoaded = true;
                enemy.characterinventorymanager.currentItemBeingUsed = enemy.characterinventorymanager.rightWeapon;
                enemy.characterinventorymanager.rightWeapon.hold_Rb_Action.PerformAction(enemy);
            
        }

        private void AimAtTarget(EnemyManager enemy, EnemyController enemyController)
        {
            float timeUntilFireAtTarget = Random.Range(enemy.minimumTimeToAimAtTarget, enemy.maximumTimeToAimAtTarget);
            enemyController.currentRecoveryTime = timeUntilFireAtTarget;
        }


        private void CheckIfTooClose(EnemyManager enemy, EnemyController enemyController, EnemyAnimator enemyAnimator)
        {
            if(enemyController.distanceFromTarget <= enemy.stoppingDistance)
            {
                enemy.animator.anim.SetFloat("Vertical", 0, 0.2f, Time.deltaTime);
                enemyAnimator.anim.SetFloat("Horizontal", HorizontalMovementvalue, 0.5f, Time.deltaTime);
            }
            else
            {
                enemyAnimator.anim.SetFloat("Vertical", VerticalMovementValue, 0.2f, Time.deltaTime);
                enemyAnimator.anim.SetFloat("Horizontal", HorizontalMovementvalue, 0.5f, Time.deltaTime);

            }
        }
     


    }
}

