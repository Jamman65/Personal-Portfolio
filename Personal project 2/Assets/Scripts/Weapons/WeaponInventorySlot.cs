using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JO
{
    public class WeaponInventorySlot : MonoBehaviour
    {
        public PlayerInventory playerinventory;
        public WeaponSlotManager weaponSlotManager;
        public QuickSlotsUI quickslotsUI;
        public UIManager uiManager;
        public Image icon;
        WeaponItem item;

        private void Awake()
        {
            //playerinventory = FindObjectOfType<PlayerInventory>();
            //uiManager = FindObjectOfType<UIManager>();
            //weaponSlotManager = FindObjectOfType<WeaponSlotManager>();
        }
        public void AddItem(WeaponItem newItem)
        {
            item = newItem;

            if (newItem)
            {
                icon.sprite = item.itemIcon;
                icon.enabled = true;
                gameObject.SetActive(true);
            }
            
        }

        public void ClearInventorySlot()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }

        public void EquipThisItem()
        {
            //Remove current item 
            //Add curent item to inventory
            //equip new item
            //remove the item from inventory

            if (uiManager.rightHandSlot01Selected)
            {
                playerinventory.weaponsInventory.Add(playerinventory.weaponsInRightHandSlots[0]);
                playerinventory.weaponsInRightHandSlots[0] = item;
              
                playerinventory.weaponsInventory.Remove(item);
               

            }

            else if (uiManager.rightHandSlot02Selected)
            {
                playerinventory.weaponsInventory.Add(playerinventory.weaponsInRightHandSlots[1]);
                playerinventory.weaponsInRightHandSlots[1] = item;
                playerinventory.weaponsInventory.Remove(item);
            }
            else if (uiManager.leftHandSlot01Selected)
            {
                playerinventory.weaponsInventory.Add(playerinventory.weaponsInLeftHandSlots[0]);
                playerinventory.weaponsInLeftHandSlots[0] = item;
                playerinventory.weaponsInventory.Remove(item);
            }
            else if(uiManager.leftHandSlot02Selected)
            {
                playerinventory.weaponsInventory.Add(playerinventory.weaponsInLeftHandSlots[1]);
                playerinventory.weaponsInLeftHandSlots[1] = item;
                playerinventory.weaponsInventory.Remove(item);
            }

            else
            {
                return;
            }

            playerinventory.rightWeapon = playerinventory.weaponsInRightHandSlots[playerinventory.currentRightWeaponIndex];
            playerinventory.leftWeapon = playerinventory.weaponsInRightHandSlots[playerinventory.currentLeftWeaponIndex];
            weaponSlotManager.LoadWeaponOnSlot(playerinventory.rightWeapon, false);
            weaponSlotManager.LoadWeaponOnSlot(playerinventory.leftWeapon, true);

            uiManager.equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerinventory);
            //uiManager.UpdateUI();
            uiManager.ResetAllSelectedSlots();
            
            
        }

    }
}
