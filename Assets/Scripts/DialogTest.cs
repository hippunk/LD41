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
                Debug.Log("EMOTION CHOICE : " + GetChoiceEmotionParameter(choice));
                Debug.Log("RELATION CHOICE + " + GetChoiceRelationParameter(choice));
            }
        }
    }
    private bool isChoiceSelectable(Dialogue.Choice choice) {
        var constraintData = this.GetChoiceConstraints(choice);
        if (constraintData != null) {
            var constraints = constraintData.Split(' ');
            foreach (var constraint in constraints) {
                if (constraint.StartsWith("E:")) {
                    var trimData = constraint.TrimStart('E', ':');
                    if (Emotion.sadness.ToString() != trimData) {
                        return false;
                    }
                }
                else if (constraint.StartsWith("R:")) {
                    int relationConstraint;
                    var trimData = constraint.TrimStart('R', ':');
                    if (trimData.StartsWith(">")) {
                        var processedTrimData = trimData.Trim('>');
                        if (!Int32.TryParse(processedTrimData, out relationConstraint)) {
                            Debug.Log("DIALOG CONTROLLER :: Error in relation constraint value (" + ")");
                        }
                        if (0 <= relationConstraint) {
                            return false;
                        }
                    }
                    else {
                        var processedTrimData = trimData.Trim('<');
                        if (!Int32.TryParse(processedTrimData, out relationConstraint)) {
                            Debug.Log("DIALOG CONTROLLER :: Error in relation constraint value (" + ")");
                        }
                        if (0 < relationConstraint) {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }

    private string GetChoiceConstraints(Dialogue.Choice choice) {
        if (choice.userData != null) {
            var splitData = choice.userData.Split('|');
            if (splitData.Length > 1) {
                return splitData[1];
            }
        }
        return null;
    }

    private string GetChoiceParameters(Dialogue.Choice choice) {
        if (choice.userData != null) {
            var splitData = choice.userData.Split('|');
            if (splitData.Length > 0) {
                return splitData[0];
            }
        }
        return null;
    }


    private Emotion GetChoiceEmotionParameter(Dialogue.Choice choice) {
        var emotion = Emotion.neutral;
        if (choice.userData != null) {
            var parametersData = this.GetChoiceParameters(choice);
            var parameters = parametersData.Split(' ');
            foreach (var parameter in parameters) {
                if (parameter.StartsWith("E:")) {
                    var trimParameter = parameter.TrimStart('E', ':');
                    if (GameConstant.StringToEmotion(trimParameter) != Emotion.none) {
                        emotion = GameConstant.StringToEmotion(trimParameter);
                    }
                }
            }
        }
        return emotion;
    }

    private int GetChoiceRelationParameter(Dialogue.Choice choice) {
        var relation = 0;
        if (choice.userData != null) {
            var parametersData = this.GetChoiceParameters(choice);
            var parameters = parametersData.Split(' ');
            foreach (var parameter in parameters) {
                if (parameter.StartsWith("R:")) {
                    int relationParameter;
                    var trimParameter = parameter.TrimStart('R', ':');
                    if (!Int32.TryParse(trimParameter, out relationParameter)) {
                        Debug.Log("DIALOG CONTROLLER :: Error in relation parameter value (" + ")");
                    }
                    relation += relationParameter;
                }
            }
        }
        return relation;
    }
}
