using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour {
    public enum WeaponStates { Loaded, Fire, NoAmmunition, EmptyMagazine, Reload, DryFire, BurstFire };
    protected InventoryManager inventoryManager;
    protected Weapon weapon;
    public Weapon MountedWeapon { get { return weapon; } }
    public bool HasMountedWeapon { get { return weapon != null; } }
    // Exercise 2.1
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public bool useScope;
    public Transform weaponHand;
    private Transform holsterLocation;

    public FSM<WeaponStates> weaponStateMachine;


   

    public void Start() {
        inventoryManager = GetComponent<InventoryManager>();
        // Exercise 2.1
        animator = GetComponent<Animator>();
        // Botch to allow same script to work on First person Controller
        if (!animator) {
            animator = transform.Find("Camera/FPS_m16_01").GetComponent<Animator>();
        }
        holsterLocation = transform.Find("Bip001/Bip001 Pelvis/Bip001 R Thigh/Holster");
     //   Debug.Log("Holster : " + holsterLocation);
    }

    void Update() {
        //if (weapon && animator.GetBool("Aim") && Input.GetButtonDown("Fire1")) {
        //     animator.SetBool("Fire", true);
        //     weapon.Fire();
        //}

        //if (Input.GetButtonDown("ToggleWeapon")){
        //    Toggle();
        //}

        //if (Input.GetButtonDown("Scope") && weapon.hasScope) {
        //    if (useScope) {
        //        useScope = false;
        //        weapon.SetVisible(true);
        //        // Exercise 2.7
        //        SetVisible(true);
        //    }
        //    else {
        //        useScope = true;
        //        weapon.SetVisible(false);
        //        // Exercise 2.7
        //        SetVisible(false);
        //    }
        //}
        // Check guards for current state transitions
        if (weaponStateMachine != null && weapon != null) {
            weaponStateMachine.Check();
        }


    }

    public virtual void SetWeaponStates() {
        // Create the state machine 
        weaponStateMachine = new FSM<WeaponStates>();

        float secondsPerFireRound = 60f / weapon.roundsPerMin;
        weaponStateMachine.AddState(new Loaded<WeaponStates>(WeaponStates.Loaded, this, 0f));
        weaponStateMachine.AddState(new DryFire<WeaponStates>(WeaponStates.DryFire, this, 0f));
        weaponStateMachine.AddState(new Fire<WeaponStates>(WeaponStates.Fire, this, secondsPerFireRound));
        weaponStateMachine.AddState(new EmptyMagazine<WeaponStates>(WeaponStates.EmptyMagazine, this, 0f));
        weaponStateMachine.AddState(new NoAmmunition<WeaponStates>(WeaponStates.NoAmmunition, this, 0f));
        // Include 1 second delay to ensure reload animation runs!
        weaponStateMachine.AddState(new Reload<WeaponStates>(WeaponStates.Reload, this, 0f));

        // The legal transitions
        weaponStateMachine.AddTransition(WeaponStates.Loaded, WeaponStates.Fire);
        weaponStateMachine.AddTransition(WeaponStates.Fire, WeaponStates.Loaded);
        weaponStateMachine.AddTransition(WeaponStates.NoAmmunition, WeaponStates.DryFire);
        weaponStateMachine.AddTransition(WeaponStates.DryFire, WeaponStates.NoAmmunition);
        weaponStateMachine.AddTransition(WeaponStates.Fire, WeaponStates.EmptyMagazine);
        weaponStateMachine.AddTransition(WeaponStates.Fire, WeaponStates.NoAmmunition); 
        weaponStateMachine.AddTransition(WeaponStates.EmptyMagazine, WeaponStates.Reload);
        weaponStateMachine.AddTransition(WeaponStates.NoAmmunition, WeaponStates.Reload);
        weaponStateMachine.AddTransition(WeaponStates.Reload, WeaponStates.Loaded);

        weaponStateMachine.SetInitialState(WeaponStates.NoAmmunition);

    }

    // The guards invoked when the state machine checks for transitions

    public bool GuardDryFireToNoAmmunition(State<WeaponStates> currentState) {
        return true;
    }

    public bool GuardNoAmmunitionToDryFire(State<WeaponStates> currentState) {
        return true;
    }

    public bool GuardEmptyMagazineToReload(State<WeaponStates> currentState) {
        return ((inventoryManager.GetBalance(weapon.ammoType) > 0));
    }

    public bool GuardNoAmmunitionToReload(State<WeaponStates> currentState) {
        return ((inventoryManager.GetBalance(weapon.ammoType) > 0));
    }

    public bool GuardReloadToLoaded(State<WeaponStates> currentState) {
        return (currentState.AnimationFinished);
    }

    public bool GuardLoadedToFire(State<WeaponStates> currentState) {
        return true;
    }

    public bool GuardFireToLoaded(State<WeaponStates> currentState) {
        return (weapon.loadedMagazineRoundCount > 0);
    }

    public bool GuardFireToEmptyMagazine(State<WeaponStates> currentState) {
        return ((weapon.loadedMagazineRoundCount == 0) && (inventoryManager.GetBalance(weapon.ammoType) > 0));
    }

    public bool GuardFireToNoAmmunition(State<WeaponStates> currentState) {
        return ((weapon.loadedMagazineRoundCount == 0) && (inventoryManager.GetBalance(weapon.ammoType) == 0));
    }


    // A special collision callback for gameObjects that have a CharacterController component
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
                hand = weaponHand;
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
                // Exercise .2
                SetWeaponStates();
            }
        }
    }

    protected void Toggle() {
        GameObject currentToggleWeapon;
        Debug.Log("Toggle Weapons");
        // Get all weapons in the inventory using the tag 
        List<GameObject> weapons = inventoryManager.GetItems("weapon");
        // If there are weapons
        if (weapons.Count > 0) {
            // Record the current mounted weapon
            currentToggleWeapon = weapon.gameObject;
            bool currentFound = false;
            bool nextFound = false;
            while (!nextFound) {
                // First find the current mounted weapon and then find the next
                // May have to iterate over list twice to achieve this
                // if current weapon is last weapon in list.
                foreach (GameObject obj in weapons) {
                    // The current mounted weapon within list
                    if (obj.Equals(currentToggleWeapon)) {
                        currentFound = true;
                    }
                    else if (currentFound) {
                        // Exercise 1.8
                        weapon.SetVisible(false);
                        // Set weapon manager's script to new weapon
                        weapon = obj.GetComponent<Weapon>();
                        // Exercise 1.8
                        weapon.SetVisible(true);
                        weapon.mounted = true;
                        weapon.inventoryManager = inventoryManager;
                        // Locate new weapon in hand - this was assigned when weapon was picked up
                        obj.transform.parent = weapon.gripHand;
                        // Correct location of weapon when mounted on player
                        obj.transform.localRotation = weapon.GetMountOffsetRotation;
                        obj.transform.localPosition = weapon.GetMountOffsetPosition;
                        nextFound = true;
                        // Exercise 4.5
                        SetWeaponStates();
                        break;
                    }
                }
                // If we have to iterate through the list again it must be re initialised
                if (!nextFound) weapons = inventoryManager.GetItems("weapon");
            }
        }
        else Debug.Log("No weapons to toggle through");
    }


    public void SetVisible(bool visible) {
        // Get all the Rendering components of the model
        Component[] allChildren = GetComponentsInChildren<Renderer>();
        foreach (Renderer child in allChildren) {
            child.enabled = visible;
        }
        if (visible) {
            weapon.muzzleFlash.enabled = false;
        }
    }

    public void Holster() {
        weapon.transform.parent = holsterLocation;
        weapon.transform.position = holsterLocation.position;
        weapon.transform.rotation = holsterLocation.rotation;
    }


    public void UnHolster() {
        weapon.transform.parent = weaponHand;
        // Correct location of weapon when mounted on player
        weapon.transform.localRotation = weapon.GetMountOffsetRotation;
        weapon.transform.localPosition = weapon.GetMountOffsetPosition;
    }

}
