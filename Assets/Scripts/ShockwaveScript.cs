using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveScript : MonoBehaviour
{
    private const float EXPAND_SPEED = 5.0f;
    private float scale = 0.25f;

    void Update()
    {
        scale += EXPAND_SPEED * Time.deltaTime;
        transform.localScale = Vector3.one * scale;
        Debug.Log("expanding by " + EXPAND_SPEED * Time.deltaTime);

        if (transform.localScale.x >= 4)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health h = collision.gameObject.GetComponent<Health>();
        if (h) h.TakeDamage(2.0f);
    }
}
