using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{


    public class UIManager : MonoBehaviour
    {

        public PlayerInventory playerinventory;
        public EquipmentWindowUI equipmentWindowUI;
        public GameObject SelectWindow;
        public GameObject HUDWindow;
        public GameObject WeaponInventoryWindow;
        public GameObject EquipmentScreenWindow;

        [Header("Weapon Inventory")]
        public GameObject weaponInventorySlotPrefab;
        public Transform weaponInventorySlotsParent;
        WeaponInventorySlot[] weaponInventorySlots;

        [Header("Equipment Window Slot Selected")]
        public bool rightHandSlot01Selected;
        public bool rightHandSlot02Selected;
        public bool leftHandSlot01Selected;
        public bool leftHandSlot02Selected;

        private void Awake()
        {
          
            //equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerinventory);
        }

        private void Start()
        {
            weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
            equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerinventory);
        }
        public void UpdateUI()
        {
            #region Weapon Inventory
            for (int i = 0; i < weaponInventorySlots.Length; i++)
            {
                if(i < playerinventory.weaponsInventory.Count)
                {
                    if(weaponInventorySlots.Length < playerinventory.weaponsInventory.Count)
                    {
                        Instantiate(weaponInventorySlotPrefab, weaponInventorySlotsParent);
                        weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
                    }
                    weaponInventorySlots[i].AddItem(playerinventory.weaponsInventory[i]);
                }

                else
                {
                    weaponInventorySlots[i].ClearInventorySlot();
                }
            }

            #endregion
        }


        public void OpenSelectWindow()
        {
            SelectWindow.SetActive(true);
        }

        public void CloseSelectWindow()
        {
            SelectWindow.SetActive(false);
        }

        public void CloseAllWindows()
        {
            ResetAllSelectedSlots();
            WeaponInventoryWindow.SetActive(false);
            EquipmentScreenWindow.SetActive(false);
        }

        public void ResetAllSelectedSlots()
        {
            rightHandSlot01Selected = false;
            rightHandSlot02Selected = false;
            leftHandSlot01Selected = false;
            leftHandSlot02Selected = false;
        }
    }
}
