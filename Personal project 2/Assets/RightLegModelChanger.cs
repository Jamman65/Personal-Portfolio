using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{


    public class RightLegModelChanger : MonoBehaviour
    {
        public List<GameObject> RightLegModels;

        private void Awake()
        {
            GetAllRightLegModels();
        }

        private void GetAllRightLegModels()
        {
            int childrenGameObjects = transform.childCount;

            for (int i = 0; i < childrenGameObjects; i++)
            {
                RightLegModels.Add(transform.GetChild(i).gameObject);
            }
        }

        public void HideAllRightLegModels()
        {
            foreach (GameObject helmetmodel in RightLegModels)
            {
                helmetmodel.SetActive(false);
            }
        }

        public void EquipRightLegModel(string TorsoName)
        {
            for (int i = 0; i < RightLegModels.Count; i++)
            {
                if (RightLegModels[i].name == TorsoName) //finds a specifc helmet by name and equips it
                {
                    RightLegModels[i].SetActive(true);
                }
            }
        }
    }
}

