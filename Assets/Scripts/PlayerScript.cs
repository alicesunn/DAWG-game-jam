using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class PlayerScript : MonoBehaviour
{
    public float speed = 10;

    [HideInInspector] public Vector2 direction = new(0.0f, 0.0f);

    private Rigidbody2D body;
    private StateScript state;

    //Runs before anything else in the script

    void Start()
    {
        
        state = GameObject.Find("State").GetComponent<StateScript>();
        body = GetComponent<Rigidbody2D>();
        
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
