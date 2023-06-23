using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{


    public class CharacterStats : MonoBehaviour
    {
        public CharacterManager character;
        public int maxHealth = 100;
        public int currentHealth;

        
        public float maxStamina;
        public float currentStamina;

        
        public float maxMana;
        public float currentMana;

        public int playerLevel = 1;

        [Header("Levels")]
        public int healthLevel = 10;
        public int staminaLevel = 100;
        public int manaLevel = 100;
        public int strengthLevel = 10;
        public int dexterityLevel = 10;
        public int intelligenceLevel = 10;
        public int faithLevel = 10;
        public int poiseLevel = 10;

        public int ExperiencePoints = 0;

        [Header("Poise/Stance")]
        public float totalPoiseDefense; //total poise during damage
        public float offensivePoiseBonus; //poise gained from an attack with a weapon
        public float armorPoiseBonus; //poise gained from armour
        public float totalPoiseResetTime = 15;
        public float PoiseResetTimer = 0;

        [Header("Armor Defense")]
        public float HelmetDefense;
        public float ChestDefense;
        public float LegDefense;
        public float HandDefense;

        public float blockingPhysicalDamage;

        //Fire Defense could be added at somepoint

        public bool isDead;
        private void Start()
        {
            totalPoiseDefense = armorPoiseBonus;
        }

        private void Update()
        {
            HandlePoiseResetTimer();
        }


        public virtual void TakeDamage(int damage, string damageAnimation = "Damage")
        {
            if (isDead)
            {
                return;
            }

            //if(character.isBlocking == true)
            //{
            //    damageAnimation = "Block";
            //}

            //Calculates the defense of each armor piece and applies it in the damage

            float TotalDefense = 1 - (1 - HelmetDefense / 100) * (1 - ChestDefense / 100) *
                (1 - LegDefense / 100) * (1 - HandDefense / 100);

            damage = Mathf.RoundToInt(damage - (damage * TotalDefense));

            Debug.Log("Total Defense: " + TotalDefense);

            float finalDamage = damage; // + fire damage if added later

            currentHealth = Mathf.RoundToInt(currentHealth - finalDamage);

            Debug.Log("Total Damage Dealt" + finalDamage);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
            }
        }

        public virtual void TakeDamageWithoutAnimations(int damage)
        {
            currentHealth = currentHealth -= damage;




            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
            }
        }

        public virtual void HandlePoiseResetTimer()
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

        public int SetMaxHealth()
        {
            //healthLevel = healthLevel;
            maxHealth = healthLevel;
            return maxHealth;
        }

        public float SetMaxStamina()
        {
            staminaLevel = staminaLevel * 6;
            maxStamina = staminaLevel;
            return maxStamina;
        }

        public float SetMaxMana()
        {
            maxMana = manaLevel;
            return maxMana;
        }

        public virtual void HealCharacter(int healAmount)
        {
            currentHealth = currentHealth + healAmount;

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

          
        }

        public virtual void TakeStamina(float stamina)
        {
            currentStamina = currentStamina -= stamina;
            
        }

    }
}