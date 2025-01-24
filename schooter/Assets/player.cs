using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Camera playerCamera; 
    private bool moveForward = false;
    private bool moveBackward = false;
    private bool moveLeft = false;
    private bool moveRight = false;
    public float moveSpeed = 5f;
    public float jumpForce = 0.5f;
    private bool isOnStairs = false; 
    public float stairsClimbSpeed = 1.0f; 
    public float fallSlowdown = 2.0f;

    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;

        forward.y = 0;
        right.y = 0;

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

         if (collision.gameObject.CompareTag("Enemy Weapon"))
        {
            PointsManager.Instance.ResetPoints();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stairs"))
        {
            isOnStairs = false;

        }
    }
}
