using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogController : MonoBehaviour, IPointerClickHandler {

    public GameObject nameGO;
    public GameObject contentGO;

    public bool defilement;
    private bool choicePending = false;
    public TextAnimator textAnimator;
    public Dialogue dialogue;
    public GameObject fonduChoix;
    public GameObject choiceContainer;
    public GameObject choicePrefab;

    public GameObject clickFeedback;
    public bool fin = false;

    void Start() {
        dialogue = GameLogicManager.currentDialogue;
        SetDialogue();
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

    public void SetDialogue() {
         
        List<Dialogue.Choice> aviableChoices = GetAviableChoices();

        if (aviableChoices.Count == 1) {
            Dialogue.Choice choice = dialogue.GetChoices()[0];
            setName(choice.speaker);
            clickFeedback.GetComponentInChildren<Text>().text = "Next...";
            setContent(choice.dialogue);
            textAnimator.ChangeText(contentGO.GetComponent<Text>().text);
            clickFeedback.SetActive(false);
            UpdateCharacterData(choice);

            dialogue.PickChoice(choice);

        }
        else {
            GenerateChoiceList(aviableChoices);
        }

        if (dialogue.GetChoices() == null || dialogue.GetChoices().Length == 0 || aviableChoices.Count == 0) {
            clickFeedback.GetComponentInChildren<Text>().text = "Fin";
            fin = true;
        }

    }

    public List<Dialogue.Choice> GetAviableChoices()
    {
        List<Dialogue.Choice> selectableChoices = new List<Dialogue.Choice>();
        foreach (Dialogue.Choice choice in dialogue.GetChoices())
        {
            if (this.isChoiceSelectable(choice))
            {
                selectableChoices.Add(choice);
            }
        }

        return selectableChoices;
    }

    void GenerateChoiceList(List<Dialogue.Choice> aviableChoices) {
        fonduChoix.SetActive(true);
        clickFeedback.GetComponentInChildren<Text>().text = "Answer";
        choicePending = true;


        foreach (Dialogue.Choice choice in aviableChoices)
        {
            GameObject newRow = Instantiate(choicePrefab, choiceContainer.transform);
            newRow.GetComponentInChildren<Text>().text = choice.dialogue;
            newRow.GetComponent<Button>().onClick.AddListener(() => ButtonHandler(choice));
        }
    }


    private bool isChoiceSelectable(Dialogue.Choice choice) {
        var constraintData = this.GetChoiceConstraints(choice);
        if (constraintData != null) {
            var constraints = constraintData.Split(' ');
            foreach (var constraint in constraints) {
                if (constraint.StartsWith("E:")) {
                    var trimData = constraint.TrimStart('E', ':');
                    if (GameLogicManager.currentCharacter.emotion.ToString() != trimData) {
                        return false;
                    }
                }
                else if (constraint.StartsWith("R:")) {
                    int relationConstraint;
                    var trimData = constraint.TrimStart('R', ':');
                    if (trimData.StartsWith(">")) {
                        var processedTrimData = trimData.Trim('>');
                        if (!Int32.TryParse(processedTrimData, out relationConstraint)) {
                            Debug.Log("DIALOG CONTROLLER :: Error in relation constraint value (" + GameLogicManager.currentCharacter.characterName + ")");
                        }
                        if (GameLogicManager.currentCharacter.relation <= relationConstraint) {
                            return false;
                        }
                    }
                    else {
                        var processedTrimData = trimData.Trim('<');
                        if (!Int32.TryParse(processedTrimData, out relationConstraint)) {
                            Debug.Log("DIALOG CONTROLLER :: Error in relation constraint value (" + GameLogicManager.currentCharacter.characterName + ")");
                        }
                        if (GameLogicManager.currentCharacter.relation >= relationConstraint) {
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
        var emotion = GameLogicManager.currentCharacter.emotion;
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

    private bool TriggerTransitionFade(Dialogue.Choice choice)
    {
        Debug.Log("trasision fade");
        if (choice.userData != null)
        {
            var parametersData = this.GetChoiceParameters(choice);

            if (parametersData.Contains('F'))
            {
                Debug.Log("fade");
                FadeManager startOptions = GameObject.Find("Canvas").GetComponent<FadeManager>();
                startOptions.fadeOutImage.raycastTarget = true;
                StartCoroutine(startOptions.FadeCanvasGroupAlpha(0f,1f));
                return true;
            }
        }
        return false;
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
                        Debug.Log("DIALOG CONTROLLER :: Error in relation parameter value (" + GameLogicManager.currentCharacter.characterName + ")");
                    }
                    relation += relationParameter;
                }
            }
        }
        return relation;
    }

    void UpdateCharacterData(Dialogue.Choice choice)
    {
        TriggerTransitionFade(choice);
        GameLogicManager.currentCharacter.emotion = this.GetChoiceEmotionParameter(choice);
        GameLogicManager.currentCharacter.relation += this.GetChoiceRelationParameter(choice);
    }

    void ButtonHandler(Dialogue.Choice choice) {
        fonduChoix.SetActive(false);
        foreach (Transform transform in choiceContainer.transform) {
            Destroy(transform.gameObject);
        }
        UpdateCharacterData(choice);
        dialogue.PickChoice(choice);
        choicePending = false;
        SetDialogue();
    }

    public void setContent(string text) {
        var processedText = text.Replace(GameConstant.PLAYER_NAME_TEMPLATE, GameLogicManager.playerName);
        Text contentText = contentGO.GetComponent<Text>();
        contentText.text = processedText;
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
            clickFeedback.GetComponentInChildren<Text>().text = "Fin";
        }
        else if (!textAnimator.finished) {
            textAnimator.finished = true;
            clickFeedback.SetActive(true);
        }
        else if (!choicePending) {
            setName(dialogue.GetChoices()[0].speaker);
            SetDialogue();
        }
    }
}
