using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JO
{


    public class AnimatorManager : MonoBehaviour
    {
        public Animator anim;
        public bool canRotate;

        public void PlayTargetAnimation(string targetAnim, bool isInteracting, bool canRotate = false)
        {

            anim.applyRootMotion = isInteracting;
            anim.SetBool("canRotate", canRotate);
            anim.SetBool("isInteracting", isInteracting);
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


    }
}
