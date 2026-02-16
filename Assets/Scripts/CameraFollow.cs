using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;       // Assign your player sphere
    public Vector3 offset = new Vector3(0f, 10f, -10f); // Camera offset from player
    public float smoothSpeed = 0.125f;                  // Smoothness factor

    void LateUpdate()
    {
        if (player == null)
            return;

        // Desired camera position
        Vector3 desiredPosition = player.position + offset;

        // Smoothly move camera
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Optional: make the camera look at the player
        transform.LookAt(player);
    }
}
