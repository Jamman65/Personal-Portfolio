using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{
    public class HelmetModelChanger : MonoBehaviour
    {
        public List<GameObject> helmetModels;

        private void Awake()
        {
            GetAllHelmetModels();
        }

        private void GetAllHelmetModels()
        {
            int childrenGameObjects = transform.childCount;

            for(int i = 0; i <childrenGameObjects; i++)
            {
                helmetModels.Add(transform.GetChild(i).gameObject);
            }
        }

        public void HideAllHelmetModels()
        {
            foreach(GameObject helmetmodel in helmetModels)
            {
                helmetmodel.SetActive(false);
            }
        }

        public void EquipHelmetModel(string helmetName)
        {
            for(int i = 0; i < helmetModels.Count; i++)
            {
                if(helmetModels[i].name == helmetName) //finds a specifc helmet by name and equips it
                {
                    helmetModels[i].SetActive(true);
                }
            }
        }
    }
}
