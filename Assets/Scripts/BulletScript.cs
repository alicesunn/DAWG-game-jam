using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BulletScript : MonoBehaviour
{
    private GameObject nearest;
    private StateScript state;

    private const float LIVE_RADIUS = 7.0f * 7.0f; // (squared) dist a bullet can be from the bird before it dies

    private int bulletHits = 1; // number of times a bullet can damage something before it dies
    private float bulletDamage = 1.0f; // damage per hit
    private float speed = 4.0f;

    // Notes will move in a sine wave
    private const float FREQUENCY = 7.0f;
    private const float MAGNITUDE = 1.0f;
    private bool isWavy;
    private Vector3 pos;
    private Vector3 axis;
    private Vector3 dir;

    void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = -2; // render bullets below chicks as well
        state = GameObject.Find("State").GetComponent<StateScript>();

        isWavy = Random.Range(0, 2) == 0;

        // some convoluted back and forth to get the right sine axes
        // without changing the actual orientation
        if (isWavy)
        {
            pos = transform.position;
            Vector3 temp = transform.up;
            transform.up = (state.NearestEnemy().transform.position - pos).normalized; // general direction of fire
            axis = transform.right;
            dir = transform.up;
            transform.up = temp;
        }
        else
        {
            speed = 8.0f;
            dir = (state.NearestEnemy().transform.position - transform.position).normalized;
        }

        // All bullets get destroyed after a few seconds regardless of hit or not
        Destroy(gameObject, 3.0f);
    }

    private void Update()
    {
        if (isWavy)
        {
            pos += Time.deltaTime * speed * dir;
            transform.position = pos + MAGNITUDE * Mathf.Sin(Time.time * FREQUENCY) * axis;
        }
        else transform.position += Time.deltaTime * speed * dir;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (state.IsEnemy(collision.gameObject))
        {
            if (collision.gameObject.GetComponent<Health>().TakeDamage(bulletDamage))
                bulletHits -= 1;

            if (bulletHits <= 0) Destroy(gameObject);
        }
    }
}
