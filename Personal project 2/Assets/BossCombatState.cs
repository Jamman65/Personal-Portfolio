using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{



    public class BossCombatState : CombatState
    {
        public bool SecondPhase;
        public EnemyAttackAction[] SecondPhaseAttacks;

        protected override void GetNewAttack(EnemyController enemyController)
        {
            if (SecondPhase)
            {
                {
                    Vector3 targetDirection = enemyController.currentTarget.transform.position - transform.position;
                    float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
                    float distanceFromTarget = Vector3.Distance(enemyController.currentTarget.transform.position, transform.position);

                    int maxScore = 0;

                    for (int i = 0; i < SecondPhaseAttacks.Length; i++)
                    {
                        EnemyAttackAction enemyAttackaction = SecondPhaseAttacks[i];

                        if (distanceFromTarget <= enemyAttackaction.maximumDistanceForAttack && distanceFromTarget >= enemyAttackaction.minimumDistanceForAttack)
                        {
                            if (viewableAngle <= enemyAttackaction.maximumAttackAngle && viewableAngle >= enemyAttackaction.minimumAttackAngle)
                            {
                                maxScore += enemyAttackaction.attackScore;
                            }


                        }
                    }

                    int randomValue = Random.Range(0, maxScore);
                    int temporaryScore = 0;

                    for (int i = 0; i < SecondPhaseAttacks.Length; i++)
                    {
                        EnemyAttackAction enemyAttackaction = SecondPhaseAttacks[i];

                        if (distanceFromTarget <= enemyAttackaction.maximumDistanceForAttack && distanceFromTarget >= enemyAttackaction.minimumDistanceForAttack)
                        {
                            if (viewableAngle <= enemyAttackaction.maximumAttackAngle && viewableAngle >= enemyAttackaction.minimumAttackAngle)
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
            else
            {

            }
            base.GetNewAttack(enemyController);
        }
    }
}
