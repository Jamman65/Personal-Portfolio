using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{

    public class WorldEventManager : MonoBehaviour
    {
        //Fog Wall
        public List<BossWall> Bosswall;
        public BossHealth BossHealthBar;
        public BossManager boss;

        public bool bossFightActive; //Currently fighting the boss
        public bool bossAwake; //Awoken the boss but the player failed
        public bool bossDead; //Boss has been defeated

        private void Awake()
        {
            BossHealthBar = FindObjectOfType<BossHealth>();

        }

        public void ActivateBossFight()
        {
            bossFightActive = true;
            bossAwake = true;
            BossHealthBar.SetHealthBarActive();
            //Activate Fog Wall
            foreach (var bosswall in Bosswall)
            {
                bosswall.ActivateWall();
            }
        }

        public void BossDefeated()
        {
            bossDead = true;
            bossFightActive = true;
            //Deactivate Fog Wall

            foreach (var bosswall in Bosswall)
            {
                bosswall.DeactivateWall();
            }
        }
    }
}
