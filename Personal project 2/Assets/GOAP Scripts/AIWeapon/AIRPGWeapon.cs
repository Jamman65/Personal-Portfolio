using UnityEngine;
using System.Collections;

public class AIRPGWeapon : AIWeapon {
    public GameObject round;
  //  public ParticleEmitter exhaustSmoke;
    ProjectileManager projectileManager;







    public override void Fire(){
        if (loadedMagazineRoundCount > 0){
            // Get the script responsible for movement
            projectileManager = GetComponentInChildren<ProjectileManager>();
            // Detach the missile from the weapon
            projectileManager.transform.parent = null;
            projectileManager.trajectory = muzzlePoint.transform.forward;
            StartCoroutine(ShowMuzzleFlash());
           // muzzleSmoke.Emit(30);
           // exhaustSmoke.Emit();
           // projectileManager.trailSmoke.emit = true;
            audio.PlayOneShot(fireSound, 0.6f);
            loadedMagazineRoundCount--;
            // Delay enabling the rocket's collider to avoid it exploding when being in contact
            // with the RPG or AI that is firing the rocket.
            StartCoroutine(Arm());
        }
        else{
            audio.PlayOneShot(dryFireSound, 0.75f);
        }
    }


    IEnumerator Arm() {
        yield return new WaitForSeconds(0.5f);
        projectileManager.gameObject.GetComponent<Collider>().enabled = true;
    }
}
