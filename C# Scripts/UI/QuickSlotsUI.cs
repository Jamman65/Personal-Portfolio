using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace JO
{


    public class QuickSlotsUI : MonoBehaviour
    {
        public Image leftweaponicon;
        public Image rightweaponicon;

        public void UpdateWeaponQuickSlots(bool isLeft, WeaponItem weapon)
        {
            if (isLeft == false)
            {
                if (weapon.itemIcon != null)
                {
                    rightweaponicon.sprite = weapon.itemIcon;
                    rightweaponicon.enabled = true;
                }

                else
                {
                    rightweaponicon.sprite = null;
                    rightweaponicon.enabled = false;
                }
            }

            else
            {
                if (weapon.itemIcon != null)
                {
                    leftweaponicon.sprite = weapon.itemIcon;
                    leftweaponicon.enabled = true;
                }
                else
                {
                    leftweaponicon.sprite = null;
                    leftweaponicon.enabled = false;
                }
            }
        }
    }
}
