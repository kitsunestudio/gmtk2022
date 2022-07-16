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
            SystemsController.systemInstance.gsm.setStateGamePaused();
            if(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Death") {
                if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f) {
                    player.psm.playerIsDead();
                }
            }
        }
    }

    public CharStates getState() {
        return currentState;
    }
}
