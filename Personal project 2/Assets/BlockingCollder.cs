using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{


    public class BlockingCollder : MonoBehaviour
    {
        public BoxCollider blockingCollider;

        public float blockingPhysicalDamage;

        private void Awake()
        {
            blockingCollider = GetComponent<BoxCollider>();
        }

        public void SetColliderBlockingDamage(WeaponItem weapon)
        {
            if(weapon != null)
            {
                blockingPhysicalDamage = weapon.PhysicalBlockingDamage;
            }
        }

        public void EnableBlockingCollider()
        {
            blockingCollider.enabled = true;
        }

        public void DisableBlockingCollider()
        {
            blockingCollider.enabled = false;
        }


    }
}
