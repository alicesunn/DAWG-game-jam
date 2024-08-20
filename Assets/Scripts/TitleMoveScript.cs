using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMoveScript : MonoBehaviour
{
    public bool flipX = true;

    private const float MOVE_SPEED = 2.5f;

    private void Start()
    {
        if (flipX)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    void Update()
    {
        // Move slowly towards left
        Vector3 pos = transform.position;
        pos.x -= Time.deltaTime * MOVE_SPEED;
        transform.position = pos;

        // Delete when out of bounds
        if (transform.position.x < -20.0f)
            Destroy(gameObject);
    }
}