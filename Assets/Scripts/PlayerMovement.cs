using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    Rigidbody2D body;
    Vector2 direction = new(0.0f, 0.0f);

    void Start()
    {
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
