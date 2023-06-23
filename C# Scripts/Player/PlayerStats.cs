using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{
    public class PlayerStats : CharacterStats
    {


        public Healthbar healthbar;
        public Staminabar staminabar;
        public ManaBar manabar;
        public bool InDamage;

        public float staminaRegen = 1f;
        public float staminaRegenTimer = 0f;
        public float tauntTimer = 0f;
        public bool tauntCooldown;
        PlayerAnimatorManager animatorhandler;
        PlayerManager playermanager;



        private void Awake()
        {
            animatorhandler = GetComponentInChildren<PlayerAnimatorManager>();
            playermanager = GetComponent<PlayerManager>();
        }

        public override void HandlePoiseResetTimer()
        {
            if (PoiseResetTimer > 0)
            {
                PoiseResetTimer = PoiseResetTimer - Time.deltaTime;
            }
            else if (PoiseResetTimer <= 0 && !playermanager.isInteracting)
            {
                totalPoiseDefense = armorPoiseBonus;
            }
        }
        // Start is called before the first frame update
        void Start()
        {


            currentHealth = maxHealth;
            maxHealth = SetMaxHealth();
            healthbar.SetMaxHealth(maxHealth);
            healthbar.SetCurrentHealth(currentHealth);

            maxStamina = SetMaxStamina();
            currentStamina = maxStamina ;
            staminabar.SetMaxStamina(maxStamina);
            staminabar.SetCurrentStamina(currentStamina);

            maxMana = SetMaxMana();
            currentMana = maxMana;
            manabar.SetMaxMana(maxMana);
            manabar.SetCurrentMana(currentMana);
            //StartCoroutine(Time());
        }
        private void Update()
        {
            tauntTimer -= Time.deltaTime;

            if (tauntTimer < 0)
            {
                tauntTimer = 0;
            }

            else if (tauntTimer <= 0.5f)
            {
                tauntCooldown = false;
            }

            


        }

        private void FixedUpdate()
        {
            healthbar.SetMaxHealth(maxHealth);
            healthbar.SetCurrentHealth(currentHealth);
        }




        public override void TakeDamage(int damage, string damageAnimation = "Damage")
        {
            if (playermanager.isInvulnerable)
            {
                return;
            }

            //if(playermanager.isBlocking == true)
            //{
            //    damageAnimation = "Block";
            //}

            base.TakeDamage(damage, damageAnimation );

            //if (isDead)
            //{
            //    return;
            //}

            // currentHealth = currentHealth -= damage;

            healthbar.SetCurrentHealth(currentHealth);

            animatorhandler.PlayTargetAnimation(damageAnimation, true);
            // Time();
            // animatorhandler.PlayTargetAnimation("Empty", false);



            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animatorhandler.PlayTargetAnimation("Dying", true);
                isDead = true;
            }
        }

        public override void TakeDamageWithoutAnimations(int damage)
        {
            currentHealth = currentHealth -= damage;




            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
            }
        }

        public override void TakeStamina(float stamina)
        {
            currentStamina = currentStamina -= stamina;
            staminabar.SetCurrentStamina(currentStamina);
        }

        public void RegenerateStamina()
        {
            if (playermanager.isInteracting)
            {
                staminaRegenTimer = 0;
            }
            //if(currentStamina < 0)
            //{
            //    playermanager.isInteracting = false;


            //}

            else
            {
                //playermanager.isInteracting = false;
                staminaRegenTimer += Time.deltaTime;

                if (currentStamina < maxStamina && staminaRegenTimer > 1f)
                {
                    currentStamina += staminaRegen * Time.deltaTime;
                    staminabar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
                }
            }


        }

        public override void HealCharacter(int healAmount)
        {
            base.HealCharacter(healAmount);
            healthbar.SetCurrentHealth(currentHealth);
        }

        public void DeductMana(int mana)
        {
            currentMana = currentMana - mana;
            if (currentMana < 0)
            {
                currentMana = 0;
            }

            manabar.SetCurrentMana(currentMana);
        }
        public void AddMana(int mana)
        {

            if (!tauntCooldown)
            {
                currentMana = currentMana + mana;
                manabar.SetCurrentMana(currentMana);
                tauntTimer = 10f;
                tauntCooldown = true;
            }






        }

        public void AddExp(int experience)
        {
            ExperiencePoints = ExperiencePoints + experience;
        }



        // IEnumerator Time()
        //{
        //    yield return new WaitForSecondsRealtime(2);
        //}
    }
}
