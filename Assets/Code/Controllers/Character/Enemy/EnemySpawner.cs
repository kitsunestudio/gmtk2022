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
    public GameObject credits;
    public bool startGame;
    public bool canSpawn = true;

    private float spawnTimer;
    public float spawnTimerMax;
    void Start() {
        waves = new Queue<EnemyWave>();
        startGame = false;
        foreach(EnemyWave wave in allWaves.waves) {
            waves.Enqueue(wave);
        }
    }

    void Update() {
        if(startGame) {
            if(SystemsController.systemInstance.gsm.getState() != GameStates.GamePaused) {
                if(spawnTimer > 0) {
                    spawnTimer -= Time.deltaTime;
                } else {
                    spawnTimer = spawnTimerMax;
                    if(getEnemiesLeft() > 0) {
                        GameObject tempEnemy = Instantiate(enemyPrefab, calculateSpawnPoint(), transform.rotation);
                        tempEnemy.GetComponent<EnemyController>().loadEnemy(calculateEnemy());
                    }
                }
            }
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if(enemies.Length == 0 && getEnemiesLeft() == 0) {
                if(currentWave.waveNumber == 7) {
                    credits.SetActive(true);
                    SystemsController.systemInstance.bgc.canOpen = false;
                    Player.playerInstance.playerTrans.position = new  Vector3(101, 0, 0);
                    Player.playerInstance.pi.dp.hide();
                    SystemsController.systemInstance.dm.gameIsOver = true;
                } else {
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
            //entry.amount = 1;
        }
        startSpawningWave();
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
        findSpawnPoints();
    }

    public void reset() {

        waves.Clear();
        foreach(EnemyWave wave in allWaves.waves) {
            waves.Enqueue(wave);
        }
    }

    private int getEnemiesLeft() {
        int amount = 0;
        foreach(EnemyWaveEntry entry in currentWave.waveEntries) {
            amount += entry.amount;
        }

        return amount;
    }
}
