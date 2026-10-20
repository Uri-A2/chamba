using UnityEngine;

public enum ItemType
{
    Weapon,
    Equipment
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemType itemType;

    [Header("Weapon stats")]
    public float damage = 10f;
    public float attackSpeed = 2f;   

    [Header("Equipment stats")]
    public float extraHealth = 0f;
}
