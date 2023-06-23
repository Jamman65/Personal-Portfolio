using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{


    public class EnemyManager : CharacterManager
    {
        
        EnemyController enemyController;
        public AnimatorManager animator;
        
        public CombatStanceStyle combatStyle;
        public bool allowAiToBlock;
        public bool allowAiToDodge;
        public bool allowAiToParry;
        public int blockLikelyHood = 50;
        public int dodgeLikelyHood = 50;
        public int parryLikelyHood = 50;
        public float stoppingDistance = 1.2f;

  


        public float minimumTimeToAimAtTarget = 3;
        public float maximumTimeToAimAtTarget = 6;
      
        

        // these values control the FOV of the AI
       

        private void Awake()
        {
            enemyController = GetComponent<EnemyController>();
            animator = GetComponent<AnimatorManager>();
           
            //backstabCollider = GetComponentInChildren<CriticalDamageCollider>();
        }

        private void Update()
        {
            animator.anim.SetBool("isBlocking", isBlocking);
            animator.anim.SetBool("IsTwoHanding", isTwoHanding);
            isAiming = animator.anim.GetBool("IsAiming");

         

            //HandleCurrentAction();
            //OnDrawGizmosSelected();
        }

        //private void HandleCurrentAction()
        //{
        //    if(enemyController.currentTarget == null)
        //    {
        //        enemyController.HandleDetection();
        //    }
        //}

        //private void OnDrawGizmosSelected()
        //{
        //    Gizmos.color = Color.red; 
        //    Gizmos.DrawWireSphere(transform.position, enemyController.detectionRadius);
        //}
    }
}
