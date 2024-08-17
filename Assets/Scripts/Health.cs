using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float health = 1;
    private void Update() {
        if(health<=0){
            Defeated();
        }
    }
    
    public void TakeDamage(float damage){
        health -= damage;
    }
    public void Defeated(){
        //Add death animation here
        Debug.Log("Defeated");
        Destroy(gameObject);
    }
}
