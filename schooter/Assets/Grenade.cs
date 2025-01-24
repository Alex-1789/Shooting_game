using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float explosionRadius = 5f;
    public float explosionForce = 500f;
    public GameObject explosionEffect;

    private bool hasExploded = false;

    void Start()
    {
        Invoke(nameof(Explode), delay);
    }

    void Explode()
    {
        if (hasExploded) return;

        hasExploded = true;

        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            if (nearbyObject.CompareTag("Enemy"))
            {
                PointsManager.Instance.AddPoints(1);

                Enemy enemyScript = nearbyObject.GetComponent<Enemy>();
                if (enemyScript != null && enemyScript.spawner != null)
                {
                    enemyScript.spawner.SpawnEnemy();
                }

                Destroy(nearbyObject.gameObject);
            }
        }

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
