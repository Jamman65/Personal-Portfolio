using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JO
{
    public class Staminabar : MonoBehaviour
    {
        public Slider slider;

        public void SetMaxStamina(float maxStamina)
        {
            slider.maxValue  = maxStamina * 10;
            slider.value = maxStamina * 10 ;
        }
        public void SetCurrentStamina(float currentStamina)
        {
            slider.value = currentStamina * 10;
        }
    }
}
