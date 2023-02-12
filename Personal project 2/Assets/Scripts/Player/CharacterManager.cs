using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{


    public class CharacterManager : MonoBehaviour
    {
        public Transform lockOnTransform;
        public BoxCollider backStabCheck;
        public CriticalDamageCollider backstabCollider;
        public CriticalDamageCollider RiposteCollider;

        [Header("Combat Flags")]
        public bool canBeRiposted;
        public bool canBeParried;
        public bool isParrying;
        public bool isBlocking;

        public bool RotateRootMotion;

        public bool isFiringSpell;

        //damage for this will be infliced in an animation event
        public int pendingCriticalDamage;
    }
}
