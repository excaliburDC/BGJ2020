using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public GameObject player;

    public string dialoguePath;

    private bool inTrigger = false;
    private bool dialogueLoaded = false;
  
    void Start()
    {
        if (dialogueManager == null)
            dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
            inTrigger = true;
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
            inTrigger = false;
    }

    private void runDialogue(bool keyTrigger)
    {
        if(keyTrigger)
        {
            if (inTrigger && !dialogueLoaded)
                dialogueLoaded = dialogueManager.LoadDialogue(dialoguePath);
            if (dialogueLoaded)
                dialogueLoaded = dialogueManager.PrintLine();
        }
    }

    // Update is called once per frame
    void Update()
    {
        runDialogue(Input.GetKeyDown(KeyCode.C));
    }
}
