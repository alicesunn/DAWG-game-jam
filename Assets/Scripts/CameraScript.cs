using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float speed;

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        speed = player.GetComponent<PlayerScript>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        //float dist = Vector2.Distance(player.transform.position, transform.position);
        //Vector2 direction = (player.transform.position - transform.position).normalized;
    }
}
