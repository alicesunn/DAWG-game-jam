using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class PlayerScript : MonoBehaviour
{
    [HideInInspector] public Vector2 direction = new(0.0f, 0.0f);

    private Rigidbody2D body;
    private StateScript state;
    private float speed;

    // hit points stuff
    private const int MAX_HEALTH = 5;
    private Health hp;
    private TextMeshProUGUI hpText;

    // for hit flash
    private SpriteRenderer rend;
    private const float FLASH_TIME = 0.3f;
    private float flashTimer = -1.0f;

    // for shoot/hit vfx
    public AudioClip hitSound;
    private AudioSource birdAudio;

    private bool playingWin = false;

    // for disable scripts on win
    private ShootScript shoot;

    void Start()
    {
        state = GameObject.Find("State").GetComponent<StateScript>();
        speed = state.playerSpeed;
        body = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        shoot = GetComponent<ShootScript>();

        // Start at origin
        transform.position = Vector3.zero;

        // Health counting
        hp = GetComponent<Health>();
        hp.health = MAX_HEALTH;
        hp.isPlayer = true;
        hpText = state.hpText;
        hpText.SetText(hp.health.ToString());

        birdAudio = GetComponent<AudioSource>();
        birdAudio.playOnAwake = false;
        birdAudio.volume = 0.1f;
        birdAudio.loop = true;
    }

    void Update()
    {
        if (playingWin) return;

        HandleMovement();

        if (flashTimer > 0.0f)
        {
            flashTimer -= Time.deltaTime;
            if (flashTimer < 0.0f)
            {
                rend.color = Color.white;
            }
        }
    }

    public void OnWin()
    {
        rend.color = Color.white;
        playingWin = true;
        body.velocity = Vector2.zero;
        shoot.enabled = false;
    }

    public void TakeDamage(int damage)
    {
        if (playingWin) return;
        if (hp.TakeDamage(damage))
        {
            hpText.SetText(hp.health.ToString());
            PlayHitEffect();
        }

        if (hp.health <= 0)
        {
            SceneManager.LoadScene("Defeat");
        }
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        direction = new Vector2(x, y).normalized;

        // Play grass walk sound
        if ((x != 0 || y != 0) && !birdAudio.isPlaying) birdAudio.Play();
        else if (x == 0 && y == 0 && birdAudio.isPlaying) birdAudio.Stop();

        body.velocity = direction * speed;

        // Prevent from walking beyond certain point
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -state.maxX, state.maxX);
        pos.y = Mathf.Clamp(pos.y, -state.maxY, state.maxY);
        transform.position = pos;

        // Flip bird depending on direction
        Vector3 scale = transform.localScale;
        if ((direction.x < 0 && scale.x > 0) || (direction.x > 0 && scale.x < 0)) scale.x *= -1;
        transform.localScale = scale;
    }

    public void PlayHitEffect()
    {
        rend.color = Color.red;
        flashTimer = FLASH_TIME;
        birdAudio.PlayOneShot(hitSound, 5.0f);
    }
}
