using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{

    private Vector3 SCROLL_DIR = new(0.0f, 1.0f, 0.0f);

    public GameObject creditsButton;
    public GameObject menuButton;
    public GameObject menuButtonBottom;

    private const float SCROLL_SPEED = 2.0f;
    private const float MAX_SCROLL = 58.0f; // stop slightly before exact picture end
    private AudioSource ohYeah;
    private bool startScroll;
    private bool playedOhYeah = false;

    // Start is called before the first frame update
    void Start()
    {
        startScroll = false;
        ohYeah = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startScroll && transform.position.y < MAX_SCROLL) {
            Vector3 delta = Time.deltaTime * SCROLL_SPEED * SCROLL_DIR;
            transform.position += delta;
            creditsButton.transform.position += delta;
            menuButton.transform.position += delta;
            menuButtonBottom.transform.position += delta;

            if (transform.position.y >= 37.0f && !playedOhYeah)
            {
                ohYeah.Play();
                playedOhYeah = true;
            }
        }
    }

    public void SetScrollToTrue() {
        startScroll = true;
    }
}
