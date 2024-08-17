using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class PlayerScript : MonoBehaviour
{
    public float speed = 10;
    Rigidbody2D body;
    Vector2 direction = new(0.0f, 0.0f);

    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        // Create conga line of chicks
        GameObject[] babies = GameObject.FindGameObjectsWithTag("Chick");
        Array.Sort(babies, delegate (GameObject b1, GameObject b2) { // Sort by distance from player
            return (Mathf.Pow(b1.transform.position.x - transform.position.x, 2) + Mathf.Pow(b1.transform.position.y - transform.position.y, 2))
                .CompareTo(Mathf.Pow(b2.transform.position.x - transform.position.x, 2) + Mathf.Pow(b2.transform.position.y - transform.position.y, 2));
        });

        // Assign line in order of closest to player
        GameObject p = babies[0];
        p.GetComponent<ChickScript>().parent = this.gameObject;
        for (int i = 1; i < babies.Length; i++)
        {
            p.GetComponent<ChickScript>().child = babies[i];
            babies[i].GetComponent<ChickScript>().parent = p;
            p = babies[i];
        }
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        direction = new Vector2(x, y).normalized;

        body.velocity = direction * speed;
    }
}
