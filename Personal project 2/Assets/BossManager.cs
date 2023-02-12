using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{

    public class BossManager : MonoBehaviour
    {
        BossHealth bosshealthbar;
        public string BossName;
        EnemyStats enemystats;
        EnemyAnimator enemyAnimator;
        BossCombatState bosscombatState;

        public GameObject particleFX;

        private void Awake()
        {
            bosshealthbar = FindObjectOfType<BossHealth>();
            enemystats = GetComponent<EnemyStats>();
            enemyAnimator = GetComponentInChildren<EnemyAnimator>();
            bosscombatState = GetComponentInChildren<BossCombatState>();
        }

        private void Start()
        {
            bosshealthbar.SetBossName(BossName);
            bosshealthbar.SetBossMaxHealth(enemystats.maxHealth);
        }

        public void SetBossHealthBar(int currentHealth, int maxHealth)
        {
            bosshealthbar.SetBossCurrentHealth(currentHealth);

            if (currentHealth <= maxHealth / 2 && !bosscombatState.SecondPhase)
            {
                bosscombatState.SecondPhase = true;
                TransitionToSecondPhase();
            }


        }

        public void TransitionToSecondPhase()
        {
            //Play Second phase animation
            //Switch Attack Actions 
            enemyAnimator.anim.SetBool("isInvulnerable", true);
            enemyAnimator.anim.SetBool("SecondPhase", true);
            enemyAnimator.PlayTargetAnimation("Second Phase", true);
            bosscombatState.SecondPhase = true;

        }


    }
}

