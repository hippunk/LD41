using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogController : MonoBehaviour, IPointerClickHandler {

    public GameObject nameGO;
    public GameObject contentGO;

    public bool defilement;
    private bool choicePending = false;
    public TextAnimator textAnimator;
    private Dialogue dialogue;

    public GameObject choiceContainer;
    public GameObject choicePrefab;

    public GameObject clickFeedback;

    void Start() {
        dialogue = GameLogicManager.currentDialogue;
        SetDialogue();

        //RawDebug();
    }

    void RawDebug() {
        Dialogue.Choice choice = dialogue.GetChoices()[0];
        Debug.Log(choice.dialogue + " count choices" + dialogue.GetChoices().Length);
        dialogue.PickChoice(dialogue.GetChoices()[0]);

        choice = dialogue.GetChoices()[0];
        Debug.Log(choice.dialogue + " count choices" + dialogue.GetChoices().Length);
        dialogue.PickChoice(dialogue.GetChoices()[0]);

        choice = dialogue.GetChoices()[0];
        Debug.Log(choice.dialogue + " count choices" + dialogue.GetChoices().Length);
        dialogue.PickChoice(dialogue.GetChoices()[0]);

        choice = dialogue.GetChoices()[0];
        Debug.Log(choice.dialogue + " count choices" + dialogue.GetChoices().Length);
        dialogue.PickChoice(dialogue.GetChoices()[0]);
    }

    private void SetDialogue() {
        if (dialogue.GetChoices() == null || dialogue.GetChoices().Length == 0) {
            clickFeedback.GetComponentInChildren<Text>().text = "Fin";
        }
        else if (dialogue.GetChoices().Length == 1) {
            clickFeedback.GetComponentInChildren<Text>().text = "Next...";
            setContent(dialogue.GetChoices()[0].dialogue);
            textAnimator.ChangeText(contentGO.GetComponent<Text>().text);
            clickFeedback.SetActive(false);
        }
        else {
            GenerateChoiceList();
        }

    }

    void GenerateChoiceList() {
        clickFeedback.GetComponentInChildren<Text>().text = "Answer";
        choicePending = true;
        foreach (Dialogue.Choice choice in dialogue.GetChoices()) {
            if (this.isChoiceSelectable(choice)) {
                GameObject newRow = Instantiate(choicePrefab, choiceContainer.transform);
                newRow.GetComponentInChildren<Text>().text = choice.dialogue;
                newRow.GetComponent<Button>().onClick.AddListener(() => ButtonHandler(choice));
            }
        }
    }

    private bool isChoiceSelectable(Dialogue.Choice choice) {
        if (choice.userData != null) {
            var constraint = choice.userData.Split(' ');
            foreach (var c in constraint) {
                if (c.StartsWith("E:")) {
                    var trimData = c.TrimStart('E', ':');
                    if (GameLogicManager.currentCharacter.emotion.ToString() != trimData) {
                        return false;
                    }
                }
                else if (c.StartsWith("R:")) {
                    int relationConstraint;
                    var trimData = c.TrimStart('R', ':');
                    if (trimData.StartsWith(">")) {
                        var processedTrimData = trimData.Trim('>');
                        if (!Int32.TryParse(processedTrimData, out relationConstraint)) {
                            Debug.Log("DIALOG CONTROLLER :: A constraint is undefined (" + GameLogicManager.currentCharacter.characterName + ")");
                        }
                        if (GameLogicManager.currentCharacter.relation <= relationConstraint) {
                            return false;
                        }
                    }
                    else {
                        var processedTrimData = trimData.Trim('<');
                        if (!Int32.TryParse(processedTrimData, out relationConstraint)) {
                            Debug.Log("DIALOG CONTROLLER :: A constraint is undefined (" + GameLogicManager.currentCharacter.characterName + ")");
                        }
                        if (GameLogicManager.currentCharacter.relation < relationConstraint) {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }

    void ButtonHandler(Dialogue.Choice choice) {
        foreach (Transform transform in choiceContainer.transform) {
            Destroy(transform.gameObject);
        }
        GameLogicManager.currentCharacter.emotion = this.GetChoiceEmotion(choice);
        dialogue.PickChoice(choice);
        choicePending = false;
        SetDialogue();
    }

    private float GetChoiceEmotion(Dialogue.Choice choice) {
        var emotion = GameLogicManager.currentCharacter.emotion;
        if (choice.userData != null) {
            var trimData = choice.userData.Split(' ');
            foreach (var data in trimData) {
                if (!(data.StartsWith("E:") || data.StartsWith("R:"))) {
                    emotion = GameConstant.EmotionToFloat(data);
                }
            }
        }
        return emotion;
    }

    public void setContent(string text) {
        Text contentText = contentGO.GetComponent<Text>();
        contentText.text = text;
    }

    public void clearContent() {
        Text contentText = contentGO.GetComponent<Text>();
        contentText.text = "";
    }

    void setName(string name) {
        Text nameText = nameGO.GetComponent<Text>();
        nameText.text = name;
    }

    void openDialogBox() {
        //set animation here
    }

    // Update is called once per frame
    void Update() {
        if (textAnimator.finished && !choicePending) {
            clickFeedback.SetActive(true);
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (dialogue.GetChoices() == null || dialogue.GetChoices().Length == 0) {
            clickFeedback.GetComponent<Text>().text = "Fin";
        }
        else if (!textAnimator.finished) {
            textAnimator.finished = true;

            clickFeedback.SetActive(true);
        }
        else if (!choicePending) {
            setName(dialogue.GetChoices()[0].speaker);
            dialogue.PickChoice(dialogue.GetChoices()[0]);
            SetDialogue();
        }
    }
}
