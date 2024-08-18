using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ArrowScript : MonoBehaviour
{
    private StateScript state;
    private new SpriteRenderer renderer;

    private Vector3 dir;
    private Quaternion targetRotation;

    private const float ROTATE_RADIUS = 1.5f;
    private const float ROTATE_SPEED = 25.0f;

    void Start()
    {
        state = GameObject.Find("State").GetComponent<StateScript>();
        renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = false;
    }

    void Update()
    {
        // The arrow indicator will always point to where the note pickup is
        if (state.notePickup)
        {
            // Get direction from player to music note
            dir = (state.notePickup.transform.position - state.player.transform.position).normalized;
            transform.position = state.player.transform.position + dir * ROTATE_RADIUS;

            // Rotate towards target
            transform.right = dir;

            if (!renderer.enabled) renderer.enabled = true;
        }
        else if (renderer.enabled) renderer.enabled = false; // Hide when there are no notes
    }
}
