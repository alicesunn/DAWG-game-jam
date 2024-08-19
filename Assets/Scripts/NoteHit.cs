using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NoteHit : MonoBehaviour
{
    private StateScript state;
    private const float BOB_TIME = 1.5f;
    private float targetBig;
    private float targetSmall;
    private float curTarget;
    private float oldTarget;
    private float timer;

    void Start()
    {
        state = GameObject.Find("State").GetComponent<StateScript>();
        targetBig = transform.localScale.x * 1.1f;
        targetSmall = transform.localScale.x * 0.9f;
        curTarget = targetBig;
        oldTarget = targetSmall;
        timer = 0.5f;
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        // Make notes stretch up and down slowly
        Vector3 cur = transform.localScale;
        if (curTarget == targetBig && cur.x >= curTarget)
        {
            curTarget = targetSmall;
            oldTarget = targetBig;
            timer = 0.0f;
        }
        else if (curTarget == targetSmall && cur.x <= curTarget)
        {
            curTarget = targetBig;
            oldTarget = targetSmall;
            timer = 0.0f;
        }

        cur.x = Mathf.Lerp(oldTarget, curTarget, timer / BOB_TIME);
        transform.localScale = cur;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.name == state.player.name){
            Destroy(gameObject);
            state.PickUpNote();
        }
    }

    public void SetRotate(float angle)
    {
        Vector3 rotate = new(0.0f, 0.0f, angle);
        transform.eulerAngles = rotate;
    }
}
