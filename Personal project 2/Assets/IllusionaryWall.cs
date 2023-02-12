using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{

    public class IllusionaryWall : MonoBehaviour
    {
        public bool WallHasBeenHit;
        public Material Illusionary;
        public float alpha;
        public float fadeTimer = 2.5f;
        public BoxCollider WallCollider;

        public AudioSource audioSource;
        public AudioClip IlusionarySound;

        private void Start()
        {
            alpha = 255;
        }

        private void Update()
        {
            if (WallHasBeenHit)
            {
                FadeWall();
            }
        }

      

        public void FadeWall()
        {
            alpha = Illusionary.color.a;
            alpha = alpha - Time.deltaTime / fadeTimer;
            Color fade = new Color(1, 1, 1, alpha);
            Illusionary.color = fade;

            if (WallCollider.enabled)
            {
                WallCollider.enabled = false;
                audioSource.PlayOneShot(IlusionarySound);
            }

            if(alpha <= 0)
            {
                Destroy (this);
            }
        }
    }
}
