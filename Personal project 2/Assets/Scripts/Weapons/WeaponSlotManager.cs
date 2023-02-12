using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{
    public class WeaponSlotManager : MonoBehaviour
    {
        public WeaponHolderSlot leftHandSlot;
        public WeaponHolderSlot rightHandSlot;
        public WeaponHolderSlot test;
        WeaponHolderSlot BackSlot;

        public DamageCollider leftDamageCollider;
        public DamageCollider RightDamageCollider;

        Animator animator;

        QuickSlotsUI quickslots;
        public InputHandler inputhandler;
        public PlayerManager playermanager;

        public  WeaponItem attackingweapon;

        PlayerStats playerstats;
        PlayerInventory playerinventory;

        private void Awake()
        {
            //playermanager = GetComponentInParent<PlayerManager>();
            animator = GetComponent<Animator>();
            quickslots = FindObjectOfType<QuickSlotsUI>();
            playerstats = GetComponentInParent<PlayerStats>();
            playerinventory = GetComponentInParent<PlayerInventory>();
            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach(WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if (weaponSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponSlot;
                }

                else if (weaponSlot.isRightHandSlot)
                {
                    rightHandSlot = weaponSlot;
                }

                else if (weaponSlot.isBackSlot)
                {
                    BackSlot = weaponSlot;
                }
            }
        }

        public void LoadBothWeapons()
        {
            LoadWeaponOnSlot(playerinventory.rightWeapon, false);
            LoadWeaponOnSlot(playerinventory.leftWeapon, true);
        }

        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if (isLeft)
            {
                leftHandSlot.currentWeapon = weaponItem;
                
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftWeaponDamageCollider();
                quickslots.UpdateWeaponQuickSlots(true, weaponItem);

                if (weaponItem != null)
                {
                    animator.CrossFade(weaponItem.Left_hand_idle, 0.2f);
                }
                else
                {
                    animator.CrossFade("Left Arm Empty", 0.2f);
                }

            }

            else
            {
                rightHandSlot.currentWeapon = weaponItem;
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();
                quickslots.UpdateWeaponQuickSlots(false, weaponItem);


                if (inputhandler.TwoHandFlag)
                {
                    BackSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
                    leftHandSlot.UnloadWeaponAndDestroy();
                    animator.CrossFade(weaponItem.Two_Handed_idle, 0.2f);
                }
                else
                {


                    animator.CrossFade("Both Arms empty", 0.2f);

                    BackSlot.UnloadWeaponAndDestroy();

                    if (weaponItem != null)
                    {
                        animator.CrossFade(weaponItem.Right_hand_idle, 0.2f);
                    }

                    else
                    {
                        animator.CrossFade("Right Arm Empty", 0.2f);
                    }
                }
            }
        }
        #region Handle Weapon Damage Colliders

        private void LoadLeftWeaponDamageCollider()
        {
            leftDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            leftDamageCollider.CurrentWeaponDamage = playerinventory.leftWeapon.baseDamage;
        }

        private void LoadRightWeaponDamageCollider()
        {
            RightDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            RightDamageCollider.CurrentWeaponDamage = playerinventory.rightWeapon.baseDamage;
        }

        public void OpenDamageCollider()
        {
             RightDamageCollider.EnableDamageCollider();
            
            //else if (playermanager.isUsingLeftHand)
            //{
            //    leftDamageCollider.EnableDamageCollider();
            //}
            
        }

        //public void OpenLeftDamageCollider()
        //{
        //    leftDamageCollider.EnableDamageCollider();
        //}

        public void CloseDamageCollider()
        {
            RightDamageCollider.DisableDamageCollider();
            leftDamageCollider.DisableDamageCollider();
        }

        //public void CloseLeftDamageCollider()
        //{
        //    leftDamageCollider.DisableDamageCollider();
        //}
        #endregion


        #region Handle Weapon Stamina
        public void DrainStaminaLightAttack()
        {
            playerstats.TakeStamina(Mathf.RoundToInt(attackingweapon.baseStamina * attackingweapon.lightStaminaMultiplier));
        }

        public void DrainStaminaHeavyAttack()
        {
            playerstats.TakeStamina(Mathf.RoundToInt(attackingweapon.baseStamina * attackingweapon.heavyStaminaMultiplier));
        }

        #endregion
    }


}
