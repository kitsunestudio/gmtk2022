using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Player player;
    private Animator animator;
    private PlayerStateMachine psm;
    private CharStates currentState;

    void Start() {
        player = Player.playerInstance;
        animator = GetComponentInChildren<Animator>();
        psm = GetComponent<PlayerStateMachine>();
    }

    void FixedUpdate() {
        currentState = psm.getState();
        if(player.sc.gsm.getState() != GameStates.GamePaused) {
            currentState = psm.getState();
            ChangeAnimationState();
        }
        if(currentState == CharStates.Death || currentState == CharStates.Dead) {
            ChangeAnimationState();
        }
    }

    void ChangeAnimationState()
    {
        animator.Play(currentState.ToString());
        if(currentState == CharStates.Death) {
            player.pa.canAttack = false;
        }

    }

    public CharStates getState() {
        return currentState;
    }
}
