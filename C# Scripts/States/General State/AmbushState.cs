using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{


    public class AmbushState : State
    {

        public bool isSleeping;
        public float detectionRadius = 2;
        public string SleepAnimation;
        public string wakeAnimation;
        public LayerMask detectionLayer;

        public PursueState pursueState;
        public override State Tick(EnemyController enemyController, EnemyStats enemyStats, EnemyAnimator enemyAnimator, EnemyManager enemy)
        {

            if(isSleeping && enemyController.isInteracting == false)
            {
                enemyAnimator.PlayTargetAnimation(SleepAnimation, true);
            }


            Collider[] colliders = Physics.OverlapSphere(enemyController.transform.position, detectionRadius, detectionLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterManager characterStats = colliders[i].transform.GetComponent<CharacterManager>();

                if(characterStats != null)
                {
                    Vector3 targetsDirection = characterStats.transform.position - enemyController.transform.position;
                    float viewableAngle = Vector3.Angle(targetsDirection, enemyController.transform.forward);

                    if(viewableAngle > enemyController.minimumDetectionAngle &&
                        viewableAngle < enemyController.maximumDetectionAngle)
                    {
                        enemyController.currentTarget = characterStats;
                        isSleeping = false;
                        enemyAnimator.PlayTargetAnimation(wakeAnimation, true);
                    }
                }
            }

            if(enemyController.currentTarget != null)
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
