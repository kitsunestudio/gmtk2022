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
    public CapsuleCollider2D smallCol;
    public BoxCollider2D bigCol;
    private GameObject bossHealth;
    public List<Sprite> bothBullets;
    private bool canSpawn;
    public GameObject enemyPrefab;
    public List<Enemy> spawnAbles;

    void Start() {
        health = maxHealth;
        canDamage = true;
        canShoot = true;
        canSpawn = false;
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
                    } else if(animationString == "Ace_Walk") {
                        if(canShoot) {  
                            aceAttack();
                        }
                        if(canSpawn) {
                            spawnMinion();
                        }
                        //add stomper here?
                        //SystemsController.systemInstance.cc.cameraShake();
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
        if(animationString != "Ace_Walk") {
            anim.Play("Card_Hit", 0);
            if(health <= 0) {
                Destroy(gameObject);
            }
        } else {
            bossHealth.GetComponent<ManualSlider>().setCurrentValue(health);
            if(health <= 0) {
                GetComponent<ParticleSystem>().Play();
                Destroy(gameObject, 7f);
                SystemsController.systemInstance.gsm.setStateGamePaused();
                SystemsController.systemInstance.cc.updateTarget(transform);
            }
        }
    }

    public void loadEnemy(Enemy me) {
        anim = gameObject.GetComponent<Animator>();
        GetComponent<AudioSource>().Stop();
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
            smallCol.enabled = true;
            bigCol.enabled = false;
        } else if(me.enemyName == "Club") {
            animationString = "Club_Walk";
            attackAnimationString = "Club_Attack";
            smallCol.enabled = true;
            bigCol.enabled = false;
        } else if(me.enemyName == "Queen") {
            animationString = "Queen_Walk";
            smallCol.enabled = true;
            bigCol.enabled = false;
        } else if(me.enemyName == "King") {
            animationString = "King_Walk";
            smallCol.enabled = true;
            bigCol.enabled = false;
        } else {
            animationString = "Ace_Walk";
            smallCol.enabled = false;
            bigCol.enabled = true;
            bossHealth = GameObject.FindGameObjectWithTag("BossHealth");
            bossHealth.SetActive(true);
            bossHealth.GetComponent<Animator>().SetBool("showHealth", true);
            bossHealth.GetComponent<Animator>().SetBool("stayDisplay", true);
            bossHealth.GetComponent<ManualSlider>().setMaxValue(maxHealth);
            bossHealth.GetComponent<ManualSlider>().setCurrentValue(health);
            bossHealth.GetComponent<ManualSlider>().setMinValue(0);
            GetComponent<Rigidbody2D>().mass = 200;
            GetComponent<AudioSource>().Play();
            StartCoroutine(spawnTimer());
            SystemsController.systemInstance.cc.cameraShake(1f);
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
            Player.playerInstance.pa.takeDamage(damage);
            canDamage = false;
            StopAllCoroutines();
            StartCoroutine(waitTillAttack());
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

    private void aceAttack() {
        GameObject temp = Instantiate(enemyBullet, transform.position, transform.rotation);
        int randomIndex = Random.Range(0,2);
        Sprite tempBullet = bothBullets[randomIndex];
        bool isDia = randomIndex == 1 ? true : false;
        temp.GetComponent<EnemyBullet>().setTarget(800, damage, tempBullet, isDia);

        canShoot = false;
        StartCoroutine(reloadShot());
    }

    private IEnumerator reloadShot() {
        float timeToFade = animationString == "Ace_Walk" ? 0.6f : 1.5f;
        float timeElapsed = 0f;

        while(timeElapsed < timeToFade) {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        canShoot = true;
    }

    private IEnumerator spawnTimer() {
        float timeToFade = 2f;
        float timeElapsed = 0f;

        while(timeElapsed < timeToFade) {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        canSpawn = true;
    }

    private void spawnMinion() {
        GameObject minion = Instantiate(enemyPrefab, transform.position, transform.rotation);
        minion.GetComponent<EnemyController>().loadEnemy(spawnAbles[Random.Range(0,2)]);
        canSpawn = false;

        StartCoroutine(spawnTimer());
    }
}
