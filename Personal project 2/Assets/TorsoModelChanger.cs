using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{


    public class TorsoModelChanger : MonoBehaviour
    {
        public List<GameObject> torsoModels;

        private void Awake()
        {
            GetAllTorsoModels();
        }

        private void GetAllTorsoModels()
        {
            int childrenGameObjects = transform.childCount;

            for (int i = 0; i < childrenGameObjects; i++)
            {
                torsoModels.Add(transform.GetChild(i).gameObject);
            }
        }

        public void HideAllTorsoModels()
        {
            foreach (GameObject helmetmodel in torsoModels)
            {
                helmetmodel.SetActive(false);
            }
        }

        public void EquipTorsoModel(string TorsoName)
        {
            for (int i = 0; i < torsoModels.Count; i++)
            {
                if (torsoModels[i].name == TorsoName) //finds a specifc helmet by name and equips it
                {
                    torsoModels[i].SetActive(true);
                }
            }
        }
    }
}
