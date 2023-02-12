using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{
    public class PlayerAnimatorManager : AnimatorManager
    {
        PlayerManager playerManager;
        PlayerStats playerstats;
        public InputHandler inputhandler;
        public PlayerController playercontroller;
        int vertical;
        int horizontal;


        public void Initalize()
        {
            playerManager = GetComponentInParent<PlayerManager>();
            playerstats = GetComponentInParent<PlayerStats>();
            anim = GetComponent<Animator>();
            playercontroller = GetComponentInParent<PlayerController>();
            inputhandler = GetComponentInParent<InputHandler>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }

        public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement, bool isSprinting)
        {
            #region Vertical
            float v = 0;

            if(verticalMovement > 0 && verticalMovement < 0.55f)
            {
                v = 0.5f;
            }

            else if(verticalMovement > 0.55f)
            {
                v = 1;
            }

            else if(verticalMovement < 0 && verticalMovement > -0.55f)
            {
                v = -0.5f;
            }

            else if(verticalMovement < -0.55f)
            {
                v = -1;
            }

            else
            {
                v = 0;
            }
            #endregion


            #region Horizontal

            float h = 0;

            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                h = 0.5f;
            }

            else if (horizontalMovement > 0.55f)
            {
                h = 1;
            }

            else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                h = -0.5f;
            }

            else if (horizontalMovement < -0.55f)
            {
                h = -1;
            }

            else
            {
                h = 0;
            }
            #endregion

            if (isSprinting)
            {
                v = 2;
                h = horizontalMovement;
            }

            anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);

        }

      

        public void CanRotate()
        {
            anim.SetBool("canRotate", true);
        }

        public void StopRotation()
        {
            anim.SetBool("canRotate", false);
        }

        public void EnableCombo()
        {
            anim.SetBool("canDoCombo", true);
        }
        public void DisableCombo()
        {
            anim.SetBool("canDoCombo", false);
        }

        private void OnAnimatorMove()
        {
            if(playerManager.isInteracting == false)
            {
                return;
            }

            float delta = Time.deltaTime;
            playercontroller.rigidbody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            playercontroller.rigidbody.velocity = velocity;
        }

        public void EnableisInvulnerable()
        {
            anim.SetBool("isInvulnerable", true);
            
        }

        public void DisableisInvulnerable()
        {
            anim.SetBool("isInvulnerable", false);
        }

        public override void TakeCriticalDamageAnimationEvent()
        {
            playerstats.TakeDamageWithoutAnimations(playerManager.pendingCriticalDamage);
            playerManager.pendingCriticalDamage = 0;
        }

        public void enableIsParrying()
        {
            playerManager.isParrying = true;
        }

        public void disableIsParrying()
        {
            playerManager.isParrying = false;
        }

        public void EnableCanBeRiposted()
        {
            playerManager.canBeRiposted = true;
        }

        public void DisableCanBeRiposted()
        {
            playerManager.canBeRiposted = false;
        }
    }
}
