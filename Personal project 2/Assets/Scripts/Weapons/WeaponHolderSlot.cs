using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{
    public class WeaponHolderSlot : MonoBehaviour
    {
        public Transform parentOverride;
        public Transform shieldOveride;
        public bool isLeftHandSlot;
        public bool isRightHandSlot;
        public bool isBackSlot;
        public WeaponItem currentWeapon;

        public GameObject currentWeaponModel;

        public void UnloadWeapon()
        {
            if(currentWeaponModel != null)
            {
                currentWeaponModel.SetActive(false);
            }
        }

        public void UnloadWeaponAndDestroy()
        {
            if(currentWeaponModel != null)
            {
                Destroy(currentWeaponModel);
            }
        }

        public void LoadWeaponModel(WeaponItem weaponItem)
        {
            UnloadWeaponAndDestroy();

            if(weaponItem == null)
            {
                //unload weapon
                UnloadWeapon();
                return;
            }

            GameObject model = Instantiate(weaponItem.modelPrefab) as GameObject;
            if(model != null)
            {
                if(parentOverride != null)
                {
                    model.transform.parent = parentOverride;
                }

                else
                {
                    model.transform.parent = transform;
                }

              

                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
                model.transform.localScale = Vector3.one;
            }

            if (model != null && weaponItem.isShieldAttack)
            {
                if (shieldOveride != null)
                {
                    model.transform.parent = shieldOveride;
                }

                else
                {
                    model.transform.parent = transform;
                }



                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
                model.transform.localScale = Vector3.one;
            }

                currentWeaponModel = model;
        }
    }
}
