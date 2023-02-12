using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JO
{


    [CreateAssetMenu(menuName =  "AI Attack actions")]
    public class EnemyAttackAction : EnemyActions
    {
        public bool canCombo;
        public EnemyAttackAction comboAction;
        public int attackScore = 3;
        public float recoveryTime = 2;

        public float maximumAttackAngle = 35;
        public float minimumAttackAngle = -35;

        public float maximumDistanceForAttack = 3;
        public float minimumDistanceForAttack = 0;
    }
}
