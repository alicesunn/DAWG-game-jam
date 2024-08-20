using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuffGuyScript : MonoBehaviour
{
    private float timer = 2.0f;
    private const float COOLDOWN = 4.0f;
    private const float SPEED = 15.0f;
    private Animator anim;
    private AudioSource flex;
    private AudioSource ohYeah;
    private int flexCount = 0;

    public GameObject shockwavePrefab;
    private Vector3 shockOffset = new(-0.65f, 2.0f, 0.0f);

    private StateScript state;

    void Start()
    {
        state = GameObject.Find("State").GetComponent<StateScript>();
        anim = GetComponent<Animator>();

        AudioSource[] sfx = GetComponentsInChildren<AudioSource>();
        flex = sfx[0];
        ohYeah = sfx[1];
    }

    void Update()
    {
        if (timer > 0.0f) timer -= Time.deltaTime;
        else
        {
            flexCount++;

            if (flexCount == 1)
            {
                Flex();
                timer = COOLDOWN;
            }
            // Flip before second flex
            else if (flexCount == 2)
            {
                Flip();
                Flex();
                timer = COOLDOWN;
            }
            else if (flexCount == 3)
            {
                Flip();
                Flex();
                timer = COOLDOWN - 2.0f;
            }
            else
            {
                SceneManager.LoadScene("Win");
            }
        }
    }

    private void Flex()
    {
        anim.SetTrigger("Flex");
        double time = AudioSettings.dspTime;
        flex.PlayScheduled(time + 0.3f);
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void Shockwave()
    {
        if (transform.localScale.x < 0) shockOffset.x *= -1;
        Instantiate(shockwavePrefab, transform.position + shockOffset, Quaternion.identity);

        if (flexCount == 1)
        {
            state.ExplodeEnemies(state.kidTag);
        }
        // Flip before second flex
        else if (flexCount == 2)
        {
            state.ExplodeEnemies(state.teenTag);
        }
        else if (flexCount == 3)
        {
            state.ExplodeEnemies(state.adultTag);
        }
    }
}