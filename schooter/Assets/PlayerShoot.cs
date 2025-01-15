using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerSchoot : MonoBehaviour
{
    public Transform bulletSpawnTransform;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public Transform grenadeSpawnTransform;
    public GameObject grenadePrefab;
    public float grenadeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnTransform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnTransform.forward * bulletSpeed, ForceMode.Impulse);
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioSource.clip);
    }

    public void Grenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, grenadeSpawnTransform.position, Quaternion.identity);
        grenade.GetComponent<Rigidbody>().AddForce(grenadeSpawnTransform.forward * grenadeSpeed, ForceMode.Impulse);
    }
}
