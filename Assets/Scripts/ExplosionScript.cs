using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public GameObject buffPrefab;

    public void SpawnGuy()
    {
        Vector3 offset = new(0.0f, 1.5f, 0.0f);
        Instantiate(buffPrefab, transform.position + offset, Quaternion.identity);
    }

    public void KillSelf()
    {
        Destroy(gameObject);
    }
}
