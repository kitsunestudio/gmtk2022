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
    }
}
