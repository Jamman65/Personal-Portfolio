using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{


    public class CharacterManager : MonoBehaviour
    {
        public Transform lockOnTransform;
        public BoxCollider backStabCheck;
        public BackStabCollider backstabCollider;
        public BackStabCollider RiposteCollider;
        public CharacterStats characterstats;
        public CharacterCombatManager characterCombatManager;
        public AnimatorManager animatormanager;
        public CharacterInventoryManager characterinventorymanager;
        public CharacterWeaponSlotManager characterweaponslotmanager;
        //public UIManager uimanager;
        //PlayerManager playermanager;
        public Transform criticalAttackRayCast;



        [Header("Combat Flags")]
        public bool canBeRiposted;
        public bool canBeParried;
        public bool isParrying;
        public bool isBlocking;
        public bool isTwoHanding;
        public bool isusingRightHand;
        public bool isusingLeftHand;
        public bool isAiming;
        public bool isAimingArrow;
        public bool isAttacking;
        public bool isBackstabbed;
        public bool isRiposted;
        public bool isPerformingBackstab;
        public bool isPerformingRiposte;

        public bool isInteracting;
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;
        public bool canDoCombo;
        public bool IsUnarmed;
        public bool WeaponEquipped = true;
        public bool isUsingRightHand;
        public bool isUsingLeftHand;
        public bool isInvulnerable;

        public bool RotateRootMotion;

        public bool isFiringSpell;

       

        //damage for this will be infliced in an animation event
        public int pendingCriticalDamage;

    

     
    }
}
