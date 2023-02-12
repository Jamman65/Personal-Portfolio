using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{
    public class ConsumableItem : Item
    {
        public int maxItemAmount;
        public int currentItemAmount;

        public GameObject Model;

        public string ConsumeAnim;
        public bool isInteracting;

        public virtual void AttemptoConsumeItem(PlayerAnimatorManager playeranim, WeaponSlotManager weaponslotManager, PlayerEffectsManager playereffectsManager)
        {
            if(currentItemAmount > 0)
            {
                playeranim.PlayTargetAnimation(ConsumeAnim, isInteracting, true);
            }

            else
            {
                return;
            }
        }
    }
}
