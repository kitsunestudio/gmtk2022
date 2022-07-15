using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public GameObject dieBullet;
    public List<Material> particles;

    public void rollDie(Item die, Vector2 target) {
        if(Player.playerInstance.pi.dieAvailable()) {
            GameObject bullet = Instantiate(dieBullet, calculateSpawnPosition(target), transform.rotation, transform);
            DieBullet bulletScript = bullet.GetComponent<DieBullet>();
            bulletScript.Damage = Random.Range(1, die.sides + 1);
            bulletScript.Target = target;
            bulletScript.myParticle = particles[bulletScript.Damage -1];
            
            Player.playerInstance.pi.removeDie();
        }
    }

    private Vector3 calculateSpawnPosition(Vector2 target) {
        Transform playerTransform = Player.playerInstance.playerTrans;
        Vector3 spawnPosition;
        if(target.y > playerTransform.position.y) {
            spawnPosition = new Vector3(playerTransform.position.x, playerTransform.position.y + 0.5f, playerTransform.position.z);
        } else {
            spawnPosition = new Vector3(playerTransform.position.x, playerTransform.position.y - 1f, playerTransform.position.z);        
        }
        return spawnPosition;
    }
}
