using UnityEngine;
using System.Collections;

public class Damagable : MonoBehaviour {
    public int armourLevel;
    public ParticleSystem fire;
    public GameObject destroyedGameObject;

    void Awake() {
        if (fire) fire.Stop();

    }

    public virtual void InflictDamage(int damage, Vector3 collisionNormal) {
        if (destroyedGameObject) {
            Instantiate(destroyedGameObject, transform.position, transform.rotation);
        }
    }


}
