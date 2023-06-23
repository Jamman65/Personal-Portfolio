using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{
    public class ArmorInteractable : Interactable
    {
        public HelmetEquipment item;
        public UIManager uiManager;

        public override void Interact(PlayerManager playerManager)
        {
            uiManager.playerinventory.helmetEquipmentInventory.Add(item);
            Debug.Log("Armor added to inventory");
        }
    }
}