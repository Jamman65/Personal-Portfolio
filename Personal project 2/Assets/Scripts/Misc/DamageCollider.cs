using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JO
{


    public class DamageCollider : MonoBehaviour
    {
        public CharacterManager charactermanager;
        Collider damageCollider;
        public int CurrentWeaponDamage = 25;
        public bool Enabled = false;

        private void Awake()
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

        private void OnTriggerEnter(Collider collision)
        {
            if(collision.tag == "Player")
            {
                PlayerStats playerstats = collision.GetComponent<PlayerStats>();
                CharacterManager enemycharactermanager = collision.GetComponent<CharacterManager>();
                BlockingCollder shield = collision.transform.GetComponentInChildren<BlockingCollder>();

                if(enemycharactermanager != null)
                {
                    if (enemycharactermanager.isParrying)
                    {
                        //check if isparried is active
                        charactermanager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Parried", true);
                        return;
                    }
                    else if (shield != null && enemycharactermanager.isBlocking)
                    {
                        //calculates the amount of damage to block with a shield
                        float physicalDamageAfterBlocking = CurrentWeaponDamage - (CurrentWeaponDamage * shield.blockingPhysicalDamage) / 100;

                        if(playerstats != null)
                        {
                            playerstats.TakeDamage(Mathf.RoundToInt(physicalDamageAfterBlocking),"Block");
                            return;
                        }
                    }
                }
              
                if (playerstats != null)
                {
                    playerstats.TakeDamage(CurrentWeaponDamage);
                }
            }
            //Blocking function for the enemy - a blocking collider needs to be created
            if(collision.tag == "Enemy")
            {
                EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
                CharacterManager enemycharactermanager = collision.GetComponent<CharacterManager>();
                BlockingCollder shield = collision.transform.GetComponentInChildren<BlockingCollder>();

                if (enemycharactermanager != null)
                {
                    if (enemycharactermanager.isParrying)
                    {
                        //check if isparried is active
                        charactermanager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Parried", true);
                        return;
                    }

                    else if (shield != null && enemycharactermanager.isBlocking)
                    {
                        //calculates the amount of damage to block with a shield
                        float physicalDamageAfterBlocking = CurrentWeaponDamage - (CurrentWeaponDamage * shield.blockingPhysicalDamage) / 100;

                        if (enemyStats != null)
                        {
                            enemyStats.TakeDamage(Mathf.RoundToInt(physicalDamageAfterBlocking), "Block");
                            return;
                        }
                    }
                }
                if (enemyStats != null)
                {
                    if (enemyStats.isBoss)
                    {
                        enemyStats.TakeDamageWithoutAnimations(CurrentWeaponDamage);
                    }
                    else
                    {
                        enemyStats.TakeDamage(CurrentWeaponDamage);
                    }
                   
                }

            }

            if(collision.tag == "Illusionary Wall")
            {
                IllusionaryWall illusion = collision.GetComponent<IllusionaryWall>();
                illusion.WallHasBeenHit = true;
            }
        }
    }
}
