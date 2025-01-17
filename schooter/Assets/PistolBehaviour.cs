using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform bulletSpawnTransform;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    private int count = 0;
    public Transform playerTransform; // Reference to the player's transform

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(count);
        if(count == 2000){
            Shooti();
            count = 0;
        }
        else {
            count++;
        }
    }

    private void Shooti(){
           if (playerTransform == null) return;

        // Calculate direction toward the player
        Vector3 directionToPlayer = (playerTransform.position - bulletSpawnTransform.position).normalized;

        // Spawn the bullet and apply force
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnTransform.position, Quaternion.identity);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.AddForce(directionToPlayer * bulletSpeed, ForceMode.Impulse);
        }
    }
    
}
