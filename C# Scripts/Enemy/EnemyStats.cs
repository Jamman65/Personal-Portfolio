using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{


    public class EnemyStats : CharacterStats
    {



        Animator animator;
        public EnemyAnimator enemyanimator;
        public EnemyHealthBar enemyHealth;
        BossManager bossManager;
        public Healthbar health;
        public SpawnEnemy spawn;
        EnemyController enemyController;
        public int ExperienceAwarded = 50;
        public bool isBoss;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            enemyanimator = GetComponent<EnemyAnimator>();
            bossManager = GetComponent<BossManager>();
            enemyHealth = GetComponentInChildren<EnemyHealthBar>();
            spawn = GetComponent<SpawnEnemy>();
            enemyController = GetComponent<EnemyController>();
            currentHealth = maxHealth;
            maxHealth = SetMaxHealth();
        }
        // Start is called before the first frame update
        void Start()
        {
            totalPoiseDefense = armorPoiseBonus;
            if (!isBoss)
            {
                enemyHealth.SetMaxHealth(maxHealth);
                enemyHealth.SetHealth(currentHealth);
            }


            isDead = false;

        }
        public override void HandlePoiseResetTimer()
        {
            if (PoiseResetTimer > 0)
            {
                PoiseResetTimer = PoiseResetTimer - Time.deltaTime;
            }
            else
            {
                totalPoiseDefense = armorPoiseBonus;
            }
        }

        private int SetMaxHealth()
        {
            maxHealth = healthLevel;
            return maxHealth;
        }

        public override void TakeDamage(int damage, string damageAnimation = "Damage")
        {
            base.TakeDamage(damage, damageAnimation = "Damage");


            if (!isBoss)
            {
                enemyHealth.SetHealth(currentHealth);
            }

            else if (isBoss && bossManager != null)
            {
                bossManager.SetBossHealthBar(currentHealth, maxHealth);

            }




            enemyanimator.PlayTargetAnimation(damageAnimation, true);

            if (currentHealth <= 0)
            {
                HandleDeath();
                enemyanimator.PlayTargetAnimation("Dying", true);
            }
        }

        public void BreakGuard()
        {
            enemyanimator.PlayTargetAnimation("Parried", true);
        }

        public override void TakeDamageWithoutAnimations(int damage)
        {
            currentHealth = currentHealth -= damage;


            if (!isBoss)
            {
                enemyHealth.SetHealth(currentHealth);
            }

            else if (isBoss && bossManager != null)
            {
                bossManager.SetBossHealthBar(currentHealth, maxHealth);

            }



            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
                enemyanimator.PlayTargetAnimation("Dying", true);
            }
        }

        private void HandleDeath()
        {
            currentHealth = 0;
            enemyanimator.PlayTargetAnimation("Dying", true);
            isDead = true;
            spawn.isDead = true;

        }


        // Update is called once per frame
        void Update()
        {
            HandlePoiseResetTimer();
        }
    }
}