using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PlayerStats))]
public class PlayerController : MonoBehaviour
{
    public float baseMovementSpeed = 5f;  // Base movement speed (before scaling with agility)
    public float attackRange = 5f;        // Maximum range to check for nearby enemies
    public float hitboxRadius = 3f;       // Radius within which the player can hit an enemy
    public int baseAttackDamage = 10;     // Base attack damage, which scales with strength
    public float jumpForce = 7f;          // Jump force
    public LayerMask groundLayer;         // Layer to identify ground for jumping

    private Rigidbody rb;
    private PlayerStats playerStats;
    private bool isGrounded = false;      // Check if player is on the ground

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        HandleMovement();

        if (Input.GetMouseButtonDown(0)) // Left mouse click to attack
        {
            Attack();
        }
    }

    void HandleMovement()
    {
        // Movement logic
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Scale the movement speed based on agility
        float movementSpeed = baseMovementSpeed + (playerStats.agility * 0.1f); // You can change the multiplier to your preference

        // Move the player based on movement speed influenced by agility
        Vector3 moveDirection = new Vector3(moveHorizontal, 0, moveVertical).normalized;
        rb.MovePosition(rb.position + moveDirection * movementSpeed * Time.deltaTime);

        // Jumping logic
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void Jump()
    {
        // Jump with jumpForce
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    void Attack()
    {
        // Calculate the actual attack damage based on the player's strength
        int attackDamage = baseAttackDamage + (playerStats.strength * 2);

        // Find all colliders within the attack range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider collider in hitColliders)
        {
            // Check if the collider belongs to an enemy
            EnemyStats enemy = collider.GetComponent<EnemyStats>();
            if (enemy != null)
            {
                // Calculate the distance to the enemy
                float distanceToEnemy = Vector3.Distance(transform.position, collider.transform.position);

                // Check if the enemy is within the hitbox radius
                if (distanceToEnemy <= hitboxRadius)
                {
                    // Apply damage to the enemy
                    enemy.TakeDamage(attackDamage);
                    Debug.Log("Hit enemy for " + attackDamage + " damage!");
                    return; // Exit after hitting the first enemy
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Ground check for jumping
        if ((groundLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Reset ground check when leaving ground
        if ((groundLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            isGrounded = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw the attack range and hitbox radius for visualization in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, hitboxRadius);
    }
}
