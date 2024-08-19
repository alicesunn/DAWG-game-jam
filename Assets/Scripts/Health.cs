using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 1;

    private StateScript state;
    private const float IFRAMES = 0.2f;
    private float iframeTimer = 0.0f;

    private void Start()
    {
        state = GameObject.Find("State").GetComponent<StateScript>();
    }

    private void Update()
    {
        if (iframeTimer > 0.0f) iframeTimer -= Time.deltaTime;
    }

    public bool TakeDamage(float damage)
    {
        if (iframeTimer > 0.0f) return false;

        iframeTimer = IFRAMES;
        health -= damage;

        if (health <= 0) Defeated();
        
        return true;
    }

    public void Defeated()
    {
        state.enemies.Remove(gameObject);
        Destroy(gameObject);
    }
}