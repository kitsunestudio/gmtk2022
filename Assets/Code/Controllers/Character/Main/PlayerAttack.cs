using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{

    public GameObject dieBullet;
    public List<DieMaterials> particleLists;

    public ManualSlider healthBar;
    public int health = 20;
    public int maxHealth = 20;

    public bool canAttack;

    public int getHealth() {
        return health;
    }

    public void takeDamage(int damage) {
        if(health > 0) {
            if(SystemsController.systemInstance.gsm.getState() != GameStates.GamePaused) {
                health -= damage;
                healthBar.setCurrentValue(health);
                if(health <= 0) {
                    Player.playerInstance.psm.playerDied();
                    SystemsController.systemInstance.bgc.canOpen = false;
                    Player.playerInstance.playerTrans.position = new Vector3(-99, 0, 0);
                    Player.playerInstance.pi.dp.GetComponent<Animator>().SetBool("showDicePanel", false);
                } else {
                    SystemsController.systemInstance.cc.cameraShake();
                }
            }
        }
    }

    public void rollDie(Item die, Vector2 target) {
        if(canAttack) {
            if(Player.playerInstance.pi.dieAvailable()) {
                GameObject bullet = Instantiate(dieBullet, calculateSpawnPosition(target), transform.rotation, transform);
                DieBullet bulletScript = bullet.GetComponent<DieBullet>();
                bulletScript.Damage = Random.Range(1, die.sides + 1);
                bulletScript.Target = target;
                bulletScript.myParticle = particleLists[die.particlePos].materials[bulletScript.Damage -1];
                
                Player.playerInstance.pi.removeDie();

                SystemsController.systemInstance.sc.playEffect(randomDieRoll());
            }
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

    private string randomDieRoll() {
        int randInt = Random.Range(0, 3);
        if(randInt == 0) {
            return "roll-1";
        } else if(randInt == 1) {
            return "roll-2";
        } else {
            return "roll-3";
        }
    }
}
