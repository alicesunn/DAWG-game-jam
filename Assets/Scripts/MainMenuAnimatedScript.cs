using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenuAnimatedScript : MonoBehaviour
{
    public GameObject playerPrefab;
    //how to make timer go
    public float timer;
    public float walking = 10f;
    

    void Awake()
    {
        playerPrefab = GameObject.Find("Player Bird").GetComponent<GameObject>();
        timer = walking;
    }

    // Update is called once per frame
    void Start()
    {
        WaddleAway();
        
    }
    void WaddleAway(){
        Instantiate(playerPrefab, new Vector3(0, 131, 0), Quaternion.identity);
        playerPrefab.transform.localScale = new Vector3(50, 50, 50);
        for(int i = 0; i<=1000;i++){
            playerPrefab.transform.position = new Vector3(0+i,131+i,0);
        }
    }
}
