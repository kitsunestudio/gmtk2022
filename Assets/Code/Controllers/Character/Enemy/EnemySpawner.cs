using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyWave currentWave;
    private Queue<EnemyWave> waves;
    public AllEnemyWaves allWaves;
    public GameObject[] spawnPoints;
    public GameObject enemyPrefab;

    void Start() {
        // foreach(EnemyWave wave in allWaves.waves) {
        //     waves.Enqueue(wave);
        // }
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        StopAllCoroutines();
        StartCoroutine(spawnEnemies());
    }

    private IEnumerator spawnEnemies() {
        float timeToFade = 2f;
        float timeElapsed = 0f;
        while(timeElapsed < timeToFade) {
            
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        GameObject tempEnemy = Instantiate(enemyPrefab, spawnPoints[0].transform.position, transform.rotation);
    }
}
