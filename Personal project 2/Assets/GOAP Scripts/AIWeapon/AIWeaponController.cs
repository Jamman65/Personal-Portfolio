using UnityEngine;
using System.Collections;

public class AIWeaponController : WeaponController {



    void Start() {
        inventoryManager = GetComponent<InventoryManager>();
    }


    // A special collision callback fo gameObjects that have a CharacterController component
    void OnControllerColliderHit(ControllerColliderHit other) {
     
        // Player collides with weapon
        if (other.gameObject.tag == Tags.Weapon) {
            // Get weapons rigidbody
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            // Switch off pyhsics
            rb.isKinematic = true;
            rb.useGravity = false;
            // Get weapon's collider
            Collider collider = other.transform.GetComponent<Collider>();
            // Turn off collision detection to avoid trigger being invoked multiple times when mounting
            collider.enabled = false;
            // Add the weapon to the inventory
            if (inventoryManager.Add(other.gameObject)) {
                Transform hand = null;
                // Get the player's model objects
                Transform[] allChildren = GetComponentsInChildren<Transform>();
                // Iterate through the model's looking for the right hand on which to mount the weapon
                foreach (Transform child in allChildren) {
                    if (child.name == "Bip01 R Hand") {
                        hand = child;
                    }
                }
                // Exercise 1.8
                if (weapon) {
                    weapon.SetVisible(false);
                }
                // Mount the new weapon
                // Get the Weapon's script
                weapon = other.gameObject.GetComponent<Weapon>();
                weapon.mounted = true;

                weapon.inventoryManager = inventoryManager;
                weapon.gripHand = hand;
                other.transform.parent = weapon.gripHand;

                // Correct location of weapon when mounted on player
                other.transform.localRotation = weapon.GetMountOffsetRotation;
                other.transform.localPosition = weapon.GetMountOffsetPosition;

            }
        }
    }
}
