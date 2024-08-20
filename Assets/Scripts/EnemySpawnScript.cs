using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    private StateScript state;

    private const float REDUCE_TIME = 10.0f;       // every [reduce] seconds,
    private const float COOLDOWN_DECREMENT = 0.5f; // decrease cooldown by [decrement]
    private const float MIN_COOLDOWN = 1.75f;
    private const float RADIUS = 20.0f; // enemies will spawn this far away
    private const float KID_CHANCE = 0.1f;
    private const float TEEN_CHANCE = 0.6f;

    private float cooldown = 4.5f;
    private float timer;
    private float reduceTimer;

    private bool isWinning = false;

    void Awake()
    {
        state = GameObject.Find("State").GetComponent<StateScript>();
        timer = cooldown;
        reduceTimer = REDUCE_TIME;
    }

    void Update()
    {
        if (!isWinning)
        {
            // Spawn 1-3 enemies every [cooldown] seconds
            if (timer > 0.0f) timer -= Time.deltaTime;
            else if (state.enemies.Count < 100)
            {
                int num = Random.Range(1, 4);
                for (int i = 0; i < num; i++)
                {
                    SpawnEnemy();
                }
                timer = cooldown;
            }

            // Decrease cooldown every [reduce] seconds
            if (reduceTimer > 0.0f) reduceTimer -= Time.deltaTime;
            else if (cooldown > MIN_COOLDOWN)
            {
                reduceTimer = REDUCE_TIME;
                cooldown -= COOLDOWN_DECREMENT;
                if (cooldown < MIN_COOLDOWN) cooldown = MIN_COOLDOWN;
            }
        }
    }

    public void SpawnEnemy()
    {
        Vector3 playerPos = state.player.transform.position;

        // *This RNG logic only works if kid chance <= teen chance
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
        e.GetComponent<Health>().SetState(state);
        e.GetComponent<AIChase>().SetState(state);
        state.enemies.Add(e);
    }

    // Called when a chick activates
    public void DecreaseCooldown()
    {
        if (cooldown > MIN_COOLDOWN)
        {
            reduceTimer = REDUCE_TIME;
            cooldown -= COOLDOWN_DECREMENT;
        }
    }

    public void OnWin()
    {
        isWinning = true;
        // Spawn a bunch of enemies to surround player
        for (int i = 0; i < 100; i++)
        {
            if (state.enemies.Count >= 100) break;
            SpawnEnemy();
        }
    }
}