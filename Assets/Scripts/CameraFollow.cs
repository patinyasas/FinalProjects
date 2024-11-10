using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;           // Reference to the player's transform
    public Vector3 offset = new Vector3(0, 5, -10); // Offset from the player
    public float smoothSpeed = 0.125f; // Smoothness factor for following

    void LateUpdate()
    {
        if (player != null)
        {
            // Calculate the target position based on player position and offset
            Vector3 targetPosition = player.position + offset;

            // Smoothly interpolate the camera's position towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

            // Optional: Make the camera look at the player
            transform.LookAt(player);
        }
    }
}