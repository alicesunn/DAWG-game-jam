using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickAnimation : MonoBehaviour
{
    // References
    Animator am;
    ChickScript cm;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        am = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        cm = GetComponent<ChickScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cm.isMoving()) {
            am.SetBool("Move", true);
            SpriteDirectionCheck();
        }
        else {
            am.SetBool("Move", false);
        }
    }

    void SpriteDirectionCheck() {
        if (cm.direction.x < 0) {
            sr.flipX = true;
        }
        else {
            sr.flipX = false;
        }
    }

}
