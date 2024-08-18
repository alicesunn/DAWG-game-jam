using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHit : MonoBehaviour
{
    public GameObject player;
    private StateScript state;
    // Start is called before the first frame update
    void Start()
    {
        state = GameObject.Find("State").GetComponent<StateScript>();
        player = state.player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.name == player.name){
            Debug.Log("Collision");
            Destroy(gameObject);
            state.music.PlayNextLayer();
        }
    }
}
