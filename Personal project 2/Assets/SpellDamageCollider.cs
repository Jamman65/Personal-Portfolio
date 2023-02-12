using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{


    public class SpellDamageCollider : DamageCollider
    {
        public GameObject impactParticles;
        public GameObject projectilePartices;
        public GameObject muzzleParticles;

        bool hascollided = false;
        CharacterStats spellTarget;
        Rigidbody rigidbody;

        Vector3 impactNormal; //controls the rotation of the impact particles

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            projectilePartices = Instantiate(projectilePartices, transform.position, transform.rotation);
            projectilePartices.transform.parent = transform;

            if (muzzleParticles)
            {
                muzzleParticles = Instantiate(muzzleParticles, transform.position, transform.rotation);
                Destroy(muzzleParticles, 2f);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!hascollided)
            {
                if (spellTarget = other.transform.GetComponent<EnemyStats>())
                {
                    hascollided = true;
                    impactParticles = Instantiate(impactParticles, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));
                    spellTarget.TakeDamage(CurrentWeaponDamage);
                }
               
                //if (spellTarget != null)
                //{
                //    spellTarget.TakeDamage(CurrentWeaponDamage);
                //}

                //hascollided = true;
                //impactParticles = Instantiate(impactParticles, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));

                Destroy(projectilePartices);
                Destroy(impactParticles, 2f);
                Destroy(gameObject, 2f);
            }
        }
    }

}
