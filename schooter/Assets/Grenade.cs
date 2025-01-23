using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f; // Time in seconds before explosion
    public float explosionRadius = 5f; // Radius of the explosion effect
    public float explosionForce = 500f; // Force of the explosion
    public GameObject explosionEffect; // Particle effect for the explosion

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

            if (nearbyObject.CompareTag("Enemy"))
            {
                // Add points
                PointsManager.Instance.AddPoints(1);

                // Spawn a new enemy if the spawner exists
                Enemy enemyScript = nearbyObject.GetComponent<Enemy>();
                if (enemyScript != null && enemyScript.spawner != null)
                {
                    enemyScript.spawner.SpawnEnemy();
                }

                // Destroy the enemy object
                Destroy(nearbyObject.gameObject);
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
