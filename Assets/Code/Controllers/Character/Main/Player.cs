using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player playerInstance;
    public SystemsController sc;
    public PlayerCharacterMovement pcm; 
    public PlayerStateMachine psm;
    public PlayerAnimationController pac;
    public PlayerInventory pi;
    public PlayerAttack pa;
    public Transform playerTrans;

    void Awake() {
        if(playerInstance != null && playerInstance != this) {
            Destroy(this);
        } else {
            playerInstance = this;
        }
        playerTrans = transform;
        DontDestroyOnLoad(this.gameObject);
    }

    public void reset() {
        SystemsController.systemInstance.bgc.canOpen = true;
        playerTrans.position = Vector3.zero;
        pa.health = pa.maxHealth;
        pa.healthBar.setCurrentValue(pa.health);
        pi.clearInventory();
        pi.dp.hide();
        GameObject.FindGameObjectWithTag("BossHealth").GetComponent<Animator>().SetBool("showHealth", false);
        GameObject.FindGameObjectWithTag("BossHealth").GetComponent<Animator>().SetBool("stayDisplay", false);
        SystemsController.systemInstance.es.startGame = false;
        SystemsController.systemInstance.es.reset();
        SystemsController.systemInstance.es.startWave();

    }
}
