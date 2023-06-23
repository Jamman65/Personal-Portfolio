using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace JO
{
    public class InputHandler : MonoBehaviour
    {
        public float Horizontal;
        public float Vertical;
        public float moveAmount;
        public float MouseX;
        public float MouseY;

        WeaponPickup weaponPickup;

        public bool b_input;
        public bool a_input;
        public bool rb_Input;
        public bool rt_Input;
        public bool Jump_input;
        public bool y_input;
        public bool lb_Input;
        public bool lt_input;
        public bool Critical_Attack_input;
        public bool Hold_Rb_Input;
        public bool X_input;
        public bool tap_lb_input;
        public bool two_Hand_input;


        public bool z_input;
        public bool Inventory_input;
        public bool taunt_input;

        public bool unarmed_input;
        public bool lockOnInput;
        public bool right_stick_right;
        public bool right_stick_left;

        public bool Dpad_up;
        public bool Dpad_down;
        public bool Dpad_right;
        public bool Dpad_left;


        public bool rollFlag;
        public bool sprintFlag;
        public bool comboFlag;
        public bool InventoryFlag;
        public bool lockOnFlag;
        public float rollInputTimer;
        public bool TwoHandFlag;
        public bool FireFlag;



        Input inputActions;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        PlayerManager playerManager;
        PlayerStats playerstats;
        BlockingCollder blockingcollider;
        public UIManager uiManager;
        CameraHandler cameraHandler;
        CharacterManager characterManager;
        WeaponSlotManager weaponslotmanager;
        public CharacterStats characterStats;
        PlayerEffectsManager playereffectsmanager;
        WeaponItem weaponitem;

        public PlayerAnimatorManager animatorhandler;

        Vector2 movementInput;
        Vector2 CameraInput;


        private void Awake()
        {
            blockingcollider = GetComponentInChildren<BlockingCollder>();
            playerAttacker = GetComponentInChildren<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
            playerManager = GetComponent<PlayerManager>();
            weaponPickup = GetComponent<WeaponPickup>();
            uiManager = FindObjectOfType<UIManager>();
            cameraHandler = FindObjectOfType<CameraHandler>();
            characterManager = FindObjectOfType<CharacterManager>();
            weaponslotmanager = GetComponentInChildren<WeaponSlotManager>();
            playerstats = GetComponent<PlayerStats>();
            playereffectsmanager = GetComponentInChildren<PlayerEffectsManager>();
        }


        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new Input();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => CameraInput = i.ReadValue<Vector2>();
                inputActions.PlayerActions.HoldRB.performed += i => Hold_Rb_Input = true;
                inputActions.PlayerActions.HoldRB.canceled += i => Hold_Rb_Input = false;
                inputActions.PlayerActions.HoldRB.canceled += i => FireFlag = true;
                inputActions.PlayerActions.TapLB.performed += i => tap_lb_input = true;




            }

            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            if (characterStats.isDead)
            {
                return;
            }
            if (playerstats.isDead)
            {
                return;
            }
            else
            {
                MoveInput(delta);
                handleRollInput(delta);
                HandleAttackInput(delta);
                HandleQuickSlotInput();
                HandleItemInput();
                HandleJumpInput();
                HandleInventoryInput();
                HandleLockTargets();
                HandleLockOnInput();
                HandleTwoHand();
                HandleTauntInput();
                HandleBackStab();
                HandleConsumable();
                HandleHoldRBInput();
                HandleBowFireInput();
                HandleTapRTInput();
                HandleTapLBInput();
                //playerAttacker.HandleRbActions();

            }

        }

        private void MoveInput(float delta)
        {

            if (playerManager.isAiming)
            {
                Horizontal = movementInput.x;
                Vertical = movementInput.y;
                moveAmount = Mathf.Clamp01(Mathf.Abs(Horizontal) + Mathf.Abs(Vertical));
                if(moveAmount > 0.5f)
                {
                    moveAmount = 0.5f;
                }
                MouseX = CameraInput.x;
                MouseY = CameraInput.y;
            }
            else
            {
                Horizontal = movementInput.x;
                Vertical = movementInput.y;
                moveAmount = Mathf.Clamp01(Mathf.Abs(Horizontal) + Mathf.Abs(Vertical));
                MouseX = CameraInput.x;
                MouseY = CameraInput.y;
            }
         
        }

        private void handleRollInput(float delta)
        {
            b_input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            inputActions.PlayerActions.Roll.canceled += i => b_input = false;

            if (b_input)
            {
                rollInputTimer += delta;
                sprintFlag = true;
                if(playerstats.currentStamina <= 0)
                {
                    b_input = false;
                    sprintFlag = false;
                }
                if(moveAmount > 0.5f && playerstats.currentStamina > 0)
                {
                    sprintFlag = true;
                }
            }
            else
            {
                sprintFlag = false;
                if (rollInputTimer > 0 && rollInputTimer < 0.5f)
                {
                    
                    rollFlag = true;
                }

                rollInputTimer = 0;
            }
        }

        public void HandleTapLBInput()
        {
            if (tap_lb_input)
            {
                tap_lb_input = false;

                


                    if (playerManager.isTwoHanding)
                    {

                    if (playerInventory.rightWeapon.tap_LB_Action != null)
                    {
                        playerManager.UpdateWhichHandCharacterISUsing(true);
                        playerInventory.currentItemBeingUsed = playerInventory.rightWeapon;
                        playerInventory.rightWeapon.tap_LB_Action.PerformAction(playerManager);
                    }
                       
                    }

                    else
                    {
                    if (playerInventory.leftWeapon.tap_LB_Action != null)
                    {
                        playerManager.UpdateWhichHandCharacterISUsing(false);
                        playerInventory.currentItemBeingUsed = playerInventory.leftWeapon;
                        playerInventory.leftWeapon.tap_LB_Action.PerformAction(playerManager);
                    }
                   
                    }

                


            }
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSecondsRealtime(2);
        }


        private void HandleAttackInput(float delta)
        {
            rt_Input = inputActions.PlayerActions.RT.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            z_input = inputActions.PlayerActions.Z.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            lb_Input = inputActions.PlayerActions.LB.phase == UnityEngine.InputSystem.InputActionPhase.Started;
           // lt_input = inputActions.PlayerActions.LT.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            inputActions.PlayerActions.LT.performed += i => lt_input = true;
            inputActions.PlayerActions.LT.canceled += i => lt_input = false;

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                rb_Input = true;
            }

            



            //if (Gamepad.current.xButton.wasPressedThisFrame)
            //{
            //    rb_Input = true;
            //}

            if (rb_Input)
            {
                //playerAttacker.HandleRbActions();


                if (playerInventory.rightWeapon.tap_RB_Action != null)
                {

                    if (playerInventory.rightWeapon.weaponType == WeaponType.isBow)
                    {
                        return;
                    }

                    else
                    {
                        playerManager.UpdateWhichHandCharacterISUsing(true);
                        playerInventory.currentItemBeingUsed = playerInventory.rightWeapon;
                        playerInventory.rightWeapon.tap_RB_Action.PerformAction(playerManager);
                    }
                    
                }
                  
            }

            if (lb_Input)
            {
                ////handle parry for weapons
                //if (TwoHandFlag)
                //{
                //    //handle two handed special attack
                //}
                
                
                    playerAttacker.HandleLBActions();
                
            }

            if (!lt_input)
            {
                playerManager.isBlocking = false;
            }

            if (lt_input)
            {
                //perform blocking

                //playerAttacker.HandleLTActions();


                if (playerManager.isTwoHanding)
                {
                    if (playerInventory.rightWeapon.hold_LT_Action != null)
                    {
                        playerManager.UpdateWhichHandCharacterISUsing(true);
                        playerInventory.currentItemBeingUsed = playerInventory.rightWeapon;
                        playerInventory.rightWeapon.hold_LT_Action.PerformAction(playerManager);
                    }

                }

                else
                {

                    if (playerInventory.leftWeapon.hold_LT_Action != null)
                    {
                        playerManager.UpdateWhichHandCharacterISUsing(true);
                        playerInventory.currentItemBeingUsed = playerInventory.leftWeapon;
                        playerInventory.leftWeapon.hold_LT_Action.PerformAction(playerManager);
                    }
                }
                
                



              
            }

            //if(lt_input && playerInventory.rightWeapon.weaponType == WeaponType.isBow)
            //{
            //    //playerManager.isAiming = true;
            //    playerAttacker.HandleLTActions();
            //}
            else
            { 
                if(playerManager.isAimingArrow)
                {
                    playerManager.isAimingArrow = false;
                    uiManager.CrossHair.SetActive(false);
                    cameraHandler.ResetAimCameraRotation();
                }
                
                

                if (blockingcollider.blockingCollider.enabled)
                {
                    playerManager.isBlocking = false;
                    blockingcollider.DisableBlockingCollider();//can also be done with animation events
                    
                }

              
            }

            //rb_Input = inputActions.PlayerActions.RB.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            
           // unarmed_input = inputActions.PlayerActions.RB.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            //inputActions.PlayerActions.RB.performed += i => rb_Input = true;
            //inputActions.PlayerActions.RT.performed += i => rt_Input = true;
            //rb handles right handed attacks
            //if (rb_Input && playerManager.WeaponEquipped)
            //{

            //    if (playerManager.canDoCombo)
            //    {
            //        comboFlag = true;
            //        playerAttacker.HandleWeaponCombo(playerInventory.rightWeapon);
            //        comboFlag = false;
            //        //playerManager.isInteracting = true;
            //    }

            //    else
            //    {
            //        if (playerManager.isInteracting)
            //        {
            //            return;
            //        }

            //        if (playerManager.canDoCombo)
            //        {
            //            return;
            //        }

            //        playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
            //    }

            //}

            //if (rb_Input && playerManager.IsUnarmed)
            //{
                
            //    playerAttacker.HandleUnarmedPunch(playerInventory.rightWeapon);
            //}


            //if (rt_Input && playerManager.WeaponEquipped)
            //{
            //    //playerManager.WeaponEquipped = true;

            //    playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
            //}

            if (rt_Input)
            {
                //playerManager.IsUnarmed = true;
                // playerManager.WeaponEquipped = false;
                playerAttacker.HandleUnarmedPunch2(playerInventory.rightWeapon);
            }

            if (z_input)
            {
                //playerManager.IsUnarmed = true;
                // playerManager.WeaponEquipped = false;
                //playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
                playerAttacker.HandleFlipKick(playerInventory.rightWeapon);
                //playerAttacker.HandleSecondAttack(playerInventory.rightWeapon);
            }

            //if (playerInventory.weaponsInRightHandSlots[0] && playerInventory.weaponsInLeftHandSlots[0])
            //{
            //  playerManager.IsUnarmed = true;

            //}

            //if (playerInventory.weaponsInRightHandSlots[1] && playerInventory.weaponsInLeftHandSlots[1])
            //{
            //    playerManager.IsUnarmed = true;

            //}

            //if (playerInventory.weaponsInRightHandSlots[2] && playerInventory.weaponsInLeftHandSlots[2])
            //{
            //    playerManager.IsUnarmed = true;
            //}

            //if (playerInventory.weaponsInRightHandSlots[-1])
            //{
            //    playerManager.IsUnarmed = false;
            //}

            //if (playerInventory.weaponsInRightHandSlots[2])
            //{
            //    playerManager.IsUnarmed = false;
            //}

        }

        private void HandleHoldRBInput()
        {

           
                if (Hold_Rb_Input)
                {
                if (playerInventory.rightWeapon.hold_Rb_Action != null)
                {
                    playerManager.UpdateWhichHandCharacterISUsing(true);
                    playerInventory.currentItemBeingUsed = playerInventory.rightWeapon;
                    playerInventory.rightWeapon.hold_Rb_Action.PerformAction(playerManager);
                }
                    
                }


            if (!Hold_Rb_Input)
            {
                animatorhandler.anim.SetBool("IsAiming", false);
            }

            else if (Hold_Rb_Input == false)
            {
                if (playerManager.isAiming)
                {
                    if (playerInventory.rightWeapon.tap_RB_Action != null)
                    {
                        playerManager.UpdateWhichHandCharacterISUsing(true);
                        playerInventory.currentItemBeingUsed = playerInventory.rightWeapon;
                        playerInventory.rightWeapon.tap_RB_Action.PerformAction(playerManager);
                    }
                }
            }

            
           
        }
        private void HandleTapRTInput()
        {
            if (rt_Input)
            {
                if (playerInventory.rightWeapon.tap_RT_Action != null)
                {
                    playerManager.UpdateWhichHandCharacterISUsing(true);
                    playerInventory.currentItemBeingUsed = playerInventory.rightWeapon;
                    playerInventory.rightWeapon.tap_RT_Action.PerformAction(playerManager);
                }
               
            }
        }

        //private void HandleTapLBInput()
        //{
        //    if (lb_Input)
        //    {
        //        if (playerManager.isTwoHanding)
        //        {
        //            playerManager.UpdateWhichHandCharacterISUsing(true);
        //            playerInventory.rightWeapon.tap_LB_Action.PerformAction(playerManager);
        //        }
        //        else
        //        {
        //            playerManager.UpdateWhichHandCharacterISUsing(false);
        //            playerInventory.rightWeapon.tap_LB_Action.PerformAction(playerManager);
        //        }
        //    }
        //}


        private void HandleAiming()
        {

        }

        private void HandleBackStab()
        {
            Critical_Attack_input = inputActions.PlayerActions.CriticalAttack.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            //if (Keyboard.current.xKey.wasPressedThisFrame)
            //{
            //    Critical_Attack_input = true;
            //}

            if (Critical_Attack_input)
            {
                Critical_Attack_input = false;
                playerAttacker.AttemptBackStabOrRiposte();
            }

            //if (Keyboard.current.eKey.IsPressed(1))
            //{
            //    Critical_Attack_input = true;
            //}
        }

        private void HandleTauntInput()
        {
            if (Keyboard.current.cKey.wasPressedThisFrame)
            {
                taunt_input = true;
            }

            if (taunt_input)
            {
                animatorhandler.PlayTargetAnimation("Taunt", true);
                //playerAttacker.Taunt();

                taunt_input = false;
            }
        }

        private void HandleQuickSlotInput()
        {
            //rb_Input = inputActions.PlayerActions.RB.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            //inputActions.PlayerActions.DPADRight.performed += i => Dpad_right = true;

            //Dpad_right = inputActions.PlayerActions.DPADRight.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            //Dpad_left = inputActions.PlayerActions.DPADLeft.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            //Dpad_down = inputActions.PlayerActions.DPADDown.phase == UnityEngine.InputSystem.InputActionPhase.Started;


            if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                Dpad_right = true;
            }



            if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
                Dpad_left = true;
            }

            if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                Dpad_down = true;
            }


            if (Dpad_right)
            {
                playerInventory.ChangeRightWeapon();
                playerManager.WeaponEquipped = true;
                playerManager.IsUnarmed = false;
            }
            else if (Dpad_left)
            {
                playerInventory.ChangeLeftWeapon();
                playerManager.WeaponEquipped = true;
                playerManager.IsUnarmed = false;

            }

            else if (Dpad_down)
            {
                playerManager.IsUnarmed = true;
            }


        }

        private void HandleItemInput()
        {
            if (Keyboard.current.tKey.wasPressedThisFrame)
            {
                a_input = true;
            }
            //if (a_input)
            //{
            //    weaponPickup.PickupItem(playerManager);
            //}



        }

        private void HandleJumpInput()
        {
            Jump_input = inputActions.PlayerActions.Jump.phase == UnityEngine.InputSystem.InputActionPhase.Started;

        }

        private void HandleInventoryInput()
        {
            //Inventory_input = inputActions.PlayerActions.Inventory.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                Inventory_input = true;
            }

            if (InventoryFlag)
            {
                uiManager.UpdateUI();
            }


            if (Inventory_input)
            {
                InventoryFlag = !InventoryFlag;
            

                if (InventoryFlag)
                {
                    uiManager.OpenSelectWindow();
                    uiManager.UpdateUI();
                    uiManager.HUDWindow.SetActive(false);
                }

                else
                {
                    uiManager.CloseSelectWindow();
                    uiManager.CloseAllWindows();
                    uiManager.HUDWindow.SetActive(true);
                }
            }
        }

        private void HandleLockOnInput()
        {
            if (Keyboard.current.fKey.wasPressedThisFrame)
            {
                lockOnInput = true;
            }

            //if (Gamepad.current.rightStickButton.wasPressedThisFrame)
            //{
            //    lockOnInput = true;
            //}

            if (lockOnInput && lockOnFlag == false)
            {
                lockOnInput = false;
                
                cameraHandler.HandleLockOn();

                if(cameraHandler.nearestLockOnTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
                    lockOnFlag = true;
                }
            }

            else if(lockOnInput && lockOnFlag)
            {
                lockOnInput = false;
                lockOnFlag = false;
                cameraHandler.ClearLockOnTargets();
            }

            if(lockOnFlag && right_stick_left)
            {
                right_stick_left = false;
                cameraHandler.HandleLockOn();
                if(cameraHandler.leftLockTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.leftLockTarget;
                }

                
            }
            if (lockOnFlag && right_stick_right)
            {
                right_stick_right = false;
                cameraHandler.HandleLockOn();
                if (cameraHandler.RightLockTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.RightLockTarget;
                }
            }

            cameraHandler.SetCameraHeight();
        }


        private void HandleLockTargets()
        {
            if (Keyboard.current.digit1Key.wasPressedThisFrame)
            {
                right_stick_left = true;

            }

            if (Keyboard.current.digit2Key.wasPressedThisFrame)
            {
                right_stick_right = true;

            }

            //if (Gamepad.current.rightStick.left.wasPressedThisFrame)
            //{
            //    right_stick_left = true;
            //}

            //if (Gamepad.current.rightStick.right.wasPressedThisFrame)
            //{
            //    right_stick_right = true;
            //}
        }

        private void HandleTwoHand()
        {
            inputActions.PlayerActions.TwoHand.performed += i => two_Hand_input = true;
            if (Keyboard.current.yKey.wasPressedThisFrame)
            {
                two_Hand_input = true;
            }

            if (two_Hand_input)
            {
                TwoHandFlag = !TwoHandFlag;
                two_Hand_input = false;
                
                if (TwoHandFlag)
                {
                    //enable two handing for weapons
                    
                    weaponslotmanager.LoadWeaponOnSlot(playerInventory.rightWeapon, false);
                    playerManager.isTwoHanding = true;
                    animatorhandler.PlayTargetAnimation("Two hand idle", false, true);
                }
                else
                {
                    //disable two handing 

                    weaponslotmanager.LoadWeaponOnSlot(playerInventory.rightWeapon, false);
                    weaponslotmanager.LoadWeaponOnSlot(playerInventory.leftWeapon, true);
                    playerManager.isTwoHanding = false;
                }
            }
        }

        private void HandleConsumable()
        {
            inputActions.PlayerActions.X.performed += i => X_input = true;

            if (X_input)
            {
                X_input = false;
                playerInventory.currentConsumable.AttemptoConsumeItem(animatorhandler, weaponslotmanager, playereffectsmanager);
                // use consumable
            }
        }



        private void HandleBowFireInput()
        {
            if (FireFlag)
            {
                if (playerManager.isAiming)
                {
                    FireFlag = false;
                    playerAttacker.FireArrowAction();
                }
            }
        }


    }
}

