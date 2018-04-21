using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHandler : MonoBehaviour {
    
    public string pathToDialog;

	// Use this for initialization
	void Start () {
        //DialogueFile dialogFile = ;

        DialogueManager manager = DialogueManager.LoadDialogueFile(pathToDialog);

        Dialogue currentDialogue = manager.GetDialogue("newDialogue");

        Dialogue.Choice currentChoice = currentDialogue.GetChoices()[0];

        currentDialogue.PickChoice(currentChoice);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
