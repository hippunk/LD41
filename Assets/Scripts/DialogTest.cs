using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTest : MonoBehaviour {

    [SerializeField]
    private CharacterHandler characterHandler;

    // Use this for initialization
    void Start() {
        var characters = this.characterHandler.LoadCharacters();
        foreach (var c in characters) {
            var dialogue = c.dialog;
            foreach (Dialogue.Choice choice in dialogue.GetChoices()) {
                Debug.Log("RAW USERDATA : " + choice.userData);
                this.isChoiceSelectable(choice);
            }
        }
    }
    private bool isChoiceSelectable(Dialogue.Choice choice) {
        if (choice.userData != null) {
            var constraint = choice.userData.Split(' ');
            foreach (var c in constraint) {
                if (c.StartsWith("E:")) {
                    var trimData = c.TrimStart('E', ':');
                    Debug.Log("EMOTION : " + trimData);
                    if (Emotion.sadness.ToString() != trimData) {
                        return false;
                    }
                }
                else if (c.StartsWith("R:")) {
                    int relationConstraint;
                    var trimData = c.TrimStart('R', ':');
                    if (trimData.StartsWith(">")) {
                        var processedTrimData = trimData.Trim('>');
                        Debug.Log("RELATION : " + ">" + processedTrimData);
                        if (!Int32.TryParse(processedTrimData, out relationConstraint)) {
                            Debug.Log("DIALOG CONTROLLER :: A constraint is undefined");
                        }
                        if (100 <= relationConstraint) {
                            return false;
                        }
                    }
                    else {
                        var processedTrimData = trimData.Trim('<');
                        Debug.Log("RELATION : " + "<" + processedTrimData);
                        if (!Int32.TryParse(processedTrimData, out relationConstraint)) {
                            Debug.Log("DIALOG CONTROLLER :: A constraint is undefined");
                        }
                        if (100 < relationConstraint) {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }
}
