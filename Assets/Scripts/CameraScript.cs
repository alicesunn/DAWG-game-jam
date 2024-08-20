using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Vector3 offset = new(0.0f, 0.0f, -10.0f);

    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;
    private GameObject player;
    private StateScript state;

    // Child of camera so that it can stay in place in pov
    [HideInInspector] public TextMeshProUGUI hpText;

    private bool isWinning = false;

    private void Awake()
    {
        state = GameObject.Find("State").GetComponent<StateScript>();
        hpText = GetComponentInChildren<TextMeshProUGUI>();
        state.hpText = hpText;
    }

    private void Start()
    {
        // Start at origin
        transform.position = Vector3.zero + offset;
        player = state.player;
    }

    private void Update()
    {
        Vector3 targetPos;
        if (isWinning)
        {
            // Pan up to just above player
            targetPos = player.transform.position + new Vector3(0.0f, 2.35f, 0.0f) + offset;
        }
        else
        {
            // Smooth follow camera on player
            targetPos = player.transform.position + offset;
        }

        Vector3 newPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime); // <- fuck you
        transform.position = newPos;
    }

    public void OnWin()
    {
        isWinning = true;
    }
}