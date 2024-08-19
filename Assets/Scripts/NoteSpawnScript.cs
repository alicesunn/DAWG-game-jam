using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    private StateScript state;

    private const float NOTE_COOLDOWN = 0.0f;
    private const float MIN_SPAWN_RADIUS = 20.0f;
    private const float MAX_SPAWN_RADIUS = 40.0f;

    private float noteTimer = 0.0f;

    private float debugTimer = NOTE_COOLDOWN;

    void Start()
    {
        state = GameObject.Find("State").GetComponent<StateScript>();
    }

    void Update()
    {
        // Spawn new note [cooldown] seconds after previous note was picked up
        if (!state.notePickup) {
            if (noteTimer > 0.0f)
            {
                noteTimer -= Time.deltaTime;
                if (noteTimer < debugTimer)
                {
                    Debug.Log("time until next note ~" + noteTimer);
                    debugTimer -= 5.0f;
                }
            }
            else
            {
                state.notePickup = SpawnNote();
                noteTimer = NOTE_COOLDOWN;
                debugTimer = NOTE_COOLDOWN - 5.0f;
}
        }
    }

    public GameObject SpawnNote()
    {

        Vector3 playerPos = state.player.transform.position;

        // First pick a random direction in which to spawn the note
        float theta = Random.Range(1.0f, 360.0f) * Mathf.Deg2Rad;

        // Hypothetically already normalized but whatever
        Vector3 dir = new Vector3((float)Mathf.Cos(theta), (float)Mathf.Sin(theta), 0.0f).normalized;
        float dist = Random.Range(MIN_SPAWN_RADIUS, MAX_SPAWN_RADIUS);

        // Check if going in said direction exceeds bounds. Rotate dir by 90 deg until it doesn't
        // We can randomly pick new directions/distances instead, but this guarantees we typically only loop at most 3 times
        Vector3 notePos = playerPos + dir * dist;
        while (OutOfBounds(notePos))
        {
            theta = Mathf.Deg2Rad * (Mathf.Rad2Deg * theta + 90.0f);
            dir = new Vector3((float)Mathf.Cos(theta), (float)Mathf.Sin(theta), 0.0f).normalized;
            notePos = playerPos + dir * dist;
        }

        // Pick treble/bass clef randomly
        GameObject prefab;
        int rand = Random.Range(0, 2);
        if (rand == 0) prefab = state.notePickupPrefab;
        else prefab = state.notePickupPrefabTwo;

        return Instantiate(prefab, notePos, Quaternion.identity);
    }

    // Check if a position is inaccessible to the player
    bool OutOfBounds(Vector3 v)
    {
        // Limit bounds slightly under world bounds to ensure entire note sprite is accessible
        float maxX = state.maxX - 10.0f;
        float maxY = state.maxY - 10.0f;
        return v.x > maxX || v.y > maxY || v.x < -maxX || v.y < -maxY;
    }
}