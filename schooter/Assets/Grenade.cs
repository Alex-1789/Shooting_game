using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f; // Time in seconds before explosion
    public float explosionRadius = 5f; // Radius of the explosion effect
    public float explosionForce = 500f; // Force of the explosion
    public GameObject explosionEffect; // Optional: Add a particle effect for the explosion

    private bool hasExploded = false;

    void Start()
    {
        // Start the explosion countdown
        Invoke(nameof(Explode), delay);
    }

    void Explode()
    {
        if (hasExploded) return; // Prevent multiple explosions

        hasExploded = true;

        // Optional: Instantiate explosion particle effect
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Get all nearby objects within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            // Apply explosion force to objects with Rigidbody
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            // Optional: Check for specific objects to damage (e.g., tagged as "Enemy")
            if (nearbyObject.CompareTag("Enemy"))
            {
                // Add damage logic here
                Debug.Log("Enemy hit by explosion!");
            }
        }

        // Destroy the grenade object
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        // Visualize the explosion radius in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
