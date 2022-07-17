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
    public GameObject craftingMenu;
    public bool canOpen = true;
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
        controller.Menu.OpenCrafting.performed += OpenCrafting;
        controller.Menu.OpenCrafting.Enable();
    }

    private void OnDisable() {
        controller.Menu.OpenCloseMenu.Disable();
        controller.Menu.OpenCrafting.Disable();
    }

    private void OpenMenuAction(InputAction.CallbackContext obj) {{
        if(canOpen) {
            openMenu();
        }
    }}

    private void OpenCrafting(InputAction.CallbackContext obj) {{
        if(SystemsController.systemInstance.gsm.getState() != GameStates.GamePaused) {
            openCraftingMenu();
        }
    }}

    public void quitGameButton() {
        Application.Quit();
    }

    public void loadGame() {
        Player.playerInstance.playerTrans.position = Vector3.zero;
        SceneManager.LoadScene(1);
        SystemsController.systemInstance.es.startWave();
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

    private void openCraftingMenu() {
        Player.playerInstance.pa.canAttack = !Player.playerInstance.pa.canAttack;
        craftingMenu.GetComponent<Animator>().SetBool("showCraft", !Player.playerInstance.pa.canAttack);
    }
}
