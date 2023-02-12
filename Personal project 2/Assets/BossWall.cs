using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{

    public class BossWall : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void ActivateWall()
        {
            gameObject.SetActive(true);
        }

        public void DeactivateWall()
        {
            gameObject.SetActive(false);
        }
    }
}
