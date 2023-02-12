using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{



    public class SpellItem : Item
    {
        public GameObject spellWarmUpFx;
        public GameObject spellCastFx;
        public string spellAnimation;

        [Header("Spell Cost")]
        public int manaCost;

        [Header("Spell Type")]
        public bool isHealthSpell;
        public bool isMagicSpell;
        public bool isPyroSpell;

        [Header("Spell Description")]
        [TextArea]
        public string spellDescription;

        



        public virtual void AttemptToCastSpell(PlayerAnimatorManager animatorhandler, PlayerStats playerstats, WeaponSlotManager weaponslotManager)
        {
            Debug.Log("You attemped to cast a spell");
        }

        public virtual void CastSpell(PlayerAnimatorManager animatorhandler, PlayerStats playerstats, WeaponSlotManager weaponslotManager, CameraHandler camera)
        {
            Debug.Log("You have casted a spell");
            playerstats.DeductMana(manaCost);
        }
    }
}
