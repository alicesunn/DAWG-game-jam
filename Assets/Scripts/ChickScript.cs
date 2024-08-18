using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ChickScript : MonoBehaviour
{
    public GameObject parent; // bird to follow
    public GameObject child; // bird that follows
    public Color[] colors; // bird will change colors to indicate it's singing

    public bool isSinging = false;
    public float speed = 4.0f; // default speed
    public float constraintRadius = 1.5f; // maintain this distance on move/stop
    public Vector2 direction = new(0.0f, 0.0f);

    private Rigidbody2D body;
    private StateScript state;
    private new SpriteRenderer renderer;

    private float normSpeed;
    private float slowSpeed;
    private float fastSpeed;

    public float colorChangeTime = 2.5f;
    private float colorT;
    private int colorInd = 0;
    private bool moving;

    // Start is called before the first frame update
    void Start()
    {
        state = GameObject.Find("State").GetComponent<StateScript>();
        body = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        normSpeed = speed;
        slowSpeed = speed * 0.5f;
        fastSpeed = speed * 1.3f;
        moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsParent();

        // Change colors when singing
        if (isSinging) RotateColor();
    }

    // Call every frame
    private void MoveTowardsParent()
    {
        float dist = Vector2.Distance(parent.transform.position, body.transform.position);
        direction = (parent.transform.position - body.transform.position).normalized;

        // We want the baby to lag behind a little when it first starts moving,
        // then adjust speed based on distance from parent
        if (body.velocity == Vector2.zero) // Initial standstill
        {
            moving = false;
            // Do not move until parent has gotten far enough away
            if (dist > constraintRadius * 1.2f)
            {
                speed = fastSpeed;
            }
        }
        else // While moving
        {
            moving = true;
            float stopConstraint = (parent != state.player) ? (constraintRadius * 0.7f) : (constraintRadius * 0.9f);

            if (dist > constraintRadius) speed = fastSpeed; // constant speed when running
            else speed = Mathf.Lerp(fastSpeed, 0.0f, (constraintRadius - dist) / (constraintRadius - stopConstraint)); // slow down smoothly
        }

        body.velocity = direction * speed;
    }

    private void RotateColor()
    {
        colorT += Time.deltaTime / colorChangeTime;
        int nextInd = (colorInd >= colors.Length - 1) ? 0 : colorInd + 1;
        renderer.color = Color.Lerp(colors[colorInd], colors[nextInd], colorT);

        if (colorT > 1.0f)
        {
            colorT = 0.0f;
            colorInd = nextInd;
        }
    }
    
    public bool isMoving() {
        return moving;
    }
}
