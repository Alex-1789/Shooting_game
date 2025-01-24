using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    private Vector3 lastPosition;
    public float rotationSpeed = 0.1f;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 delta = touch.deltaPosition;

                float currentRotationX = NormalizeAngle(transform.rotation.eulerAngles.x);

                float newRotationX = currentRotationX - delta.y * rotationSpeed;

                newRotationX = Mathf.Clamp(newRotationX, -30f, 30f);

                float newRotationY = transform.rotation.eulerAngles.y + delta.x * rotationSpeed;

                transform.rotation = Quaternion.Euler(newRotationX, newRotationY, 0);
            }
        }
    }

    private float NormalizeAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }
}
