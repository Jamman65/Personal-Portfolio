using UnityEngine;
using System.Collections;

public class CollisionEffectsManager : MonoBehaviour {
    public GameObject woodBulletDecal;
    public GameObject metalBulletDecal;
    public GameObject concreteBulletDecal;
    public GameObject woodParticleEffect;
    public GameObject metalParticleEffect;
    public GameObject concreteParticleEffect;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ApplyDecal(RaycastHit hitData) {
       // Debug.Log("DECAL " + hitData.collider.tag);
        switch (hitData.collider.tag) {
            case "wood": if (woodBulletDecal) {
                    GameObject clone = Instantiate(woodBulletDecal, (hitData.point + (hitData.normal * 0.0001f)), Quaternion.FromToRotation(Vector3.up, hitData.normal)) as GameObject;
                }; break;
            case "concrete": if (concreteBulletDecal) {
                    GameObject clone = Instantiate(concreteBulletDecal, (hitData.point + (hitData.normal * 0.0001f)), Quaternion.FromToRotation(Vector3.up, hitData.normal)) as GameObject;
                }; break;
            case "metal": if (metalBulletDecal) {
                    GameObject clone = Instantiate(metalBulletDecal, (hitData.point + (hitData.normal * 0.0001f)), Quaternion.FromToRotation(Vector3.up, hitData.normal)) as GameObject;
                }; break;
            case "dirt": if (woodBulletDecal) {
                    GameObject clone = Instantiate(woodBulletDecal, (hitData.point + (hitData.normal * 0.0001f)), Quaternion.FromToRotation(Vector3.up, hitData.normal)) as GameObject;
                }; break;
        }
    }


    public void ApplyParticleEffect(RaycastHit hitData) {
       // Debug.Log("Apply Effect");
        switch (hitData.collider.tag) {
            case "wood": if (woodParticleEffect) {
                    GameObject damage = GameObject.Instantiate(woodParticleEffect, hitData.point, Quaternion.FromToRotation(Vector3.up, hitData.normal)) as GameObject;
                }; break;
            case "concrete": if (concreteParticleEffect) {
                    GameObject damage = GameObject.Instantiate(concreteParticleEffect, hitData.point, Quaternion.FromToRotation(Vector3.up, hitData.normal)) as GameObject;
                }; break;
            case "metal": if (metalParticleEffect) {
                    GameObject damage = GameObject.Instantiate(metalParticleEffect, hitData.point, Quaternion.FromToRotation(Vector3.up, hitData.normal)) as GameObject;
                }; break;
            case "dirt": if (woodParticleEffect) {
                    GameObject damage = GameObject.Instantiate(woodParticleEffect, hitData.point, Quaternion.FromToRotation(Vector3.up, hitData.normal)) as GameObject;
                }; break;
            case "glass": {
                BreakableObject breakable = hitData.collider.gameObject.GetComponent<BreakableObject>();
                if (breakable != null) {
                    breakable.triggerBreak();
                }
                }; break;
        }
    }

}
