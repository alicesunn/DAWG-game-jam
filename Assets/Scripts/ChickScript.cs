using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ChickScript : MonoBehaviour
{
    public float speed = 4.0f; // default speed
    public float constraintRadius = 1.5f; // maintain this distance on move/stop

    public GameObject parent; // bird to follow
    public GameObject child; // bird that follows

    private float normSpeed;
    private float slowSpeed;
    private float fastSpeed;
    private Rigidbody2D body;
    private VarScript vars;

    // Start is called before the first frame update
    void Start()
    {
        vars = GameObject.Find("Variables").GetComponent<VarScript>();
        body = GetComponent<Rigidbody2D>();
        normSpeed = speed;
        slowSpeed = speed * 0.5f;
        fastSpeed = speed * 1.3f;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsParent();
    }

    // Call every frame
    private void MoveTowardsParent()
    {
        float dist = Vector2.Distance(parent.transform.position, body.transform.position);
        Vector2 direction = (parent.transform.position - body.transform.position).normalized;

        // We want the baby to lag behind a little when it first starts moving,
        // then adjust speed based on distance from parent
        if (body.velocity == Vector2.zero) // Initial standstill
        {
            // Do not move until parent has gotten far enough away
            if (dist > constraintRadius * 1.2f)
            {
                speed = fastSpeed;
            }
        }
        else // While moving
        {
            float stopConstraint = (parent != vars.player) ? (constraintRadius * 0.7f) : (constraintRadius * 0.9f);

            if (dist > constraintRadius) speed = fastSpeed; // constant speed when running
            else speed = Mathf.Lerp(fastSpeed, 0.0f, (constraintRadius - dist) / (constraintRadius - stopConstraint)); // slow down smoothly
        }

        body.velocity = direction * speed;
    }
}
