using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JO
{
    [CreateAssetMenu(menuName = "Items/Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;

        [Header("Damage")]
        public int baseDamage = 25;
        public int criticalDamageMultiplier = 4;

        [Header("Blocking")]
        public float PhysicalBlockingDamage;

        [Header("Idle animations")]
        public string Right_hand_idle;
        public string Left_hand_idle;
        public string Two_Handed_idle;

        [Header("Attack Animations")]
        public string OH_Light_Attack_1;
        public string OH_Heavy_Attack_1;
        public string Flip_Kick;
        public string OH_Light_Attack_2;
        public string Two_Handed_Attack_1;
        public string Two_Handed_Attack_2;
        public string Parry;


        [Header("Unarmed Attacks")]
        public string UnarmedAttack1;
        public string UnarmedAttack2;

        [Header("Stamina Costs")]
        public int baseStamina;
        public float lightStaminaMultiplier;
        public float heavyStaminaMultiplier;

        [Header("Weapon Types")]
        public bool isSpellcaster;
        public bool isHealthcaster;
        public bool isPyrocaster;
        public bool isMeleeAttack;
        public bool isUnarmedAttack;
        public bool isShieldAttack;
    }
}