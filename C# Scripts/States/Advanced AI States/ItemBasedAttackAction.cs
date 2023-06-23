using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{
    [CreateAssetMenu(menuName = "Advanced AI Actions/ Item based attack action")]
    public class ItemBasedAttackAction : ScriptableObject
    {
        [Header("Attack Type")]
        public AttackActionType attackActionType = AttackActionType.meleeAttackAction;
        public AttackType attackType = AttackType.light;
        

        public bool actionCanCombo = false;

        bool isRightHandedAction = true;

        public int attackScore = 3;
        public float recoveryTime = 2;

        public float maximumAttackAngle = 35;
        public float minimumAttackAngle = -35;

        public float maximumDistanceForAttack = 3;
        public float minimumDistanceForAttack = 0;

       

        public void PerformAttackAction(EnemyManager enemy)
        {


            PerformRightHandActionBasedOnAttackType(enemy);
            //if (isRightHandedAction)
            //{
            //    enemy.isusingRightHand = true;
            //}
            //else
            //{
            //    enemy.isusingLeftHand = true;
            //}
        }

        private void PerformRightHandActionBasedOnAttackType(EnemyManager enemy)
        {
            if (attackActionType == AttackActionType.meleeAttackAction)
            {
                PerformRightHandMeleeAction(enemy);
            }

            else if (attackActionType != AttackActionType.rangedAttackAction)
            {

            }
        }


        private void PerformLeftHandActionBasedOnAttackType(EnemyManager enemy)
        {
            if (attackActionType == AttackActionType.meleeAttackAction)
            {

            }

            else if (attackActionType != AttackActionType.rangedAttackAction)
            {

            }

        }

        private void PerformRightHandMeleeAction(EnemyManager enemy)
        {
            if (enemy.isTwoHanding)
            {
                if(attackType == AttackType.light)
                {
                    enemy.characterinventorymanager.rightWeapon.tap_RB_Action.PerformAction(enemy);
                }

                else if(attackType == AttackType.heavy)
                {
                    enemy.characterinventorymanager.rightWeapon.tap_RT_Action.PerformAction(enemy);
                }
            }

            else
            {
                if (attackType == AttackType.light)
                {
                    enemy.characterinventorymanager.rightWeapon.tap_RB_Action.PerformAction(enemy);
                }

                else if (attackType == AttackType.heavy)
                {
                    enemy.characterinventorymanager.rightWeapon.tap_RT_Action.PerformAction(enemy);
                }
            }
        }
    }
}
