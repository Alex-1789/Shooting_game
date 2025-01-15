using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string weaponName;
    public float fireRate = 0.5f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public Transform fireRotation;

    private float nextFireTime = 0f;

    public virtual void Shoot()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            // Instantiate the projectile
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position , fireRotation.rotation);

            // Apply force to the projectile's Rigidbody
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.position*5f); // Adjust the force multiplier (e.g., 1000) as needed

        }
    }

}
