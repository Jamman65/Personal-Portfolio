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

        public override void AttemptToCastSpell(CharacterManager character)
        {
            if (character.isusingLeftHand) {
                Vector3 startpos = character.characterstats.GetComponentInChildren<ProjectileStartPos>().transform.position;
                base.AttemptToCastSpell(character);
                GameObject WarmUpSpellFx = Instantiate(spellWarmUpFx, character.characterweaponslotmanager.leftHandSlot.transform);

                character.animatormanager.PlayTargetAnimation(spellAnimation, true, false, character.isusingLeftHand);
            }

            else
            {
                Vector3 startpos = character.characterstats.GetComponentInChildren<ProjectileStartPos>().transform.position;
                base.AttemptToCastSpell(character);
                GameObject WarmUpSpellFx = Instantiate(spellWarmUpFx, character.characterweaponslotmanager.rightHandSlot.transform);

                character.animatormanager.PlayTargetAnimation(spellAnimation, true, false, character.isusingLeftHand);
            }
            //Instantiate the spell in the casting hand of the player
            //play an animaton for the cast
        }

        public override void CastSpell(CharacterManager character)
        {
            PlayerManager player = character as PlayerManager;

            if(player != null)
            {
                if (player.isusingLeftHand)
                {
                    Vector3 startpos = character.characterstats.GetComponentInChildren<ProjectileStartPos>().transform.position;
                    GameObject SpellFx = Instantiate(spellCastFx, startpos, player.cameraHandler.cameraPivotTransform.rotation);

                    base.CastSpell(player);
                    //GameObject SpellFx = Instantiate(spellCastFx, startpos, camera.cameraPivotTransform.rotation);
                    rb = SpellFx.GetComponent<Rigidbody>();

                    if (player.cameraHandler.currentLockOnTarget != null)
                    {
                        SpellFx.transform.LookAt(player.cameraHandler.currentLockOnTarget.transform);
                    }
                    else
                    {
                        //Directs the position and rotation of where the projectile will be thrown
                        SpellFx.transform.rotation = Quaternion.Euler(player.cameraHandler.cameraPivotTransform.eulerAngles.x, player.playerstats.transform.eulerAngles.y, 0);

                    }

                    rb.AddForce(SpellFx.transform.forward * projectileSpeed);
                    rb.AddForce(SpellFx.transform.up * projectileSpeedUp); //controls the upwards velocity of the spell
                    rb.useGravity = isEffectedbyGravity;
                    rb.mass = projectileMass;
                    SpellFx.transform.parent = null;

                }

                else
                {
                    Vector3 startpos = player.playerstats.GetComponentInChildren<ProjectileStartPos>().transform.position;
                    GameObject SpellFx = Instantiate(spellCastFx, startpos, player.cameraHandler.cameraPivotTransform.rotation);

                    base.CastSpell(player);
                    // GameObject SpellFx = Instantiate(spellCastFx, startpos, camera.cameraPivotTransform.rotation);
                    rb = SpellFx.GetComponent<Rigidbody>();

                    if (player.cameraHandler.currentLockOnTarget != null)
                    {
                        SpellFx.transform.LookAt(player.cameraHandler.currentLockOnTarget.transform);
                    }
                    else
                    {
                        //Directs the position and rotation of where the projectile will be thrown
                        SpellFx.transform.rotation = Quaternion.Euler(player.cameraHandler.cameraPivotTransform.eulerAngles.x, player.playerstats.transform.eulerAngles.y, 0);

                    }

                    rb.AddForce(SpellFx.transform.forward * projectileSpeed);
                    rb.AddForce(SpellFx.transform.up * projectileSpeedUp); //controls the upwards velocity of the spell
                    rb.useGravity = isEffectedbyGravity;
                    rb.mass = projectileMass;
                    SpellFx.transform.parent = null;

                }
                //Vector3 startpos = playerstats.GetComponentInChildren<ProjectileStartPos>().transform.position;
                //base.CastSpell(animatorhandler, playerstats,weaponslotManager,camera, isLeft);
                //GameObject SpellFx = Instantiate(spellCastFx, startpos, camera.cameraPivotTransform.rotation);
                //rb = SpellFx.GetComponent<Rigidbody>();

            }

            else

            {

            }
          

        }
    }
}
