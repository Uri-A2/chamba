using UnityEngine;

public class ResourcePickup : MonoBehaviour
{
    public int amount = 5;

    void OnTriggerEnter(Collider other)
    {
        Inventory inventory = other.GetComponentInParent<Inventory>();
        if (inventory == null) return;

        inventory.AddResources(amount);
        Destroy(gameObject);
    }
}
