using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
    public int maxInventoryCapacity;
    private AudioSource audioSource;
    public AudioClip sound;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    protected void PlayPickupSound() {
        if (sound) {
            audioSource.PlayOneShot(sound, 1.0f);
        }
    }

}
