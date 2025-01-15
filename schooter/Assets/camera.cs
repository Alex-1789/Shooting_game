using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    private Vector3 lastPosition;
    public float rotationSpeed = 0.1f;

    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // If the touch phase is moved (dragging)
            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 delta = touch.deltaPosition;

                // Get the current rotation and normalize the X value to a range of -180 to 180 degrees
                float currentRotationX = NormalizeAngle(transform.rotation.eulerAngles.x);

                // Calculate the new rotation based on touch delta
                float newRotationX = currentRotationX - delta.y * rotationSpeed;

                // Clamp the rotation to stay within -30 and 0 degrees
                newRotationX = Mathf.Clamp(newRotationX, -30f, 30f);

                float newRotationY = transform.rotation.eulerAngles.y + delta.x * rotationSpeed;

                // Apply the new rotation
                transform.rotation = Quaternion.Euler(newRotationX, newRotationY, 0);
            }
        }
    }

    // Helper function to normalize an angle to the range -180 to 180 degrees
    private float NormalizeAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }
}
