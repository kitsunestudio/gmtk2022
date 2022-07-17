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
    public DefaultableText waveText;

    private bool isSpawning;
    private bool startGame;

    void Start() {
        waves = new Queue<EnemyWave>();
        isSpawning = false;
        startGame = false;
        foreach(EnemyWave wave in allWaves.waves) {
            waves.Enqueue(wave);
        }
    }

    void Update() {
        if(startGame) {
            if(!isSpawning) {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                if(enemies.Length == 0) {
                    SystemsController.systemInstance.mc.crossFadeClip("shuffle");
                    startGame = false;
                    StartCoroutine(nextWave());
                }
            }
        }
    }

    public void startWave() {
        startGame = true;
        currentWave = waves.Dequeue();
        waveText.updateText(currentWave.waveNumber.ToString());
        if(currentWave.waveNumber == 7) {
            SystemsController.systemInstance.mc.crossFadeClip("boss");
        } else {
            SystemsController.systemInstance.mc.crossFadeClip("craps");
        }
        foreach(EnemyWaveEntry entry in currentWave.waveEntries) {
            entry.amount = entry.maxAmount;
        }
        startSpawningWave();
    }

    private IEnumerator spawnEnemies() {
        if(isSpawning) {
            float timeToFade = 2f;
            float timeElapsed = 0f;
            while(timeElapsed < timeToFade) {
                
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            Enemy myEnemy = calculateEnemy();

            if(myEnemy != null) {
                GameObject tempEnemy = Instantiate(enemyPrefab, calculateSpawnPoint(), transform.rotation);
                tempEnemy.GetComponent<EnemyController>().loadEnemy(myEnemy);
                StartCoroutine(spawnEnemies());
            } else {
                isSpawning = false;
                StopAllCoroutines();
                //StartCoroutine(nextWave());
            }
        }
    }

    private IEnumerator nextWave() {
        float timeToFade = 10f;
        float timeElapsed = 0f;

        while(timeElapsed < timeToFade) {    
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        startWave();
    }

    private Vector3 calculateSpawnPoint() {
        int index = Random.Range(0, spawnPoints.Length);
        return spawnPoints[index].transform.position;
    }

    private Enemy calculateEnemy() {
        List<int> possibleIndexes = new List<int>();
        for(int i = 0; i < currentWave.waveEntries.Count; i++) {
            if(currentWave.waveEntries[i].amount > 0) {
                possibleIndexes.Add(i);
            }
        }

        if(possibleIndexes.Count == 0) {
            Debug.Log("has stuff");
            return null;
        }

        int randomIndex = possibleIndexes[Random.Range(0, possibleIndexes.Count)];
        currentWave.waveEntries[randomIndex].amount -= 1;

        return currentWave.waveEntries[randomIndex].myEnemy;
    }

    private void findSpawnPoints() {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }

    private void startSpawningWave() {
        isSpawning = true;
        findSpawnPoints();
        StopAllCoroutines();
        StartCoroutine(spawnEnemies());
    }
}
