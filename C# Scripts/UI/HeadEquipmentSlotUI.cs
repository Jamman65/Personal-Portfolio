using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JO
{

    public class HeadEquipmentSlotUI : MonoBehaviour
    {
        public Image icon;
        public HelmetEquipment HelmetItem;

        UIManager uiManager;
        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
        }

        public void AddItem(HelmetEquipment helmetEquipment)
        {
            HelmetItem = helmetEquipment;
            if (helmetEquipment)
            {
                icon.sprite = HelmetItem.itemIcon;
                icon.enabled = true;
                gameObject.SetActive(true);
            }

            

        }

        public void ClearItem()
        {
            HelmetItem = null;
            icon.sprite = null;
            icon.enabled = false;
            //gameObject.SetActive(false);
        }

        public void SelectThisSlot()
        {
            uiManager.helmetEquipmentSlotSelected = true;
            uiManager.itemStatsWindowUI.UpdateArmorItemStats(HelmetItem);
            uiManager.itemStatsWindowUI.armorStats.SetActive(true);
        }
    }
}

