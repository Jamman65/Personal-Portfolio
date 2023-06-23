using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{
    public enum WeaponType
    {
    isSpellcaster,
    isHealthcaster,
    isPyrocaster,
    isMeleeAttack,
    isUnarmedAttack,
    isShieldAttack,
    isBow,
    
}

    public enum AmmoType
    {
        Arrow,
        Bolt,
    }

    public enum CombatStanceStyle
    {
        swordAndShield,
        Archer,
    }

    public enum AttackType
    {
        light,
        heavy,
    }

    public enum AttackActionType
    {
        meleeAttackAction,
        magicAttackAction,
        rangedAttackAction,
    }


    public class Enums : MonoBehaviour
    {

    }
}
