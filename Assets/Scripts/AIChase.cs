using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject player;
    public float speed;

    [SerializeField] private float minSpeed = .5f;
    [SerializeField] private float maxSpeed = 3.0f;
    private float distance;
    private StateScript state;

    void Start()
    {
        state = GameObject.Find("State").GetComponent<StateScript>();

        //We are going to variate the speed of mobs on intialization
        speed = Random.Range(minSpeed,maxSpeed);

        player = state.player;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position,player.transform.position);
        //now the actual move towards enemy
        transform.position = Vector2.MoveTowards(this.transform.position,player.transform.position,speed * Time.deltaTime);

    }

    //Enemy causing damage
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.name == player.name){
            Debug.Log("Collision");
            player.GetComponent<Health>().TakeDamage(1.0f);
        }
    }
}
