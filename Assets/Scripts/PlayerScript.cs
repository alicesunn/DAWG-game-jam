using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class PlayerScript : MonoBehaviour
{
    [HideInInspector] public Vector2 direction = new(0.0f, 0.0f);

    private Rigidbody2D body;
    private StateScript state;
    private float speed;

    void Start()
    {
        state = GameObject.Find("State").GetComponent<StateScript>();
        GetComponent<Health>().SetState(state);
        speed = state.playerSpeed;
        body = GetComponent<Rigidbody2D>();

        // Start at origin
        transform.position = Vector3.zero;
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        direction = new Vector2(x, y).normalized;

        body.velocity = direction * speed;

        // Prevent from walking beyond certain point
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -state.maxX, state.maxX);
        pos.y = Mathf.Clamp(pos.y, -state.maxY, state.maxY);
        transform.position = pos;

        // Flip bird depending on direction
        Vector3 scale = transform.localScale;
        if ((direction.x < 0 && scale.x > 0) || (direction.x > 0 && scale.x < 0)) scale.x *= -1;
        transform.localScale = scale;
    }
}
