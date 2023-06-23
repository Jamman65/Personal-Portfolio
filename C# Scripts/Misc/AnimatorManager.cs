using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JO
{


    public class AnimatorManager : MonoBehaviour
    {
        public Animator anim;
        public bool canRotate;
        public CharacterManager character;

        public void PlayTargetAnimation(string targetAnim, bool isInteracting, bool canRotate = false, bool mirrorAnimation = false)
        {

            anim.applyRootMotion = isInteracting;
            anim.SetBool("canRotate", canRotate);
            anim.SetBool("isInteracting", isInteracting);
            anim.SetBool("IsMirrored", mirrorAnimation);
            anim.CrossFade(targetAnim, 0.05f);
        }

        public void PlayTargetAnimationRootRotation(string targetAnim, bool isInteracting)
        {
            anim.applyRootMotion = isInteracting;
            anim.SetBool("RootMotionRotate", true);
            anim.SetBool("isInteracting", isInteracting);
            anim.CrossFade(targetAnim, 0.05f);
        }

        public virtual void TakeCriticalDamageAnimationEvent()
        {

        }

        public virtual void enableIsParrying()
        {
            character.isParrying = true;
        }

        public virtual void disableIsParrying()
        {
            character.isParrying = false;
        }


    }
}
