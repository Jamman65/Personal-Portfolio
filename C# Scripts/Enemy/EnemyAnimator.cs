using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{


    public class EnemyAnimator : AnimatorManager
    {
        EnemyController enemyController;
        EnemyManager enemymanager;
        EnemyStats enemystats;
        BossManager bossmanager;
        private void Awake()
        {
            anim = GetComponent<Animator>();
            enemyController = GetComponent<EnemyController>();
            enemystats = GetComponentInParent<EnemyStats>();
            enemymanager = GetComponent<EnemyManager>();
            bossmanager = GetComponentInParent<BossManager>();

        }

        public override void TakeCriticalDamageAnimationEvent()
        {
            enemystats.TakeDamageWithoutAnimations(enemymanager.pendingCriticalDamage);
            enemymanager.pendingCriticalDamage = 0;
        }

        public void AwardExpOnDeath()
        {
            PlayerStats playerstats = FindObjectOfType<PlayerStats>();
            ExperiencePoints exppoints = FindObjectOfType<ExperiencePoints>();
            if (enemystats.isDead)
            {
                playerstats.AddExp(enemystats.ExperienceAwarded);

                if (exppoints != null)
                {
                    exppoints.SetExperienceText(playerstats.ExperiencePoints);
                }
            }

           
        }
        public void CanRotate()
        {
            anim.SetBool("canRotate", true);
        }

        public void StopRotation()
        {
            anim.SetBool("canRotate", false);
        }

        public void EnableCombo()
        {
            anim.SetBool("canDoCombo", true);
        }
        public void DisableCombo()
        {
            anim.SetBool("canDoCombo", false);
        }

        public void EnableisInvulnerable()
        {
            anim.SetBool("isInvulnerable", true);

        }

        public void DisableisInvulnerable()
        {
            anim.SetBool("isInvulnerable", false);
        }


        public void EnableCanBeRiposted()
        {
            enemymanager.canBeRiposted = true;
        }

        public void DisableCanBeRiposted()
        {
            enemymanager.canBeRiposted = false;
        }

        public void InstantiateBossParticleFx()
        {
            BossEffects bossEffectsTransform = GetComponentInChildren<BossEffects>();

            GameObject phaseFx = Instantiate(bossmanager.particleFX, bossEffectsTransform.transform);
        }

        private void OnAnimatorMove()
        {

            //Resets the position of the enemy after an animation
            float delta = Time.deltaTime;
            enemyController.Enemyrb.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            enemyController.Enemyrb.velocity = velocity;

            if (enemyController.RotateWithRootMotion)
            {
                enemyController.transform.rotation *= anim.deltaRotation;
            }
        }
    }
}
