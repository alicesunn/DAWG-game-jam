using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    private StateScript state;
    private Health playerHealth;
    private Rigidbody2D body;

    private float speed;

    void Start()
    {
        state = GameObject.Find("State").GetComponent<StateScript>();
        playerHealth = state.player.GetComponent<Health>();
        body = GetComponent<Rigidbody2D>();

        // Vary speed depending on if object holding this instance is a kid/teen/adult
        float playerSpeed = state.player.GetComponent<PlayerScript>().speed;
        if (gameObject.CompareTag(state.kidTag)) speed = playerSpeed * 1.5f;
        else if (gameObject.CompareTag(state.teenTag)) speed = playerSpeed;
        else speed = playerSpeed * 0.85f;
    }

    // Walk towards player
    void Update()
    {
        Vector3 dir = (state.player.transform.position - transform.position).normalized;
        body.velocity = dir * speed;
    }

    //Enemy causing damage
    private void OnCollisionStay2D(Collision2D collision){
        if(collision.gameObject.name == state.player.name)
        {
            playerHealth.TakeDamage(1.0f);
        }
    }
}
