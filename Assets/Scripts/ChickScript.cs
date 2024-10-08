using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ChickScript : MonoBehaviour
{
    [HideInInspector] public GameObject parent; // bird to follow
    [HideInInspector] public GameObject child; // bird that follows

    public bool isSinging = false;
    private float constraintRadius = 2.0f; // maintain this distance on move/stop
    private Vector3 direction;
    private Vector3 velocity;

    private StateScript state;
    private Rigidbody2D body;
    private ChickSpriteScript spriteScript;
    private ShootScript chickShoot;

    [HideInInspector] public float fastSpeed;
    [HideInInspector] public float speed;

    private bool isWinning = false;
    [HideInInspector] public float winTimer = 0.0f;
    [HideInInspector] public float timeToFly = 5.0f;

    void Start()
    {
        state = GameObject.Find("State").GetComponent<StateScript>();
        body = GetComponent<Rigidbody2D>();
        spriteScript = gameObject.GetComponentInChildren<ChickSpriteScript>();
        speed = state.chickSpeed;
        fastSpeed = state.chickSpeed * 1.3f;
        chickShoot = GetComponent<ShootScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWinning) MoveTowardsParent();
        else
        {
            if (transform.position.y > state.cam.transform.position.y + 12.0f)
            {
                state.cameraScript.OnWin();
                Destroy(gameObject);
            }
            else
            {
                // spin aggressively and shoot up after short delay
                winTimer += Time.deltaTime;
                if (winTimer > timeToFly) body.velocity = new(0.0f, 40.0f);
            }
        }
    }

    private void MoveTowardsParent()
    {
        float dist = Vector2.Distance(parent.transform.position, body.transform.position);
        direction = (parent.transform.position - body.transform.position).normalized; 

        // We want the baby to lag behind a little when it first starts moving,
        // then adjust speed based on distance from parent
        if (body.velocity == Vector2.zero) // Initial standstill
        {
            // Do not move until parent has gotten far enough away
            if (dist > constraintRadius * 1.2f) speed = fastSpeed;
        }
        else // While moving
        {
            float stopConstraint = (parent != state.player) ? (constraintRadius * 0.7f) : (constraintRadius * 0.9f);

            if (dist > constraintRadius) speed = fastSpeed; // constant speed when running
            else speed = Mathf.Lerp(fastSpeed, 0.0f, (constraintRadius - dist) / (constraintRadius - stopConstraint)); // slow down smoothly
        }

        velocity = speed * direction;
        body.velocity = velocity;
    }

    public void OnPickup(int count)
    {
        spriteScript.OnPickup(count);
    }

    public void OnWin()
    {
        isWinning = true;
        spriteScript.OnWin();
        body.velocity = Vector2.zero;
    }

    public void Activate()
    {
        isSinging = true;
        spriteScript.Activate();
        state.playerShoot.IncreaseSpeed();
        chickShoot.IncreaseSpeed();
    }
    
    public bool IsMoving() {
        return body.velocity.magnitude > 0.25f;
    }

    public bool FacingRight()
    {
        return direction.x >= 0;
    }
}
