using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JO
{
    public class PlayerAttacker : CharacterCombatManager
    {
        CameraHandler camera;
        PlayerAnimatorManager animatorHandler;
        PlayerEquipmentManager playerEquipmentManager;
        public InputHandler inputhandler;
        WeaponSlotManager weaponslotmanager;
        public PlayerManager playermanager;
        PlayerStats playerstats;
        PlayerInventory playerinventory;
        public PlayerEffectsManager playereffects;
        //public string lastAttack;
        //public bool isInCombo;
        //public int tauntvalue = 20;
        //public Animator anim;
        public Vector3 StringPosition;
        public Vector3 FireStringPosition;



        //public string OH_Light_Attack_1 = "OH_Light_Attack_1";
        //public string OH_Light_Attack_2 = "OH_Light_Attack_2";
        //public string Two_Handed_Attack_1 = "Two_Handed_Attack_1";
        //public string Two_Handed_Attack_2 = "Two_Handed_Attack_2";
        //public string OH_Heavy_Attack_1 = "OH_Heavy_Attack_1";
        //public string Parry = "Parry";
        //public string Flip_Kick = "Flip_Kick";
        //public string OH_Running_Attack = "Power_Attack";
        //public string Jumping_Attack = "Jump_Attack";




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
            playereffects = GetComponentInChildren<PlayerEffectsManager>();

        }



        public void HandleHeavyWeaponCombo(WeaponItem weapon)
        {
            //ADD HEAVY ATTACK ANIMATIONS FOR HEAVY COMBO IF I CAN FIND ANIMATIONS LATER
            if (playerstats.currentStamina <= 0)
            {
                return;
            }
            animatorHandler.anim.SetBool("isUsingRightHand", true);
            if (inputhandler.comboFlag)
            {
                animatorHandler.anim.SetBool("canDoCombo", true);
                if (lastAttack == OH_Heavy_Attack_1)
                {
                    animatorHandler.PlayTargetAnimation(OH_Running_Attack, true);
                    isInCombo = true;
                    lastAttack = OH_Running_Attack;


                }

                //else if (lastAttack == weapon.OH_Light_Attack_1 && isInCombo)
                //{
                //    animatorHandler.PlayTargetAnimation("Empty", false);
                //    isInCombo = false;
                //    lastAttack = weapon.OH_Light_Attack_1;
                //}

                else if (lastAttack == Two_Handed_Attack_1)
                {
                    animatorHandler.PlayTargetAnimation(Two_Handed_Attack_2, true);
                }
            }
        }






        public void FireArrowAction()
        {
            //StringLocation stringlocation;
            //stringlocation = player.weaponslotManager.rightHandSlot.GetComponentInChildren<StringLocation>();
            //stringlocation.transform.localPosition = player.playerAttacker.FireStringPosition;
            //Create live arrow at a specific action
            ArrowLocation arrowLocation;
            arrowLocation = weaponslotmanager.rightHandSlot.GetComponentInChildren<ArrowLocation>();

            //Animate the bow firing the arrow
            Animator bowanimator = weaponslotmanager.rightHandSlot.GetComponentInChildren<Animator>();
            anim = bowanimator;
            bowanimator.SetBool("IsDrawn", false);
            bowanimator.Play("Standing Aim Fire");
            Destroy(playereffects.currentRangeFx); //destroys the loaded arrow model

            animatorHandler.PlayTargetAnimation("Standing Aim Fire", true);
            animatorHandler.anim.SetBool("IsAiming", false);
            GameObject liveArrow = Instantiate(playerinventory.currentAmmo.liveAmmoModel, arrowLocation.transform.position, camera.cameraPivotTransform.rotation);
            Rigidbody rigidBody = liveArrow.GetComponentInChildren<Rigidbody>();
            RangedDamageCollider damageCollider = liveArrow.GetComponentInChildren<RangedDamageCollider>();

            if (playermanager.isAimingArrow)
            {
                Ray ray = camera.cameraobject.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hitPoint;

                if (Physics.Raycast(ray, out hitPoint, 100.0f))
                {
                    liveArrow.transform.LookAt(hitPoint.point);
                    Debug.Log(hitPoint.transform.name);
                }
                else
                {
                    liveArrow.transform.rotation = Quaternion.Euler(camera.cameraTransform.localEulerAngles.x, playermanager.lockOnTransform.eulerAngles.y, 0);
                }
            }
            else
            {
                //Give ammo velocity
                if (camera.currentLockOnTarget != null)
                {
                    //Always facing the target once locked on
                    Quaternion arrowRotation = Quaternion.LookRotation(camera.currentLockOnTarget.transform.position - liveArrow.transform.position);
                    liveArrow.transform.rotation = arrowRotation;

                }
                else
                {
                    liveArrow.transform.rotation = Quaternion.Euler(camera.cameraPivotTransform.eulerAngles.x, playermanager.lockOnTransform.eulerAngles.y, 0);
                }
            }




            rigidBody.AddForce(liveArrow.transform.forward * playerinventory.currentAmmo.forwardVelocity);
            rigidBody.AddForce(liveArrow.transform.up * playerinventory.currentAmmo.upwardVelocity);
            rigidBody.useGravity = playerinventory.currentAmmo.useGravity;
            rigidBody.mass = playerinventory.currentAmmo.ammoMass;
            liveArrow.transform.parent = null;


            //Destroy previous loaded arrow fx
            //Set damage
            damageCollider.charactermanager = playermanager;
            damageCollider.ammoItem = playerinventory.currentAmmo;
            damageCollider.CurrentWeaponDamage = playerinventory.currentAmmo.physicalDamage;
        }





        public void HandleFlipKick(WeaponItem weapon)
        {
            if (playerstats.currentStamina <= 0)
            {
                return;
            }
            animatorHandler.PlayTargetAnimation(Flip_Kick, true);
            lastAttack = Flip_Kick;
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









        public void HandleLBActions()
        {
            if (playerinventory.leftWeapon.weaponType == WeaponType.isShieldAttack)
            {
                //perform parry
                PerformLBActions(inputhandler.TwoHandFlag);

            }
            else if (playerinventory.leftWeapon.weaponType == WeaponType.isMeleeAttack)
            {
                if (isInCombo == false)
                {

                    animatorHandler.PlayTargetAnimation(OH_Light_Attack_1, true, false, true);
                    //isInCombo = true;
                    lastAttack = OH_Light_Attack_1;

                }

                if (inputhandler.comboFlag)
                {
                    animatorHandler.anim.SetBool("canDoCombo", true);



                    if (lastAttack == OH_Light_Attack_1)
                    {
                        animatorHandler.PlayTargetAnimation(OH_Light_Attack_2, true, false, true);
                        isInCombo = true;
                        lastAttack = OH_Light_Attack_2;


                    }

                    else
                    {
                        animatorHandler.PlayTargetAnimation(OH_Light_Attack_1, true, false, true);
                        lastAttack = OH_Light_Attack_1;
                    }
                }
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
                animatorHandler.PlayTargetAnimation(Parry, true);
            }
        }







        private void PerformUnarmedAction()
        {
            if (inputhandler.rb_Input)
            {

                HandleUnarmedPunch(playerinventory.rightWeapon);
            }
        }



        public void Taunt()
        {

            playerstats.AddMana(tauntvalue);
        }

        private void CastSpell()
        {
            playerinventory.currentSpell.CastSpell(character);
            animatorHandler.anim.SetBool("isFiringSpell", true);
        }

        public override void AttemptBackStabOrRiposte()
        {
            if (playerstats.currentStamina <= 0)
            {
                return;
            }
            RaycastHit hit;

            if (Physics.Raycast(character.criticalAttackRayCast.position, transform.TransformDirection(Vector3.forward),
                out hit, 0.5f, backStabLayer))
            {
                CharacterManager enemycharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
                DamageCollider rightweapon = weaponslotmanager.RightDamageCollider;

                if (enemycharacterManager != null)
                {
                    //stops the player from backstabbing itself
                    //pull the player into a transform behind the enemy
                    //rotate into the backstab
                    //play animation

                    playermanager.transform.position = enemycharacterManager.backstabCollider.backStabPoint.position;
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

            else if (Physics.Raycast(character.criticalAttackRayCast.position, transform.TransformDirection(Vector3.forward),
                out hit, 0.7f, riposteLayer))
            {
                CharacterManager enemycharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
                DamageCollider rightweapon = weaponslotmanager.RightDamageCollider;

                if (enemycharacterManager != null && enemycharacterManager.canBeRiposted)
                {
                    playermanager.transform.position = enemycharacterManager.RiposteCollider.backStabPoint.position; //places the player on the riposte point

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
                    if (enemycharacterManager.pendingCriticalDamage == 100)
                    {
                        enemycharacterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("BackStab Dead", true);
                    }
                }



            }
        }
    } 
    }
