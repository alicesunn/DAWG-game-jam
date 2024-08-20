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

    public Vector2 dir = new(0.0f, 0.0f);

    private float speed;

    void Start()
    {
        playerHealth = state.player.GetComponent<Health>();
        body = GetComponent<Rigidbody2D>();

        // Vary speed depending on if object holding this instance is a kid/teen/adult
        if (gameObject.CompareTag(state.kidTag)) speed = state.playerSpeed * 1.1f;
        else if (gameObject.CompareTag(state.teenTag)) speed = state.playerSpeed * 0.8f;
        else speed = state.playerSpeed * 0.6f;
    }

    // Walk towards player
    void Update()
    {
        dir = (state.player.transform.position - transform.position).normalized;
        body.velocity = dir * speed;

        // Flip depending on direction
        Vector3 scale = transform.localScale;
        if ((dir.x < 0 && scale.x > 0) || (dir.x > 0 && scale.x < 0)) scale.x *= -1;
        transform.localScale = scale;
    }

    // Damage player
    private void OnCollisionStay2D(Collision2D collision){
        if(collision.gameObject.name == state.player.name)
        {
            playerHealth.TakeDamage(1.0f);
            state.enemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    public bool isMoving() {
        return speed > 0;
    }

    public void SetState(StateScript s)
    {
        state = s;
    }
}
