using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHit : MonoBehaviour
{
    public GameObject player;
    private StateScript state;

    void Start()
    {
        state = GameObject.Find("State").GetComponent<StateScript>();
        player = state.player;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.name == player.name){
            Destroy(gameObject);
            state.music.PlayNextLayer();
        }
    }
}
