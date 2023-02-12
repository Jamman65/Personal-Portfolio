using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour {
    public GameObject explosion;
    protected Vector3 position;
    protected Collider[] colliders;
    public int damage = 100;
    public int explosionPower = 1500;
    public float explosionRadius = 10f;
    //[HideInInspector]
    //public Vector3 trajectory;
    //public ParticleEmitter trailSmoke;
    //public float speed = 10f;
    protected float firedTime;
    public float lifeTime = 10f;
    protected bool timerStarted = false;



    protected virtual void OnCollisionEnter(Collision collision) {
        Debug.Log("Grenade Collided " + collision.gameObject.tag);
        // Ensure projectile doesn't collide with weapon it is mounted on
        if (collision.gameObject.tag != "weapon") {
            // Rotate the object so that the y-axis faces along the normal of the surface
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 position = contact.point;
            Instantiate(explosion, position, rot);
            Explode(position, explosionPower, explosionRadius);
            // If the projectile collided with an gameobject with a health manager component
            HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();
            if (healthManager) {
                // Inflict damage
                healthManager.ApplyDamage(damage);
            }
            // Get the gameObject that was hit by the grenade
            GameObject hitObject = collision.contacts[0].otherCollider.gameObject;
            if (hitObject) {
                // Does the gameObject have Damagable component (vehicles)
                Damagable damagable = hitObject.GetComponent<Damagable>();
                if (damagable) {
                    damagable.InflictDamage(damage, collision.contacts[0].normal);
                }
            }
            // Destroy the projectile
            Destroy(gameObject);
        }

    }

    protected virtual void Explode(Vector3 position, float power, float radius) {
        // Get all colliders within the explosion area
        colliders = Physics.OverlapSphere(position, radius);
        foreach (Collider hit in colliders) {
            // For physics objects apply a force
            if (hit.GetComponent<Rigidbody>()) {
                hit.GetComponent<Rigidbody>().AddExplosionForce(explosionPower, position, explosionRadius);
            }
            HealthManager hm = hit.gameObject.GetComponent<HealthManager>();
            // For those object with a health manager
            if (hm) {
                // The objects proximity to the centre of the explosion
                float distance = (position - hit.gameObject.transform.position).magnitude;
                Debug.Log("Distance : " + distance + "  : " + hit.name);
                // Damage is proportional to the distance from the centre of the explosion
                int relativeDamage = Mathf.RoundToInt(damage * (1 - (distance / explosionRadius)));
                hm.ApplyDamage(relativeDamage);
                Debug.Log("Hit " + hit.name + " : " + relativeDamage);

            }
        }

    }


    // Destroy the projectile if its life time expires
    protected void CheckLifeTime() {
        if (!timerStarted) {
            timerStarted = true;
            firedTime = Time.time;
        }
        else {
            if (Time.time > (firedTime + lifeTime)) {
                Destroy(gameObject);
            }
        }
    }
}
