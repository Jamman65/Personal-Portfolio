using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace JO
{


    public class EnemyController : MonoBehaviour
    {
        EnemyManager enemyManager;
        //public LayerMask detectionLayer;
        public CharacterStats currentTarget;
        EnemyStats enemyStats;
        public float detectionRadius = 20f;

        public CapsuleCollider characterCollider;
        public CapsuleCollider characterColliderBlocker;
        public AttackState attackstate;

        public float maximumDetectionAngle = 50f;
        public float minimumDetectionAngle = -50f;
        public float maximumAttackRange = 1.5f;

        public bool canDoCombo;
        public bool canRotate;
        public bool IsSecondPhase;
        public bool isInvulnerable;

        public State currentState;
        public bool isPerformingAction;
        public bool isInteracting;
        public bool RotateWithRootMotion;

        EnemyAnimator enemyAnimator;
        EnemyController enemyController;
        CharacterStats characterstats;
        PlayerAnimatorManager animatorhandler;
       // BackStabCollider backstabcollider;
        public PlayerStats playerstats;
        public Rigidbody Enemyrb;
        public float viewableAngle;

        public bool allowAiToCombo;
        public float comboLikelyHood;


        //public EnemyAttackAction[] enemyAttacks;
        //public EnemyAttackAction currentAttack;

        public float distanceFromTarget;
        public float StoppingDistance = 1f;

        public float currentRecoveryTime = 0;

        public float rotationSpeed = 15f;

       public NavMeshAgent navmeshAgent;
        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimator = GetComponent<EnemyAnimator>();
            navmeshAgent = GetComponentInChildren<NavMeshAgent>();
            enemyController = GetComponent<EnemyController>();
            enemyStats = GetComponent<EnemyStats>();
            Enemyrb = GetComponent<Rigidbody>();
            characterstats = GetComponent<CharacterStats>();
            playerstats = FindObjectOfType<PlayerStats>();
           
        }

        private void Start()
        {
            navmeshAgent.enabled = false;
            Enemyrb.isKinematic = false;
            Physics.IgnoreCollision(characterCollider, characterColliderBlocker, true);
        }

        private void Update()
        {
           HandleAction();
           HandleRecovery();
            //attackstate.isComboing = canDoCombo;
           navmeshAgent.transform.localPosition = Vector3.zero;
           navmeshAgent.transform.localRotation = Quaternion.identity;
           
           isInteracting = enemyAnimator.anim.GetBool("isInteracting");
           canDoCombo = enemyAnimator.anim.GetBool("canDoCombo");
           RotateWithRootMotion = enemyAnimator.anim.GetBool("RootMotionRotate");
           canRotate = enemyAnimator.anim.GetBool("canRotate");
            IsSecondPhase = enemyAnimator.anim.GetBool("SecondPhase");
            isInvulnerable = enemyAnimator.anim.GetBool("isInvulnerable");

          
           enemyAnimator.anim.SetBool("IsDead", enemyStats.isDead);

        }
        public void HandleDetection()
        {
            }

            
       
        
    
       public void HandleAction()
        {
            if (enemyStats.isDead)
            {
                return;
            }

            if (playerstats.isDead)
            {
                enemyAnimator.PlayTargetAnimation("Dance", false);
                //return;
               
                
            }

            else if(currentState != null)
            {
                State nextState = currentState.Tick(this, enemyStats, enemyAnimator);

                if(nextState != null)
                {
                    switchToNextState(nextState);
                }
            }

            //if(currentTarget != null)
            //{
            //    distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
            //}

           

            //if(currentTarget == null)
            //{
            //    HandleDetection();
            //}

            //else if (distanceFromTarget > StoppingDistance)
            //{
            //    enemyController.HandleMoveToTarget();
            //}

            //else if(distanceFromTarget <= StoppingDistance)
            //{
            //    //Handle attacks for enemy

            //    AttackTarget();
            //}
        } 

        private void switchToNextState(State state)
        {
            currentState = state;
        }

       

        

       

        private void HandleRecovery()
        {
            if(currentRecoveryTime > 0)
            {
                currentRecoveryTime -= Time.deltaTime;
            }

            if (isPerformingAction)
            {
                if(currentRecoveryTime <= 0)
                {
                    isPerformingAction = false;
                }
            }
        }
    }
}
