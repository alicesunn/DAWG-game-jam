﻿using System;
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
    public int layerCount = 6; // number of track layers, which is same as number of chicks

    // World boundaries. Player cannot walk past these
    [HideInInspector] public float maxX;
    [HideInInspector] public float maxY;

    [HideInInspector] public GameObject player;
    [HideInInspector] public GameObject[] chicks;

    void Awake()
    {
        maxX = 10.0f;
        maxY = 10.0f;
        player = GameObject.Find(playerName);
        chicks = GameObject.FindGameObjectsWithTag("Chick");

        // TODO: spawn layerCount amount of chicks near player (so no manual drag-drop chick spawning later)
        // ensuring chicks spawn progressively further away will also prevent need for sorting chick line

        // Create chick conga line relationships
        Array.Sort(chicks, delegate (GameObject b1, GameObject b2) { // Sort by distance from player
            return (Mathf.Pow(b1.transform.position.x - transform.position.x, 2) + Mathf.Pow(b1.transform.position.y - transform.position.y, 2))
                .CompareTo(Mathf.Pow(b2.transform.position.x - transform.position.x, 2) + Mathf.Pow(b2.transform.position.y - transform.position.y, 2));
        });

        // Assign line in order of closest to player
        GameObject p = chicks[0];
        p.GetComponent<ChickScript>().parent = player;
        for (int i = 1; i < chicks.Length; i++)
        {
            p.GetComponent<ChickScript>().child = chicks[i];
            chicks[i].GetComponent<ChickScript>().parent = p;
            p = chicks[i];
        }
    }
}
