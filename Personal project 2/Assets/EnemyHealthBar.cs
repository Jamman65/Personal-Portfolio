using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JO
{


    public class EnemyHealthBar : MonoBehaviour
    {
        public Slider slider;
        float timeUntilBarIsHidden = 0;
        EnemyStats enemystats;

        private void Awake()
        {
            slider = GetComponentInChildren<Slider>();
            enemystats = GetComponentInParent<EnemyStats>();
        }

        public void SetHealth(int health)
        {
            health = enemystats.currentHealth;
            slider.value = health;
            
            //timeUntilBarIsHidden = 3;
        }

        public void SetMaxHealth(int maxHealth)
        {
            maxHealth = enemystats.maxHealth;
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }

        private void Update()
        {

            slider.value = enemystats.currentHealth;
            slider.maxValue = enemystats.maxHealth;

            if(enemystats.currentHealth <= 0)
            {
                Destroy(gameObject);
            }
            
            //timeUntilBarIsHidden = timeUntilBarIsHidden - Time.deltaTime;

            //if(slider != null)
            //{
            //    if (timeUntilBarIsHidden <= 0)
            //    {
            //        timeUntilBarIsHidden = 0;
            //        slider.gameObject.SetActive(false);
            //    }
            //    else
            //    {
            //        if (!slider.gameObject.activeInHierarchy)
            //        {
            //            slider.gameObject.SetActive(true);
            //        }
            //    }

            //    if (slider.value <= 0)
            //    {
            //        Destroy(gameObject);
            //    }
            //}
            
        }
    }
}
