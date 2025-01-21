using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Camera playerCamera; // Reference to the player's camera
    private bool moveForward = false;
    private bool moveBackward = false;
    private bool moveLeft = false;
    private bool moveRight = false;
    public float moveSpeed = 1f;
    public float jumpForce = 0.5f;
    private bool isOnStairs = false; // To track if the player is on the stairs
    public float stairsClimbSpeed = 1.0f; // Speed for climbing stairs
    public float fallSlowdown = 2.0f;

    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        // Get the forward and right directions relative to the camera's orientation
        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;

        // Remove the y component to ensure movement stays on a horizontal plane
        forward.y = 0;
        right.y = 0;

        // Normalize the directions to ensure consistent speed
        forward.Normalize();
        right.Normalize();
        if ((moveForward || moveBackward || moveLeft || moveRight) && isOnStairs)
        {
            moveDirection.y += stairsClimbSpeed;
        }
        else if (moveForward) { moveDirection += forward; }
        if (moveBackward) moveDirection -= forward;
        if (moveLeft) moveDirection -= right;
        if (moveRight) moveDirection += right;

        if (!isOnStairs)
        {
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }



        // If the player is on stairs, add upward movement


        // Apply the movement to the player
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void MoveForward(bool moveForwardStatus)
    {
        moveForward = moveForwardStatus;
    }

    public void MoveBackward(bool moveBackwardStatus)
    {
        moveBackward = moveBackwardStatus;
    }

    public void MoveLeft(bool moveLeftStatus)
    {
        moveLeft = moveLeftStatus;
    }

    public void MoveRight(bool moveRightStatus)
    {
        moveRight = moveRightStatus;
    }

    public void Jump()
    {
        this.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        // Slow down falling by applying an upward force
        if (this.GetComponent<Rigidbody>().velocity.y < 0) // Only apply when falling
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * fallSlowdown, ForceMode.Acceleration);
        }
    }

    // Detect collision with the stairs
    private void OnCollisionEnter(Collision collision)
    {
       // Debug.Log(collision.gameObject.tag);
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Stairs"))
        {
            isOnStairs = true;
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
            //Debug.Log("on stairs");
        }

         if (collision.gameObject.CompareTag("Enemy Weapon"))
        {
            //Destroy(this.gameObject);
             PointsManager.Instance.ResetPoints();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stairs"))
        {
            isOnStairs = false;
            
            //Debug.Log("not stairs");
        }
    }
}
