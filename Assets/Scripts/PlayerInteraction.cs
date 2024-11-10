/*using Item_Scripts;
using UI_Scripts;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public PlayerInventory playerInventory;  // Reference to the PlayerInventory component
    private InventoryUIManager inventoryUIManager;  // Reference to InventoryUIManager

    void Start()
    {
        if (playerInventory == null)
        {
            playerInventory = GetComponent<PlayerInventory>(); // Get the PlayerInventory component if not assigned
        }

        inventoryUIManager = FindObjectOfType<InventoryUIManager>(); // Get reference to InventoryUIManager
        if (inventoryUIManager == null)
        {
            Debug.LogError("InventoryUIManager not found in scene.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player collided with an object that has a Weapon component attached
        Weapon weapon = other.GetComponent<Weapon>(); // Ensure the Weapon script is attached to the object
        if (weapon != null)
        {
            //PickupWeapon(weapon); // Pickup the weapon
        }
    }

    // Function to pick up the weapon
}*/