using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{


    public class CharacterStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHealth = 100;
        public int currentHealth;

        public int staminaLevel = 100;
        public float maxStamina;
        public float currentStamina;

        public int manaLevel = 100;
        public float maxMana;
        public float currentMana;

        public int ExperiencePoints = 0;

        [Header("Armor Defense")]
        public float HelmetDefense;
        public float ChestDefense;
        public float LegDefense;
        public float HandDefense;

        //Fire Defense could be added at somepoint

        public bool isDead;

        public virtual void TakeDamage(int damage, string damageAnimation = "Damage")
        {
            if (isDead)
            {
                return;
            }

           //Calculates the defense of each armor piece and applies it in the damage

            float TotalDefense = 1 - (1 - HelmetDefense / 100) * (1 - ChestDefense / 100) *
                (1 - LegDefense / 100) * (1 - HandDefense / 100);

            damage = Mathf.RoundToInt(damage - (damage * TotalDefense));

            Debug.Log("Total Defense: " + TotalDefense);

            float finalDamage = damage; // + fire damage if added later

            currentHealth = Mathf.RoundToInt(currentHealth - finalDamage);

            Debug.Log("Total Damage Dealt" + finalDamage);

            if(currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
            }
        }
    }
}
