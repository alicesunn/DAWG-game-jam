using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject enemyPrefabTwo;
    [SerializeField]
    private GameObject enemyPrefabThree;

    [SerializeField]
    private float minimumSpawnTime;

    [SerializeField]
    private float maximumSpawnTime;

    private float timeUntilSpawn;
    private float timeUntilSpawnTwo;
    private float MAGICNUMBERMULTIPLIERONE = 2f;
    private float timeUntilSpawnThree;
    private float MAGICNUMBERMULTIPLIERTWO = 3f;

    void Awake()
    {
        SetTimeUntilSpawn();
        SetTimeUntilSpawnTwo();
        SetTimeUntilSpawnThree();
    }

    void Update()
    {
        //IM SORRY ABOUT THE SPAGHETTI
        timeUntilSpawn -= Time.deltaTime;
        timeUntilSpawnTwo -= Time.deltaTime;
        timeUntilSpawnThree -= Time.deltaTime;

        if (timeUntilSpawn <= 0)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            SetTimeUntilSpawn();
        }
        else if(timeUntilSpawnTwo <= 0){
            Instantiate(enemyPrefabTwo, transform.position, Quaternion.identity);
            SetTimeUntilSpawnTwo();
        }
        else if(timeUntilSpawnThree <=0){
            Instantiate(enemyPrefabThree, transform.position, Quaternion.identity);
            SetTimeUntilSpawnThree();
        }
    }

    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }
    private void SetTimeUntilSpawnTwo(){
        timeUntilSpawnTwo = Random.Range(minimumSpawnTime, maximumSpawnTime) * MAGICNUMBERMULTIPLIERONE;
    }
    private void SetTimeUntilSpawnThree(){
        timeUntilSpawnThree = Random.Range(minimumSpawnTime, maximumSpawnTime) * MAGICNUMBERMULTIPLIERTWO;
    }
    
}
