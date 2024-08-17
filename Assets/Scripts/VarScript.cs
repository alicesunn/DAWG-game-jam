using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class for objects to reference external objects
public class VarScript : MonoBehaviour
{
    // Change these when tags/names are changed
    public string playerName = "Player";
    public string playerTag = "Bird";

    public GameObject player;
    public GameObject[] chicks;

    void Start()
    {
        player = GameObject.Find(playerName);
        chicks = GameObject.FindGameObjectsWithTag("Chick");
    }
}
