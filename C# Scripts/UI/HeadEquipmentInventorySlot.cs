using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JO
{
    public class HeadEquipmentInventorySlot : MonoBehaviour
    {
        public PlayerInventory playerinventory;
        public WeaponSlotManager weaponSlotManager;
        public QuickSlotsUI quickslotsUI;
        public UIManager uiManager;
        public Image icon;
        public HelmetEquipment item;

        private void Awake()
        {
           //item = playerinventory.currentHelmetEquipment;
            //playerinventory = FindObjectOfType<PlayerInventory>();
            //uiManager = FindObjectOfType<UIManager>();
            //weaponSlotManager = FindObjectOfType<WeaponSlotManager>();
        }
        public void AddItem(HelmetEquipment newItem)
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

            if (uiManager.helmetEquipmentSlotSelected)
            {
                //add the current equipped helmet to the helmet inventory
                //remove the current equipment and replace it with a new equipment
                //remove the new equipped helment from the inventory
                //load the new equipment

                
                    uiManager.playerinventory.helmetEquipmentInventory.Add(uiManager.playerinventory.currentHelmetEquipment);

                item = playerinventory.helmetEquipmentInventory[0];
                uiManager.playerinventory.currentHelmetEquipment = item;
                
                
                uiManager.playerinventory.helmetEquipmentInventory.Remove(item);
                uiManager.playerManager.playerequipmentManager.EquipAllEquipment();
                //uiManager.playerManager.playerequipmentManager.helmet = playerinventory.currentHelmetEquipment;
                //uiManager.playerManager.playerequipmentManager.helmet.SetActive(false);

            }

           

            else
            {
                return;
            }

            //update the new equipment on the UI/equipment screen
            uiManager.equipmentWindowUI.LoadArmorOnEquipmentScreen(uiManager.playerinventory);
            uiManager.ResetAllSelectedSlots();


        }
    }
}
