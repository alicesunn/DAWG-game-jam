using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    private StateScript state;
    //private Rigidbody2D body;

    private float speed;
    private float magLimit;

    void Start()
    {
        // Vary speed depending on if object holding this instance is a kid/teen/adult
        if (gameObject.CompareTag(state.kidTag)) speed = state.playerSpeed * 1.1f;
        else if (gameObject.CompareTag(state.teenTag)) speed = state.playerSpeed * 0.8f;
        else speed = state.playerSpeed * 0.6f;

        magLimit = Random.Range(5.0f, 15.0f);
    }

    // Walk towards player
    void Update()
    {
        Vector3 dir = (state.player.transform.position - transform.position).normalized;
        //body.velocity = dir * speed;
        transform.position += Time.deltaTime * dir * speed;

        // Flip depending on direction
        Vector3 scale = transform.localScale;
        if ((dir.x < 0 && scale.x > 0) || (dir.x > 0 && scale.x < 0)) scale.x *= -1;
        transform.localScale = scale;

        if (state.startedWin && speed > 0.0f && (state.player.transform.position - transform.position).magnitude <= magLimit)
        {
            speed = 0.0f;
            GetComponent<Rigidbody2D>().simulated = false;
        }
    }

    // Damage player
    private void OnCollisionStay2D(Collision2D collision){
        if(collision.gameObject.name == state.player.name)
        {
            state.playerScript.TakeDamage(1);
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
