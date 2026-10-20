using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemData> items = new List<ItemData>();
    public int resources = 0;

    public ItemData CurrentWeapon { get; private set; }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CycleWeapon();
        }
    }

    public void AddItem(ItemData item)
    {
        if (item == null) return;

        items.Add(item);
        Debug.Log("Received: " + item.itemName + " (" + item.itemType + ")");

        if (item.itemType == ItemType.Weapon && CurrentWeapon == null)
        {
            CurrentWeapon = item;
            Debug.Log("Equipped weapon: " + CurrentWeapon.itemName);
        }
    }

    public void AddResources(int amount)
    {
        resources += amount;
        Debug.Log("Resources: " + resources);
    }

    void CycleWeapon()
    {
        if (items.Count == 0) return;

        int start = CurrentWeapon != null ? items.IndexOf(CurrentWeapon) : -1;

        for (int i = 1; i <= items.Count; i++)
        {
            ItemData candidate = items[(start + i + items.Count) % items.Count];
            if (candidate.itemType == ItemType.Weapon)
            {
                CurrentWeapon = candidate;
                Debug.Log("Switched to: " + CurrentWeapon.itemName);
                return;
            }
        }
    }
}
