using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour {

    public List<Button> choiceList;
    public UINameBox nameBox;
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

    void openDialogBox()
    {
        //set animation here

    }


	// Update is called once per frame
	void Update () {
		
	}
}
