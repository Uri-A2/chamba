using UnityEngine;
using static MeleeAttack;

public class Tree : MonoBehaviour, IDamageable
{
    public float maxHealth = 20f;
    public GameObject resourceDrop;      

    private float currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("You hit the tree and did " + amount + " damage!");

        if (currentHealth <= 0f)
        {
            if (resourceDrop != null)
            {
                Instantiate(resourceDrop, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}