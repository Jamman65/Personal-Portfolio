using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JO
{
    public class EquipmentSlotUI : MonoBehaviour
    {
        public Image icon;
        WeaponItem weapon;

        UIManager uiManager;

        public bool rightHandSlot01;
        public bool rightHandSlot02;
        public bool leftHandSlot01;
        public bool leftHandSlot02;

        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
        }

        public void AddItem(WeaponItem newWeapon)
        {
            weapon = newWeapon;
            if (newWeapon)
            {
                icon.sprite = weapon.itemIcon;
                icon.enabled = true;
                gameObject.SetActive(true);
            }

            else
            {
                ClearItem();
            }
           
        }

        public void ClearItem()
        {
            weapon = null;
            icon.sprite = null;
            icon.enabled = false;
            //gameObject.SetActive(false);
        }

        public void SelectThisSlot()
        {
            uiManager.ResetAllSelectedSlots();
            if (rightHandSlot01)
            {
                uiManager.rightHandSlot01Selected = true;
                
            }
            else if (rightHandSlot02)
            {
                uiManager.rightHandSlot02Selected = true;
            }
            else if (leftHandSlot01)
            {
                uiManager.leftHandSlot01Selected = true;
            }
            else if(leftHandSlot02)
            {
                uiManager.leftHandSlot02Selected = true;
            }
            uiManager.itemStatsWindowUI.UpdateWeaponItemStats(weapon);
        }
    }
}