using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform bulletSpawnTransform;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    private int count = 0;
    public Transform playerTransform; 

    // Start is called before the first frame update
    void Start()
    {
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (count ==100)
        {
            ShootEnemy();
            count = 0;
        }
        else
        {
            count++;
        }
    }

    private void ShootEnemy()
    {
        if (playerTransform == null) return;

        Vector3 directionToPlayer = (playerTransform.position - bulletSpawnTransform.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnTransform.position, Quaternion.identity);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.AddForce(directionToPlayer * bulletSpeed, ForceMode.Impulse);
        }
    }

}
