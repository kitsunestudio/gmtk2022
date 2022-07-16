using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float speed;
    private int health;
    private int maxHealth;

    void Start() {
        health = maxHealth;
    }

    private void FixedUpdate() {
        transform.position = Vector3.Lerp(transform.position, Player.playerInstance.playerTrans.position, Time.deltaTime * speed);
    }

    public void takeDamage(int damage) {
        health -= damage;
        if(health <= 0) {
            Destroy(gameObject);
        }
    }

    public void loadEnemy(Enemy me) {
        maxHealth = me.maxHealth;
        health = maxHealth;
        gameObject.GetComponent<SpriteRenderer>().sprite = me.gameSprite;
        speed = me.speed;
        string animation;
        if(me.enemyName == "Spade") {
            animation = "Spade_Walk";
        } else if(me.enemyName == "Club") {
            animation = "Club_Walk";
        } else if(me.enemyName == "Queen") {
            animation = "Queen_Walk";
        } else if(me.enemyName == "King") {
            animation = "King_Walk";
        } else {
            animation = "";
        }
        gameObject.GetComponent<Animator>().Play(animation, 0);
    }
}
