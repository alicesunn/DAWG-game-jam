using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NoteHit : MonoBehaviour
{
    private StateScript state;
    private const float BOB_SPEED = 0.5f;
    private float upTarget;
    private float downTarget;
    private Vector3 curTarget;

    void Start()
    {
        state = GameObject.Find("State").GetComponent<StateScript>();
        upTarget = transform.position.y + 2.0f;
        downTarget = transform.position.y - 2.0f;
        curTarget = transform.position;
        curTarget.y = upTarget;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        // Make notes bob up and down slowly
        Vector3 curPos = transform.position;
        if (curPos == curTarget)
        {
            float newTarget = (curTarget.y == upTarget) ? downTarget : upTarget;
            curTarget.y = newTarget;
        }
        transform.position = Vector3.MoveTowards(curPos, curTarget, BOB_SPEED * Time.deltaTime);

        if (collision.gameObject.name == state.player.name){
            Destroy(gameObject);
            state.PickUpNote();
        }
    }
}
