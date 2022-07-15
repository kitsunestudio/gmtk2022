using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieBullet : MonoBehaviour
{
    public float speed;
    private int _damage;
    public int Damage {get; set;}
    private Rigidbody2D rb;
    public Vector2 Target { get; set;}
    private bool stop;
    public ParticleSystem ps;
    public Material myParticle;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        stop = false;
        StartCoroutine(countDown());
    }

    void FixedUpdate() {
        if(!stop) {
            Vector2 f = Target - new Vector2(transform.position.x, transform.position.y);
            if(Vector2.Distance(Target, new Vector2(transform.position.x, transform.position.y)) < 1f) {
                stop = true;
            }
            f = f.normalized;
            f = f * speed;
            rb.AddForce(f);
        }
    }

    private IEnumerator countDown() {
        float timeToFade = 2f;
        float timeElapsed = 0f;

        while(timeElapsed < timeToFade) {
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Enemy")) {
            ps.GetComponent<ParticleSystemRenderer>().material = myParticle;
            ps.Play();
            Debug.Log("hit");
        }
    }
}
