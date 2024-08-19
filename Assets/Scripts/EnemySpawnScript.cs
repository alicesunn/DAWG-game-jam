using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    private StateScript state;

    private const float REDUCE_TIME = 30.0f;       // every [reduce] seconds,
    private const float COOLDOWN_DECREMENT = 0.5f; // decrease cooldown by [decrement]
    private const float MIN_COOLDOWN = 2.5f;
    private const float RADIUS = 10.0f; // enemies will spawn this far away
    private const float KID_CHANCE = 0.1f;  // 0 - 0.1 -> spawn kid (10%)
    private const float TEEN_CHANCE = 0.4f; // 0.1 - 0.4 -> spawn teen (30%)
                                            // else, spawn adult (60%)

    private float cooldown = 5.0f;
    private float timer;
    private float reduceTimer;

    void Start()
    {
        state = GameObject.Find("State").GetComponent<StateScript>();
        timer = cooldown;
        reduceTimer = REDUCE_TIME;
    }

    void Update()
    {
        // Spawn an enemy every [cooldown] seconds
        if (timer > 0.0f) timer -= Time.deltaTime;
        else
        {
            SpawnEnemy();
            timer = cooldown;
        }

        // Decrease cooldown every [reduce] seconds
        if (reduceTimer > 0.0f) reduceTimer -= Time.deltaTime;
        else if (cooldown > MIN_COOLDOWN) {
            reduceTimer = REDUCE_TIME;
            cooldown -= COOLDOWN_DECREMENT;
        }
    }

    public void SpawnEnemy()
    {
        Vector3 playerPos = state.player.transform.position;

        // *This RNG logic only works if kid chance <= teen chance <= adult chance
        GameObject prefab;
        float randomType = Random.Range(0.0f, 1.0f);
        if (randomType <= KID_CHANCE) prefab = state.kidPrefab;
        else if (randomType <= TEEN_CHANCE) prefab = state.teenPrefab;
        else prefab = state.adultPrefab;

        // Spawn in a random direction at a distance of minSpawnRad
        float theta = Random.Range(1.0f, 360.0f) * Mathf.Deg2Rad;
        Vector3 dir = new Vector3((float)Mathf.Cos(theta), (float)Mathf.Sin(theta), 0.0f).normalized;
        Vector3 pos = playerPos + dir * RADIUS;

        GameObject e = Instantiate(prefab, pos, Quaternion.identity);
        state.enemies.Add(e);
    }
}
