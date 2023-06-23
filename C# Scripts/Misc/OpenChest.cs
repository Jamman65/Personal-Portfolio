using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{


    public class OpenChest : Interactable
    {
        Animator anim;
        OpenChest openChest;
        public Transform playerStandingPosition;
        public GameObject itemspawner;
        public WeaponItem IteminChest;
        public string AnimationName;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            openChest = GetComponent<OpenChest>();
        }
        public override void Interact(PlayerManager playerManager)
        {
            //rotate player towards chest
            //lock transform infront of chest
            //open the chest lid and animate the player
            //spawn an item inside the chest


            Vector3 rotationDirection = transform.position - playerManager.transform.position;
            rotationDirection.y = 0;
            rotationDirection.Normalize();

            Quaternion tr = Quaternion.LookRotation(rotationDirection);
            Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 300 * Time.deltaTime);
            playerManager.transform.rotation = targetRotation;

            playerManager.OpenChestInteraction(playerStandingPosition);
            anim.Play(AnimationName);
            StartCoroutine(SpawnItemInChest());

            WeaponPickup weaponpickup = itemspawner.GetComponent<WeaponPickup>();

            if(weaponpickup != null)
            {
                weaponpickup.weapon = IteminChest;
            }
          
        }
        private IEnumerator SpawnItemInChest()
        {
            yield return new WaitForSeconds(1f);
            Instantiate(itemspawner, transform);
            Destroy(openChest);
        }
    }
}