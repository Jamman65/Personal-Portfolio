using UnityEngine;
using System.Collections;

public class ProjectileManager : MonoBehaviour {
	public GameObject explosion;
	protected Vector3 position;
	protected Collider[] colliders;
    public int damage = 100;
    public int explosionPower = 5500;
    public float explosionRadius = 10f;
    [HideInInspector]
    public Vector3 trajectory;
  //  public ParticleEmitter trailSmoke;
    public float speed = 10f;
    protected float firedTime;
    public float lifeTime = 10f;
    protected bool timerStarted = false;
    protected Vector3 firedPosition;


    public void Awake(){
   
    }

    public void Update() {
        if (!gameObject.transform.parent) {
            // Exercise 6.5
            CheckLifeTime();
            transform.Translate(trajectory * speed * Time.deltaTime, Space.World);
        }
    }

    protected virtual void OnCollisionEnter(Collision collision) {
            // Rotate the object so that the y-axis faces along the normal of the surface
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 position = contact.point;
            Instantiate(explosion, position, rot);
            // Exercise 6.6
            Explode(position, explosionPower, explosionRadius);
            // Destroy the projectile
            Destroy(gameObject);
            Damagable damage = collision.gameObject.GetComponent<Damagable>();
            if (damage) {
                damage.InflictDamage(0, contact.normal);
            }
            Destroy(collision.gameObject);
    }

    protected virtual void Explode(Vector3 position, float power, float radius) {
        // Get all colliders within the explosion area
        colliders = Physics.OverlapSphere(position, radius);
        foreach (Collider hit in colliders) {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            // For physics objects apply a force
            if (rb) {
                rb.AddExplosionForce(explosionPower, position, explosionRadius);
                Debug.Log("Hit " + position);
            }
        }
    }

    //// Destroy the projectile if its life time expires
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
