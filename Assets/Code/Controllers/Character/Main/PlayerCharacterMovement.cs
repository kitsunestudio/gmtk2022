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
    public bool canDoStuff { get; set; }

    void Awake() {
      topDownController = new TopDownPlayerController();
      rb = GetComponent<Rigidbody2D>();
    }

    void Start() {
      player = Player.playerInstance;
      canDoStuff = false;
    }

    private void OnEnable() {
      movement = topDownController.Character.Movement;
      movement.Enable();
      attack = topDownController.Character.Attack;
      attack.performed += onAttack;
      attack.Enable();
      topDownController.Character.d4.performed += selectD4;
      topDownController.Character.d4.Enable();
      topDownController.Character.d6.performed += selectD6;
      topDownController.Character.d6.Enable();
      topDownController.Character.d8.performed += selectD8;
      topDownController.Character.d8.Enable();
      topDownController.Character.d12.performed += selectD12;
      topDownController.Character.d12.Enable();
      topDownController.Character.d20.performed += selectD20;
      topDownController.Character.d20.Enable();
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
      if(canDoStuff) {
        MousePos = SystemsController.systemInstance.cc.mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Player.playerInstance.pa.rollDie(Player.playerInstance.pi.selectedDie.MyItem, MousePos);
      }
    }}

    private void selectD4(InputAction.CallbackContext obj) {{
      Player.playerInstance.pi.selectDie(4);
    }}

    private void selectD6(InputAction.CallbackContext obj) {{
      Player.playerInstance.pi.selectDie(6);
    }}

    private void selectD8(InputAction.CallbackContext obj) {{
      Player.playerInstance.pi.selectDie(8);
    }}

    private void selectD12(InputAction.CallbackContext obj) {{
      Player.playerInstance.pi.selectDie(12);
    }}

    private void selectD20(InputAction.CallbackContext obj) {{
      Player.playerInstance.pi.selectDie(20);
    }}
}
