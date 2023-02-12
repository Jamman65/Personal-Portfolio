using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{


    public class EnemyWeaponSlotManager : MonoBehaviour
    {
        public WeaponItem rightHandWeapon;
        public WeaponItem leftHandWeapon;
        

        WeaponHolderSlot rightHandSlot;
        WeaponHolderSlot leftHandSlot;

        DamageCollider leftDamageCollider;
        DamageCollider RightDamageCollider;

        private void Awake()
        {
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


            }
        }

        private void Start()
        {
            LoadWeaponsOnBothHands();
        }

        public void LoadWeaponOnSlot(WeaponItem weapon, bool isLeft)
        {
            if (isLeft)
            {
                leftHandSlot.currentWeapon = weapon;
                leftHandSlot.LoadWeaponModel(weapon);
                loadDamageColliders(true);
            }

            else
            {
                rightHandSlot.currentWeapon = weapon;
                rightHandSlot.LoadWeaponModel(weapon);
                loadDamageColliders(false);
            }
        }

        public void LoadWeaponsOnBothHands()
        {
            if (rightHandWeapon != null)
            {
                LoadWeaponOnSlot(rightHandWeapon, false);
            }

            if (leftHandWeapon != null)
            {
                LoadWeaponOnSlot(leftHandWeapon, true);
            }
        }

        public void loadDamageColliders(bool isLeft)
        {
            if (isLeft)
            {
                leftDamageCollider= leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
                leftDamageCollider.charactermanager = GetComponentInParent<CharacterManager>();
            }

            else
            {
                RightDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
                RightDamageCollider.charactermanager = GetComponentInParent<CharacterManager>();
            }
        }

        public void OpenDamageCollider()
        {
            RightDamageCollider.EnableDamageCollider();

        }

        public void CloseDamageCollider()
        {
            RightDamageCollider.DisableDamageCollider();
        }

        #region Handle Weapon Stamina
        public void DrainStaminaLightAttack()
        {
            //playerstats.TakeStamina(Mathf.RoundToInt(attackingweapon.baseStamina * attackingweapon.lightStaminaMultiplier));
        }

        public void DrainStaminaHeavyAttack()
        {
            //playerstats.TakeStamina(Mathf.RoundToInt(attackingweapon.baseStamina * attackingweapon.heavyStaminaMultiplier));
        }

        #endregion

        public void EnableCombo()
        {
            //anim.SetBool("canDoCombo", true);
        }
        public void DisableCombo()
        {
            //anim.SetBool("canDoCombo", false);
        }
    }

}
