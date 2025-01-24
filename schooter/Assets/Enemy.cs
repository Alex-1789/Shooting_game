using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    public float moveSpeed = 0.5f;
    public float fallSlowdown = 2.0f;
    public EnemySpawner spawner;
    private bool isOnStairs = false; 

  
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

    void Update()
    {
        if (playerTransform != null)
        {
            RotateTowardsPlayer();
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > 1f)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
    private void RotateTowardsPlayer()
    {
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void FixedUpdate()
    {
        if (this.GetComponent<Rigidbody>().velocity.y < 0)
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * fallSlowdown, ForceMode.Acceleration);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Stairs"))
        {
            isOnStairs = true;
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }


        if (collision.gameObject.CompareTag("Weapon"))
        {
            Debug.Log("punkt");
            PointsManager.Instance.AddPoints(1);
            
            if (spawner != null)
            {
                spawner.SpawnEnemy();
            }

            Destroy(this.gameObject);
        }
    }
}
