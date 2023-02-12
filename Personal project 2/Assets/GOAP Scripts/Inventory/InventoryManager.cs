using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {
    // List to store GameObjects in
    private List<GameObject> inventory;

    void Awake() {
        // Create the empty list
        inventory = new List<GameObject>();
    }


    public bool Add(GameObject item) {
        // GameObjects that can be placed in the Inventory must have a pickup script component attached
        Pickup pickupScript = item.GetComponent<Pickup>();
        if (pickupScript) {
            inventory.Add(item);
            return true;
        }
        else {
            return false;
        }
    }


    public bool Use(GameObject searchItem) {
        // Find a GameObject in the List with a matching tag
        GameObject foundItem = inventory.Find(delegate(GameObject obj) { return obj.tag == searchItem.tag; });
        if (foundItem != null) {
            // Remove from list
            inventory.Remove(foundItem);
            return true;
        }
        else return false;
    }


    public int GetBalance(GameObject searchItem) {
        // Return one or more GameObjects with a matching tag
        List<GameObject> foundItems = inventory.FindAll(delegate(GameObject obj) { return obj.tag == searchItem.tag; });
        return foundItems.Count;
    }

    // The maximum number of this type of GameObject that can be stored in the list
    public int GetMax(GameObject searchItem) {
        // GameObjects that can be placed in the Inventory must have a pickup script component attached
        Pickup pickupScript = searchItem.GetComponent<Pickup>();
        if (pickupScript) {
            // the pickup component property indicating the max number that can be held in the inventory
            return pickupScript.maxInventoryCapacity;
        }
        else {
            return 1;
        }
    }

    public List<GameObject> GetItems(string tag) {
        // Get a list of all the GameObjects with a matching type
        List<GameObject> foundItems = inventory.FindAll(delegate(GameObject obj) { return obj.tag == tag; });
        return foundItems;
    }

}
