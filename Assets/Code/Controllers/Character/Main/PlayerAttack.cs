using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{

    public GameObject dieBullet;
    public List<DieMaterials> particleLists;

    public ManualSlider healthBar;
    private int health = 20;
    public int maxHealth = 20;

    public int getHealth() {
        return health;
    }

    public void takeDamage(int damage) {
        health -= damage;
        healthBar.setCurrentValue(health);
        SystemsController.systemInstance.cc.cameraShake();
    }

    public void rollDie(Item die, Vector2 target) {
        if(Player.playerInstance.pi.dieAvailable()) {
            GameObject bullet = Instantiate(dieBullet, calculateSpawnPosition(target), transform.rotation, transform);
            DieBullet bulletScript = bullet.GetComponent<DieBullet>();
            bulletScript.Damage = Random.Range(1, die.sides + 1);
            bulletScript.Target = target;
            bulletScript.myParticle = particleLists[die.particlePos].materials[bulletScript.Damage -1];
            
            Player.playerInstance.pi.removeDie();
        }
    }

    private Vector3 calculateSpawnPosition(Vector2 target) {
        Transform playerTransform = Player.playerInstance.playerTrans;
        Vector3 spawnPosition;
        if(target.x > playerTransform.position.x) {
            spawnPosition = new Vector3(playerTransform.position.x + 0.3f, playerTransform.position.y, playerTransform.position.z);
        } else {
            spawnPosition = new Vector3(playerTransform.position.x - 0.3f, playerTransform.position.y, playerTransform.position.z);        
        }
        return spawnPosition;
    }
}
