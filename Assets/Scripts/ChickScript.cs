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
    private SpriteRenderer rend;

    private float fastSpeed;

    public float colorChangeTime = 2.5f;
    private float colorT;
    private int colorInd = 0;

    // Start is called before the first frame update
    void Start()
    {
        state = GameObject.Find("State").GetComponent<StateScript>();
        body = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        fastSpeed = speed * 1.3f;
        rend.sortingOrder = -1;
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
            // Do not move until parent has gotten far enough away
            if (dist > constraintRadius * 1.2f)
            {
                speed = fastSpeed;
            }
        }
        else // While moving
        {
            float stopConstraint = (parent != state.player) ? (constraintRadius * 0.7f) : (constraintRadius * 0.9f);

            if (dist > constraintRadius) speed = fastSpeed; // constant speed when running
            else speed = Mathf.Lerp(fastSpeed, 0.0f, (constraintRadius - dist) / (constraintRadius - stopConstraint)); // slow down smoothly
        }

        body.velocity = direction * speed;
    }

    // Chick will cycle through rainbow colors when singing for prototype purposes. Probably don't include in final game
    private void RotateColor()
    {
        colorT += Time.deltaTime / colorChangeTime;
        int nextInd = (colorInd >= colors.Length - 1) ? 0 : colorInd + 1;
        rend.color = Color.Lerp(colors[colorInd], colors[nextInd], colorT);

        if (colorT > 1.0f)
        {
            colorT = 0.0f;
            colorInd = nextInd;
        }
    }
    
    public bool IsMoving() {
        return body.velocity.magnitude > 0.25f;
    }
}
