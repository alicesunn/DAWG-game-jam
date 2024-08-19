using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class PlayerScript : MonoBehaviour
{
    [HideInInspector] public Vector2 direction = new(0.0f, 0.0f);
    public float speed = 10;

    private Rigidbody2D body;
    private StateScript state;
    //healthBar prefab dragged into hiearchy
    public Image healthBar;
    //Refrenching the attached Health Script
    public Health healthAmount;
    //whatever we set the players total health
    private float PLAYERTOTALHEALTH = 10;

    void Start()
    {
        // Start at origin
        transform.position = Vector3.zero;

        state = GameObject.Find("State").GetComponent<StateScript>();
        body = GetComponent<Rigidbody2D>();
        //Helath component attached to player
        healthAmount = GetComponent<Health>();

    }

    void Update()
    {
        HandleMovement();
        HealthBar();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        direction = new Vector2(x, y).normalized; // for animation purposes

        body.velocity = new Vector2(x, y).normalized * speed;

        // Prevent from walking beyond certain point
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -state.maxX, state.maxX);
        pos.y = Mathf.Clamp(pos.y, -state.maxY, state.maxY);
        transform.position = pos;
    }

    void HealthBar(){
        //Whatever the total health of player is 
        healthBar.fillAmount = healthAmount.health / PLAYERTOTALHEALTH;
    }
}
