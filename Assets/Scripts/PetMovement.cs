using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PetMovement : MonoBehaviour
{
    public Transform player;            // Reference to the player object
    public float speed = 2f;            // Speed at which the pet follows the player
    public float followDistance = 1.5f; // Desired distance from the player
    public float distanceTolerance = 0.1f; // Small buffer to prevent shaking

    private Rigidbody rb;               // Rigidbody component of the pet

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Freeze rotation to prevent tipping over
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (player != null)
        {
            // Calculate the direction and target position behind the player
            Vector3 directionToPlayer = (transform.position - player.position).normalized;
            Vector3 targetPosition = player.position + directionToPlayer * followDistance;

            // Calculate the current distance to the target position
            float currentDistance = Vector3.Distance(transform.position, targetPosition);

            // Only move the pet if it’s outside the tolerance range
            if (currentDistance > distanceTolerance)
            {
                Vector3 moveDirection = (targetPosition - transform.position).normalized * speed * Time.deltaTime;

                // Move the pet without affecting the Y-axis
                rb.MovePosition(new Vector3(
                    transform.position.x + moveDirection.x,
                    rb.position.y,
                    transform.position.z + moveDirection.z
                ));
            }

            // Rotate the pet to face the player
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
        }
    }
}