using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] DayNightCycle cycle;
    [SerializeField] Transform player;

    [Header("Nigt")]
    [SerializeField] Enemy nightEnemyPrefab;
    [SerializeField] int nightWaveSize = 10;
    [SerializeField] float nightSpawnInterval = 0.5f;

    [Header("Map")]
    [SerializeField] float mapRadius = 30f;
    [SerializeField] float minDistanceFromPlayer = 10f;

    readonly List<Enemy> dayEnemies = new();
    int nightEnemiesLeft;
    Coroutine routine;

    void OnEnable()
    {
        
        cycle.OnNightStarted += HandleNight;
    }

    void OnDisable()
    {
        
        cycle.OnNightStarted -= HandleNight;
    }

    
    void HandleNight()
    {
        StopCurrentRoutine();

   
        foreach (var e in dayEnemies)
            if (e) Destroy(e.gameObject);
        dayEnemies.Clear();

        routine = StartCoroutine(NightRoutine());
    }

    void StopCurrentRoutine()
    {
        if (routine != null) StopCoroutine(routine);
        routine = null;
    }

 

    IEnumerator NightRoutine()
    {
      
        nightEnemiesLeft = nightWaveSize;
        var wait = new WaitForSeconds(nightSpawnInterval);

        for (int i = 0; i < nightWaveSize; i++)
        {
            var enemy = Spawn(nightEnemyPrefab, alwaysChase: true);
            enemy.Died += OnNightEnemyDied;
            yield return wait;
        }
    }

    void OnNightEnemyDied(Enemy e)
    {
        e.Died -= OnNightEnemyDied;
        nightEnemiesLeft--;
        if (nightEnemiesLeft <= 0) cycle.EndNight();
    }

    Enemy Spawn(Enemy prefab, bool alwaysChase)
    {
        Vector3 pos;
        int tries = 0;
        do
        {
            Vector2 p = Random.insideUnitCircle * mapRadius;
            pos = new Vector3(p.x, 0f, p.y);
        }
        while (Vector3.Distance(pos, player.position) < minDistanceFromPlayer && ++tries < 20);

        var enemy = Instantiate(prefab, pos, Quaternion.identity);
        enemy.Init(player, alwaysChase);
        return enemy;
    }
}
