using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    int current;

    void Awake() => current = maxHealth;

    public void TakeDamage(int amount)
    {
        current -= amount;
        Debug.Log($"Vida del jugador: {current}/{maxHealth}");
        if (current <= 0) Debug.Log("El jugador murió");
    }
}
