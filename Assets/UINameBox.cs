using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UINameBox : MonoBehaviour {
	bool isSetup = false;
	bool isDisplayed;
	public string toDisplay;
	public Text name;

	// Use this for initialization
	void Start () {
		isDisplayed = false;
		isSetup = true;
		toDisplay = "";	
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.activeSelf == false && string.IsNullOrEmpty (toDisplay) == false)
			displayNameBox ();
		else if (this.gameObject.activeSelf == true && string.IsNullOrEmpty (toDisplay) == true) {
			closeNameBox ();
		}
			
	}

	public void setName(string name){
		toDisplay = name;
	}

	void displayNameBox(){
		this.gameObject.SetActive (true);
		name.text = toDisplay;
		//launch animation here
		if (isDisplayed == false) {
			isDisplayed = true;//launch animation here
		}

	}

	void closeNameBox(){
		name.text = "";
		if (isDisplayed == true) {
			isDisplayed = false;
			this.gameObject.SetActive (false);
		}
	}



}
