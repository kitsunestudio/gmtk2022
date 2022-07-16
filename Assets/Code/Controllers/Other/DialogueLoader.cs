using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLoader : MonoBehaviour
{
    public Dialouge myDialogue;

    private void startDialogue() {
        DialogueManager dm = SystemsController.systemInstance.dm;
        dm.loadDialogue(myDialogue);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            startDialogue();
        }   
    }
}
