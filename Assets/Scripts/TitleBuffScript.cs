using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBuffScript : MonoBehaviour
{
    private float timer = 0.0f;
    private const float COOLDOWN = 4.0f;
    private Animator anim;
    private AudioSource flex;
    private AudioSource ohYeah;
    private bool ohYeahD = false;
    private bool playedFirstAnim = false;

    void Start()
    {
        anim = GetComponent<Animator>();

        AudioSource[] sfx = GetComponentsInChildren<AudioSource>();
        flex = sfx[0];
        flex.loop = false;
        flex.playOnAwake = false;
        flex.volume = 0.1f;
        ohYeah = sfx[1];
        ohYeah.loop = false;
        ohYeah.playOnAwake = false;
        ohYeah.volume = 0.5f;
    }

    void Update()
    {
        if (!ohYeahD){
            if (transform.position.x <= 15.5f)
            {
                ohYeah.Play();
                ohYeahD = true;
            }
        }
        else
        {
            if (timer > 0.0f) timer -= Time.deltaTime;
            else
            {
                if (playedFirstAnim)
                {
                    Vector3 scale = transform.localScale;
                    scale.x *= -1;
                    transform.localScale = scale;
                }

                anim.SetTrigger("Flex");
                double time = AudioSettings.dspTime;
                flex.PlayScheduled(time + 0.3f);

                timer = COOLDOWN;
                if (!playedFirstAnim) playedFirstAnim = true;
            }
        }
    }
}
