using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{
    public class DamagePlayer : MonoBehaviour
    {
        public int damage = 10;
        private void OnTriggerEnter(Collider other)
        {
            PlayerStats playerstats = other.GetComponent<PlayerStats>();

            if(playerstats != null)
            {
                playerstats.TakeDamage(damage);
            }
        }
    }
}
