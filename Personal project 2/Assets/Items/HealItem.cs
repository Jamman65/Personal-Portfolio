using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{
    [CreateAssetMenu(menuName ="Items/Consumables/HealItem")]
    public class HealItem : ConsumableItem
    {
        public bool Healitem;
        public bool ManaItem;

        public int healthAmount;
        public int manaAmount;

        public GameObject recoveryFx;

        public override void AttemptoConsumeItem(PlayerAnimatorManager playeranim, WeaponSlotManager weaponslotManager, PlayerEffectsManager playereffectsManager)
        {
            base.AttemptoConsumeItem(playeranim, weaponslotManager, playereffectsManager);
            GameObject flask = Instantiate(Model, weaponslotManager.rightHandSlot.transform);
            playereffectsManager.currentFx = recoveryFx;
            playereffectsManager.healAmount = healthAmount;
            playereffectsManager.instantiatedFxModel = flask;
            Destroy(recoveryFx.gameObject);

            weaponslotManager.rightHandSlot.UnloadWeapon();
           
            // add health or mana
            // instantiate item in hand and play anim
            // play recovery fx
        }
    }
}
