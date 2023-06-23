using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{
    public class EquipmentWindowUI : MonoBehaviour
    {
        public bool rightHand01Selected;
        public bool rightHand02Selected;
        public bool leftHand01Selected;
        public bool leftHand02Selected;


        public EquipmentSlotUI[] handEquipmentSlotUI;
        public HeadEquipmentSlotUI headEquipmentSlot;
        public BodyEquipmentSlotUI bodyEquipmentSlot;
        public LegEquipmentSlotUI LegEquipmentSlot;
        public HandEquipmentSlotUI HandEquipmentSlot;

        private void Awake()
        {
           // handEquipmentSlotUI = GetComponentsInChildren<EquipmentSlotUI>();
        }

        

        public void LoadWeaponsOnEquipmentScreen(PlayerInventory playerInventory)
        {
            for(int i = 0; i < handEquipmentSlotUI.Length; i++)
            {
                if (handEquipmentSlotUI[i].rightHandSlot01)
                {
                    handEquipmentSlotUI[i].AddItem(playerInventory.weaponsInRightHandSlots[0]);
                }

                else if (handEquipmentSlotUI[i].rightHandSlot02)
                {
                    handEquipmentSlotUI[i].AddItem(playerInventory.weaponsInRightHandSlots[1]);
                }

                else if (handEquipmentSlotUI[i].leftHandSlot01)
                {
                    handEquipmentSlotUI[i].AddItem(playerInventory.weaponsInLeftHandSlots[0]);
                }

                else  
                {
                    handEquipmentSlotUI[i].AddItem(playerInventory.weaponsInLeftHandSlots[1]);
                }
            }
        }

        public void LoadArmorOnEquipmentScreen(PlayerInventory playerInventory)
        {
            if(playerInventory.currentHelmetEquipment != null)
            {
                headEquipmentSlot.AddItem(playerInventory.currentHelmetEquipment);
            }

            if (playerInventory.currentTorsoEquipment != null)
            {
                bodyEquipmentSlot.AddItem(playerInventory.currentTorsoEquipment);
            }

            if (playerInventory.currentLegEquipment != null)
            {
                LegEquipmentSlot.AddItem(playerInventory.currentLegEquipment);
            }

            if (playerInventory.currentHandEquipment != null)
            {
                HandEquipmentSlot.AddItem(playerInventory.currentHandEquipment);
            }

        }



        public void SelectRightHandSlot01()
        {
            rightHand01Selected = true;
        }

        public void SelectRightHandSlot02()
        {
            rightHand02Selected = true;
        }

        public void SelectLeftHandSlot01()
        {
            leftHand01Selected = true;
        }

        public void SelectLeftHandSlot02()
        {
            leftHand02Selected = true;
        }
    }
}
