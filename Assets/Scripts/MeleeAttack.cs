using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [Header("Settings")]
    public float attackRange = 2f;
    public float attackArc = 90f;
    public float attackCooldown = 0.5f;
    public float attackDamage = 10f;
    public LayerMask hitLayers;

    private float nextAttackTime = 0f;
    private Inventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>();
    }
    public interface IDamageable
    {
        void TakeDamage(float amount);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime)
        {
           
            float damage = attackDamage;
            float cooldown = attackCooldown;
            if (inventory != null && inventory.CurrentWeapon != null)
            {
                damage = inventory.CurrentWeapon.damage;
                cooldown = 1f / Mathf.Max(inventory.CurrentWeapon.attackSpeed, 0.01f);
            }

            Attack(damage);
            nextAttackTime = Time.time + cooldown;
        }
    }

    void Attack(float damage)
    {
        Vector3 origin = transform.position;
        Vector3 forward = transform.forward;

        RaycastHit[] hits = Physics.SphereCastAll(
            origin,
            0.5f,
            forward,
            attackRange,
            hitLayers
        );

      
        HashSet<IDamageable> alreadyHit = new HashSet<IDamageable>();

        foreach (RaycastHit hit in hits)
        {
            
            Vector3 toTarget = hit.collider.transform.position - origin;
            toTarget.y = 0f;
            if (Vector3.Angle(forward, toTarget) > attackArc / 2f) continue;

            IDamageable damageable = hit.collider.GetComponentInParent<IDamageable>();
            if (damageable != null && alreadyHit.Add(damageable))
            {
                damageable.TakeDamage(damage);
            }
        }
    }

  
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 forward = transform.forward * attackRange;
        Vector3 left = Quaternion.Euler(0, -attackArc / 2, 0) * forward;
        Vector3 right = Quaternion.Euler(0, attackArc / 2, 0) * forward;

        Gizmos.DrawRay(transform.position, left);
        Gizmos.DrawRay(transform.position, right);
        Gizmos.DrawWireSphere(transform.position + forward, 0.5f);
    }
}