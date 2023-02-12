using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
    [HideInInspector]
    public InventoryManager inventoryManager;
    [HideInInspector]
    public Transform gripHand;
    private Transform ikHandPosition;
    public Transform IKHandPosition { get { return ikHandPosition; } }
    [HideInInspector]
    public bool mounted = false;

    // Fine tune position of the weapon when mounted to the player
    public Vector3 mountRotationOffset = new Vector3(23f, 280f, 344f);
    public Quaternion GetMountOffsetRotation { get { return Quaternion.Euler(mountRotationOffset); } }
    public Vector3 mountPositionOffset = new Vector3(0.18f, 0.20f, -0.01f);
    public Vector3 GetMountOffsetPosition { get { return mountPositionOffset; } }

    public AudioClip fireSound;
    public AudioClip dryFireSound;
    public AudioClip reloadSound;
    public AudioClip pickupSound;
    public Transform muzzlePoint;
    public Renderer muzzleFlash;
    //public ParticleEmitter muzzleSmoke;
    //public ParticleEmitter ejector;
    protected AudioSource audio;

    // 2D image of the weapon displayed in hud when mounted
    public Texture hudWeaponImage;

    public bool hasScope = false;

    // Length of ray when raycasting used 
    public float range = 100.0f;
    [HideInInspector]
    public CollisionEffectsManager collisionEffectsManager;

    public GameObject ammoType;
    public int roundsInMagazine = 20;
    public int roundsPerMin = 200;
    [HideInInspector]
    public int loadedMagazineRoundCount;

    public float loudness = 50f;


    [HideInInspector]
    public Vector3 trajectory;





    // Use this for initialization
	void Awake () {
        ikHandPosition = transform.Find("HandGrip");
      
        audio = GetComponent<AudioSource>();
        // Exercises 3.1
        collisionEffectsManager = GetComponent<CollisionEffectsManager>();
        loadedMagazineRoundCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void Fire() {
        // Exercise 4.1
        if (loadedMagazineRoundCount > 0) {
            loadedMagazineRoundCount--;
            // Eject cartridge
           // ejector.Emit(1);
           // muzzleSmoke.Emit(30);
            audio.PlayOneShot(fireSound, 1.0f);
            // Play muzzle flash animation
            StartCoroutine(ShowMuzzleFlash());
            RaycastHit hitData;
            // Get details of what the ray collided with, if anything
            bool hit = Aim(out hitData);
            // If ray collided with something
            if (hit) {
                // Apply damage to collided object
                if (!InflictDamage(hitData)) {
                    if (collisionEffectsManager != null) {
                        collisionEffectsManager.ApplyDecal(hitData);
                        collisionEffectsManager.ApplyParticleEffect(hitData);
                    }
                }
            }
        }
        else {
            audio.PlayOneShot(dryFireSound, 0.75f);
        }

    }

    public IEnumerator ShowMuzzleFlash() {
        if (muzzleFlash) {
            muzzleFlash.transform.localRotation *= Quaternion.Euler(0, 0, Random.Range(-360, 360));
            muzzleFlash.enabled = true;
            yield return new WaitForSeconds(0.05f);
            muzzleFlash.enabled = false;
        }
    }


    // When a player has multiple weapons only one should be visible at any one time.
    // Used when toggling player's weapons
    public void SetVisible(bool visible) {
        // Get all the Rendering components of the weapon model
        Component[] allChildren = GetComponentsInChildren<Renderer>();
        foreach (Renderer child in allChildren) {
            child.enabled = visible;
        }
    }

    public virtual bool Aim(out RaycastHit hitData) {
        // Get centre of screen world coordinate + normal
        Ray cameraRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0.0f));
        // Cast ray from centre of screen 
        return Physics.Raycast(cameraRay.origin, cameraRay.direction, out hitData, range);
    }

    public virtual bool InflictDamage(RaycastHit hitData) {
        // Check the topmost transform in hierachy is ai
        if (hitData.collider.transform.root.tag == Tags.AI) {
            Debug.Log("hit AI");
            // Get the AI's health manager 
            HealthManager hm = hitData.collider.transform.root.GetComponent<HealthManager>();
            // Can damage be inflicted on the ai
            if (hm) {
                hm.OnRaycastHit(hitData);
                return true;
            }
            else {
                Debug.Log("No Health Manager attached to this AI GameObject");
                return false;
            }
        }
        else return false;

    }

    public void Reload() {
        // Debug.Log("Audio : " + audio);
        audio.PlayOneShot(reloadSound, 0.75f);
        loadedMagazineRoundCount = roundsInMagazine;
        inventoryManager.Use(ammoType);
    }

    // Used by HUD
    public int GetRoundCount() {
        int magazines = inventoryManager.GetBalance(ammoType);
        return (magazines * roundsInMagazine) + loadedMagazineRoundCount;
    }

    // Used by HUD
    public int GetMagazineCount() {
        return inventoryManager.GetBalance(ammoType);
    }



}
