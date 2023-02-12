using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{
    [CreateAssetMenu(menuName ="Spells/ProjectileSpell")]

    public class ProjectileSpell : SpellItem
    {
        public float baseDamage;
        public float projectileSpeed;
        public float projectileSpeedUp;
        public bool isEffectedbyGravity;
        public float projectileMass;
        Rigidbody rb;

        public override void AttemptToCastSpell(PlayerAnimatorManager animatorhandler, PlayerStats playerstats, WeaponSlotManager weaponslotManager)
        {
            Vector3 startpos = playerstats.GetComponentInChildren<ProjectileStartPos>().transform.position;
            base.AttemptToCastSpell(animatorhandler, playerstats,weaponslotManager);
            GameObject WarmUpSpellFx = Instantiate(spellWarmUpFx, weaponslotManager.rightHandSlot.transform);
            //WarmUpSpellFx.gameObject.transform.localScale = new Vector3(100, 100, 100);
            animatorhandler.PlayTargetAnimation(spellAnimation, true);
            //Instantiate the spell in the casting hand of the player
            //play an animaton for the cast
        }

        public override void CastSpell(PlayerAnimatorManager animatorhandler, PlayerStats playerstats,
            WeaponSlotManager weaponslotManager, CameraHandler camera)
        {
            Vector3 startpos = playerstats.GetComponentInChildren<ProjectileStartPos>().transform.position;
            base.CastSpell(animatorhandler, playerstats,weaponslotManager,camera);
            GameObject SpellFx = Instantiate(spellCastFx, startpos, camera.cameraPivotTransform.rotation);
            rb = SpellFx.GetComponent<Rigidbody>();

            if(camera.currentLockOnTarget != null)
            {
                SpellFx.transform.LookAt(camera.currentLockOnTarget.transform);
            }
            else
            {
                //Directs the position and rotation of where the projectile will be thrown
                SpellFx.transform.rotation = Quaternion.Euler(camera.cameraPivotTransform.eulerAngles.x, playerstats.transform.eulerAngles.y, 0);

            }

            rb.AddForce(SpellFx.transform.forward * projectileSpeed);
            rb.AddForce(SpellFx.transform.up * projectileSpeedUp); //controls the upwards velocity of the spell
            rb.useGravity = isEffectedbyGravity;
            rb.mass = projectileMass;
            SpellFx.transform.parent = null;

        }
    }
}
