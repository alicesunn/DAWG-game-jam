using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 1;

    private const float IFRAMES = 0.2f;
    private float iframeTimer = 0.0f;

    private void Update()
    {
        if (iframeTimer > 0.0f) iframeTimer -= Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        if (iframeTimer > 0.0f) return;

        iframeTimer = IFRAMES;
        health -= damage;

        if (health <= 0) Defeated();
    }

    public void Defeated(){
        //Add death animation here
        Debug.Log("Defeated");
        Destroy(gameObject);
    }
}