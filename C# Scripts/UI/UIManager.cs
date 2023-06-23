using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        public GameObject levelUpWindow;
        public GameObject itemStatsWindow;
        public PlayerManager playerManager;
        public ItemStatsWindowUI itemStatsWindowUI;

        public GameObject CrossHair;
        public Text ExperiencePoints;

        [Header("Weapon Inventory")]
        public GameObject weaponInventorySlotPrefab;
        public Transform weaponInventorySlotsParent;
        WeaponInventorySlot[] weaponInventorySlots;

        [Header("Equipment Window Slot Selected")]
        public bool rightHandSlot01Selected;
        public bool rightHandSlot02Selected;
        public bool leftHandSlot01Selected;
        public bool leftHandSlot02Selected;
        public bool helmetEquipmentSlotSelected;
        public bool BodyEquipmentSlotSelected;
        public bool LegEquipmentSlotSelected;
        public bool HandEquipmentSlotSelected;

        [Header("Helmet Inventory")]
        public GameObject helmetEquipmentInventorySlotPrefab;
        public Transform helmetEquipmentInventorySlotParent;
        public HeadEquipmentInventorySlot[] helmetEquipmentInventorySlots;

        [Header("Body Inventory")]
        public GameObject BodyEquipmentInventorySlotPrefab;
        public Transform BodyEquipmentInventorySlotParent;
        public BodyEquipmentInventorySlot[] BodyEquipmentInventorySlots;


        [Header("Leg Inventory")]
        public GameObject LegEquipmentInventorySlotPrefab;
        public Transform LegEquipmentInventorySlotParent;
        public LegEquipmentSlot[] LegEquipmentInventorySlots;

        [Header("Hand Inventory")]
        public GameObject HandEquipmentInventorySlotPrefab;
        public Transform HandEquipmentInventorySlotParent;
        public HandEquipmentInventorySlot[] HandEquipmentInventorySlots;

        private void Awake()
        {
            playerManager = FindObjectOfType<PlayerManager>();
            weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
            helmetEquipmentInventorySlots = helmetEquipmentInventorySlotParent.GetComponentsInChildren<HeadEquipmentInventorySlot>();
            BodyEquipmentInventorySlots = BodyEquipmentInventorySlotParent.GetComponentsInChildren<BodyEquipmentInventorySlot>();
            LegEquipmentInventorySlots = LegEquipmentInventorySlotParent.GetComponentsInChildren<LegEquipmentSlot>();
            HandEquipmentInventorySlots = HandEquipmentInventorySlotParent.GetComponentsInChildren<HandEquipmentInventorySlot>();
            //equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerinventory);
        }

        private void Start()
        {
            
            equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerinventory);
            ExperiencePoints.text = playerManager.playerstats.ExperiencePoints.ToString();
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

               
            }

            #endregion

            #region Helmet Inventory

            for (int i = 0; i < helmetEquipmentInventorySlots.Length; i++)
            {
                if (i < playerinventory.helmetEquipmentInventory.Count)
                {
                    if (helmetEquipmentInventorySlots.Length < playerinventory.helmetEquipmentInventory.Count)
                    {
                        Instantiate(helmetEquipmentInventorySlotPrefab, helmetEquipmentInventorySlotParent);
                        helmetEquipmentInventorySlots = helmetEquipmentInventorySlotParent.GetComponentsInChildren<HeadEquipmentInventorySlot>();

                    }
                    helmetEquipmentInventorySlots[i].AddItem(playerinventory.helmetEquipmentInventory[i]);
                }

              
                
            }

            #endregion

            #region Body Inventory

             for (int i = 0; i < BodyEquipmentInventorySlots.Length; i++)
            {
                if (i < playerinventory.torsoEquipmentInventory.Count)
                {
                    if (BodyEquipmentInventorySlots.Length < playerinventory.torsoEquipmentInventory.Count)
                    {
                        Instantiate(BodyEquipmentInventorySlotPrefab, BodyEquipmentInventorySlotParent);
                        BodyEquipmentInventorySlots = BodyEquipmentInventorySlotParent.GetComponentsInChildren<BodyEquipmentInventorySlot>();

                    }
                    BodyEquipmentInventorySlots[i].AddItem(playerinventory.torsoEquipmentInventory[i]);
                }

              
                
            }



            #endregion

            #region Leg Inventory


            for (int i = 0; i < LegEquipmentInventorySlots.Length; i++)
            {
                if (i < playerinventory.legEquipmentInventory.Count)
                {
                    if (LegEquipmentInventorySlots.Length < playerinventory.legEquipmentInventory.Count)
                    {
                        Instantiate(LegEquipmentInventorySlotPrefab, LegEquipmentInventorySlotParent);
                        LegEquipmentInventorySlots = LegEquipmentInventorySlotParent.GetComponentsInChildren<LegEquipmentSlot>();

                    }
                    LegEquipmentInventorySlots[i].AddItem(playerinventory.legEquipmentInventory[i]);
                }



            }

            #endregion

            #region Hand Inventory


            for (int i = 0; i < HandEquipmentInventorySlots.Length; i++)
            {
                if (i < playerinventory.HandEquipmentInventory.Count)
                {
                    if (HandEquipmentInventorySlots.Length < playerinventory.HandEquipmentInventory.Count)
                    {
                        Instantiate(HandEquipmentInventorySlotPrefab, HandEquipmentInventorySlotParent);
                        HandEquipmentInventorySlots = HandEquipmentInventorySlotParent.GetComponentsInChildren<HandEquipmentInventorySlot>();

                    }
                    HandEquipmentInventorySlots[i].AddItem(playerinventory.HandEquipmentInventory[i]);
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
            itemStatsWindow.SetActive(false);
        }

        public void ResetAllSelectedSlots()
        {
            rightHandSlot01Selected = false;
            rightHandSlot02Selected = false;
            leftHandSlot01Selected = false;
            leftHandSlot02Selected = false;

            helmetEquipmentSlotSelected = false;
        }
    }
}
