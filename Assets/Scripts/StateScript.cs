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
    public string playerTag = "Bird";
    public string notePickupTag = "NotePickup";
    public int layerCount = 6; // number of track layers, which is same as number of chicks

    // Prefab references which must be assigned through drag/drop in inspector
    public GameObject chickPrefab;
    public GameObject notePickupPrefab;
    public GameObject kidPrefab;
    public GameObject teenPrefab;
    public GameObject adultPrefab;
    //public GameObject bulletPrefab;

    // World boundaries. Player cannot walk past these
    [HideInInspector] public float maxX;
    [HideInInspector] public float maxY;
    [HideInInspector] public Vector2 maxPos;

    // Scene object references
    [HideInInspector] public GameObject player;
    [HideInInspector] public GameObject notePickup; // currently spawned note pickup
    [HideInInspector] public GameObject[] chicks;

    [HideInInspector] public AudioScript music;

    void Awake()
    {
        maxX = 100.0f;
        maxY = 100.0f;
        maxPos = new(maxX, maxY);
        player = GameObject.Find(playerName);
        //chicks = GameObject.FindGameObjectsWithTag("Chick");
        music = GameObject.Find("AudioController").GetComponent<AudioScript>();

        // TODO: spawn layerCount amount of chicks near player (so no manual drag-drop chick spawning later)
        // ensuring chicks spawn progressively further away will also prevent need for sorting chick line

        // Create chick conga line relationships
        //Array.Sort(chicks, delegate (GameObject b1, GameObject b2) { // Sort by distance from player
        //    return (Mathf.Pow(b1.transform.position.x - transform.position.x, 2) + Mathf.Pow(b1.transform.position.y - transform.position.y, 2))
        //        .CompareTo(Mathf.Pow(b2.transform.position.x - transform.position.x, 2) + Mathf.Pow(b2.transform.position.y - transform.position.y, 2));
        //});

        // Assign chick line
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
