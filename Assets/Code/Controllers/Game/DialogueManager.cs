using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public SystemsController sc;
    private Queue<DialogueSentence> sentences;
    public GameObject dialogueBox;
    public GameObject responseBox;
    public GameObject responseButton;
    public TMP_Text characterNameText;
    public TMP_Text dialougeText;
    public GameObject continueText;
    private Animator anim;
    public float typingSpeed;
    private TopDownPlayerController controller;
    private DialogueSentence currentSentence;
    private List<GameObject> responseButtons;
    private int buttonClicked;
    private bool canTalk = false;
    private bool firstDialogueDone = false;
    public bool gameIsOver = false;

    void Awake()
    {
        sentences = new Queue<DialogueSentence>();
        anim = dialogueBox.GetComponent<Animator>();
        controller = new TopDownPlayerController();
        responseButtons = new List<GameObject>();
    }

    private void OnEnable() {
      controller.Menu.NextDialogue.performed += displayNextSentenceOnAction;
      controller.Menu.NextDialogue.Enable();
      controller.Menu.SelectDialogue1.performed += selectResponse1;
      controller.Menu.SelectDialogue1.Enable();
      controller.Menu.SelectDialogue2.performed += selectResponse2;
      controller.Menu.SelectDialogue2.Enable();
      controller.Menu.SelectDialogue3.performed += selectResponse3;
      controller.Menu.SelectDialogue3.Enable();
    }

    private void displayNextSentenceOnAction(InputAction.CallbackContext obj) {{
        if(canTalk && responseButtons.Count == 0) {
            displayNextSentence();
        }
    }}

    private void selectResponse1(InputAction.CallbackContext obj) {{
        if(currentSentence.type == DialogueType.question) {
            responseButtonClicked(0);
        }
    }}

    private void selectResponse2(InputAction.CallbackContext obj) {{
        if(currentSentence.type == DialogueType.question) {
            responseButtonClicked(1);
        }
    }}

    private void selectResponse3(InputAction.CallbackContext obj) {{
        if(currentSentence.type == DialogueType.question) {
            responseButtonClicked(2);
        }
    }}

    private void OnDisable() {
        controller.Menu.OpenCloseMenu.Disable();
        controller.Menu.SelectDialogue1.Disable();
        controller.Menu.SelectDialogue2.Disable();
        controller.Menu.SelectDialogue3.Disable();
    }

    public void loadDialogue(Dialouge dialouge) {
        canTalk = true;
        sc.gsm.setStateGamePaused();
        anim.SetBool("isShow", true);
        sentences.Clear();
        foreach(DialogueSentence sentence in dialouge.sentences) {
            sentences.Enqueue(sentence);
        }

        characterNameText.text = dialouge.characterName;

        displayNextSentence();
    }

    public void displayNextSentence() {
        if(sentences.Count == 0) {
            endDialogue();
            if(gameIsOver) {
                Application.Quit();
            }
            if(buttonClicked == 0) {
                if(!firstDialogueDone) {
                    SystemsController.systemInstance.bgc.loadGame();
                    SystemsController.systemInstance.cc.startFollowing = true;
                    buttonClicked = 100;
                    firstDialogueDone = true;
                } else {
                    Player.playerInstance.reset();
                    SystemsController.systemInstance.es.reset();
                    SceneManager.LoadScene(1);
                }
            } else if(buttonClicked == 1){
                if(!firstDialogueDone) {
                    SystemsController.systemInstance.bgc.openMenu();
                } else {
                    Application.Quit();
                }
            } else if(buttonClicked == 2) {
                SystemsController.systemInstance.bgc.quitGameButton();
            }
            return;
        }

        currentSentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(writeSentence(currentSentence.sentence));
        if(currentSentence.type == DialogueType.question) {
            showResponses(currentSentence);
        } else {
            if(!continueText.activeSelf) {
                continueText.SetActive(true);
                responseBox.SetActive(false);
            }
        }
    }

    private void endDialogue() {
        anim.SetBool("isShow", false);
        canTalk = false;
        SystemsController.systemInstance.gsm.setStateInGame();
    }

    private IEnumerator writeSentence(string sentence) {
        dialougeText.text = "";

        foreach(char letter in sentence.ToCharArray()) {
            dialougeText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }

    private void showResponses(DialogueSentence currentSentence) {
        continueText.SetActive(false);
        responseBox.SetActive(true);
        for(int i = 0; i < currentSentence.questions.Length; i++) {
            GameObject responseButt = Instantiate(responseButton, transform.position, transform.rotation, responseBox.transform);
            responseButt.GetComponent<DialogueResponseButton>().setResponseText(currentSentence.questions[i], i);
            responseButt.GetComponent<Button>().onClick.AddListener(delegate {responseButtonClicked(responseButt.GetComponent<DialogueResponseButton>().getIndex()); });
            responseButtons.Add(responseButt);
        }
    }

    public void responseButtonClicked(int index) {
        continueText.SetActive(true);
        responseBox.SetActive(false);
        clearButtons();
        StopAllCoroutines();
        StartCoroutine(writeSentence(currentSentence.responses[index]));

        buttonClicked = index;
    }

    private void clearButtons() {
        foreach(GameObject button in responseButtons) {
            Destroy(button);
        }
        responseButtons.Clear();
    }
}
