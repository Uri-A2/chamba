using System;
using UnityEngine;
using static MeleeAttack;

public class Enemy : MonoBehaviour, IDamageable   
{
    [SerializeField] float speed = 3f;
    [SerializeField] float detectionRange = 8f;   
    [SerializeField] float attackRange = 1.5f;
    [SerializeField] int damage = 5;
    [SerializeField] float attackCooldown = 1f;
    [SerializeField] float health = 3f;

    [Header("Drops")]
    [SerializeField] ItemData[] loot;
    [SerializeField] GameObject resourceDrop;

    public event Action<Enemy> Died;

    Transform player;
    bool alwaysChase;
    bool chasing;
    float nextAttackTime;

    public void Init(Transform target, bool chaseForever)
    {
        player = target;
        alwaysChase = chaseForever;
        chasing = chaseForever;
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f) Die();
    }
    void Update()
    {
        if (!player) return;

        float dist = Vector3.Distance(transform.position, player.position);

        if (!chasing && dist <= detectionRange) chasing = true;
        if (!chasing) return;

        if (dist > attackRange)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, player.position, speed * Time.deltaTime);
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        }
        else if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;
            if (player.TryGetComponent(out PlayerHealth ph)) ph.TakeDamage(damage);
        }
    }


    void Die()
    {
        if (player && player.TryGetComponent(out Inventory inventory))
        {
            foreach (ItemData item in loot) inventory.AddItem(item);
        }

        if (resourceDrop != null)
            Instantiate(resourceDrop, transform.position, Quaternion.identity);

        Died?.Invoke(this);
        Destroy(gameObject);
    }
}