using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JO
{


    public class DamageCollider : MonoBehaviour
    {
        public CharacterManager charactermanager;
        public Collider damageCollider;
        public int CurrentWeaponDamage = 25;
        public bool Enabled = false;

        public float poiseBreak;
        public float offensivePoiseBonus;

        public virtual void Awake()
        {
           
            damageCollider = GetComponent<Collider>();
          
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = Enabled;

        }

        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }

        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }

        protected virtual void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Player")
            {
                PlayerStats playerstats = collision.GetComponent<PlayerStats>();
                CharacterManager enemycharactermanager = collision.GetComponent<CharacterManager>();
                //charactermanager = FindObjectOfType<EnemyManager>();
                BlockingCollder shield = collision.transform.GetComponentInChildren<BlockingCollder>();
                CharacterEffectsManager characterEffects = collision.GetComponent<CharacterEffectsManager>();

                CharacterStats characterStats = enemycharactermanager.characterstats;
                Vector3 DirectionfromPlayerToEnemy = (charactermanager.transform.position - enemycharactermanager.transform.position);
                float dotValueFromPlayerToEnemy = Vector3.Dot(DirectionfromPlayerToEnemy, enemycharactermanager.transform.forward);

                if (enemycharactermanager != null)
                {
                    if (enemycharactermanager.isParrying)
                    {
                        //check if isparried is active
                        charactermanager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Parried", true);
                        return;
                    }
                    else if (enemycharactermanager.isBlocking && dotValueFromPlayerToEnemy > 0.3f)
                    {
                        //calculates the amount of damage to block with a shield
                        float physicalDamageAfterBlocking = CurrentWeaponDamage - (CurrentWeaponDamage * shield.blockingPhysicalDamage) / 100;

                        enemycharactermanager.characterCombatManager.AttemptBlock(this, physicalDamageAfterBlocking, "Block");
                        characterStats.TakeDamage(Mathf.RoundToInt(physicalDamageAfterBlocking), "Block");
                    }

                    else
                    {
                        playerstats.PoiseResetTimer = playerstats.totalPoiseResetTime;
                        playerstats.totalPoiseDefense = playerstats.totalPoiseDefense - poiseBreak;
                        if (playerstats.totalPoiseDefense > poiseBreak)
                        {
                            playerstats.TakeDamageWithoutAnimations(CurrentWeaponDamage);
                        }
                        else
                        {
                            playerstats.TakeDamage(CurrentWeaponDamage);
                        }
                    }
                }

            }
            //Blocking function for the enemy - a blocking collider needs to be created
            if (collision.tag == "Enemy")
            {
                EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
                CharacterManager enemycharactermanager = collision.GetComponent<CharacterManager>();
                charactermanager = FindObjectOfType<PlayerManager>();
                BlockingCollder shield = collision.transform.GetComponentInChildren<BlockingCollder>();
                CharacterStats characterStats = enemycharactermanager.characterstats;
                CharacterEffectsManager characterEffects = collision.GetComponent<CharacterEffectsManager>();
                EnemyController enemycontroller = collision.GetComponent<EnemyController>();
                Vector3 DirectionfromPlayerToEnemy = (charactermanager.transform.position - enemycharactermanager.transform.position);
                float dotValueFromPlayerToEnemy = Vector3.Dot(DirectionfromPlayerToEnemy, enemycharactermanager.transform.forward);

                if (enemycharactermanager != null)
                {
                    if (enemycharactermanager.isParrying)
                    {
                        //check if isparried is active
                        charactermanager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Parried", true);
                        return;
                    }

                    else if (enemycharactermanager.isBlocking && dotValueFromPlayerToEnemy > 0.3f)
                    {
                        //calculates the amount of damage to block with a shield
                        float physicalDamageAfterBlocking = CurrentWeaponDamage - (CurrentWeaponDamage * shield.blockingPhysicalDamage) / 100;

                        enemycharactermanager.characterCombatManager.AttemptBlock(this, physicalDamageAfterBlocking, "Block");
                        enemyStats.TakeDamage(Mathf.RoundToInt(physicalDamageAfterBlocking), "Block");
                        return;
                    }
                }
                if (enemyStats != null)
                {
                    enemyStats.PoiseResetTimer = enemyStats.totalPoiseResetTime;
                    enemyStats.totalPoiseDefense = enemyStats.totalPoiseDefense - poiseBreak;

                    if (enemyStats.isBoss)
                    {
                        if (enemyStats.totalPoiseDefense > poiseBreak)
                        {
                            enemyStats.TakeDamageWithoutAnimations(CurrentWeaponDamage);
                        }
                        else
                        {
                            enemyStats.TakeDamageWithoutAnimations(CurrentWeaponDamage);
                            enemyStats.BreakGuard();
                        }
                    }

                    else
                    {
                        if (enemyStats.totalPoiseDefense > poiseBreak)
                        {
                            enemyStats.TakeDamageWithoutAnimations(CurrentWeaponDamage);
                        }
                        else
                        {
                            characterEffects.InterruptEffect();
                            enemyStats.TakeDamage(CurrentWeaponDamage);
                        }
                    }

                    enemycontroller.currentTarget = charactermanager;

                }

            }

            if (collision.tag == "Illusionary Wall")
            {
                IllusionaryWall illusion = collision.GetComponent<IllusionaryWall>();
                illusion.WallHasBeenHit = true;
            }
        }
    }
}
