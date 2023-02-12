using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JO
{
    public class Dash : MonoBehaviour
    {
        public float dashSpeed;
        public float dashTime;
        public float cooldownTimer;
        // Start is called before the first frame update
        void Start()
        {
            cooldownTimer = 3f;
        }

        // Update is called once per frame
        void Update()
        {
            cooldownTimer -= Time.deltaTime;

            if(cooldownTimer <= 0)
            {
                cooldownTimer = 0;
            }

            if (Keyboard.current.qKey.wasPressedThisFrame && cooldownTimer == 0)
            {
                StartCoroutine(Dash1());
                cooldownTimer = 3;
            }
            else
            {
                return;
            }
        }

        IEnumerator Dash1()
        {
            float startTime = Time.time;

            while(Time.time < startTime + dashTime)
            {
                transform.Translate(Vector3.forward * dashSpeed);

                yield return null;
            }
        }
    }
}
