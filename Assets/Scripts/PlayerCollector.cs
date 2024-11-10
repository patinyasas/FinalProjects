using Item_Scripts;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    public Inventory inventory; // Reference to the inventory

    void OnTriggerEnter(Collider other)
    {
        ItemOnGround itemOnGround = other.GetComponent<ItemOnGround>();
        if (itemOnGround != null && inventory != null)
        {
            // Add the item to the inventory
            inventory.AddItem(itemOnGround.item);
            Debug.Log("Item collected: " + itemOnGround.item.itemName);
            Destroy(other.gameObject); // Destroy the item after collecting
        }
        else
        {
            Debug.LogError("Inventory is not assigned or item is not collectible!");
        }
    }

}