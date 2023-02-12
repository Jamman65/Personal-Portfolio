using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{


    public class EnemyManager : CharacterManager
    {
        
        EnemyController enemyController;

        

        // these values control the FOV of the AI
       

        private void Awake()
        {
            enemyController = GetComponent<EnemyController>();
            //backstabCollider = GetComponentInChildren<CriticalDamageCollider>();
        }

        private void Update()
        {
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
