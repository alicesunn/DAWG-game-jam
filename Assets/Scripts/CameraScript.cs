using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Vector3 offset = new(0.0f, 0.0f, -10.0f);

    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("State").GetComponent<StateScript>().player;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = player.transform.position + offset;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime); // <- fuck you
        transform.position = newPos;
    }
}