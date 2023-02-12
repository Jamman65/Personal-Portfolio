using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JO
{


    public class WeaponPickup : Interactable
    {
        public WeaponItem weapon;
        UIManager uimanager;

        private void Awake()
        {
            uimanager = FindObjectOfType<UIManager>();
        }

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);

            PickupItem(playerManager);
            
            //will allow the player to pick up items and add to inventory
        }

        public void PickupItem(PlayerManager playerManager)
        {
            PlayerInventory playerInventory;
            PlayerController playerController;
            PlayerAnimatorManager animatorHandler;

            playerInventory = playerManager.GetComponent<PlayerInventory>();
            playerController = playerManager.GetComponent<PlayerController>();
            animatorHandler = playerManager.GetComponentInChildren<PlayerAnimatorManager>();

            playerController.rigidbody.velocity = Vector3.zero; //stops the player from moving when picking up an item
            animatorHandler.PlayTargetAnimation("Backstep", true);
            playerInventory.weaponsInventory.Add(weapon);
            playerManager.InteractableItemGameObject.SetActive(true);
           
            playerManager.InteractableItemGameObject.GetComponentInChildren<Text>().text = weapon.itemName;
            playerManager.InteractableItemGameObject.GetComponentInChildren<RawImage>().texture = weapon.itemIcon.texture;

            //uimanager.UpdateUI();

            
             Destroy(gameObject,2.0f);
        }
    }
}
