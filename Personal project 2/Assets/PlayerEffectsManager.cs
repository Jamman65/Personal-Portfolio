using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{

    public class PlayerEffectsManager : MonoBehaviour
    {
        public GameObject currentFx;
        WeaponSlotManager weaponslotmanager;
        PlayerStats playerstats;
        public int healAmount;
        public GameObject instantiatedFxModel;
        public bool destroy;

        private void Awake()
        {
            playerstats = GetComponentInParent<PlayerStats>();
            weaponslotmanager = GetComponent<WeaponSlotManager>();
        }

        private void Update()
        {
            if(playerstats.currentHealth == 100)
            {
                Destroy(currentFx.gameObject);
            }
        }

        public void HealPlayer(HealItem healitem)
        {
            playerstats.HealPlayer(healAmount);
            //GameObject healparticles = Instantiate(currentFx, playerstats.transform);
            Destroy(instantiatedFxModel.gameObject);
            Destroy(currentFx.gameObject, 2f);
           
            weaponslotmanager.LoadBothWeapons();
        }
    }
}