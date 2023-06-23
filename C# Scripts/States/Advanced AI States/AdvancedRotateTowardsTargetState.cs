using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{
    public class AdvancedRotateTowardsTargetState : State
    {
        public AdvancedCombatState combatState;

        private void Awake()
        {
            combatState = GetComponent<AdvancedCombatState>();
        }
        public override State Tick(EnemyController enemyController, EnemyStats enemyStats, EnemyAnimator enemyAnimator, EnemyManager enemy)
        {
            enemyAnimator.anim.SetFloat("Vertical", 0);
            enemyAnimator.anim.SetFloat("Horizontal", 0.2f);



            if (enemyController.isInteracting)
            {
                return this;
            }

            if (enemyController.viewableAngle >= 100 && enemyController.viewableAngle <= 180 && !enemyController.isInteracting)
            {
                enemyAnimator.PlayTargetAnimationRootRotation("Turn Behind", true);
                return combatState;
            }

            else if (enemyController.viewableAngle <= -101 && enemyController.viewableAngle >= -180 && !enemyController.isInteracting)
            {
                enemyAnimator.PlayTargetAnimationRootRotation("Turn Behind", true);
                return combatState;
            }

            else if (enemyController.viewableAngle <= -45 && enemyController.viewableAngle >= -100 && !enemyController.isInteracting)
            {
                enemyAnimator.PlayTargetAnimationRootRotation("Right Turn", true);
                return combatState;
            }

            else if (enemyController.viewableAngle >= 45 && enemyController.viewableAngle <= 100 && !enemyController.isInteracting)
            {
                enemyAnimator.PlayTargetAnimationRootRotation("Left Turn", true);
                return combatState;
            }


            return combatState;
        }
    }
}
