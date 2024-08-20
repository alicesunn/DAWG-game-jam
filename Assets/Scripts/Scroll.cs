using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    private bool startScroll;
    private int maxScroll;

    private Vector3 dir;

    public GameObject camButton; 

    public GameObject menuButton;

    // Start is called before the first frame update
    void Start()
    {
        startScroll = false;
        dir = new Vector3(0.0f, 1.0f, 0.0f);
        camButton = GameObject.Find("Credits");
        menuButton = GameObject.Find("Main Menu");
        maxScroll = 35;
    }

    // Update is called once per frame
    void Update()
    {
        if (startScroll == true && transform.position.y < maxScroll) {
            transform.position += dir * Time.deltaTime * 3;
        }
    }

    public void setScrollToTrue() {
        startScroll = true;
        Destroy(camButton);
        Destroy(menuButton);
    }
}
