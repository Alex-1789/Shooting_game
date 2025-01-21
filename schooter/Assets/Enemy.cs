using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    public float moveSpeed = 0.5f; // Speed at which the enemy follows the player
    public float fallSlowdown = 2.0f;
    public EnemySpawner spawner;
    private bool isOnStairs = false; // To track if the player is on the stairs

  
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

      /*  GameObject uiObject = GameObject.FindWithTag("Points");
        if (uiObject != null)
        {
            UIPoints = uiObject.GetComponent<TextMeshProUGUI>();
        }
*/

    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            RotateTowardsPlayer(); // Face the player
            FollowPlayer();        // Move toward the player
        }


        //UpdatePointsText();
    }

    private void FollowPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > 1f) // Stop moving when closer than 1 unit
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
        // Slow down falling by applying an upward force
        if (this.GetComponent<Rigidbody>().velocity.y < 0) // Only apply when falling
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * fallSlowdown, ForceMode.Acceleration);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.tag);

        if (collision.gameObject.CompareTag("Stairs"))
        {
            isOnStairs = true;
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
           // Debug.Log("on stairs");
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
