using UnityEngine;
using System.Collections;

public class EnergyManager : MonoBehaviour {
    private const float ENERGY_UPDATE_TIME_STEP = 0.5f;
    private const int MAX_ENERGY = 100;
    private const int ENERGY_DECREMENT = 1;
    private const int ENERGY_INCREMENT = 1;
    public int energy;
    public int Energy { get { return energy; } }
    public int MaxEnergy { get { return MAX_ENERGY; } }
    private Vector3 lastPosition;


    void Awake(){
        energy = MAX_ENERGY;
        lastPosition = transform.position;
    }

	// Use this for initialization
	void Start () {
        // Exercise 2
        StartCoroutine(UpdateEnergy());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator UpdateEnergy(){
        if (Vector3.Magnitude(lastPosition - transform.position) > 0.1){
            energy -= ENERGY_DECREMENT;
        }
        else{
            energy += ENERGY_INCREMENT;
        }
        if (energy > MAX_ENERGY) energy = MAX_ENERGY;
        else if (energy < 0) energy = 0;
        lastPosition = transform.position;
        yield return new WaitForSeconds(ENERGY_UPDATE_TIME_STEP);
        StartCoroutine(UpdateEnergy());
    }

}
