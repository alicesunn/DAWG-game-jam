using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSpawnerScript : MonoBehaviour
{
    public GameObject birdPrefab;
    public GameObject chickPrefab;
    public GameObject adultPrefab;
    public GameObject buffPrefab;

    private int nextIndex = 0;

    private const float BUFF_CHANCE = 0.05f;
    private const float SPAWN_CD = 13.0f;
    private float timer = 3.0f;

    private Vector3 birdSpawn = new(17.0f, -5.5f, 0.0f);
    private Vector3 adultSpawn = new(17.0f, -3.94f, 0.0f);
    private Vector3 buffSpawn = new(17.0f, -2.45f, -1.0f);
    private Vector3 chickSpawn = new(21.0f, -6.83f, 0.0f);
    private const float CHICK_SPACE = 2.5f;

    void Update()
    {
        Debug.Log(timer);
        if (timer > 0.0f) timer -= Time.deltaTime;
        else
        {
            // spawn bird + chicks -> cop -> maybe buff
            if (nextIndex == 0)
            {
                Vector3 spawn = birdSpawn;
                Instantiate(birdPrefab, spawn, Quaternion.identity);
                spawn = chickSpawn;
                for (int i = 0; i < 3; i++)
                {
                    Instantiate(chickPrefab, spawn, Quaternion.identity);
                    spawn.x += CHICK_SPACE;
                }
                timer = SPAWN_CD;
            }
            else if (nextIndex == 1)
            {
                Instantiate(adultPrefab, adultSpawn, Quaternion.identity);
                timer = SPAWN_CD;
            }
            else if (nextIndex == 2)
            {
                // Small chance to spawn buff guy after police
                if (Random.Range(0.0f, 1.0f) <= BUFF_CHANCE)
                {
                    Instantiate(buffPrefab, buffSpawn, Quaternion.identity);
                    timer = SPAWN_CD;
                }
            }
            nextIndex++;
            if (nextIndex >= 3) nextIndex = 0;
        }
    }
}
