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

    void Start() {
        calculateNextSpawn();
        dieAvailable = true;
        displayDie.SetActive(true);
        displayDie.GetComponent<SpriteRenderer>().sprite = nextSpawn.myItem.gameImage;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            if(dieAvailable) {
                Player.playerInstance.pi.addItemToInventory(nextSpawn);
                dieAvailable = false;
                displayDie.SetActive(false);
                calculateNextSpawn();
                StopAllCoroutines();
                StartCoroutine(spawnTimer());
            }
        }
    }

    private void calculateNextSpawn() {
        nextSpawn = possibleSpawns[Random.Range(0, possibleSpawns.Count)];
    }

    private IEnumerator spawnTimer() {
        float timeElapsed = 0f;

        while(timeElapsed < timeToSpawn) {    
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        dieAvailable = true;
        displayDie.SetActive(true);
        displayDie.GetComponent<SpriteRenderer>().sprite = nextSpawn.myItem.gameImage;
    }
}