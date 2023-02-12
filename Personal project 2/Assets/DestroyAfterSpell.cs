using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{


    public class DestroyAfterSpell : MonoBehaviour
    {
        CharacterManager charactermanager;

        private void Awake()
        {
            charactermanager = FindObjectOfType<CharacterManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (charactermanager.isFiringSpell)
            {
                Destroy(gameObject);
            }
            else
            {
                return;

            }
        }
    }
}