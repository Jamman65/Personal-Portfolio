using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO {
    public class PlayerManager : CharacterManager
    {
        public InputHandler inputhandler;
        public CameraHandler cameraHandler;
        public UIManager uiManager;
        public Animator anim;
        public PlayerController playerController;
        public InteractableUI interactableUI;
        public PlayerStats playerstats;
        //public BackStabCollider backstabcollider;
        public PlayerAnimatorManager playeranimator;
        public PlayerInventory playerInventory;
        public WeaponSlotManager weaponslotManager;
        public PlayerAttacker playerAttacker;
        public PlayerEquipmentManager playerequipmentManager;

        public GameObject interactableUIGameObject;
        public GameObject InteractableItemGameObject;

        
        //public bool isInteracting;
        //public bool isSprinting;
        //public bool isInAir;
        //public bool isGrounded;
        //public bool canDoCombo;
        //public bool IsUnarmed;
        //public bool WeaponEquipped = true;
        //public bool isUsingRightHand;
        //public bool isUsingLeftHand;
        //public bool isInvulnerable;
       
        //public bool isBlocking;
        // Start is called before the first frame update
        void Start()
        {
            cameraHandler = CameraHandler.singelton;
            playerController = GetComponent<PlayerController>();
            inputhandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
            interactableUI = FindObjectOfType<InteractableUI>();
            playerstats = GetComponent<PlayerStats>();
            //backstabCollider = GetComponentInChildren<BackStabCollider>();
            playeranimator = GetComponentInChildren<PlayerAnimatorManager>();
            playerInventory = GetComponent<PlayerInventory>();
            playerAttacker = GetComponentInChildren<PlayerAttacker>();
            weaponslotManager = GetComponentInChildren<WeaponSlotManager>();
            playerequipmentManager = GetComponentInChildren<PlayerEquipmentManager>();
            uiManager = FindObjectOfType<UIManager>();

            WeaponEquipped = true;

           // IsUnarmed = true;

           // WeaponEquipped = true;
        }

        public virtual void UpdateWhichHandCharacterISUsing(bool usingRightHand)
        {
            if (usingRightHand)
            {
                isUsingLeftHand = false;
                isUsingRightHand = true;
            }
            else
            {
                isUsingLeftHand = true;
                isUsingRightHand = false;
            }
        }



        // Update is called once per frame
        void Update()
        {
            //float delta = Time.deltaTime;
            //isInteracting = anim.GetBool("isInteracting");
            canDoCombo = anim.GetBool("canDoCombo");
            anim.SetBool("IsInAir", isInAir);
            anim.SetBool("IsDead", playerstats.isDead);

            isUsingRightHand = anim.GetBool("isUsingRightHand");
            isUsingLeftHand = anim.GetBool("isUsingLeftHand");
            isInvulnerable = anim.GetBool("isInvulnerable");
            anim.SetBool("isBlocking", isBlocking);
            anim.SetBool("IsTwoHanding", isTwoHanding);
            isFiringSpell = anim.GetBool("isFiringSpell");
            isAiming = anim.GetBool("IsAiming");
            inputhandler.rollFlag = false;
            inputhandler.sprintFlag = false;
            inputhandler.rb_Input = false;
            inputhandler.rt_Input = false;
            inputhandler.Dpad_down = false;
            inputhandler.Dpad_left = false;
            inputhandler.Dpad_right = false;
            inputhandler.Dpad_up = false;
            inputhandler.a_input = false;
            inputhandler.Jump_input = false;
            inputhandler.Inventory_input = false;
            inputhandler.lb_Input = false;
            float delta = Time.deltaTime;
            //isSprinting = inputhandler.b_input;
            inputhandler.TickInput(delta);
            playeranimator.canRotate = anim.GetBool("canRotate");
            playerstats.RegenerateStamina();


            //playerController.HandleMovement(delta);
            //playerController.handleRollingAndSprinting(delta);
            //playerController.HandleFalling(delta, playerController.moveDirection);
            //playerController.HandleJumping();

            CheckForInteractableObject();

            playerController.handleRollingAndSprinting(delta);
            playerController.HandleJumping();

            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation();
            }


            if (isInAir)
            {
                playerController.inAirTimer = playerController.inAirTimer + Time.deltaTime;
            }




        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            playerController.HandleMovement(delta);
            playerController.HandleFalling(delta, playerController.moveDirection);
            playerController.HandleRotation(delta);
           


            
        }

        public void CheckForInteractableObject()
        {
            RaycastHit hit;

            if (Physics.SphereCast(transform.position, 1f, transform.forward, out hit, 1f, cameraHandler.ignoreLayers))
            {
                if(hit.collider.tag == "Interactable")
                {
                    Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                    if(interactableObject != null)
                    {
                        string interactableText = interactableObject.interactableText;
                        interactableUI.InteractableText.text = interactableText;
                        interactableUIGameObject.SetActive(true);


                        if (inputhandler.a_input)
                        {
                            hit.collider.GetComponent<Interactable>().Interact(this);
                        }
                    }
                }
            }
            else
            {
                if(interactableUIGameObject != null)
                {
                    interactableUIGameObject.SetActive(false);
                }

                if(InteractableItemGameObject != null && inputhandler.a_input)
                {
                    InteractableItemGameObject.SetActive(false);
                }
            }
        }

        public void OpenChestInteraction(Transform playerStandingPosition)
        {
            playerController.rigidbody.velocity = Vector3.zero;
            transform.position = playerStandingPosition.transform.position;
            playeranimator.PlayTargetAnimation("Backstep", true);
        }
  

    }
}
