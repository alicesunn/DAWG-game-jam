using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    private StateScript state;
    private ChickScript chickScript;
    private Animator anim;
    private float timer;
    private float cooldown;

    void Start()
    {
        state = GameObject.Find("State").GetComponent<StateScript>();
        cooldown = 1.5f; // set default here because unity is being a fucking bitch ! ! !
        timer = 0.0f;

        chickScript = gameObject.GetComponent<ChickScript>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (CanShoot())
        {
            if (timer > 0.0f)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                // Play shooting animation, if script attached to mother bird
                if (gameObject.name == state.playerName)
                {
                    anim.SetTrigger("ShootTrigger");
                }

                // Pick a random prefab to instantiate
                int rand = Random.Range(1, 4);
                if (rand == 1) Instantiate(state.bulletPrefabOne, gameObject.transform.position, Quaternion.identity);
                else if (rand == 2) Instantiate(state.bulletPrefabTwo, gameObject.transform.position, Quaternion.identity);
                else Instantiate(state.bulletPrefabThree, gameObject.transform.position, Quaternion.identity);

                timer = cooldown;
            }
        }
    }

    private bool CanShoot()
    {
        return state.enemies.Count > 0
            && (!chickScript || chickScript.isSinging);
    }
}
