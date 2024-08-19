using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidAnimation : MonoBehaviour
{
    Animator am;
    AIChase em;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        am = GetComponent<Animator>();
        em = GetComponent<AIChase>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (em.isMoving()) {
            am.SetBool("Move", true);
            SpriteDirectionCheck();
        }
        else {
            am.SetBool("Move", false);
        }
    }

    void SpriteDirectionCheck() {
        if (em.dir.x < 0) {
            sr.flipX = true;
        }
        else {
            sr.flipX = false;
        }
    }


}
