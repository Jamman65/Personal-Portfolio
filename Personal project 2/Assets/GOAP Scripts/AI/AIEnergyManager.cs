using UnityEngine;
using System.Collections;

public class AIEnergyManager : MonoBehaviour {
    private const float ENERGY_UPDATE_TIME_STEP = 0.5f;
    private const int MAX_ENERGY = 100;
    private const int ENERGY_DECREMENT = 1;
    private const int ENERGY_INCREMENT = 1;
    private int energy;
    public int Energy { get { return energy; } }
    public int MaxEnergy { get { return MAX_ENERGY; } }
    private CharacterController characterController;


    void Awake() {
        characterController = GetComponent<CharacterController>();
        energy = MAX_ENERGY;
    }

    // Use this for initialization
    void Start() {
        // Exercise 2
        StartCoroutine(UpdateEnergy());
    }

    // Update is called once per frame
    void Update() {

    }

    IEnumerator UpdateEnergy() {
        if (characterController.velocity.magnitude > 0.1) {
            energy -= ENERGY_DECREMENT;
        }
        else {
            energy += ENERGY_INCREMENT;
        }
        if (energy > MAX_ENERGY) energy = MAX_ENERGY;
        else if (energy < 0) energy = 0;
        yield return new WaitForSeconds(ENERGY_UPDATE_TIME_STEP);
        StartCoroutine(UpdateEnergy());
    }
}
