using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] GameManager gameManagerrr;
    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] GameObject enemy;

    float spawnTimer=3f;
    float spawnRateIncrease=4f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnNextEnemy());
        StartCoroutine(SpawnRateIncrease());
    }

    IEnumerator SpawnNextEnemy()
    {
        int nextSpawnLocation=Random.Range(0,spawnPoints.Length);

        Instantiate(enemy,spawnPoints[nextSpawnLocation].transform.position,Quaternion.identity);
        yield return new WaitForSeconds(spawnTimer);

        if(!gameManagerrr.gameOver)
        {
            StartCoroutine(SpawnNextEnemy());
        }
    }

    IEnumerator SpawnRateIncrease()
    {
        yield return new WaitForSeconds(spawnRateIncrease);
        if(spawnTimer>=0.5f)
        {
            spawnTimer-=0.2f;
        }
        StartCoroutine(SpawnRateIncrease());
    }
}
