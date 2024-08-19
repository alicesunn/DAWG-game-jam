using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class for scripts to find commonly used data/references.
// All other scripts should reference state variables instead of finding them themselves
public class StateScript : MonoBehaviour
{
    // Names
    public string playerName = "Player";
    public string musicControllerName = "AudioController";
    public string cameraName = "Main Camera";

    // Tags
    public string playerTag = "Bird";
    public string chickTag = "Chick";
    public string notePickupTag = "NotePickup";
    public string kidTag = "Kid";
    public string teenTag = "Teen";
    public string adultTag = "Adult";

    // Layers
    public int enemyLayer = 6;
    public int bulletLayer = 7;

    // Prefabs (assign through drag/drop inspector)
    public GameObject chickPrefab;
    public GameObject notePickupPrefab;
    public GameObject notePickupPrefabTwo;
    public GameObject kidPrefab;
    public GameObject teenPrefab;
    public GameObject adultPrefab;
    public GameObject arrowPrefab;
    public GameObject bulletPrefabOne;
    public GameObject bulletPrefabTwo;
    public GameObject bulletPrefabThree;

    // Values
    [HideInInspector] public float maxX; // world
    [HideInInspector] public float maxY; // boundary
    [HideInInspector] public int layerCount = 6; // number of music layers, same as number of chicks

    // Scene objects
    [HideInInspector] public GameObject player;
    [HideInInspector] public GameObject notePickup;
    [HideInInspector] public GameObject arrow;

    // Unique components
    [HideInInspector] public Camera cam;
    [HideInInspector] public AudioScript music;

    // Data
    [HideInInspector] public GameObject[] chicks;
    [HideInInspector] public List<GameObject> enemies;
    private HashSet<string> enemyTags;
    private int notesSoFar = 0;
    private const int NOTES_PER_CHICK = 3;

    void Awake()
    {
        maxX = 100.0f;
        maxY = 100.0f;
        player = GameObject.Find(playerName);
        cam = GameObject.Find(cameraName).GetComponent<Camera>();
        music = GameObject.Find(musicControllerName).GetComponent<AudioScript>();
        enemyTags = new HashSet<string>{kidTag, teenTag, adultTag};
        enemies = new List<GameObject>();
    }

    // Initial object spawning
    void Start()
    {
        // Create layerCount number of chicks and assign line relationships
        chicks = new GameObject[layerCount];
        GameObject p = player;
        for (int i = 0; i < layerCount; i++)
        {
            chicks[i] = Instantiate(chickPrefab, player.transform.position, Quaternion.identity);

            if (p != player) p.GetComponent<ChickScript>().child = chicks[i];
            chicks[i].GetComponent<ChickScript>().parent = p;
            p = chicks[i];
        }

        // Indicator arrow
        arrow = Instantiate(arrowPrefab, player.transform.position, Quaternion.identity);
    }
    
    // Finds the nearest enemy to the player
    public GameObject NearestEnemy()
    {
        // Inefficient but should be fine as long as there aren't too many enemies.

        // Possible optimization is to have a sphere collider that triggers when an enemy enters,
        // and form a list of enemies off of that. That way we only check enemies within a certain
        // radius of the player, instead of checking all enemies including off-screen ones.

        float minDist = -1;
        GameObject enemy = null;
        foreach (GameObject e in enemies)
        {
            float dist = DistSquared(player.transform.position, e.transform.position);
            if (dist < minDist || minDist == -1)
            {
                minDist = dist;
                enemy = e;
            }
        }
        return enemy;
    }

    public bool IsEnemy(GameObject e)
    {
        // Can also check enemy list, but because enemy spawns are
        // auto added/removed, we don't have to worry about checking a
        // non-existent enemy so this is probably(?) more efficient
        return enemyTags.Contains(e.tag);
    }

    // Returns squared distance between a and b since skipping
    // square root optimizes distance calc speed
    public float DistSquared(Vector3 a, Vector3 b)
    {
        return Mathf.Pow(a.x - b.x, 2) + Mathf.Pow(a.y - b.y, 2);
    }

    public void PickUpNote()
    {
        notesSoFar++;

        if (notesSoFar == NOTES_PER_CHICK)
        {
            music.PlayNextLayer();
            notesSoFar = 0;
        }
        else
        {
            chicks[music.layerIndex].GetComponent<ChickScript>().OnPickup(notesSoFar);
        }
    }
}
