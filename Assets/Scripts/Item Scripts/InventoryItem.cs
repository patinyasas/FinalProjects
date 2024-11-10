using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public Sprite icon;

    // Stat modifiers for the item
    public int strengthModifier;
    public int intelligenceModifier;
    public int vitalityModifier;
    public int agilityModifier;
}