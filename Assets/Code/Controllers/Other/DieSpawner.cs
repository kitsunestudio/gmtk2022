using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieSpawner : MonoBehaviour
{
    private bool dieAvailable;
    public float timeToSpawn;
    public List<ItemInstance> possibleSpawns;
    private ItemInstance nextSpawn;
    public GameObject displayDie;

    private float spawnTimer;
    private float spawnTimerMax = 2f;

    void Start() {
        calculateNextSpawn();
        dieAvailable = true;
        displayDie.SetActive(true);
        displayDie.GetComponent<SpriteRenderer>().sprite = nextSpawn.myItem.gameImage;
    }

    void Update() {
        if(SystemsController.systemInstance.gsm.getState() != GameStates.GamePaused && !dieAvailable) {
            if(spawnTimer > 0) {
                spawnTimer -= Time.deltaTime;
            } else {
                dieAvailable = true;
                displayDie.SetActive(true); 
                displayDie.GetComponent<SpriteRenderer>().sprite = nextSpawn.myItem.gameImage;
                spawnTimer = spawnTimerMax;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            if(dieAvailable) {
                SystemsController.systemInstance.sc.playEffect("grab-die");
                Player.playerInstance.pi.addItemToInventory(nextSpawn);
                dieAvailable = false;
                displayDie.SetActive(false);
                calculateNextSpawn();
            }
        }
    }

    private void calculateNextSpawn() {
        nextSpawn = possibleSpawns[Random.Range(0, possibleSpawns.Count)];
    }
}