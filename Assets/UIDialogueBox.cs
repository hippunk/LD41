using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIDialogueBox : MonoBehaviour {

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
	//public UITextBox textBox;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void setName(string name){
		this.nameBox.toDisplay = name;
	}

	void fillDialog(List<string> content, NodeContent property){
		switch (property) {//to complete with vince
		case (NodeContent.DIALOG):
			dialogContentBox.setContent(content[0]);
			break;
		case (NodeContent.CHOICE):
			break;
		}
	}


	void openDialogBox(){
		//set animation here

	}
}
