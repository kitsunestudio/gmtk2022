using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float speed;
    private int health;
    private int maxHealth;
    public string animationString;
    private string attackAnimationString;
    private Animator anim;
    private float attackDistance;
    private int damage;
    private bool canDamage;
    public GameObject enemyBullet;
    private bool canShoot;
    private Sprite bulletSprite;

    void Start() {
        health = maxHealth;
        canDamage = true;
        canShoot = true;
    }

    private void FixedUpdate() {
        if(SystemsController.systemInstance.gsm.getState() != GameStates.GamePaused) {
            if(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == animationString) {
                transform.position = Vector3.MoveTowards(transform.position, Player.playerInstance.playerTrans.position, speed * Time.deltaTime);
                float distance = Vector3.Distance(transform.position, Player.playerInstance.playerTrans.position);
                if(distance <= attackDistance) {
                    if(animationString == "Club_Walk" || animationString == "Spade_Walk") {
                        anim.Play(attackAnimationString, 0);
                    } else if(animationString == "Queen_Walk" || animationString == "King_Walk") {
                        if(canShoot) {
                            queenAttack();
                        }
                    }
                }
            } else if(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Card_Hit" || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == attackAnimationString) {
                if(Player.playerInstance.playerTrans.position.x < transform.position.x) {
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                } else {
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
                if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) {
                    anim.Play(animationString, 0);
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
            }
        }
    }

    public void takeDamage(int damage) {
        health -= damage;
        anim.Play("Card_Hit", 0);
        if(health <= 0) {
            Destroy(gameObject);
        }
    }

    public void loadEnemy(Enemy me) {
        anim = gameObject.GetComponent<Animator>();
        maxHealth = me.maxHealth;
        health = maxHealth;
        gameObject.GetComponent<SpriteRenderer>().sprite = me.gameSprite;
        speed = me.speed;
        attackDistance = me.attackDistance;
        damage = me.damage;
        bulletSprite = me.bulletSprite;
        if(me.enemyName == "Spade") {
            animationString = "Spade_Walk";
            attackAnimationString = "Spade_Attack";
        } else if(me.enemyName == "Club") {
            animationString = "Club_Walk";
            attackAnimationString = "Club_Attack";
        } else if(me.enemyName == "Queen") {
            animationString = "Queen_Walk";
        } else if(me.enemyName == "King") {
            animationString = "King_Walk";
        } else {
            animationString = "Ace_Walk";
        }
        anim.Play(animationString, 0);
    }

    public bool getIsAttacking() {
        if(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == attackAnimationString) {
            return true;
        } else {
            return false;
        }
    }

    public int getDamage() {
        return damage;
    }

    private void OnCollisionStay2D(Collision2D other) {
      if(other.gameObject.CompareTag("Player")) {
        anim.Play(attackAnimationString, 0);
        if(canDamage) {
            if(Player.playerInstance.psm.getState() != CharStates.Death || Player.playerInstance.psm.getState() != CharStates.Dead) {
                Player.playerInstance.pa.takeDamage(damage);
                canDamage = false;
                StopAllCoroutines();
                StartCoroutine(waitTillAttack());
            }
        }
      }
    }

    private IEnumerator waitTillAttack() {
        float timeToFade = 0.166f;
        float timeElapsed = 0f;

        while(timeElapsed < timeToFade) {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        canDamage = true;
    }

    private void queenAttack() {
        GameObject temp = Instantiate(enemyBullet, transform.position, transform.rotation);
        bool isDia = animationString == "King_Walk" ? true : false;
        temp.GetComponent<EnemyBullet>().setTarget(800, damage, bulletSprite, isDia);

        canShoot = false;
        StartCoroutine(reloadShot());
    }

    private IEnumerator reloadShot() {
        float timeToFade = 1.5f;
        float timeElapsed = 0f;

        while(timeElapsed < timeToFade) {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        canShoot = true;
    }
}
