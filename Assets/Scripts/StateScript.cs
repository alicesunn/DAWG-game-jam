using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class for objects to reference external objects
public class StateScript : MonoBehaviour
{
    // Global variables, only change these when names/tags/etc are changed
    // All other scripts should reference these variables instead of finding them themselves
    public string playerName = "Player";
    public string musicControllerName = "AudioController";
    public string cameraName = "Main Camera";

    public string playerTag = "Bird";
    public string notePickupTag = "NotePickup";
    public string kidTag = "Kid";
    public string teenTag = "Teen";
    public string adultTag = "Adult";

    // Prefab references which must be assigned through drag/drop in inspector
    public GameObject chickPrefab;
    public GameObject notePickupPrefab;
    public GameObject kidPrefab;
    public GameObject teenPrefab;
    public GameObject adultPrefab;
    //public GameObject bulletPrefab;

    // World boundaries
    [HideInInspector] public float maxX;
    [HideInInspector] public float maxY;
    [HideInInspector] public Vector2 maxPos;

    // Scene object/component references
    [HideInInspector] public GameObject player;
    [HideInInspector] public GameObject notePickup; // currently spawned note pickup
    [HideInInspector] public GameObject[] chicks;
    [HideInInspector] public Camera cam;
    [HideInInspector] public AudioScript music;

    [HideInInspector] public int layerCount = 6; // number of track layers, which is same as number of chicks

    void Awake()
    {
        maxX = 100.0f;
        maxY = 100.0f;
        maxPos = new(maxX, maxY);
        player = GameObject.Find(playerName);
        cam = GameObject.Find(cameraName).GetComponent<Camera>();
        music = GameObject.Find(musicControllerName).GetComponent<AudioScript>();
    }

    void Start()
    {
        // Instantiate layerCount number of chicks and assign line relationships
        chicks = new GameObject[layerCount];
        GameObject p = player;
        for (int i = 0; i < layerCount; i++)
        {
            chicks[i] = Instantiate(chickPrefab, player.transform.position, Quaternion.identity);

            if (p != player) p.GetComponent<ChickScript>().child = chicks[i];
            chicks[i].GetComponent<ChickScript>().parent = p;
            p = chicks[i];
        }
    }
}
