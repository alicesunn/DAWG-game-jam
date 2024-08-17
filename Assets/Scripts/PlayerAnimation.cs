using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // References
    Animator am;
    PlayerScript pm;
    SpriteRenderer sr;

    void Start()
    {
        am = GetComponent<Animator>();
        pm = GetComponent<PlayerScript>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //if x or y are 0 they arent moving thus 
        if (pm.direction.x != 0 || pm.direction.y != 0) {
            am.SetBool("Move", true);
            SpriteDirectionCheck();
        }
        else {
            am.SetBool("Move", false);
        }
    }
    
    void SpriteDirectionCheck() {
        if (pm.direction.x < 0) {
            sr.flipX = true;
        }
        else {
            sr.flipX = false;
        }
    }
}
