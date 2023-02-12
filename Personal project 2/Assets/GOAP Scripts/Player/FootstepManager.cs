using UnityEngine;
using System.Collections;

public class FootstepManager : MonoBehaviour {
    private CharacterController characterController;
    private AudioSource audio;
    public AudioClip woodFootStepSound;
    public AudioClip concreteFootStepSound;
    private RaycastHit hitData;
    private string lastMaterial;


	// Use this for initialization
	void Start () {
        characterController = GetComponent<CharacterController>();
        audio = GetComponent<AudioSource>();
        audio.loop = true;
        audio.volume = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
     
        bool hit = Physics.Raycast(transform.position, Vector3.down, out hitData, 5.0f);
        if (hit) {
            if (hitData.collider.tag == Tags.Wood) {
                audio.clip = woodFootStepSound;
            }
            else if (hitData.collider.tag == Tags.Concrete) {
                audio.clip = concreteFootStepSound;
            }
            else {
                audio.clip = null;
            }
        }
        if ((characterController.velocity.magnitude > 0.1f) && (audio.clip)) {
            if (!audio.isPlaying || lastMaterial != hitData.collider.tag) {
                audio.pitch = characterController.velocity.magnitude * 1.6f;
                audio.Play();
            }

        }
        else {
            audio.Stop();
        }
        if (hitData.collider != null) {
            lastMaterial = hitData.collider.tag;
        }


	}
}
