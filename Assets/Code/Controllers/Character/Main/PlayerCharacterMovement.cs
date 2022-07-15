using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacterMovement : MonoBehaviour
{
    private Player player;
    private Rigidbody2D rb;
    private TopDownPlayerController topDownController;
    private InputAction movement;
    private InputAction attack;
    public float moveSpeed = 5f;
    private Vector2 mousePos;
    public Vector2 MousePos { get; set;}

    void Awake() {
      topDownController = new TopDownPlayerController();
      rb = GetComponent<Rigidbody2D>();
    }

    void Start() {
      player = Player.playerInstance;
    }

    private void OnEnable() {
      movement = topDownController.Character.Movement;
      movement.Enable();
      attack = topDownController.Character.Attack;
      attack.performed += onAttack;
      attack.Enable();
    }

    private void OnDisable() {
      movement.Disable();
      attack.Disable();
    }

    private void FixedUpdate() {
      if(player.sc.gsm.getState() != GameStates.GamePaused) {
        rb.MovePosition(rb.position + movement.ReadValue<Vector2>() * moveSpeed * Time.fixedDeltaTime);
      }
    }

    public Vector2 getMovement() {
      return movement.ReadValue<Vector2>();
    }

    private void onAttack(InputAction.CallbackContext obj) {{
      MousePos = SystemsController.systemInstance.cc.mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
      Player.playerInstance.pa.rollDie(Player.playerInstance.pi.selectedDie.MyItem, MousePos);
    }}
}
