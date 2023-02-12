using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{

    public class RotateTowardsTargetState : State
    {
        public CombatState combatState;

        public override State Tick(EnemyController enemyController, EnemyStats enemyStats, EnemyAnimator enemyAnimator)
        {
            enemyAnimator.anim.SetFloat("Vertical", 0);
            enemyAnimator.anim.SetFloat("Horizontal", 0);

            Vector3 targetDirection = enemyController.currentTarget.transform.position - enemyController.transform.position;
            float viewableAngle = Vector3.SignedAngle(targetDirection, enemyController.transform.forward, Vector3.up);

            if (enemyController.isInteracting)
            {
                return this;
            }

            if(viewableAngle >= 100 && viewableAngle <= 180 && !enemyController.isInteracting)
            {
                enemyAnimator.PlayTargetAnimationRootRotation("Turn Behind", true);
                return combatState;
            }
            
            else if(viewableAngle <= -101 && viewableAngle >= -180 && !enemyController.isInteracting)
            {
                enemyAnimator.PlayTargetAnimationRootRotation("Turn Behind", true);
                return combatState;
            }

            else if (viewableAngle <= -45 && viewableAngle >= -100 && !enemyController.isInteracting)
            {
                enemyAnimator.PlayTargetAnimationRootRotation("Right Turn", true);
                return combatState;
            }

            else if (viewableAngle >= 45 && viewableAngle <= 100 && !enemyController.isInteracting)
            {
                enemyAnimator.PlayTargetAnimationRootRotation("Left Turn", true);
                return combatState;
            }


            return combatState;
        }
    }
}
