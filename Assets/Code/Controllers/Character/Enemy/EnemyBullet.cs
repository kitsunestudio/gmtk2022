using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Vector2 target;
    private float speed;
    private Rigidbody2D rb;
    public int damage;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        Vector2 f = target - new Vector2(transform.position.x, transform.position.y);
        f = f.normalized;
        f = f * speed;
        rb.AddForce(f * Time.deltaTime);
    }

    public void setTarget(float newSpeed, int newDamage, Sprite bullet) {
        target = Player.playerInstance.playerTrans.position;
        speed = newSpeed;
        damage = newDamage;
        gameObject.GetComponent<SpriteRenderer>().sprite = bullet;
        Destroy(gameObject, 1.5f);
    }
}
