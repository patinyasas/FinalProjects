using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public InventoryItem[] availableItems; // Array of possible items to spawn
    public float spawnInterval = 2f; // How often items spawn
    public Vector3 spawnAreaSize = new Vector3(10, 1, 10); // Area to spawn items within

    void Start()
    {
        InvokeRepeating("SpawnItem", 0f, spawnInterval); // Spawn items periodically
    }

    void SpawnItem()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            0, // Keep items on the ground
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );

        InventoryItem itemToSpawn = availableItems[Random.Range(0, availableItems.Length)];
        GameObject itemObject = new GameObject("Item");
        itemObject.transform.position = spawnPosition;

        // Add a collider and ItemOnGround script to the item
        Collider itemCollider = itemObject.AddComponent<BoxCollider>();
        itemCollider.isTrigger = true;
        ItemOnGround itemOnGround = itemObject.AddComponent<ItemOnGround>();
        itemOnGround.item = itemToSpawn;
    }
}