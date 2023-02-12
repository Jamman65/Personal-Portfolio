using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{


public class HipModelChanger : MonoBehaviour
{
    public List<GameObject> HipModels;

    private void Awake()
    {
        GetAllHipModels();
    }

    private void GetAllHipModels()
    {
        int childrenGameObjects = transform.childCount;

        for (int i = 0; i < childrenGameObjects; i++)
        {
            HipModels.Add(transform.GetChild(i).gameObject);
        }
    }

    public void HideAllHipModels()
    {
        foreach (GameObject helmetmodel in HipModels)
        {
            helmetmodel.SetActive(false);
        }
    }

    public void EquipHipModel(string TorsoName)
    {
        for (int i = 0; i < HipModels.Count; i++)
        {
            if (HipModels[i].name == TorsoName) //finds a specifc helmet by name and equips it
            {
                HipModels[i].SetActive(true);
            }
        }
    }
}
}
