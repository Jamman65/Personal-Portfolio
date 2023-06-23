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
        public CharacterManager currentTarget;
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
        public CharacterStats characterstats;
        PlayerAnimatorManager animatorhandler;
       // BackStabCollider backstabcollider;
        public PlayerStats playerstats;
        public Rigidbody Enemyrb;
        public float viewableAngle;

        public bool allowAiToCombo;
        public float comboLikelyHood;
        public float distanceFromTarget;
        public bool isStationaryArcher;
        Vector3 targetDirection;
        public Transform groundCheck;
        public LayerMask groundMask;
        public float groundDistance = 0.1f;
        public float jumpForce = 10f;

        public float maxDistanceFromHost;
        public float minDistanceFromHost;
        public float returnDistanceFromHost = 2; //how close the companion gets to the host when returning
        public float distanceFromHost;
        public CharacterManager companion;



        //public EnemyAttackAction[] enemyAttacks;
        //public EnemyAttackAction currentAttack;


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
            groundMask = ~(1 << 8 | 1 << 11);

        }

        private void Start()
        {
            navmeshAgent.enabled = false;
            navmeshAgent.speed = 20f;
            navmeshAgent.acceleration = 20f;
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

            //Enemyrb.AddForce(Vector3.down * 20);
            isInteracting = enemyAnimator.anim.GetBool("isInteracting");
           canDoCombo = enemyAnimator.anim.GetBool("canDoCombo");
           RotateWithRootMotion = enemyAnimator.anim.GetBool("RootMotionRotate");
           canRotate = enemyAnimator.anim.GetBool("canRotate");
            IsSecondPhase = enemyAnimator.anim.GetBool("SecondPhase");
            isInvulnerable = enemyAnimator.anim.GetBool("isInvulnerable");


            if(currentTarget != null)
            {
                distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
                targetDirection = currentTarget.transform.position - transform.position;
                viewableAngle = Vector3.Angle(targetDirection, transform.forward);
            }

            if(companion != null)
            {
                distanceFromHost = Vector3.Distance(companion.transform.position, transform.position);
            }
          
           enemyAnimator.anim.SetBool("IsDead", enemyStats.isDead);

        }

        private void FixedUpdate()
        {
            RaycastHit hit;
            if (Physics.Raycast(groundCheck.position, Vector3.down, out hit, groundDistance, groundMask))
            {
                Enemyrb.velocity = new Vector3(Enemyrb.velocity.x, 0, Enemyrb.velocity.z);
            }
            else
            {
                Enemyrb.velocity += new Vector3(0, -jumpForce * Time.deltaTime, 0);
            }
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
                enemyAnimator.PlayTargetAnimation("Idle", false);
                //return;


            }

            else if(currentState != null)
            {
                State nextState = currentState.Tick(this, enemyStats, enemyAnimator, enemyManager);

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
