using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{
    public class WeaponSlotManager : CharacterWeaponSlotManager
    {
    
  

        Animator animator;

        QuickSlotsUI quickslots;
        public InputHandler inputhandler;
        public PlayerManager playermanager;

        
        public WeaponItem unarmedweapon;

        PlayerStats playerstats;
        PlayerInventory playerinventory;
        PlayerAnimatorManager playeranimator;

        private void Awake()
        {
            //playermanager = GetComponentInParent<PlayerManager>();
            animator = GetComponent<Animator>();
            quickslots = FindObjectOfType<QuickSlotsUI>();
            playerstats = GetComponentInParent<PlayerStats>();
            playerinventory = GetComponentInParent<PlayerInventory>();
            playeranimator = GetComponent<PlayerAnimatorManager>();
            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
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
                playeranimator.PlayTargetAnimation(weaponItem.offHandIdleAnimation, false, true);
                

                if (weaponItem != null)
                {
                   //animator.CrossFade(weaponItem.Left_hand_idle, 0.2f);
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
                playeranimator.anim.runtimeAnimatorController = weaponItem.weaponController;


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
                        //animator.CrossFade(weaponItem.Right_hand_idle, 0.2f);
                    }

                    else
                    {
                        weaponItem = unarmedweapon;
                        if (isLeft)
                        {
                            animator.CrossFade("Left Arm Empty", 0.2f);
                            playerinventory.leftWeapon = unarmedweapon;
                            leftHandSlot.currentWeapon = weaponItem;
                            leftHandSlot.LoadWeaponModel(weaponItem);
                            LoadLeftWeaponDamageCollider();
                            quickslots.UpdateWeaponQuickSlots(true, weaponItem);

                        }
                        else
                        {
                            animator.CrossFade("Right Arm Empty", 0.2f);
                            playerinventory.rightWeapon = unarmedweapon;
                            leftHandSlot.currentWeapon = weaponItem;
                            leftHandSlot.LoadWeaponModel(weaponItem);
                            LoadRightWeaponDamageCollider();
                            quickslots.UpdateWeaponQuickSlots(false, weaponItem);
                            playeranimator.anim.runtimeAnimatorController = weaponItem.weaponController;

                        }
                    }
                    

                }

            }

             

           
        }

        #region Handle Weapon Damage Colliders

        private void LoadLeftWeaponDamageCollider()
        {
            leftDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            leftDamageCollider.CurrentWeaponDamage = playerinventory.leftWeapon.baseDamage;
            leftDamageCollider.poiseBreak = playerinventory.leftWeapon.poiseBreak;
        }

        private void LoadRightWeaponDamageCollider()
        {
            RightDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            RightDamageCollider.CurrentWeaponDamage = playerinventory.rightWeapon.baseDamage;
            RightDamageCollider.poiseBreak = playerinventory.rightWeapon.poiseBreak;
        }

        public void WeaponAttackPoiseBonus()
        {
            WeaponItem currentWeaponBeingUsed = playerinventory.currentItemBeingUsed as WeaponItem;
            playerstats.totalPoiseDefense = playerstats.totalPoiseDefense + currentWeaponBeingUsed.offensivePoiseBonus;
        }

        public void ResetPoiseBonus()
        {
            playerstats.totalPoiseDefense = playerstats.armorPoiseBonus;
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
            if (RightDamageCollider != null)
            {
                RightDamageCollider.DisableDamageCollider();
            }
            if (leftDamageCollider != null)
            {
                leftDamageCollider.DisableDamageCollider();
            }

        }

        //public void CloseLeftDamageCollider()
        //{
        //    leftDamageCollider.DisableDamageCollider();
        //}
        #endregion


        #region Handle Weapon Stamina
       

        #endregion
    }


}
