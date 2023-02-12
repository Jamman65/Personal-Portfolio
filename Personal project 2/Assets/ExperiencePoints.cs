using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace JO
{


    public class ExperiencePoints : MonoBehaviour
    {
        public Text Experiencetext;

        public void SetExperienceText(int expcount)
        {
            Experiencetext.text = expcount.ToString();
        }
    }
}
