using UnityEngine;
using System.Collections;

public class AmmoPickup : Pickup {
    public int magazines;

    void OnTriggerEnter(Collider other) {
        if (other.transform.root.tag == Tags.Player) {
            gameObject.GetComponent<Collider>().enabled = false;
            Debug.Log("Pickup2 " + this.tag);
            PlayPickupSound();
            InventoryManager inventory = other.transform.root.GetComponent<InventoryManager>();
            for (int n = 0; n < magazines; n++) {
                inventory.Add(gameObject);
            }
            gameObject.SetActive(false);
        }
    }
}
