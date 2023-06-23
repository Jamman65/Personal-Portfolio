using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{

    public class CharacterCombatManager : MonoBehaviour
    {


        public string OH_Light_Attack_1 = "OH_Light_Attack_1";
        public string OH_Light_Attack_2 = "OH_Light_Attack_2";
        public string Two_Handed_Attack_1 = "Two_Handed_Attack_1";
        public string Two_Handed_Attack_2 = "Two_Handed_Attack_2";
        public string OH_Heavy_Attack_1 = "OH_Heavy_Attack_1";
        public string Parry = "Parry";
        public string Flip_Kick = "Flip_Kick";
        public string OH_Running_Attack = "Power_Attack";
        public string Jumping_Attack = "Jump_Attack";

        public Transform backstabTransform;
        public Transform riposteTransform;

        public string lastAttack;
        public bool isInCombo;
        public int tauntvalue = 20;
        public CharacterManager character;
        public float criticalAttackRange = 0.7f;
        public LayerMask characterLayer;
        public LayerMask backStabLayer = 1 << 12;
        public LayerMask riposteLayer = 1 << 8;
        public Animator anim;
        public int pendingCriticalDamage;

        private void Awake()
        {
            character = GetComponent<CharacterManager>();
        }
        public virtual void AttemptBlock(DamageCollider attackingWeapon, float physicaldamage, string blockAnimation)
        {
            if(character.characterstats.currentStamina <= 0)
            {
                character.animatormanager.PlayTargetAnimation("Parried", true);

            }
            else
            {
                character.animatormanager.PlayTargetAnimation(blockAnimation, true);
            }
        }


        private void CastSpell()
        {
            character.characterinventorymanager.currentSpell.CastSpell(character);
            //animator.anim.SetBool("isFiringSpell", true);
        }

        IEnumerator MoveCharacterToEnemyBackStabPosition(CharacterManager characterPerformingBackstab)
        {
            for(float timer = 0.05f; timer <0.5f; timer = timer + 0.05f)
            {
                Quaternion backstabRotation = Quaternion.LookRotation(characterPerformingBackstab.transform.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, backstabRotation, 1);
                transform.parent = characterPerformingBackstab.characterCombatManager.backstabTransform;
                transform.localPosition = characterPerformingBackstab.characterCombatManager.backstabTransform.localPosition;
                transform.parent = null;
                Debug.Log("Coroutine running");
                yield return new WaitForSeconds(0.05f);
            }
        }

        public void GetBackStabbed(CharacterManager characterPerformingBackstab)
        {
            //play sound fx
            character.isBackstabbed = true;

            //lock position
            StartCoroutine(MoveCharacterToEnemyBackStabPosition(characterPerformingBackstab));
            character.animatormanager.PlayTargetAnimation("BackStab Dead", true);
        }

        IEnumerator MoveCharacterToEnemyRipostePosition(CharacterManager characterPerformingRiposte)
        {
            for (float timer = 0.05f; timer < 0.5f; timer = timer + 0.05f)
            {
                Quaternion backstabRotation = Quaternion.LookRotation(-characterPerformingRiposte.transform.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, backstabRotation, 1);
                transform.parent = characterPerformingRiposte.characterCombatManager.riposteTransform;
                transform.localPosition = characterPerformingRiposte.characterCombatManager.riposteTransform.localPosition;
                transform.parent = null;
                Debug.Log("Coroutine running");
                yield return new WaitForSeconds(0.05f);
            }
        }

        public void GetRiposted(CharacterManager characterPerformingRiposte)
        {
            //play sound fx
            character.isRiposted = true;

            //lock position
            StartCoroutine(MoveCharacterToEnemyRipostePosition(characterPerformingRiposte));
            character.animatormanager.PlayTargetAnimation("BackStab Dead", true);
        }


        public virtual void AttemptBackStabOrRiposte()
        {
            Debug.Log("can attempt riposte  HELO");
            if (character.isInteracting)
            {
                return;
            }

            if (character.characterstats.currentStamina <= 0)
            {
                return;
            }

            RaycastHit hit;
            Debug.Log("can attempt riposte  HELO1");
            if (Physics.Raycast(character.criticalAttackRayCast.transform.position,
             character.transform.TransformDirection(Vector3.forward), out hit, criticalAttackRange, characterLayer))
            {
                CharacterManager enemyCharacter = hit.transform.GetComponent<CharacterManager>();
                Vector3 directionFromCharacterToEnemy = transform.position - enemyCharacter.transform.position;
                float dotValue = Vector3.Dot(directionFromCharacterToEnemy, enemyCharacter.transform.forward);

                Debug.Log("Current Dot value is" + dotValue);

                if (enemyCharacter.canBeRiposted)
                {
                    Debug.Log("can attempt riposte");
                 
                    if (dotValue <=1.2f && dotValue >= 0.6f)
                    {
                        //Attempt a riposte on target
                        AttemptRiposteAction(hit);
                        return;
                    }
                    return;
                }



                if (dotValue >= -0.7 && dotValue <= -0.6f)
                {
                    //Attempt a backstab on target
                    Debug.Log("Attempt backstab Test");
                    AttemptBackStabAction(hit);

                }
            }
        }

        private void AttemptBackStabAction(RaycastHit hit)
        {
            Debug.Log("Attempt backstab");
            CharacterManager enemyCharacter = hit.transform.GetComponent<CharacterManager>();
            Debug.Log("ATTEWMPT", enemyCharacter);
            enemyCharacter.isBackstabbed = true;

            if (enemyCharacter != null)
            {
                if(enemyCharacter.isBackstabbed || enemyCharacter.isRiposted)
                {
                    EnableIsInvulnerable();
                   
                    character.isPerformingBackstab = true;

                    character.animatormanager.PlayTargetAnimation("Stab", true);
                    enemyCharacter.animatormanager.PlayTargetAnimation("BackStab Dead", true);

                    float criticalDamage = (character.characterinventorymanager.rightWeapon.criticalDamageMultiplier * (character.characterinventorymanager.rightWeapon.baseDamage));

                    int roundedCriticalDamage = Mathf.RoundToInt(criticalDamage);
                    enemyCharacter.characterCombatManager.pendingCriticalDamage = roundedCriticalDamage;
                    enemyCharacter.characterCombatManager.GetBackStabbed(character);
                    enemyCharacter.isBackstabbed = false;
                }
            }
        }

        private void AttemptRiposteAction(RaycastHit hit)
        {
            Debug.Log("Attempt backstab");
            CharacterManager enemyCharacter = hit.transform.GetComponent<CharacterManager>();
            Debug.Log("ATTEWMPT", enemyCharacter);
            enemyCharacter.isRiposted = true;

            if (enemyCharacter != null)
            {
                if (enemyCharacter.isBackstabbed || enemyCharacter.isRiposted)
                {
                    EnableIsInvulnerable();
                    character.isPerformingRiposte = true;

                    character.animatormanager.PlayTargetAnimation("Stab", true);
                    enemyCharacter.animatormanager.PlayTargetAnimation("BackStab Dead", true);

                    float criticalDamage = (character.characterinventorymanager.rightWeapon.criticalDamageMultiplier * (character.characterinventorymanager.rightWeapon.baseDamage));

                    int roundedCriticalDamage = Mathf.RoundToInt(criticalDamage);
                    enemyCharacter.characterCombatManager.pendingCriticalDamage = roundedCriticalDamage;
                    enemyCharacter.characterCombatManager.GetRiposted(character);
                    enemyCharacter.isRiposted = false;
                    enemyCharacter.canBeRiposted = false;
                    character.characterstats.healthLevel += pendingCriticalDamage;
                }
            }
        }
        private void EnableIsInvulnerable()
        {
            character.animatormanager.anim.SetBool("isInvulnerable", true);
            character.isInvulnerable = true;
        }

        public void ApplyPendingDamage()
        {
            character.characterstats.TakeDamageWithoutAnimations(pendingCriticalDamage);
        }

        public void EnableCanBeParried()
        {
            character.canBeParried = true;
        }

        public void DisableCanBeParried()
        {
            character.canBeParried = false;
        }

    }
}
