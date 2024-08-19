using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChickAnimation : MonoBehaviour
{
    // References
    Animator am;
    ChickScript cm;
    SpriteRenderer sr;
    StateScript state;
    
    void Start()
    {
        state = GameObject.Find("State").GetComponent<StateScript>();

        am = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        cm = GetComponent<ChickScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cm.IsMoving()) {
            am.SetBool("Move", true);
            SpriteDirectionCheck();
        }
        else {
            am.SetBool("Move", false);
        }
    }

    // HERE
    void SpriteDirectionCheck() {
        if (cm.direction.x < 0) {
            sr.flipX = false;

        }
        else {
            sr.flipX = true;

        }
    }
    
}
