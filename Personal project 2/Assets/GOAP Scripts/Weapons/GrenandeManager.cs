using UnityEngine;
using System.Collections;

public class GrenandeManager : MonoBehaviour {
    public GameObject GrenadePrefab;
    private Transform throwHand;
    private GameObject grenade;
    public float throwForce = 15f;
    private Transform throwDirection;


	void Start () {
        throwHand = GameObject.FindGameObjectWithTag(Tags.GrenadeHand).transform;
        throwDirection = GameObject.FindGameObjectWithTag(Tags.GrenadeThrowDirection).transform;
	}


    public void HoldGrenade() {
        grenade = Instantiate(GrenadePrefab, throwHand.transform.position, throwHand.transform.rotation) as GameObject;
        grenade.transform.parent = throwHand;
        grenade.transform.localPosition = new Vector3(-0.001f, 0.03f, -0.04f);
        grenade.transform.localRotation = Quaternion.Euler(42, 70, 15);
    }
	
	void ThrowGrenade () {
        Debug.Log("Throw");
        grenade.transform.parent = null;
        grenade.GetComponent<Rigidbody>().isKinematic = false;
        grenade.GetComponent<Rigidbody>().useGravity = true;
        grenade.GetComponent<Rigidbody>().AddForce(throwDirection.forward * throwForce, ForceMode.Impulse);
        StartCoroutine(ArmGrenade());
	}

    IEnumerator ArmGrenade() {
        yield return new WaitForSeconds(0.1f);
        grenade.GetComponent<CapsuleCollider>().isTrigger = false;
    }
}
