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
        public bool TwoHandable;
        [Header("Animator Replacer")]
        public WeaponType weaponType;
        public AnimatorOverrideController weaponController;
        public string offHandIdleAnimation = "Left_Arm_Idle";


        [Header("Damage")]
        public int baseDamage = 25;
        public int criticalDamageMultiplier = 4;

        [Header("Poise")]
        public float poiseBreak;
        public float offensivePoiseBonus;

        [Header("Blocking")]
        public float PhysicalBlockingDamage;

    
      


        [Header("Unarmed Attacks")]
        public string UnarmedAttack1;
        public string UnarmedAttack2;
        public string Two_Handed_idle;

        [Header("Stamina Costs")]
        public int baseStamina;
        public float lightStaminaMultiplier;
        public float heavyStaminaMultiplier;

        [Header("Item Actions")]
        public ItemAction hold_Rb_Action;
        public ItemAction tap_RB_Action;
        public ItemAction tap_LB_Action;
        public ItemAction hold_LB_Action;
        public ItemAction tap_RT_Action;
        public ItemAction hold_RT_Action;
        public ItemAction tap_LT_Action;
        public ItemAction hold_LT_Action;

        public AudioClip[] WeaponSounds;


    }
}