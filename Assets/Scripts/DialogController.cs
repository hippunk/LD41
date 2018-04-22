using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogController : MonoBehaviour, IPointerClickHandler {

    public GameObject nameGO;
    public GameObject  contentGO;

    public bool defilement;
    private bool choicePending = false;
    public TextAnimator textAnimator;
    private Dialogue dialogue;

    public GameObject choiceContainer;
    public GameObject choicePrefab;

    public GameObject clickFeedback;
    public bool fin = false;

    void Start()
    {
        dialogue = GameLogicManager.currentDialogue;
        SetDialogue();
        
        //RawDebug();
    }

    void RawDebug()
    {
        Dialogue.Choice choice = dialogue.GetChoices()[0];
        Debug.Log(choice.dialogue+" count choices"+ dialogue.GetChoices().Length);
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

    private void SetDialogue()
    {

        if (dialogue.GetChoices().Length == 1)
        {
            clickFeedback.GetComponentInChildren<Text>().text = "Next...";
            setContent(dialogue.GetChoices()[0].dialogue);
            textAnimator.ChangeText(contentGO.GetComponent<Text>().text);
            clickFeedback.SetActive(false);
            dialogue.PickChoice(dialogue.GetChoices()[0]);
        }
        else
        {
            GenerateChoiceList();
        }

        if (dialogue.GetChoices() == null || dialogue.GetChoices().Length == 0)
        {
            clickFeedback.GetComponentInChildren<Text>().text = "Fin";
            fin = true;
        }

    }

    void GenerateChoiceList()
    {
        clickFeedback.GetComponentInChildren<Text>().text = "Answer";
        choicePending = true;
        foreach (Dialogue.Choice choice in dialogue.GetChoices())
        {
            GameObject newRow = Instantiate(choicePrefab, choiceContainer.transform);
            newRow.GetComponentInChildren<Text>().text = choice.dialogue;
            newRow.GetComponent<Button>().onClick.AddListener(() => ButtonHandler(choice));
        }
    }

    void ButtonHandler(Dialogue.Choice choice)
    {

        foreach (Transform transform in choiceContainer.transform)
        {
            Destroy(transform.gameObject);
        }
        dialogue.PickChoice(choice);
        choicePending = false;
        SetDialogue();
    }

    public void setContent(string text)
    {
        Text contentText = contentGO.GetComponent<Text>();
        contentText.text = text;
    }

    public void clearContent()
    {
        Text contentText = contentGO.GetComponent<Text>();
        contentText.text = "";
    }

    void setName(string name)
    {
        Text nameText = nameGO.GetComponent<Text>();
        nameText.text = name;
    }

    void openDialogBox()
    {
        //set animation here
    }

    // Update is called once per frame
    void Update()
    {
        if (textAnimator.finished && !choicePending)
        {
            clickFeedback.SetActive(true);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (fin)
        {
            Debug.Log("Fin");
            //TODO Chargement de l'ecran de fin
        }
        else if (!textAnimator.finished)
        {
            textAnimator.finished = true;
            clickFeedback.SetActive(true);
        }
        else if(!choicePending)
        {
            setName(dialogue.GetChoices()[0].speaker);
            SetDialogue();
        }
    }
}
