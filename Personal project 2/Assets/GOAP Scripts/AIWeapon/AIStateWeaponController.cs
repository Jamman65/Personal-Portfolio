using UnityEngine;
using System.Collections;

public class AIStateWeaponController :  WeaponController {
    public enum BurstStates { BurstOn, BurstOff };
    public enum TimeSeconds { One = 1, Two = 2, Three = 3 };
    // The weapon assigned to the ai on spawning
    public GameObject weaponObject;
    [HideInInspector]
    public Senses senses;
    public enum FireMode { SingleShot, Burst };
    private BasicAIController aiController;
    private GameObject loadedRocket;
    public bool displayFSMTransitions = false;
    [HideInInspector]
    public bool fire;
    [HideInInspector]
    public bool frameFire = false;
    public int initialMagazineCount = 6;


    void Awake(){
        aiController = gameObject.GetComponent<BasicAIController>();
        senses = gameObject.GetComponent<Senses>();
    }

    void Start() {
        base.Start();
        MountWeapon();
        AssignAmmo();
        fire = false;
    }

 
    void Update(){
        if (weaponStateMachine != null) {
            weaponStateMachine.Check();
        }
    }

    public virtual void SetWeaponStates() {
        // Create the state machine 
        weaponStateMachine = new FSM<WeaponStates>(displayFSMTransitions);

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

    

    public bool GuardDryFireToNoAmmunition(State<WeaponStates> currentState) {
        return true;
    }

    public bool GuardNoAmmunitionToDryFire(State<WeaponStates> currentState) {
        return (fire);
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
        return (fire);
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


    public void MountWeapon(){
        if (weaponObject != null) {
            weaponObject = Instantiate(weaponObject) as GameObject;
            Rigidbody rb = weaponObject.GetComponent<Rigidbody>();
            // Switch off pyhsics
            rb.isKinematic = true;
            rb.useGravity = false;
            // Get weapon's collider
            Collider collider = weaponObject.GetComponent<Collider>();
            // Turn off collision detection to avoid trigger being invoked multiple times when mounting
            collider.enabled = false;
            // Add the weapon to the inventory
            if (inventoryManager.Add(weaponObject)) {
                // Get the Weapon's script
                weapon = weaponObject.GetComponent<AIWeapon>();
                weapon.mounted = true;
                // The weapon script will need access to a number of the AI's components
                (weapon as AIWeapon).aiWeaponController = this;
                weapon.inventoryManager = inventoryManager;
                //(weapon as AIWeapon).target = senses.target;
                //(weapon as AIWeapon).targetCharacterController = senses.target.GetComponent<CharacterController>();
                Transform hand = null;
                hand = weaponHand;
                weapon.gripHand = hand;
                weapon.transform.parent = weapon.gripHand;
                // Correct location of weapon when mounted on player
                weapon.transform.localRotation = weapon.GetMountOffsetRotation;
                weapon.transform.localPosition = weapon.GetMountOffsetPosition;
                SetWeaponStates();
            }
        }
    }

    public void AssignAmmo(){
        if (weaponObject != null) {
            // Debug.Log("AMMO : " + gameObject.name);
            for (int n = 0; n < initialMagazineCount; n++) {
                GameObject ammo = Instantiate(weapon.ammoType) as GameObject;
                inventoryManager.Add(ammo);
            }
        }
    }


    public void DiscardWeapon() {
        Debug.Log("Discard Weapon " + weapon.gameObject);
        weapon.gameObject.transform.parent = null;
        // Switch pyhsics on
        weapon.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        weapon.gameObject.GetComponent<Rigidbody>().useGravity = true;
        weapon.gameObject.GetComponent<Collider>().enabled = true;
        weapon = null;
    }



    public void ReloadRPGStart() {
        Debug.Log("Reload RPG Start");
        Transform hand = null;
        // Get the ai's model objects
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        // Iterate through the model's looking for the left hand on which to mount the rocket
        foreach (Transform child in allChildren) {
            if (child.name == "Bip01 L Hand") {
                hand = child;
            }
        }
        // Create the rocket to be mounted on the weapon
        loadedRocket = Instantiate((weapon as AIRPGWeapon).round, hand.position, hand.rotation) as GameObject;
        // Ensure it moves with the weapon
        loadedRocket.transform.parent = hand;
        // Correct the orientation of the rocket on the Hand
        loadedRocket.transform.localRotation = Quaternion.Euler(338f, 60f, 276f);
        loadedRocket.transform.localPosition = new Vector3(-0.04f, 0.004f, -0.048f);
        loadedRocket.GetComponent<Collider>().enabled = false;
    }

    public void ReloadRPGEnd() {
        Debug.Log("Reload RPG End");
        loadedRocket.transform.parent = weapon.transform;
        // Correct the orientation of the rocket on the RPG
        loadedRocket.transform.localRotation = Quaternion.Euler(338f, 269f, 87f);
        loadedRocket.transform.localPosition = new Vector3(0.01f, 0.158f, 0.183f);
    }

    public void OnFrameFire() {
        frameFire = true;
    }


}
