using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BaseGameController : MonoBehaviour
{
    public SystemsController systems;
    private TopDownPlayerController controller;
    public GameObject inGameMenuPanel;
    private void Awake() {
        controller = new TopDownPlayerController();
        inGameMenuPanel.SetActive(true);
    }

    void Start() {
        inGameMenuPanel.SetActive(false);
    }

    private void OnEnable() {
      controller.Menu.OpenCloseMenu.performed += OpenMenuAction;
      controller.Menu.OpenCloseMenu.Enable();
    }

    private void OnDisable() {
        controller.Menu.OpenCloseMenu.Disable();
    }

    private void OpenMenuAction(InputAction.CallbackContext obj) {{
        openMenu();
    }}

    public void quitGameButton() {
        Application.Quit();
    }

    public void loadGame() {
        Player.playerInstance.playerTrans.position = Vector3.zero;
        SceneManager.LoadScene(1);
    }

    public void openMenu() {
        if(systems.gsm.getState() == GameStates.InGame || systems.gsm.getState() == GameStates.GamePaused) {
            if(inGameMenuPanel.activeSelf) {
                inGameMenuPanel.SetActive(false);
                systems.gsm.setStateInGame();
            } else {
                inGameMenuPanel.SetActive(true);
                systems.gsm.setStateGamePaused();
            }
        }
    }
}
