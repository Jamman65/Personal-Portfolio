using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JO
{
    public class PlayerAttacker : MonoBehaviour
    {
        CameraHandler camera;
        PlayerAnimatorManager animatorHandler;
        PlayerEquipmentManager playerEquipmentManager;
        public InputHandler inputhandler;
        WeaponSlotManager weaponslotmanager;
        public PlayerManager playermanager;
        PlayerStats playerstats;
        PlayerInventory playerinventory;
        public string lastAttack;
        public bool isInCombo;
        public int tauntvalue = 20;

        LayerMask backStabLayer = 1 << 12;
        LayerMask riposteLayer = 1 << 8;

        public void Awake()
        {
            playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
            animatorHandler = GetComponent<PlayerAnimatorManager>();
            inputhandler = GetComponentInParent<InputHandler>();
            weaponslotmanager = GetComponent<WeaponSlotManager>();
            playermanager = GetComponentInParent<PlayerManager>();
            playerinventory = GetComponentInParent<PlayerInventory>();
            playerstats = GetComponentInParent<PlayerStats>();
            camera = FindObjectOfType<CameraHandler>();

        }

        public void HandleWeaponCombo(WeaponItem weapon)
        {
            if (playerstats.currentStamina <= 0)
            {
                return;
            }
            animatorHandler.anim.SetBool("isUsingRightHand", true);
            if (inputhandler.comboFlag)
            {
                animatorHandler.anim.SetBool("canDoCombo", true);
                if (lastAttack == weapon.OH_Light_Attack_1)
                {
                    animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_2, true);
                    isInCombo = true;
                    lastAttack = weapon.OH_Light_Attack_2;


                }

                //else if (lastAttack == weapon.OH_Light_Attack_1 && isInCombo)
                //{
                //    animatorHandler.PlayTargetAnimation("Empty", false);
                //    isInCombo = false;
                //    lastAttack = weapon.OH_Light_Attack_1;
                //}

                else if(lastAttack == weapon.Two_Handed_Attack_1)
                {
                    animatorHandler.PlayTargetAnimation(weapon.Two_Handed_Attack_2, true);
                }
            }
           
        }

        public void HandleLightAttack(WeaponItem weapon)
        {
            if (playerstats.currentStamina <= 0)
            {
                return;
            }
            weaponslotmanager.attackingweapon = weapon;

            animatorHandler.anim.SetBool("isUsingRightHand", true);
            if (inputhandler.TwoHandFlag)
            {
                animatorHandler.PlayTargetAnimation(weapon.Two_Handed_Attack_1, true);
                lastAttack = weapon.Two_Handed_Attack_1;
            }
            else if (isInCombo == false)
            {

                animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_1, true);
                isInCombo = true;
                lastAttack = weapon.OH_Light_Attack_1;

            }

            if (isInCombo || lastAttack == weapon.OH_Light_Attack_2) 
            {
                
                animatorHandler.PlayTargetAnimation("Empty", false);
                isInCombo = false;
            }
           


        }

        

        public void HandleSecondAttack(WeaponItem weapon)
        {
            if (playerstats.currentStamina <= 0)
            {
                return;
            }
            animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_2, true);
            lastAttack = weapon.OH_Light_Attack_2;
        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {

            if (playerstats.currentStamina <= 0)
            {
                return;
            }

            //if (inputhandler.TwoHandFlag)
            //{
            //    animatorHandler.PlayTargetAnimation(weapon.Two_Handed_Attack_2, true);
            //    lastAttack = weapon.Two_Handed_Attack_2;
            //}
            animatorHandler.anim.SetBool("isUsingRightHand", true);
            weaponslotmanager.attackingweapon = weapon;
            animatorHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true);
            lastAttack = weapon.OH_Heavy_Attack_1;
            
      
        }

        public void HandleFlipKick(WeaponItem weapon)
        {
            if (playerstats.currentStamina <= 0)
            {
                return;
            }
            animatorHandler.PlayTargetAnimation(weapon.Flip_Kick, true);
            lastAttack = weapon.Flip_Kick;
        }

        public void HandleUnarmedPunch(WeaponItem weapon)
        {
            if (playerstats.currentStamina <= 0)
            {
                return;
            }
            animatorHandler.PlayTargetAnimation(weapon.UnarmedAttack1, true);
            lastAttack = weapon.UnarmedAttack1;
        }

        public void HandleUnarmedPunch2(WeaponItem weapon)
        {
            if (playerstats.currentStamina <= 0)
            {
                return;
            }
            animatorHandler.PlayTargetAnimation(weapon.UnarmedAttack2, true);
            lastAttack = weapon.UnarmedAttack2;
        }

        public void HandleRbActions()
        {

            if (playerinventory.rightWeapon.isMeleeAttack)
            {
                //handle melee attacks
                PerformRBMeleeAction();
            }

            if (playerinventory.rightWeapon.isUnarmedAttack)
            {
                PerformUnarmedAction();
            }

            else if (playerinventory.rightWeapon.isSpellcaster)
            {
                //handle magic spells
                performRBMagicAction(playerinventory.rightWeapon);
            }

            else if (playerinventory.rightWeapon.isHealthcaster)
            {
                //handle healing spells
                performRBMagicAction(playerinventory.rightWeapon);
            }
            else if (playerinventory.rightWeapon.isPyrocaster)
            {
                //handle pyro spells
                performRBMagicAction(playerinventory.rightWeapon);
            }

       

            
        }

        public void HandleLBActions()
        {
            if (playerinventory.leftWeapon.isShieldAttack)
            {
                //perform parry
                PerformLBActions(inputhandler.TwoHandFlag);

            }
            else if (playerinventory.leftWeapon.isMeleeAttack)
            {

            }
        }

        private void PerformLBActions(bool isTwoHanding)
        {
            if (playermanager.isInteracting)
            {
                return;
            }

            if (isTwoHanding)
            {
                
            }
            else
            {
                animatorHandler.PlayTargetAnimation(playerinventory.leftWeapon.Parry, true);
            }
        }

        public void HandleLTActions()
        {
            PerformLTBlockingAction();
        }

        private void PerformRBMeleeAction()
        {
            //if (Keyboard.current.eKey.wasPressedThisFrame)
            //{
            //    inputhandler.rb_Input = true;
            //}



            //if (Gamepad.current.xButton.wasPressedThisFrame)
            //{
            //    inputhandler.rb_Input = true;
            //}

            if (inputhandler.rb_Input && playermanager.WeaponEquipped)
            {

                if (playermanager.canDoCombo)
                {
                    inputhandler.comboFlag = true;
                    HandleWeaponCombo(playerinventory.rightWeapon);
                    inputhandler.comboFlag = false;
                    //playerManager.isInteracting = true;
                }

                else
                {
                    if (playermanager.isInteracting)
                    {
                        return;
                    }

                    if (playermanager.canDoCombo)
                    {
                        return;
                    }

                    HandleLightAttack(playerinventory.rightWeapon);
                }

            }
        }

        private void PerformUnarmedAction()
        {
            if (inputhandler.rb_Input)
            {

                HandleUnarmedPunch(playerinventory.rightWeapon);
            }
        }

        private void performRBMagicAction(WeaponItem weapon)
        {
            if (playermanager.isInteracting)
            {
                return;
            }

            if (weapon.isHealthcaster)
            {
                if (playerinventory.currentSpell != null && playerinventory.currentSpell.isHealthSpell)
                {
                    //check for mana/focus points
                    //attempt to cast chosen spell
                    if (playerstats.currentMana >= playerinventory.currentSpell.manaCost)
                    {
                        playerinventory.currentSpell.AttemptToCastSpell(animatorHandler, playerstats, weaponslotmanager);
                    }

                    else
                    {
                        //Put animation for no mana to cast a spell
                        return;
                    }


                }
            }

            else if (weapon.isPyrocaster)
            {
                if (playerinventory.currentSpell != null && playerinventory.currentSpell.isPyroSpell)
                {
                    //check for mana/focus points
                    //attempt to cast chosen spell
                    if (playerstats.currentMana >= playerinventory.currentSpell.manaCost)
                    {
                        playerinventory.currentSpell.AttemptToCastSpell(animatorHandler, playerstats, weaponslotmanager);
                    }

                    else
                    {
                        //Put animation for no mana to cast a spell
                        return;
                    }


                }
            }
        }

        public void Taunt()
        {
            
            playerstats.AddMana(tauntvalue);
        }

        private void CastSpell()
        {
            playerinventory.currentSpell.CastSpell(animatorHandler, playerstats,weaponslotmanager,camera);
            animatorHandler.anim.SetBool("isFiringSpell", true);
        }

        public void AttemptBackStab()
        {
            if (playerstats.currentStamina <= 0)
            {
                return;
            }
            RaycastHit hit;

            if (Physics.Raycast(inputhandler.criticalAttackRayCast.position, transform.TransformDirection(Vector3.forward),
                out hit, 0.5f, backStabLayer))
            {
                CharacterManager enemycharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
                DamageCollider rightweapon = weaponslotmanager.RightDamageCollider;

                if(enemycharacterManager != null)
                {
                    //stops the player from backstabbing itself
                    //pull the player into a transform behind the enemy
                    //rotate into the backstab
                    //play animation

                    playermanager.transform.position = enemycharacterManager.backstabCollider.CriticalDamageStandPoint.position;
                    Vector3 rotationDirection = playermanager.transform.root.eulerAngles;
                    rotationDirection = hit.transform.position - playermanager.transform.position;
                    rotationDirection.y = 0;
                    rotationDirection.Normalize();
                    Quaternion tr = Quaternion.LookRotation(rotationDirection);
                    Quaternion targetRotation = Quaternion.Slerp(playermanager.transform.rotation, tr, 500 * Time.deltaTime);
                    playermanager.transform.rotation = targetRotation;

                    int criticalDamage = playerinventory.rightWeapon.criticalDamageMultiplier * rightweapon.CurrentWeaponDamage;
                    enemycharacterManager.pendingCriticalDamage = criticalDamage;
                    

                    animatorHandler.PlayTargetAnimation("Stab", true);
                    enemycharacterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("BackStab Dead", true);

                }
            }

            else if (Physics.Raycast(inputhandler.criticalAttackRayCast.position, transform.TransformDirection(Vector3.forward),
                out hit, 0.7f, riposteLayer))
            {
                CharacterManager enemycharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
                DamageCollider rightweapon = weaponslotmanager.RightDamageCollider;

                if(enemycharacterManager!= null && enemycharacterManager.canBeRiposted)
                {
                    playermanager.transform.position = enemycharacterManager.RiposteCollider.CriticalDamageStandPoint.position; //places the player on the riposte point

                    //rotates the player towards the enemy
                    Vector3 rotationDirection = playermanager.transform.root.eulerAngles;
                    rotationDirection = hit.transform.position - playermanager.transform.position;
                    rotationDirection.y = 0;
                    rotationDirection.Normalize();
                    Quaternion tr = Quaternion.LookRotation(rotationDirection);
                    Quaternion targetRotation = Quaternion.Slerp(playermanager.transform.rotation, tr, 500 * Time.deltaTime);
                    playermanager.transform.rotation = targetRotation;

                    //controls the damage of the riposte and applies it  
                    int criticalDamage = playerinventory.rightWeapon.criticalDamageMultiplier * rightweapon.CurrentWeaponDamage;
                    enemycharacterManager.pendingCriticalDamage = criticalDamage;

                    animatorHandler.PlayTargetAnimation("Stab", true);
                    enemycharacterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Parried", true);
                    if(enemycharacterManager.pendingCriticalDamage == 100)
                    {
                        enemycharacterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("BackStab Dead", true);
                    }
                }
               
            }
        }

        private void PerformLTBlockingAction()
        {
            if (playermanager.isInteracting)
            {
                return;
            }

            if (playermanager.isBlocking)
            {
                return;
            }

            animatorHandler.PlayTargetAnimation("Block", false, true);
            playerEquipmentManager.OpenBlockingCollider();
            playermanager.isBlocking = true;
        }
    }
}
