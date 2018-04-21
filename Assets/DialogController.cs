using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour {

    public enum NodeContent
    {
        DIALOG,
        CHOICE
    };

    public string name;
    public string text;

    public List<Button> choiceList;

    public UINameBox nameBox;
    public DialogContent dialogContentBox;

    public Text dialogText;

    public Dialogue.Choice[] choiceBundle;

    public Object choicePrefab;

    void Start()
    {
        choiceList = new List<Button>();
        choicePrefab = Resources.Load("Prefabs/ChoiceButton");
    }


    public void setContent(string text)
    {
        dialogText.text = text;
    }

    public void clear()
    {
        dialogText.text = "";
    }

    void setName(string name)
    {
        this.nameBox.toDisplay = name;
    }

    void fillDialog(List<string> content, NodeContent property)
    {
        switch (property)
        {//to complete with vince
            case (NodeContent.DIALOG):
                dialogContentBox.setContent(content[0]);
                break;
            case (NodeContent.CHOICE):
                break;
        }
    }

    void openDialogBox()
    {
        //set animation here

    }


	// Update is called once per frame
	void Update () {
		
	}
}
