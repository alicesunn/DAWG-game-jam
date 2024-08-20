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
                GameObject prefabType;
                int rand = Random.Range(1, 4);
                if (rand == 1) prefabType = state.bulletPrefabOne;
                else if (rand == 2) prefabType = state.bulletPrefabTwo;
                else prefabType = state.bulletPrefabThree;

                GameObject bullet = Instantiate(prefabType, gameObject.transform.position, Quaternion.identity);
                bullet.GetComponent<BulletScript>().SetState(state);

                timer = cooldown;
            }
        }
    }

    private bool CanShoot()
    {
        return state.enemies.Count > 0
            && (!chickScript || chickScript.isSinging);
    }

    public void IncreaseSpeed()
    {
        cooldown -= 0.25f;
    }
}
