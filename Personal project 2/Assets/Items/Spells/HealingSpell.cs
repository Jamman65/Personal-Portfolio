using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{

    [CreateAssetMenu(menuName = "Spells/Healing Spell")]
    public class HealingSpell : SpellItem
    {
        public int healAmount;

        public override void AttemptToCastSpell(PlayerAnimatorManager animatorhandler, PlayerStats playerstats, WeaponSlotManager weaponslotManager)
        {
            base.AttemptToCastSpell(animatorhandler,playerstats,weaponslotManager);
            GameObject instantiateWarmUpSpellFx = Instantiate(spellWarmUpFx, animatorhandler.transform);
            Destroy(instantiateWarmUpSpellFx, 2.0f);
            animatorhandler.PlayTargetAnimation(spellAnimation, true);
            Debug.Log("attempting to cast spell");
        }

        public override void CastSpell(PlayerAnimatorManager animatorhandler, PlayerStats playerstats, WeaponSlotManager weaponslotManager, CameraHandler camera)
        {
            base.CastSpell(animatorhandler, playerstats,weaponslotManager,camera);
            GameObject instantiateSpellCastFx = Instantiate(spellCastFx, animatorhandler.transform);
            Destroy(instantiateSpellCastFx, 2.0f);
            playerstats.HealPlayer(healAmount);
            //playerstats.currentHealth = playerstats.currentHealth + healAmount;
            Debug.Log("Spell has been cast");

        }
    }
}
