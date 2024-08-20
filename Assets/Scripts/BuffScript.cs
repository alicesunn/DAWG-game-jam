using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffScript : MonoBehaviour
{
    private StateScript state;
    private float timer = 3.0f;
    private const float COOLDOWN = 4.0f;
    private Animator anim;
    private AudioSource flex;
    private AudioSource ohYeah;

    void Start()
    {
        state = GameObject.Find("State").GetComponent<StateScript>();
        anim = GetComponent<Animator>();

        AudioSource[] sfx = GetComponentsInChildren<AudioSource>();
        flex = sfx[0];
        flex.loop = false;
        flex.playOnAwake = false;
        flex.volume = 0.3f;
        ohYeah = sfx[1];
        ohYeah.loop = false;
        ohYeah.Play();
    }

    void Update()
    {
        if (timer > 0.0f) timer -= Time.deltaTime;
        else
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;

            anim.SetTrigger("Flex");
            double time = AudioSettings.dspTime;
            flex.PlayScheduled(time + 0.3f);
            timer = COOLDOWN;
        }
    }
}
